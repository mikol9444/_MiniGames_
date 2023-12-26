using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _11_CarMover : TweenAnimator
{
    // Start is called before the first frame update
    public bool getPooled = true;
    void Start()
    {
        Invoke("ReturnAfterTime", timeTillDestination);
    }
    protected override void Awake()
    {
        // base.Awake();
            // startPosition = transform.position;
        // StartMovement();
    }
    private void OnEnable() {
        startPosition = transform.position;
        StartMovement();
    }
    private void OnDisable() {
        StopAllCoroutines();
    }
    private void ReturnAfterTime()
    {if(getPooled)
        ObjectPoolManager.ReturnObjectToPool(gameObject);
        else Destroy(gameObject);
    }
}
