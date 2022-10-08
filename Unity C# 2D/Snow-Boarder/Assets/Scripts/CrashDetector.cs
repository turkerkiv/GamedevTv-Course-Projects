using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float _sceneReloadDelay = 1f;
    [SerializeField] PlayerController _playerController;
    [SerializeField] ParticleSystem _crashEffect;
    [SerializeField] AudioClip _crashSFX;
    private bool _hasCrashed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" && !_hasCrashed)
        {
            _playerController.DisableControls();

            _hasCrashed = true;
            GetComponent<AudioSource>().PlayOneShot(_crashSFX);
            _crashEffect.Play();

            Debug.Log("Ouch!");
            Invoke("ReloadScene", _sceneReloadDelay);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
