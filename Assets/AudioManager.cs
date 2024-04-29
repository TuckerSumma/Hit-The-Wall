using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip background, jump, wall, death;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    } 
}
