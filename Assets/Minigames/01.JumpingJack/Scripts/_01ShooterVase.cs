using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public class _01ShooterVase : TweenAnimator
{
    public GameObject projectile;
    public Transform shootingPoint;
    public Vector3 lookingRotation; 
    protected override IEnumerator MoveByRoutine()
    {
        if (pingAndPong)
        {
           GameObject obj= Instantiate(projectile, shootingPoint.position, Quaternion.Euler(lookingRotation));
            AudioManager_Test.Instance.PlaySound("shoot");
            Destroy(obj, 2f);
        }
        return base.MoveByRoutine();

    }
}
