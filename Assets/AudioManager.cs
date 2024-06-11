using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public static AudioManager instance;
    public float loopStartTime = 6.957f;
    public AudioClip background, jump, wall, death;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        musicSource.clip = background;
        musicSource.Play();
    }
    void Update()
    {
        if (musicSource.time >= 205.813)
        {
            musicSource.time = loopStartTime;
        }
    }
}
