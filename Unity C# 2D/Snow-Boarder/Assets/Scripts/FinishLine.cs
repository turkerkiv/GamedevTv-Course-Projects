using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float _sceneReloadDelay = 1f;
    [SerializeField] ParticleSystem _finishEffect;
    private bool _hasWin = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !_hasWin)
        {
            _hasWin = true;
            GetComponent<AudioSource>().Play();
            _finishEffect.Play();
            Debug.Log("Winner is " + other.name);
            Invoke("ReloadScene", _sceneReloadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
