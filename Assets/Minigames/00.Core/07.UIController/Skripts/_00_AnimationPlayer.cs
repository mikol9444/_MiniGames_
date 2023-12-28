using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
[System.Serializable]
public struct AnimationEventData
{
    public string id;
    public string clipName;
    public Animation animationComponent;
    public UnityEvent<string> onAnimationBegin;
    public UnityEvent onAnimationEnd;
}

public class _00_AnimationPlayer : MonoBehaviour
{

    // Public list of AnimationEventData structs
    public List<AnimationEventData> animationEventsList = new List<AnimationEventData>();
    private void Start()
    {
        // Subscribe to the sceneUnloaded event
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneUnloaded event
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        // Unsubscribe from events when the scene is unloaded
        foreach (var eventData in animationEventsList)
        {
            eventData.onAnimationBegin.RemoveAllListeners();
            eventData.onAnimationEnd.RemoveAllListeners();
        }
    }
    
    // Method to be called when the animation begins
    private void OnAnimationBegin(AnimationEventData eventData)
    {
        Debug.Log($"Animation {eventData.clipName} has started.");
        // eventData.animationComponent.Play(eventData.clipName);
    }

    // Method to be called when the animation ends
    private void OnAnimationEnd()
    {
        // Debug.Log($"Animation {animationName} has ended.");
    }

    // Function to play the specified animation by name
    public void PlayAnimation(string id)
    {
        // Find the AnimationEventData for the specified animationName
        AnimationEventData eventData = GetEventData(id);

        // Check if the AnimationEventData is found
        if (eventData.animationComponent != null)
        {
            // Invoke the onAnimationBegin event
            eventData.onAnimationBegin.Invoke(eventData.clipName);

            // Play the specified animation
            eventData.animationComponent.Play(eventData.clipName);

            // Start a coroutine to detect when the animation ends
            StartCoroutine(WaitForAnimationEnd(id,eventData));
        }
        else
        {
            Debug.LogError($"AnimationEventData for animation '{id}' not found in the list.");
        }
    }
    // Function to find the AnimationEventData for a given animation name
    public AnimationEventData GetEventData(string animID)
    {
        return animationEventsList.Find(x => x.id == animID);
    }

    // Coroutine to detect when the animation ends
    private System.Collections.IEnumerator WaitForAnimationEnd(string animationName,AnimationEventData eventData)
    {
        do
        {
            yield return null;
        } while (eventData.animationComponent.IsPlaying(eventData.clipName));

        // Animation has ended, invoke the onAnimationEnd event
        eventData.onAnimationEnd.Invoke();
    }
}
