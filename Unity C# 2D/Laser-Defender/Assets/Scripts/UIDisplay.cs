using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Health _health;
    [SerializeField] Slider _healthSlider;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI _scoreText;
    ScoreKeeper _scoreKeeper;

    void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        _healthSlider.maxValue = _health.GetHealth();
    }

    void Update()
    {
        _scoreText.text = _scoreKeeper.GetScore().ToString("0000000000000");
        _healthSlider.value = _health.GetHealth();
    }
}
