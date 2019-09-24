using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance
    {
        get
        {
            return s_Instance;
        }
    }
    private static SoundManager s_Instance;


    //Single audio files
    private AudioClip _towerCrumble;
    private AudioClip _towerShoot;
    private AudioClip _bombExplode;
    private AudioClip _sandTrack;
    private AudioClip _mainExplosion;
    private AudioClip _freeze;

    private AudioSource bombExplode;
    private AudioSource towerShoot;
    private AudioSource towerCrumble;
    private AudioSource mainExplosion;
    private AudioSource soundTrack;
    private AudioSource freeze;
    private ArrayList activeSounds = new ArrayList();

    void Start()
    {
        activeSounds = new ArrayList();

        //Singleton initialization
        s_Instance = this;

        //List of all sounds
        _towerCrumble = (AudioClip)Resources.Load("Sounds/TowerCrumble");
        _towerShoot = (AudioClip)Resources.Load("Sounds/TowerShoot");
        _bombExplode = (AudioClip)Resources.Load("Sounds/BombExplode");
        _sandTrack = (AudioClip)Resources.Load("Sounds/SandTrack");
        _mainExplosion = (AudioClip)Resources.Load("Sounds/MainExplosion");
        _freeze = (AudioClip)Resources.Load("Sounds/Freeze");



        freeze = gameObject.AddComponent<AudioSource>();
        freeze.clip = _freeze;
        freeze.volume = 0.25f;


        mainExplosion = gameObject.AddComponent<AudioSource>();
        mainExplosion.clip = _mainExplosion;
        mainExplosion.volume = 0.8f;


        bombExplode = gameObject.AddComponent<AudioSource>();
        bombExplode.clip = _bombExplode;
        bombExplode.volume = 0.2f;

        towerShoot = gameObject.AddComponent<AudioSource>();
        towerShoot.clip = _towerShoot;

        towerShoot.volume = 0.5f;

        soundTrack = gameObject.AddComponent<AudioSource>();
        soundTrack.clip = _sandTrack;
        soundTrack.volume = 1f;
        soundTrack.loop = true;

        PlayMusic();
    }

    void Update()
    {

    }


    /*
    Several methodes that play a specific sound. These methodes can be reached by using the Singleton.
    */
    public void PlayTowerCrumble()
    {
        AudioSource towerCrumble = gameObject.AddComponent<AudioSource>();
        towerCrumble.clip = _towerCrumble;

        towerCrumble.volume = 0.08f;
        towerCrumble.Play();
        activeSounds.Add(towerCrumble);
    }

    public void PlayTowerShoot()
    {
        towerShoot.Play();
    }

    public void PlayBombExplode()
    {
        bombExplode.Play();
    }
    public void PlayMainExplosion()
    {
        mainExplosion.Play();
    }
    public void PlayFreeze()
    {
        freeze.Play();
    }

    public void PlayMusic()
    {
        soundTrack.Play();
    }
}
