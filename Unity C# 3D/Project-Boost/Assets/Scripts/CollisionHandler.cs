using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float _delayToLoadNext = 1f;
    [SerializeField] float _delayToReload = 1f;
    [SerializeField] AudioClip _winningSound;
    [SerializeField] AudioClip _crashSound;
    [SerializeField] ParticleSystem _winningParticles;
    [SerializeField] ParticleSystem _crashParticles;

    AudioSource _audioSource;

    bool _isTransitioning;
    bool _isCollisionDisabled;


    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }

    void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning || _isCollisionDisabled)
            return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartWinningSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartWinningSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_winningSound);
        _winningParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", _delayToLoadNext);
    }

    void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_crashSound);
        _crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", _delayToReload);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            _isCollisionDisabled = !_isCollisionDisabled;
        }
    }

}
