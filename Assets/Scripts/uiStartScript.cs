using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class uiStartScript : MonoBehaviour
{

    public TMP_Text pressKeyText;
    public GameObject uiCanvas;

    public string gameSceneName = "SampleScene";  // Name of your gameplay scene

    void Start(){

    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        Debug.Log("Starting Game...");

        // Load the gameplay scene
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}