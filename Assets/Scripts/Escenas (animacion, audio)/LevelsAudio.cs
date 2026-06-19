using UnityEngine;

public class LevelsAudio : MonoBehaviour
{
    public static LevelsAudio instance;
    private AudioSource audioSource;
    private AudioClip musicaFondo;
    [SerializeField] private AudioClip musicaDerrota;
    [SerializeField] private AudioClip musicaVictoria;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.enabled = true;
            audioSource.loop = true;
            musicaFondo = audioSource.clip;
            audioSource.Play();
        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
            Destroy(gameObject);
            return;
        }
    }

    public void DetenerMusica()
    {
        audioSource.Stop();
    }

    public void ReanudarMusica()
    {
        audioSource.Stop();
        audioSource.clip = musicaFondo;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void CambiarMusicaDerrota()
    {
        audioSource.Stop();
        audioSource.clip = musicaDerrota;
        audioSource.Play();
    }

    public void CambiarMusicaVictoria()
    {
        audioSource.Stop();
        audioSource.clip = musicaVictoria;
        audioSource.Play();
    }
}
