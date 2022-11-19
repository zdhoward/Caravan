using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSpaceCameraCanvasSetup : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
