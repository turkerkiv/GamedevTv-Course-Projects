using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //istenirse can da eklenebilir
    GameOverHandler _gameOverHandler;
    
    private void Awake()
    {
        _gameOverHandler = FindObjectOfType<GameOverHandler>();
    }
    
    public void Crash()
    {
        transform.parent.gameObject.SetActive(false);
        _gameOverHandler.EndGame();
    }
}
