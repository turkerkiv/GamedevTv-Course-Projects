using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> _questions;
    QuestionSO _currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image _timerImage;
    Timer _timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI _scoreText;
    ScoreKeeper _scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider _progressBar;

    private bool _isComplete;

    public bool IsComplete
    {
        get { return _isComplete; }
        set { _isComplete = value; }

    }

    void Awake()
    {
        _timer = FindObjectOfType<Timer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _progressBar.maxValue = _questions.Count;
        _progressBar.value = 0;
    }

    void Start()
    {
        _scoreText.text = "Score\n0%";
    }

    void Update()
    {
        _timerImage.fillAmount = _timer._fillFraction;

        if (_timer._loadNextQuestion)
        {
            if (_progressBar.value == _progressBar.maxValue)
            {
                _isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            _timer._loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !_timer.IsAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void GetNextQuestion()
    {
        if (_questions.Count > 0)
        {
            SetDefaultButtonSprites();
            SetButtonState(true);
            GetRandomQuestion();
            DisplayQuestion();
            _progressBar.value++;
            _scoreKeeper.QuestionSeen++;
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, _questions.Count);

        if (_questions[index] != null)
        {
            _currentQuestion = _questions[index];
        }
        if (_questions.Contains(_currentQuestion))
        {
            _questions.Remove(_currentQuestion);
        }
    }

    void DisplayQuestion()
    {
        questionText.text = _currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI answerButtonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerButtonText.text = _currentQuestion.GetAnswer(i);
        }
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == _currentQuestion.GetCorrectAnswerIndex())
        {
            TextMeshProUGUI correctAnswerButtonText = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            questionText.text = correctAnswerButtonText.text + " doğru cevap!!!";

            buttonImage = answerButtons[index].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;

            _scoreKeeper.CorrectAnswers++;
        }
        else
        {
            correctAnswerIndex = _currentQuestion.GetCorrectAnswerIndex();
            string correctAnswerText = _currentQuestion.GetAnswer(correctAnswerIndex);

            if (index >= 0)
            {
                TextMeshProUGUI wrongAnswerButtonText = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>();
                questionText.text = wrongAnswerButtonText.text + " yanlış cevaptı... \n Doğru cevap: " + correctAnswerText;

                buttonImage = answerButtons[index].GetComponentInChildren<Image>();
                buttonImage.sprite = wrongAnswerSprite;

            }
            else
            {
                questionText.text = "Boş bıraktınız...\nDoğru cevap: " + correctAnswerText;
            }

            buttonImage = answerButtons[correctAnswerIndex].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;

        }
    }
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;

        DisplayAnswer(index);

        SetButtonState(false);

        _timer.CancelTimer();

        _scoreText.text = "Score\n" + _scoreKeeper.CalculateScore() + "%";
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button buttonInteracts = answerButtons[i].GetComponent<Button>();
            buttonInteracts.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image defaultButtonImage = answerButtons[i].GetComponent<Image>();
            defaultButtonImage.sprite = defaultAnswerSprite;
        }
    }

}
