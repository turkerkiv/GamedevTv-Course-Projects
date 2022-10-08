using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip _shootingClip;
    [SerializeField][Range(0f, 1f)] float _shootingVolume = 1f;

    [Header("Explosion")]
    [SerializeField] AudioClip _damageClip;
    [SerializeField][Range(0f, 1f)] float _damageVolume = 1f;

    static AudioPlayer _audioPlayerInstance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if(_audioPlayerInstance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            _audioPlayerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(_shootingClip, _shootingVolume);
    }

    public void PlayExplosionClip()
    {
        PlayClip(_damageClip, _damageVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
