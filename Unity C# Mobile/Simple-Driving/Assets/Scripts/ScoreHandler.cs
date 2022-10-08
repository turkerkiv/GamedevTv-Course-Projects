using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] float _scorePerSecond;

    public const string HighScoreKey = "HighScore";

    float _score;

    void Update()
    {
        _score += _scorePerSecond * Time.deltaTime;

        _scoreText.text = _score.ToString("0");
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if ((int)_score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, (int)_score);
        }
    }
}
