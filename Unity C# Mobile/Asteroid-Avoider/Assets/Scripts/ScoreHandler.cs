using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] float _scorePerSecond = 5f;

    float _score;
    TextMeshProUGUI _scoreText;
    float _scorePerSecondAtStart;

    private void Awake()
    {
        _scoreText = GetComponentInChildren<TextMeshProUGUI>();
        _scorePerSecondAtStart = _scorePerSecond;
    }

    void Update()
    {
        _score += Time.deltaTime * _scorePerSecond;

        _scoreText.text = _score.ToString("0");
    }

    public int EndScore()
    {
        _scorePerSecond = 0;
        _scoreText.enabled = false;
        return Mathf.FloorToInt(_score);
    }

    public void StartTimer()
    {
        _scoreText.enabled = true;
        _scorePerSecond = _scorePerSecondAtStart;
    }
}
