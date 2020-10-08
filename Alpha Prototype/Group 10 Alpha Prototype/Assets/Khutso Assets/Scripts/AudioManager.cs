using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Temp Audio Manager", typeof(AudioManager)).GetComponent<AudioManager>();

                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    //Sources
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource soundEffectsSource;

    bool isSourceOnePlaying;
    bool isMusicSourceOne;

    private float timer;
    //Initialising
    private void Awake()
    {
        //persist throughout scenes
        DontDestroyOnLoad(this.gameObject);

        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        soundEffectsSource = this.gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource2.loop = true;
    }

    //Play sounds.
    public void PlayMusic(AudioClip musicClip)
    {
        //which souce is playing?
        AudioSource playingSource = (isSourceOnePlaying) ? musicSource : musicSource2;

        playingSource.clip = musicClip;
        playingSource.volume = 0.5f;
        playingSource.Play();
    }
    public void PlayFadeMusic(AudioClip newMusicClip, float trasistionTime = 1f)
    {
        AudioSource playingSource = (isSourceOnePlaying) ? musicSource : musicSource2;

        StartCoroutine(UpdateFadingMusic(playingSource, newMusicClip, trasistionTime));
    }
    public void PlayChangingMusic(AudioClip newMusicClip, float transitionTime = 1f, float volume = 1f)
    {
        //find out which source is playing music
        AudioSource playingSource = (isMusicSourceOne) ? musicSource : musicSource2;
        AudioSource newSource = (isMusicSourceOne) ? musicSource2 : musicSource;

        //swap the sources
        isMusicSourceOne = !isMusicSourceOne;
        //set paramiters for the new source and then switch songs
        newSource.clip = newMusicClip;
        newSource.volume = volume;
        newSource.Play();
        StartCoroutine(UpdateChangingMusic(playingSource, newSource, transitionTime));

    }

    public IEnumerator UpdateFadingMusic(AudioSource playingSource, AudioClip newMusicClip, float trasistionTime)
    {
        if (!playingSource.isPlaying)
            playingSource.Play();

        float tyme = 0.0f;

        //fade out
        for (tyme = 0; tyme < trasistionTime; tyme += Time.deltaTime)
        {
            playingSource.volume = (1 - (tyme / trasistionTime));
            yield return null;
        }

        //stop the current song to change
        playingSource.Stop();
        playingSource.clip = newMusicClip;
        playingSource.Play();

        //fade in the new song
        for (tyme = 0; tyme < trasistionTime; tyme += Time.deltaTime)
        {
            playingSource.volume = (tyme / trasistionTime);
            yield return null;
        }

    }
    public IEnumerator UpdateChangingMusic(AudioSource origionalSource, AudioSource newMusicClip, float trasistionTime)
    {
        float tyme = 0.0f;
        for (tyme = 0; tyme < trasistionTime; tyme += Time.deltaTime)
        {
            origionalSource.volume = (1 - (tyme / trasistionTime));
            newMusicClip.volume = (tyme / trasistionTime);
            yield return null;
        }

        origionalSource.Stop();
    }
    //Play sound effects
    public void PlayEffects(AudioClip soundClip)
    {
        soundEffectsSource.PlayOneShot(soundClip);
    }
    public void PlayEffects(AudioClip soundClip, float volume)
    {
        soundEffectsSource.PlayOneShot(soundClip, volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }
    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectsSource.volume = volume;
    }


    //**********************************************//
    public void DroneTest(AudioClip Track, float Volume, float TransisionTime)
    {
        AudioSource Sound = (isSourceOnePlaying) ? musicSource : musicSource2;
        Sound.clip = Track;
        float soundLength = Sound.clip.length - 0.2f;
        if (Sound.isPlaying)
        {
            timer += Time.deltaTime;
        }
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (timer >= soundLength)
            {
                //  audio.PlayOneShot(sound);
                // audio.Play();
                AudioManager.Instance.PlayMusic(Track);
                timer = 0.0f;
            }
        }
    }



}
