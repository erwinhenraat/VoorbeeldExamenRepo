using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreCounter : MonoBehaviour
    {
        public static ScoreCounter Instance;
        public int currentScore = 0;
        public TMP_Text scoreText;

        private void Awake() => Instance = this;

        private void Start() => scoreText.text = "SCORE: " + currentScore;

        public void IncreaseScore(int v)
        {
            currentScore += v;
            scoreText.text = "SCORE: " + currentScore;
        }
    }
}
