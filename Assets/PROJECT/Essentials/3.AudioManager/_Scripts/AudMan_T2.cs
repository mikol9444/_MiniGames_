using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum sfxCategory { Default, Music, sfx, Weapon, Character, Ambient }
public class AudMan_T2 : MonoBehaviour
{
    public static AudMan_T2 instance;
    public MusicCategory[] musicCategories;

    private Dictionary<sfxCategory, MusicCategory> categoryDictionary;
    [SerializeField] private int numAudioSources = 10;
    private List<AudioSource> audioSources = new List<AudioSource>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InitializeCategoriesDict();
        foreach (var item in musicCategories)
        {
            item.InitializeAudioClipDictionary();
        }
        // Create AudioSources and add as children of AudioManager object
        GameObject obj = new GameObject("child");
        obj.transform.parent = gameObject.transform;
        for (int i = 0; i < numAudioSources; i++)
        {
            AudioSource newSource = obj.AddComponent<AudioSource>();
            newSource.playOnAwake = false;
            newSource.loop = false;
            newSource.enabled = true;
            audioSources.Add(newSource);
        }
        PlaySound("defaultSound");
    }
    public void PlaySound(string soundID, sfxCategory cat = default)
    {
        MusicCategory _cat = GetCategory(cat);
        AudioClip clip = _cat.GetAudioFile(soundID).audioClip;
        AudioSource source = GetAvailableAudioSource();
        source.clip = clip;
        source.PlayOneShot(clip);
    }
    public void InitializeCategoriesDict()
    {
        categoryDictionary = new Dictionary<sfxCategory, MusicCategory>();

        foreach (var item in musicCategories)
        {
            categoryDictionary.Add(item.Category, item);
        }
    }
    public MusicCategory GetCategory(sfxCategory cat)
    {
        if (categoryDictionary.TryGetValue(cat, out MusicCategory category))
        {
            Debug.LogWarning("Able to find AudioClip with soundID " + category.name);
            return category;
        }
        else
        {
            Debug.LogError("Unable to find AudioClip with soundID " + category);
            return null;
        }
    }
    public AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        // If no available AudioSource is found, create a new one
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.playOnAwake = false;
        newSource.loop = false;
        newSource.enabled = false;
        audioSources.Add(newSource);

        return newSource;
    }
}