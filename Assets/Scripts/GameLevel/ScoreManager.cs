using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    private int allScore = 0;
    private int scoreIncrease;
    [SerializeField] private Text scoreText;
    private void Start()
    {
        scoreText.text = allScore.ToString();
    }
    public void ScoreIncrease(string difficultyLevel)
    {
        switch (difficultyLevel)
        {
            case "Easy":
                scoreIncrease = 5;
                break;
            case "Medium":
                scoreIncrease = 10;
                break;
            case "Hard":
                scoreIncrease = 15;
                break;
        }
        allScore += scoreIncrease;
        scoreText.text = allScore.ToString();
    }
}
