using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _finalScoreText;
    ScoreKeeper _scoreKeeper;
    
    void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        _finalScoreText.text = "Tebrikler, oyunu bitirdiniz.\nSkorunuz " + _scoreKeeper.CalculateScore() + "%";
    }
}
