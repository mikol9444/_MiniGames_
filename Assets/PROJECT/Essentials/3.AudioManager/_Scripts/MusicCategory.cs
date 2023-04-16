using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMusicCategory", menuName = "Music Category")]
public class MusicCategory : ScriptableObject
{
    [SerializeField] private sfxCategory categoryID = sfxCategory.Default;
    public sfxCategory Category { get => categoryID; }
    public AudioFile_Test[] audioFiles = new AudioFile_Test[1];

    private Dictionary<string, AudioFile_Test> audioClipDictionary;

    public void InitializeAudioClipDictionary()
    {
        audioClipDictionary = new Dictionary<string, AudioFile_Test>();

        foreach (AudioFile_Test audioFile in audioFiles)
        {
            audioClipDictionary.Add(audioFile.soundID, audioFile);
        }
    }
    public AudioFile_Test GetAudioFile(string soundID)
    {
        AudioFile_Test audioFile;
        if (audioClipDictionary.TryGetValue(soundID, out audioFile))
        {
            Debug.LogWarning("Able to find AudioClip with soundID " + soundID);
            return audioFile;
        }
        else
        {
            Debug.LogError("Unable to find AudioClip with soundID " + soundID);
            return null;
        }
    }
}



[System.Serializable]
public class AudioFile_Test
{
    public string soundID = "defaultSound";
    public AudioClip audioClip;
    [Range(0f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float volume = 1f;
    public float pausedAtTimer = 0f;
    public bool isRunning = false;
    public bool loop = false;
    public bool playOnStart = false;


}
