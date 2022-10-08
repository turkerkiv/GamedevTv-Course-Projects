using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] bool _testMode = true;
    GameOverHandler _gameOverHandler;
    public static AdManager Instance;

#if UNITY_ANDROID
    string _gameId = "4879098";
#endif

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
        _gameOverHandler = gameOverHandler;

        Advertisement.Show("Rewarded_Android", this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("initialization completed");
        Advertisement.Load("Rewarded_Android", this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"initialization failed {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"ad loading completed {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"ad loading failed {placementId}: {error} - {message}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        //logic switch case
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                _gameOverHandler.ContinueGame();
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                //ad was skipped
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                Debug.LogWarning("Ad state unknown");
                break;
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"ads show failure {placementId}: {error} - {message}");
    }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowStart(string placementId) { }
}
