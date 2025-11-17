using System.Collections.Generic;
using UnityEngine;

public class SoundContainer : MonoBehaviour, ISoundContainer
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private GameSound[] _sounds;

    private const float VolumeScale = 0.75f;

    private readonly Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>(10);

    private AudioClip _currentClip;

    private void OnValidate()
    {
        _source ??= GetComponent<AudioSource>();
    }

    private void Awake()
    {
        foreach (var sound in _sounds)
            _clips.Add(sound.Name, sound.Clip);
    }

    public void Play(string soundName)
    {
        if (_clips.TryGetValue(soundName, out AudioClip clip))
        {
            _currentClip = clip;
            PlayInternal();
        }
        else
        {
            Debug.LogError($"Sound for ability {soundName} not found");
        }
    }

    public void PlayWhenFree(string soundName)
    {
        if (_source.isPlaying)
            return;

        Play(soundName);
    }

    public void Stop()
    {
        _source.Stop();
    }

    protected void PlayCycle(string soundName)
    {
        if (_clips.TryGetValue(soundName, out AudioClip clip))
        {
            _source.clip = clip;
            _source.Play();
        }
        else
        {
            Debug.LogError($"Sound for ability {soundName} not found");
        }
    }

    protected void SetPitch(float value) => _source.pitch = value;

    private void PlayInternal()
    {
        if (_currentClip == null)
        {
            Debug.LogError("Current sound is null");
            return;
        }

        _source.PlayOneShot(_currentClip, VolumeScale);
    }

    public void MuteToggle(bool isOn) => _source.mute = isOn;
}
