using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance;

    [SerializeField] Transform responseButtonPrefab;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one DialogSystem in the scene.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
