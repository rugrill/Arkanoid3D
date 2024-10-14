using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int score = 0;
    private int lives = 3;
    public bool gameWinner = false;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public string gameSceneName = "GameOverScene";
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the GameManager across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures there's only one instance
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
       livesText.text = lives.ToString();
       scoreText.text = score.ToString();

    }

    // Update is called once per frame
    void Update()
    {

       
            
    }

    private void ResetUI() {
        
    }

    public void LostLife() {
        lives--;
        //Debug.Log("Lives: " + lives);
        livesText.text = lives.ToString();
        if (lives <= 0) {
            lives = 3;
            score = 0;
            SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }
    }

    public void AddToScore(int value) {
        //Debug.Log("Score: " + score);
        score += value;
        scoreText.text = score.ToString();
    }

    public void AddLife() {
        lives++;
        livesText.text = lives.ToString();
    }

    public void CheckGameWin()
    {
        Debug.Log("CheckGameWin: Obstacle count: " + GameObject.FindGameObjectsWithTag("Obstacle").Length);
        if ((GameObject.FindGameObjectsWithTag("Obstacle").Length - 1) == 0)
        {
            Debug.Log("You win!");
            gameWinner = true;
            SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }
    }

}
