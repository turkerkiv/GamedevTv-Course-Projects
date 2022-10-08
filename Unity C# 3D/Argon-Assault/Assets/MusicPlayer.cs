using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer _musicPlayer;

    private void Awake()
    {  
        if(_musicPlayer != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _musicPlayer = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
