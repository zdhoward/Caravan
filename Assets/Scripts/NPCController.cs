using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] string displayName = "NoName";

    public void Interact()
    {
        Debug.Log("Interacting");
    }
}
