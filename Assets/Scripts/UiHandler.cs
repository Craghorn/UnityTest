using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    public Toggle musicToggle;
    public Toggle soundToggle;
    public Slider volumeSlider;

    public TMP_InputField serverAddressInputfield;
    public TMP_InputField serverPortInputfield;
    public TMP_InputField streamAddressInputfield;

    public GameObject audioManager;


    private void Awake()
    {
        audioManager = GameObject.Find("Audio Manager");

        // Add read from file address and ports and write to static
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            
            musicToggle.isOn = SettingsHandler.onMusic;
            soundToggle.isOn = SettingsHandler.onSounds;
            volumeSlider.value = SettingsHandler.levelSound;

            serverAddressInputfield.text = SettingsHandler.serverAddress;
            serverPortInputfield.text = SettingsHandler.serverPort;
            streamAddressInputfield.text = SettingsHandler.streamAddress;
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickM()
    {
        SettingsHandler.onMusic = musicToggle.isOn;        
        audioManager.GetComponent<AudioManager>().UpdateMusicVolume();
    }
    public void ClickS()
    {
        SettingsHandler.onSounds = soundToggle.isOn;
        audioManager.GetComponent<AudioManager>().UpdateSoundVolume();
    }
    public void SliderSoundChanged()
    {
        SettingsHandler.levelSound = volumeSlider.value;
        audioManager.GetComponent<AudioManager>().UpdateMixerVolume();
    }
    public void ServerAddressChanged()
    {
        SettingsHandler.serverAddress = serverAddressInputfield.text;
    }
    public void ServerPortChanged()
    {
        SettingsHandler.serverPort = serverPortInputfield.text;
    }
    public void SteamAddressChanged()
    {
        SettingsHandler.streamAddress = streamAddressInputfield.text;
    }
    public void MenuButton()
    {
        audioManager.GetComponent<AudioManager>().PlayClipByName("ClickClip");

        if (SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(1);
        else
        {
            SaveDataHandler.instance.SaveData();
            SceneManager.LoadScene(0);
        }
            
    }
}
