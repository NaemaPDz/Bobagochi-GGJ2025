using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { private set; get; }

    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource soundEffectsSource;

    private float musicVolume = 0.25f;
    private float sfxVolume = 0.25f;

    private SoundDatabase sound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        sound = Resources.Load<SoundDatabase>("Data/SoundDatabase");
    }

    private void Start()
    {
        backgroundMusicSource.volume = musicVolume;
        soundEffectsSource.volume = sfxVolume;
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusicSource.clip = sound.bgm;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    public void PlaySoundEffect(string name)
    {
        soundEffectsSource.PlayOneShot(sound.SoundDatas.FirstOrDefault(i => i.soundName == name).soundClip);
    }
}
