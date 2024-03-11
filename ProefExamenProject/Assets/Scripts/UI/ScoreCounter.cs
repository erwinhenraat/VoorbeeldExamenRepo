using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance;
    private int currentScore = 0;
    private TMP_Text scoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreText.text = "SCORE: " + currentScore.ToString();
    }

    public void IncreaseScore(int v)
    {
        currentScore += v;
        scoreText.text = "SCORE: " + currentScore.ToString();
    }
}
