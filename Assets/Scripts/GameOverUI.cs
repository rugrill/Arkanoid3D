using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    public TMP_Text gameOverText; // Reference to the TMP text
    public string gameSceneName = "SampleScene";  // Name of your gameplay scene


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.gameWinner)
        {
            gameOverText.text = "Congratulation You Win!";
        }
        else
        {
            gameOverText.text = "Game Over!";
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameOverButtonClick() {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}
