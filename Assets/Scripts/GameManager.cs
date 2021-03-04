using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScoreBoard();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int ammount)
    {
        score += ammount;
        UpdateScoreBoard();
    }
    private void UpdateScoreBoard()
    {
        scoreBoard.text = "Score: " + score;
    }
}
