using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip musicaFondo;
    [SerializeField] private AudioClip audioGameOver;
    [SerializeField] private AudioClip audioVictoria;
    [SerializeField] private AudioClip sonidoDisparo;
    [SerializeField] private AudioClip sonidoBoton;

    private void Awake()
    {
        // Singleton seguro
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        ReproducirMusicaFondo();
    }

    private void OnDestroy()
    {
        // Evita suscripciones duplicadas
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOver")
        {
            DetenerMusicaFondo();
            ReproducirSFX(audioGameOver);
        }
        else if (scene.name == "Victoria")
        {
            DetenerMusicaFondo();
            ReproducirSFX(audioVictoria);
        }
        else
        {
            if (!musicSource.isPlaying)
                ReproducirMusicaFondo();
        }
    }

    private void ReproducirMusicaFondo()
    {
        if (musicaFondo == null || musicSource == null)
            return;

        musicSource.clip = musicaFondo;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void DetenerMusicaFondo()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    private void ReproducirSFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null)
            return;

        sfxSource.PlayOneShot(clip);
    }

public void ReproducirDisparo()
{
    Debug.Log("Intento reproducir sonido");

    if (sfxSource == null)
        Debug.LogError("NO HAY SFX SOURCE ASIGNADO");

    if (sonidoDisparo == null)
        Debug.LogError("NO HAY CLIP DE DISPARO ASIGNADO");

    sfxSource.PlayOneShot(sonidoDisparo);
}



    public void ReproducirBoton()
    {
        ReproducirSFX(sonidoBoton);
    }
}

