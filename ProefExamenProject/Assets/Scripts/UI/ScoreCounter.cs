using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance;
    public int currentScore = 0;
    public TMP_Text scoreText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = "SCORE: " + currentScore.ToString();
    }

    public void IncreaseScore(int v)
    {
        currentScore += v;
        scoreText.text = "SCORE: " + currentScore.ToString();
    }
}
