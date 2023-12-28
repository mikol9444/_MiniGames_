using UnityEngine;

public class AnimationEventSubscriber : MonoBehaviour
{
    // private _00_AnimationPlayer animationPlayer;

    public Animation anim;
    // public string animID = "RR00";

    private void Start()
    {
        // // Find the _00_AnimationPlayer script in the scene
        // animationPlayer = FindObjectOfType<_00_AnimationPlayer>();

        // // Check if the _00_AnimationPlayer script is found
        // if (animationPlayer == null)
        // {
        //     Debug.LogError("The _00_AnimationPlayer script is not found in the scene.");
        // }
        // AnimationEventData eventData =  animationPlayer.GetEventData(animID);
        // eventData.onAnimationBegin.AddListener(OnAnimationBegin);
        // eventData.onAnimationEnd.AddListener(OnAnimationEnd);
    }

        public void Play(string animName)
    {
        anim.Play(animName);
    }
    

}
