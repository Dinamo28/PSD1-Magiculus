﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public GameObject musicController;
    public ControllerAudioMusic controllerAudioMusic;

    public Slider sliderMusic;
    public Slider sliderSound;

    public Toggle toggleCompleta;
    public Toggle toggleVentana;

    public GameObject soundManager;
    public ControllerAudio audioController;

    public Savedata savedata;

    void Awake()
    {
        musicController = GameObject.Find("MusicController");
        soundManager = GameObject.Find("SoundManager");
        savedata = GameObject.Find("Savedata").GetComponent<Savedata>();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioController = soundManager.GetComponent<ControllerAudio>();
        controllerAudioMusic = musicController.GetComponent<ControllerAudioMusic>();
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        sliderSound.value = PlayerPrefs.GetFloat("SoundVolume", 0.4f);
        controllerAudioMusic.MusicSrc.volume = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        controllerAudioMusic.PlaySong(controllerAudioMusic.bgmTitle);

        if (PlayerPrefs.GetInt("Fullscreen", 1) == 1)
        {
            toggleCompleta.isOn = true;
            toggleVentana.isOn = false;
            SetFullscreen(true);
        }
        else
        {
            toggleCompleta.isOn = false;
            toggleVentana.isOn = true;
            SetFullscreen(false);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetFloat("MusicVolume", 0.2f) != sliderMusic.value)
        {
            PlayerPrefs.SetFloat("MusicVolume", sliderMusic.value);
            PlayerPrefs.Save();
            controllerAudioMusic.MusicSrc.volume = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        }
        if (PlayerPrefs.GetFloat("SoundVolume", 0.4f) != sliderSound.value)
        {
            PlayerPrefs.SetFloat("SoundVolume", sliderSound.value);
            PlayerPrefs.Save();
        }
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.SetResolution(1920, 1080, fullscreen);
    }

    public void TogglePantalla()
    {
        audioController.PlaySound(audioController.sndClick);
        if (toggleCompleta.isOn)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
            PlayerPrefs.Save();
            SetFullscreen(true);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
            PlayerPrefs.Save();
            SetFullscreen(false);
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetAfterCreditsScene(string afterCreditsScene)
    {
        savedata.SetAfterCreditsScene(afterCreditsScene);
    }
}
