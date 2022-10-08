using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int _correctAnswers;
    int _questionSeen;

    public int CorrectAnswers
    {
        get { return _correctAnswers; }
        set { _correctAnswers = value; }
    }

    public int QuestionSeen
    {
        get { return _questionSeen; }
        set { _questionSeen = value; }
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(_correctAnswers / (float)_questionSeen * 100);
    }
}
