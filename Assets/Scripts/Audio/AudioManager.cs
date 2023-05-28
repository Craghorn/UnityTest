using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.playOnAwake = s.playOnAwake;
            s.source.volume = SettingsHandler.levelSound;
            s.source.loop = s.isLoop;

            //check for type of sound
            
            switch (s.audioType)
            {
                case Sound.AudioTypes.soundEffect:
                    s.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                    break;

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            if (s.playOnAwake)
                s.source.Play();
        }
    }

   public void PlayClipByName(string _clipName)
   {
        //lf array
        Sound soundToPlay = System.Array.Find(sounds, s => s.clipName == _clipName);
        soundToPlay.source.Play();

        //same loop
        //foreach (Sound s in sounds)
        //{
        //    if (s.clipName == _clipName)
        //        soundToPlay = s;
        //}
   }

    public void StopClipByName(string _clipName)
    {
        //lf array
        Sound soundToStop = System.Array.Find(sounds, s => s.clipName == _clipName);
        soundToStop.source.Stop();

        //same loop
        //foreach (Sound s in sounds)
        //{
        //    if (s.clipName == _clipName)
        //        soundToPlay = s;
        //}
    }

    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("Music Mixer", Mathf.Log10(SettingsHandler.levelSound) * 20);
        soundEffectsMixerGroup.audioMixer.SetFloat("Sound Mixer", Mathf.Log10(SettingsHandler.levelSound) * 20);
    }

    public void UpdateMusicVolume()
    {
        if (SettingsHandler.onMusic)
            musicMixerGroup.audioMixer.SetFloat("Music Mixer", Mathf.Log10(SettingsHandler.levelSound) * 20);
        else
            musicMixerGroup.audioMixer.SetFloat("Music Mixer", -80);
    }
    public void UpdateSoundVolume()
    {
        if (SettingsHandler.onSounds)
            soundEffectsMixerGroup.audioMixer.SetFloat("Sound Mixer", Mathf.Log10(SettingsHandler.levelSound) * 20);
        else
            musicMixerGroup.audioMixer.SetFloat("Sound Mixer", -80);
    }    

    public void MuteBackgroundMusic()
    {
        musicMixerGroup.audioMixer.SetFloat("Music Mixer", -80);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //todo add checker for vlc gameobject with music
    }
}
