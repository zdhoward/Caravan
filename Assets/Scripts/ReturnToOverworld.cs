using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToOverworld : MonoBehaviour
{
    [SerializeField] bool playerEntered = true;

    string overworldSceneName = "DebugOverworld";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerEntered)
        {
            playerEntered = false;
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is returning to Overworld");
            PlayerState.isInOverworld = true;
            SceneManager.LoadScene(overworldSceneName);
        }
    }
}
