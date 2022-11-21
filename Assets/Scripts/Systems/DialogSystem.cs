using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using MoreMountains.Feedbacks;
using Ink.Runtime;
using Ink.UnityIntegration;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance;

    [SerializeField] InkFile globalsInkFile;
    [SerializeField] Transform responseButtonPrefab;

    Transform dialogCanvas;
    Transform dialogPanel;

    TextMeshProUGUI speakerName;
    TextMeshProUGUI dialog;
    Transform responseContainer;

    Story story;

    DialogVariables dialogVariables;

    MMF_Player showDialogFeedback;
    MMF_Player hideDialogFeedback;

    //bool dialogWindowIsHidden = true;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one DialogSystem in the scene.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        dialogCanvas = transform.Find("DialogCanvas");
        dialogPanel = dialogCanvas.Find("DialogPanel");

        speakerName = dialogPanel.Find("SpeakerName").GetComponent<TextMeshProUGUI>();
        dialog = dialogPanel.Find("Dialog").GetComponent<TextMeshProUGUI>();
        responseContainer = dialogPanel.Find("Responses");

        dialogVariables = new DialogVariables(globalsInkFile.filePath);

        showDialogFeedback = transform.Find("Feedbacks").Find("ShowDialog").GetComponent<MMF_Player>();
        hideDialogFeedback = transform.Find("Feedbacks").Find("HideDialog").GetComponent<MMF_Player>();
    }

    void Start()
    {
        BindInputs();
    }

    void BindInputs()
    {
        InputManager.Instance.playerInputActions.Dialog.Continue.performed += OnContinue;
    }

    void OnContinue(InputAction.CallbackContext ctx)
    {
        if (story.currentChoices.Count < 1)
            LoadNextStoryBlock();
    }

    public void ShowDialog(string displayName, TextAsset dialogToShow)
    {
        InputManager.Instance.EnableDialogcontrols();

        story = new Story(dialogToShow.text);

        dialogVariables.StartListening(story);

        speakerName.text = displayName;

        LoadNextStoryBlock();

        showDialogFeedback.PlayFeedbacks();
    }

    public void HideDialog()
    {
        InputManager.Instance.DisableDialogcontrols();

        dialogVariables.StopListening(story);

        hideDialogFeedback.PlayFeedbacks();
    }

    // Ink
    void LoadNextStoryBlock()
    {
        if (story.canContinue)
        {
            dialog.text = story.Continue();
            LoadChoices();
        }
        else
        {
            HideDialog();
        }
    }

    void LoadChoices()
    {
        // Clear buttons
        foreach (Transform button in responseContainer)
            Destroy(button.gameObject);

        // Repopulate buttons
        foreach (Choice choice in story.currentChoices)
        {
            Transform response = Instantiate(responseButtonPrefab, responseContainer);
            response.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            response.GetComponent<Button>().onClick.AddListener(delegate
            {
                story.ChooseChoiceIndex(choice.index);
                LoadNextStoryBlock();
            });
        }
    }

    Ink.Runtime.Object GetVariableState(string variableName)
    {
        if (dialogVariables.variables.TryGetValue(variableName, out Ink.Runtime.Object variableValue))
        {
            return variableValue;
        }

        Debug.LogError($"Ink Variable is null: {variableName}");
        return null;
    }
}
