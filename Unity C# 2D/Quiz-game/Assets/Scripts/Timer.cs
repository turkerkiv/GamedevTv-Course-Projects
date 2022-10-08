using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float _timeToPickAnswer = 30f;
    [SerializeField] float _timeToShowCorrectAnswer = 10f;

    bool _isAnsweringQuestion;
    float _timerValue;

    public bool _loadNextQuestion;
    public float _fillFraction;

    public bool IsAnsweringQuestion
    {
        get { return _isAnsweringQuestion; }
        set { _isAnsweringQuestion = value; }
    }

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        _timerValue = 0;
    }

    void UpdateTimer()
    {
        _timerValue -= Time.deltaTime;

        if (_isAnsweringQuestion)
        {
            if (_timerValue > 0)
            {
                _fillFraction = _timerValue / _timeToPickAnswer;
            }
            else
            {
                _isAnsweringQuestion = false;
                _timerValue = _timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (_timerValue > 0)
            {
                _fillFraction = _timerValue / _timeToShowCorrectAnswer;
            }
            else
            {
                _isAnsweringQuestion = true;
                _timerValue = _timeToPickAnswer;
                _loadNextQuestion = true;
            }
        }
    }
}
