//answered Dec 28, 2020 at 2:14
//Dejan Ignjatovic's user avatar
//Dejan Ignjatovic under following link
//https://gamedev.stackexchange.com/questions/117423/unity-detect-animations-end
namespace Essentials
{
    using UnityEngine;
    using UnityEngine.Events;

    [System.Serializable]
    public class UnityAnimationEvent : UnityEvent<string> { };
    [RequireComponent(typeof(Animator))]
    public class AnimationEventDispatcher : MonoBehaviour
    {
        public UnityAnimationEvent OnAnimationStart;
        public UnityAnimationEvent OnAnimationComplete;

       protected Animator animator;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            for (int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];

                AnimationEvent animationStartEvent = new AnimationEvent();
                animationStartEvent.time = 0;
                animationStartEvent.functionName = "AnimationStartHandler";
                animationStartEvent.stringParameter = clip.name;

                AnimationEvent animationEndEvent = new AnimationEvent();
                animationEndEvent.time = clip.length;
                animationEndEvent.functionName = "AnimationCompleteHandler";
                animationEndEvent.stringParameter = clip.name;

                clip.AddEvent(animationStartEvent);
                clip.AddEvent(animationEndEvent);
            }
        }

        protected virtual void AnimationStartHandler(string name)
        {
            Debug.Log($"{name} animation start.");
            OnAnimationStart?.Invoke(name);
        }
        protected virtual void AnimationCompleteHandler(string name)
        {
            if (!animator.GetBool("isAlive"))
            {
                gameObject.SetActive(false);
            }
            Debug.Log($"{name} animation complete.");
            OnAnimationComplete?.Invoke(name);
        }
    }
}