using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI FPS;
    public float fpsRefreshRate;
    private string scoreBoardBaseText;
    private string fpsBaseText;
    private float timer;
    private float avgFramerate;

    private int score;
    private int winScreenIndex = 2;
    private int gameOverScreenIndex = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreBoardBaseText = scoreBoard.text;
        fpsBaseText = FPS.text;
        SceneManager.activeSceneChanged += ChangedActiveScene;
        DontDestroyOnLoad(this);
        score = 0;
        UpdateScoreBoard();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void ChangedActiveScene(Scene current, Scene next)
    {
        scoreBoard = GameObject.Find("ScoreBoard").GetComponent<TextMeshProUGUI>();
        scoreBoardBaseText = scoreBoard.text;
        UpdateScoreBoard();
        SceneManager.activeSceneChanged -= ChangedActiveScene;
        Destroy(gameObject);
        //if (next.name != "00 Starter Scene")
        //{
        //    Destroy(gameObject);
        //    Destroy(this);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ?  1/fpsRefreshRate : timer -= timelapse;

        if (timer <= 0)
        {
            avgFramerate = (int)(1f / timelapse);
            FPS.text = string.Format(fpsBaseText, avgFramerate.ToString());
        }

    }

    public void AddScore(int ammount)
    {
        score += ammount;
        UpdateScoreBoard();
    }
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(gameOverScreenIndex);
    }
    public void GameWon()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(winScreenIndex);
    }
    private void UpdateScoreBoard()
    {
        scoreBoard.text = scoreBoardBaseText + score;
    }
}
