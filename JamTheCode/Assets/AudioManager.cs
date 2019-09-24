using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class AudioManager : MonoBehaviour
{
    #region Public variables

    public Sound[] DefaultSounds;                           //Default sounds that will be attached to the audiomanager at start.
    public List<BackgroundMusic> BackgroundMusics;

    [Serializable]
    public class Sound
    {
        public AudioClip Clip;
        public float Volume = 1f;
        public bool Loop;
        public bool KeepAfterPlayed;
        public bool PlayImmediately;
    }

    [Serializable]
    public class BackgroundMusic
    {
        public AudioClip Clip;
        public List<string> ForScenes;
        public float FadeInSpeed = 0.1f;
        public float FadeOutSpeed = 0.1f;
        public float Volume = 1f;
        public bool FadeIn = true;
        public bool FadeOut = false;
    }

    #endregion

    #region Private variables

    private Dictionary<string, AudioSource> _AudioSources;
    private GameObject _BGMusic;

    private BackgroundMusic _CurrentBackgroundMusic;

    #endregion

    // Use this for initialization
    void Awake()
    {
        _BGMusic = transform.FindChild("BackgroundMusic").gameObject;

        _CurrentBackgroundMusic = null;

        _AudioSources = new Dictionary<string, AudioSource>();
        //Add the default sounds to the list.
        for (int i = 0; i < DefaultSounds.Length; i++)
            CreateSource(DefaultSounds[i]);
    }

    /// <summary>
    /// Add a new sound to the sounds that can be played on the fly.
    /// </summary>
    /// <param name="newSound"></param>
    public void AddNewSound(Sound newSound)
    {
        if (_AudioSources.ContainsKey(newSound.Clip.name))
            return;
        CreateSource(newSound);
    }

    /// <summary>
    /// Get a audiosource that is currently playing or attached to the audiomanager.
    /// </summary>
    /// <param name="clipName"></param>
    /// <returns></returns>
    public AudioSource GetAudioSource(string clipName)
    {
        return _AudioSources[clipName];
    }

    /// <summary>
    /// Play a background music. NOTE: only one background music can be played at a time.
    /// </summary>
    /// <param name="music">The music you want to be played.</param>
    public void PlayBackGroundMusic(BackgroundMusic music)
    {
        AudioSource bgAudioSource = _BGMusic.GetComponent<AudioSource>();

        
            bgAudioSource.Stop();
            bgAudioSource.volume = music.Volume;
            bgAudioSource.clip = music.Clip;
            bgAudioSource.Play();

        _CurrentBackgroundMusic = music;
    }

    /// <summary>
    /// Play a background music. NOTE: only one background music can be played at a time.
    /// </summary>
    /// <param name="forScene">Play the defaultbackgroundmusic for this level, if there's one</param>
    public void PlayBackGroundMusic(string forScene)
    {
        BackgroundMusic music = BackgroundMusics.Find(m => m.ForScenes.Contains(forScene));
        if (music == null)
        {
            if (_CurrentBackgroundMusic != null)
            {
                _BGMusic.GetComponent<AudioSource>().Stop();
            }

            Debug.LogWarning("[AudioManager] No background music found for scene [" + forScene + "] stopping the music!");

            _CurrentBackgroundMusic = null;
            return;
        }

        if (_CurrentBackgroundMusic != null && _CurrentBackgroundMusic.ForScenes.Contains(forScene))
        {
            Debug.Log("[AudioManager] Already playing the background music for scene [" + forScene + "]");
            return;
        }


        PlayBackGroundMusic(music);
    }


    /// <summary>
    /// Loop a sound clip.
    /// </summary>
    /// <param name="clip">Clip to be looped.</param>
    public void LoopClip(AudioClip clip)
    {
        CreateSource(new Sound
        {
            Clip = clip,
            PlayImmediately = true,
            KeepAfterPlayed = false,
            Loop = true
        });
    }

    /// <summary>
    /// Play a clip once, will be removed after being played.
    /// </summary>
    /// <param name="clip">Clip to play.</param>
    public void PlayClipOnce(AudioClip clip)
    {
        CreateSource(new Sound
        {
            Clip = clip,
            PlayImmediately = true,
            KeepAfterPlayed = false,
            Loop = false
        });
    }

    /// <summary>
    /// Play clip that's already attached to the audiomanager.
    /// </summary>
    /// <param name="clipName"></param>
    public void PlayClip(string clipName)
    {
        if (_AudioSources.ContainsKey(clipName))
        {
            _AudioSources[clipName].Play();
            return;
        }

        Debug.LogWarning("No clip with the name [" + clipName + "] exists in the [AudioManager], add it first.");
    }

    /// <summary>
    /// Play a sound, can be set to keep after played, or looped.
    /// </summary>
    /// <param name="sound">Sound to play</param>
    public void PlaySound(Sound sound)
    {
        if (_AudioSources.ContainsKey(sound.Clip.name))
        {
            _AudioSources[sound.Clip.name].Play();
            return;
        }

        CreateSource(sound);
    }

    /// <summary>
    /// Create an audio source and attach it to the audiomanager.
    /// </summary>
    /// <param name="newSound"></param>
    private void CreateSource(Sound newSound)
    {
        //Create a new soundsource object on which the sound will be played.
        GameObject soundSource = new GameObject();
        soundSource.transform.SetParent(transform);
        soundSource.name = newSound.Clip.name;

        AudioSource newSource = soundSource.AddComponent<AudioSource>();

        newSource.clip = newSound.Clip;
        newSource.loop = newSound.Loop;
        newSource.volume = newSound.Volume;

        if (newSound.PlayImmediately)
            newSource.Play();

        if (newSound.KeepAfterPlayed || newSound.Loop)
            _AudioSources.Add(newSound.Clip.name, newSource);
        else
            StartCoroutine(WaitForEndAndRemove(newSource));
    }

    /// <summary>
    /// Remove an audiosource after it has player its clip.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private IEnumerator WaitForEndAndRemove(AudioSource source)
    {
        //Wait for the source if it's still playing
        while (source.isPlaying)
        {
            yield return null;
        }

        //Remove the source after playing.
        source.Stop();
        Destroy(source.gameObject);
    }
}
