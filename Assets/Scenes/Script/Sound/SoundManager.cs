using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }

    public float masterVolume = 1f;
    public float sfxVolume = 1f;
    public float bgmVolume = 1f;

    private AudioSource bgmSource;
    private Dictionary<SoundType, AudioClip> audioClips = new();

    public enum SoundType
    {
        None,
        ButtonClick,
        Jump,
        Explosion,
        Coin,
        MainTheme,
        BossTheme,
        VictoryFanfare,
        GameOver
    }

    [System.Serializable]
    public class SoundItem
    {
        public SoundType type;
        public AudioClip clip;
    }

    public SoundItem[] preloadedSounds;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        LoadPreloadedSounds();
    }

    private void LoadPreloadedSounds()
    {
        foreach (var soundItem in preloadedSounds)
        {
            if (soundItem.clip != null)
            {
                audioClips[soundItem.type] = soundItem.clip;
            }
        }
    }

    public void LoadSound(SoundType type, AudioClip clip)
    {
        audioClips[type] = clip;
    }

    public void LoadSoundFromResources(SoundType type, string path)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip != null)
        {
            audioClips[type] = clip;
        }
        else
        {
            Debug.LogWarning($"オーディオクリップのパスが見つかりません。: {path}");
        }
    }

    public void PlaySFX(SoundType type, float volume = 1f)
    {
        if (audioClips.TryGetValue(type, out AudioClip clip))
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume * sfxVolume * masterVolume);
        }
        else
        {
            Debug.LogWarning($"サウンド {type} が見つかりません。");
        }
    }

    public void PlayBGM(SoundType type, bool fade = false, float fadeDuration = 1f)
    {
        if (audioClips.TryGetValue(type, out AudioClip clip))
        {
            if (fade)
            {
                StartCoroutine(FadeBGM(clip, fadeDuration));
            }
            else
            {
                bgmSource.clip = clip;
                bgmSource.volume = bgmVolume * masterVolume;
                bgmSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"BGM {type} が見つかりません。");
        }
    }

    private System.Collections.IEnumerator FadeBGM(AudioClip newClip, float fadeDuration)
    {
        float startVolume = bgmSource.volume;
        float timer = 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(startVolume, 0, timer / fadeDuration);
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.clip = newClip;
        bgmSource.Play();

        timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(0, bgmVolume * masterVolume, timer / fadeDuration);
            yield return null;
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume * masterVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume * masterVolume;
    }
}