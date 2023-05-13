using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public struct AudioFile
{
    public string NAME;
    public AudioClip audioClip;
    [HideInInspector] public AudioSource audioSource;
    [Range(0f, 3f)] public float pitch;
    [Range(0f, 1f)] public float volume;
    public bool isRunning;
    public bool loop;
    public bool playOnStart;

}
public class AudioManager_Test : MonoBehaviour
{
    //
    public static AudioManager_Test Instance;
    [SerializeField] private AudioFile[] audioFiles;
    private Dictionary<string, int> audioFileIndex = new Dictionary<string, int>();
    AudioSource[] sources;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        sources = new AudioSource[audioFiles.Length];
        for (int i = 0; i < audioFiles.Length; i++)
        {
            audioFileIndex[audioFiles[i].NAME] = i;
            sources[i] = gameObject.AddComponent<AudioSource>();
            audioFiles[i].audioSource = sources[i];
            audioFiles[i].audioSource.clip = audioFiles[i].audioClip;
            audioFiles[i].audioSource.pitch = audioFiles[i].pitch;
            audioFiles[i].audioSource.volume = audioFiles[i].volume;
            audioFiles[i].audioSource.loop = audioFiles[i].loop;
        }
        foreach (var item in audioFiles)
        {
            if (item.playOnStart)
            {
                PlaySound(item.NAME);
            }
        }
    }
    public void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, audioFiles.Length);
        string audioName = audioFiles[randomIndex].NAME;
        PlaySound(audioName);
    }

    public void PlaySound(string name)
    {
        if (audioFileIndex.TryGetValue(name, out int index))
        {
            AudioFile audioFile = audioFiles[index];
            audioFile.isRunning = true;
            // if (audioFile.isRunning)
            // {
            //     // Handle if audio is already playing.
            // }
            // else
            // {


            if (audioFile.audioClip != null && audioFile.audioSource != null)
            {
                audioFile.audioSource.clip = audioFile.audioClip;
                audioFile.audioSource.Play();
            }
            else
            {
                // Handle if audio clip or source is null.
            }
            // }
        }
        else
        {
            // Handle if audio file with name doesn't exist.
        }
    }
    public void StopSound(string name)
    {
        if (audioFileIndex.TryGetValue(name, out int index))
        {
            AudioFile audioFile = audioFiles[index];


            audioFile.isRunning = false;

            if (audioFile.audioSource != null)
            {
                audioFile.audioSource.Stop();
            }
            else
            {
                // Handle if audio source is null.
                Debug.LogWarning("Audio source is null");
            }


        }
        else
        {
            // Handle if audio file with name doesn't exist.
            Debug.LogWarning($"File with name {name} doesnt exist");
        }
    }

}