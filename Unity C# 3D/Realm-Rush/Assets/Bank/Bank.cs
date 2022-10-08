using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int _startingBalance = 150;
    [SerializeField] TextMeshProUGUI _goldText;

    int _currentBalance;

    public int CurrentBalance { get { return _currentBalance; } }

    private void Awake()
    {
        _currentBalance = _startingBalance;
        DisplayBalance();
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
        DisplayBalance();
    }

    public void WithDraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);
        DisplayBalance();

        if(_currentBalance < 0)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void DisplayBalance()
    {
        _goldText.text = "Gold: " + _currentBalance;
    }
}
