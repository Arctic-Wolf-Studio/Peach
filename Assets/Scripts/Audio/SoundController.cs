using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public static SoundController Instance { get; private set; }

    public enum Sound {
        M_MainTheme,
        M_ForestTheme,
        SFX_Reload,
        SFX_ShotgunShot,
        SFX_FlyEyeDeathOne,
        SFX_FlyEyeDeathTwo,
        SFX_FlyEyeDeathThree,
    }
    
    private AudioSource audioSource;
    [SerializeField] private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private List<AudioClip> audioGameLibrary;

    private void Awake() {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        audioGameLibrary = new ();

        foreach (Sound sound in System.Enum.GetValues(typeof(Sound))) {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    private void Start() { 
        
    }


    public void PlaySound(Sound sound) {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound]);
    }

    public void StopSound() {
        audioSource.Stop();
    }


    public void StopAllSound() {

        for (int i = 0; i < audioGameLibrary.Count; i++) {
             audioSource.Pause();
        }
    }
}