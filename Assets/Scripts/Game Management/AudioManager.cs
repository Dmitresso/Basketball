using UnityEngine;

public struct AudioSrc {
    public static AudioSource Soundtrack, SceneSound, PlayerSound, BallSound;
}


public class AudioManager : Singleton<MonoBehaviour> {
    private SettingsMenu settingsMenu;
    
    [HideInInspector] public AudioManager audioManager;

    private void Awake() {
        audioManager = this;
        
        var sources = GetComponents<AudioSource>();
        AudioSrc.Soundtrack = sources[0];
        AudioSrc.SceneSound = sources[1];
        AudioSrc.PlayerSound = sources[2];
        AudioSrc.BallSound = sources[3];

        AudioSrc.Soundtrack.volume = Settings.UserVolume;
        AudioSrc.SceneSound.volume = Settings.UserVolume;
        AudioSrc.PlayerSound.volume = Settings.UserVolume;
        AudioSrc.BallSound.volume = Settings.UserVolume * 0.5f;

        
        AudioSrc.Soundtrack.loop = true;
        if (gameObject.scene.name == Scenes.Main) {
            AudioSrc.Soundtrack.clip = Res.Music.MainSoundtrack;
            AudioSrc.Soundtrack.Play();
        }
        else if (gameObject.scene.name == Scenes.Hoops) {
            AudioSrc.Soundtrack.clip = Res.Music.HoopsSoundtrack;
            AudioSrc.Soundtrack.Play();
        }
    }
    
    public void Play(AudioClip audioClip) {
        AudioSrc.SceneSound.PlayOneShot(audioClip);
    }
    
    public void Play(AudioClip audioClip, float volumeScale) {
        if (volumeScale > Settings.UserVolume) volumeScale = Settings.UserVolume;
        AudioSrc.SceneSound.PlayOneShot(audioClip, volumeScale);
    }
    
    public void Play(AudioClip audioClip, AudioSource audioSource) {
        audioSource.PlayOneShot(audioClip);
    }
    
    public void Play(AudioClip audioClip, AudioSource audioSource, float volumeScale) {
        if (volumeScale > Settings.UserVolume) volumeScale = Settings.UserVolume;
        
        audioSource.PlayOneShot(audioClip, volumeScale);
    }

    
    public void Stop() {
        AudioSrc.SceneSound.Stop();
    }
    
    public void Stop(AudioSource audioSource) {
        audioSource.Stop();
    }

    
    public void SetVolume(AudioSource audioSource, float volume) {
        audioSource.volume = volume > Settings.UserVolume ? Settings.UserVolume : volume;
    }

    public void SetVolume(float volume) {
        AudioSrc.Soundtrack.volume = volume;
        AudioSrc.BallSound.volume = volume > Settings.UserVolume ? Settings.UserVolume : volume;
        AudioSrc.PlayerSound.volume = volume > Settings.UserVolume ? Settings.UserVolume : volume;
        AudioSrc.SceneSound.volume = volume > Settings.UserVolume ? Settings.UserVolume : volume;
    }
    
    
    public bool IsPlaying(AudioSource audioSource) {
        return audioSource.isPlaying;
    }
    
    public bool IsPlaying() {
        return AudioSrc.SceneSound.isPlaying;
    }
}