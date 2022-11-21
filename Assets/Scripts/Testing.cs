using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Testing : MonoBehaviour
{
    [SerializeField] TextAsset testDialog;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogSystem.Instance.ShowDialog("", testDialog);
        }
    }
}
