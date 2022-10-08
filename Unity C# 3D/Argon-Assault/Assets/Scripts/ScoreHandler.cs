using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    TextMeshProUGUI _scoreTMP;

    int _score;

    private void Awake()
    {
        _scoreTMP = GetComponent<TextMeshProUGUI>();
        _scoreTMP.text = $"Score: {_score}";
    }

    public void IncreaseScore(int amount)
    {
        _score += amount;
        _scoreTMP.text = $"Score: {_score}";
    }
}
