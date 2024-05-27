using UnityEngine;
using UnityEngine.Audio;

namespace AIBERG{

public class SoundManager : MonoBehaviour
{
    // Singleton instance
    public static SoundManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to set the volume
    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    // Method to play a sound from a specified AudioSource with optional pitch range
    public void PlaySound(AudioSource source, AudioClip clip, float minPitch = 1f, float maxPitch = 1f)
    {
        source.pitch = Random.Range(minPitch, maxPitch);
        source.clip = clip;
        source.loop = false;
        source.Play();
    }

    // Method to play and loop a sound from a specified AudioSource with optional pitch range
    public void PlayLoopingSound(AudioSource source, AudioClip clip, float minPitch = 1f, float maxPitch = 1f)
    {
        source.pitch = Random.Range(minPitch, maxPitch);
        source.clip = clip;
        source.loop = true;
        source.Play();
    }

    // Method to stop a sound from a specified AudioSource
    public void StopSound(AudioSource source)
    {
        source.Stop();
    }

    // Method to stop a looping sound from a specified AudioSource
    public void StopLoopingSound(AudioSource source)
    {
        source.loop = false;
        source.Stop();
    }
}

}