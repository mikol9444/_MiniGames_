//answered Dec 28, 2020 at 2:14
//Dejan Ignjatovic's user avatar
//Dejan Ignjatovic under following link
//https://gamedev.stackexchange.com/questions/117423/unity-detect-animations-end
namespace Essentials
{
    using UnityEngine;


    [System.Serializable]

    [RequireComponent(typeof(Animator))]
    public class AnimationEventDispatcher : MonoBehaviour
    {


       protected Animator animator;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }


    }
}