using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] TMP_Text _highScoreText;
    [SerializeField] TMP_Text _energyText;
    [SerializeField] AndroidNotificationHandler _androidNotificationHandler;
    [SerializeField] Button _playButton;
    [SerializeField] int _maxEnergy;
    [SerializeField] int _energyRechargeDurationMin;

    int _energy;

    const string EnergyKey = "Energy";
    const string EnergyReadyKey = "EnergyReady";
    const string RechargingKey = "IsRecharging";

    bool _inGame;
    bool _isRecharging;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreHandler.HighScoreKey);
        _highScoreText.text = $"High Score: {highScore}";

        _energy = PlayerPrefs.GetInt(EnergyKey, _maxEnergy);

        _inGame = false;

        SetEnergyReadyTime();
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
            return;

        RechargeEnergy();
    }

    public void Play()
    {
        if (_energy < 1)
            return;
        _energy--;
        PlayerPrefs.SetInt(EnergyKey, _energy);

        _inGame = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void RechargeEnergy()
    {
        _energyText.text = $"PLAY ({_energy})";

        CancelInvoke();

        //Made _inGame bool to not recharge instantly after clicking play button.
        if (_energy != 0 || _inGame)
            return;

        string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
        if (energyReadyString == string.Empty)
            return;

        DateTime energyReadyDateTime = DateTime.Parse(energyReadyString);
        if (DateTime.Now > energyReadyDateTime)
        {
            SetEnergyToMax();
        }
        else
        {
            _playButton.interactable = false;
            Invoke(nameof(SetEnergyToMax), (energyReadyDateTime - DateTime.Now).Seconds);
        }
    }

    void SetEnergyToMax()
    {
        _playButton.interactable = true;
        _energy = _maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, _energy);
        PlayerPrefs.SetInt(RechargingKey, 0); // 0 means not already recharging
        _energyText.text = $"PLAY ({_energy})";
    }

    void SetEnergyReadyTime()
    {
        if (PlayerPrefs.GetInt(RechargingKey, 0) == 1 ||_energy != 0)
            return;

        DateTime dateTimeForRecharge = DateTime.Now.AddMinutes(_energyRechargeDurationMin);
        PlayerPrefs.SetString(EnergyReadyKey, dateTimeForRecharge.ToString());
        PlayerPrefs.SetInt(RechargingKey, 1); //1 means not already recharging

#if UNITY_ANDROID
        _androidNotificationHandler.ScheduleNotification(dateTimeForRecharge);
#endif
    }
}
