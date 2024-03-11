using TMPro;
using UnityEngine;
public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance;
    private int currentScore = 0;
    private TMP_Text scoreText;

    private void Awake()
    {
        public static ScoreCounter Instance;
        public int currentScore = 0;
        public TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = "SCORE: " + currentScore.ToString();
    }

        private void Start() => scoreText.text = "SCORE: " + currentScore;

        public void IncreaseScore(int v)
        {
            currentScore += v;
            scoreText.text = "SCORE: " + currentScore;
        }
    }
}
