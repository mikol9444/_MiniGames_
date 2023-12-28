using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _12_TweenChild : TweenAnimator
{
    public bool rightDir=true;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override IEnumerator MoveToLocal()
    {
        Vector3 a = transform.position;
        Vector3 b = pingAndPong ? startPosition : destination;
        float timeElapsed = 0f;
        while (timeElapsed < timeTillDestination)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / timeTillDestination);
            transform.position = Vector3.Lerp(a, b, t);
            yield return null;
        }
        onComplete();
        rightDir=!rightDir;
        transform.rotation= rightDir?Quaternion.Euler(0,77,0):Quaternion.Euler(0,-120,0);
    }
}
