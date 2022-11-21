using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] string displayName = "NoName";
    [SerializeField] TextAsset dialog;

    public void Interact()
    {
        Debug.Log($"Interacting with {displayName}");

        if (dialog != null)
        {
            DialogSystem.Instance.ShowDialog(displayName, dialog);
        }
    }
}
