using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldCity : MonoBehaviour
{
    [SerializeField] string cityName;
    [SerializeField] string sceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log($"Player has stepped onto: {cityName}");
            PlayerState.isInOverworld = false;
            SceneManager.LoadScene(sceneName);
        }
    }
}
