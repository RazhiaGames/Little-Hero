using Joyixir.GameManager.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    public AudioListener audioListener;
    public AudioSource audioSource;
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    protected override void Awake()
    {
        base.Awake();
        audioSource.enabled = GMPrefs.IsMusic;
    }

    public void EnableMusic()
    {
        audioSource.enabled = GMPrefs.IsMusic;
    }
}
