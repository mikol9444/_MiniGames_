using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _03Portal : MonoBehaviour
{
    [SerializeField] private float spawnTime = .5f;
    [SerializeField] private float repeatRate = 1f;
    private void Start()
    {
        InvokeRepeating(nameof(SpawnFollower), spawnTime, repeatRate);
    }
    private void SpawnFollower()
    {

        GameObject obj =  Essentials.ObjectPooler.Instance.GetObjectFromPool("WayPointsFollower");
    }
}
