using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _gameOverText;
    [SerializeField] GameObject _gameOverDisplay;
    [SerializeField] Button _continueButton;

    AsteroidSpawner _asteroidSpawner;
    ScoreHandler _scoreHandler;
    PlayerMovement _playerMovement;

    private void Awake()
    {
        _asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
        _scoreHandler = FindObjectOfType<ScoreHandler>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void EndGame()
    {
        _asteroidSpawner.enabled = false;

        int finalScore = _scoreHandler.EndScore();

        _gameOverText.text = $"GAME OVER \nYour score: {finalScore}";
        _gameOverDisplay.SetActive(true);
    }

    public void ContinueGame()
    {
        _scoreHandler.StartTimer();

        _playerMovement.transform.position = Vector3.zero;
        _playerMovement.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _playerMovement.gameObject.SetActive(true);

        _asteroidSpawner.enabled = true;

        _gameOverDisplay.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueWatchAd()
    {
        AdManager.Instance.ShowAd(this);
        
        _continueButton.interactable = false;
    }
}
