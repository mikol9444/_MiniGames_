using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Minigames._01JumpingJack
{

    public class FinishFlag01 : Interactable01, ICollider
    {
        public static Action OnWinEvent;
        private SpriteRenderer rend;

        public override void OnCollide()
        {
            rend.color = Color.green;
            OnWinEvent?.Invoke();
        }

        public override void Awake()
        {
            rend = GetComponentInChildren<SpriteRenderer>();
        }
    }
}