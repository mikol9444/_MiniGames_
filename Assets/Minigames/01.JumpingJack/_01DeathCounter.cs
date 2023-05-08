using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class _01DeathCounter : MonoBehaviour
{
    TextMeshProUGUI deathCount;
    private void Awake()
    {
        deathCount = GetComponentInChildren<TextMeshProUGUI>();
        
    }
    private void Start()
    {
        deathCount.text = _01EvaGameMaster.Instance?.deathCounter.ToString();
        _01EvaCollisionHandler.deadAction += SetDeathCounterText;
    }

    private void OnApplicationQuit()
    {
        _01EvaCollisionHandler.deadAction -= SetDeathCounterText;
    }

    private void SetDeathCounterText()
    {
        deathCount.text = _01EvaGameMaster.Instance?.deathCounter.ToString();
    }
}
