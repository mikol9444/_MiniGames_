using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public float updateInterval = 0.5f;

    private float fpsAccumulator = 0f;
    private int fpsFrames = 0;
    private float fpsCurrent = 0f;
    private void Start()
    {
        InvokeRepeating("UpdateFPS", 0f, updateInterval);
    }

    private void UpdateFPS()
    {
        fpsCurrent = fpsAccumulator / fpsFrames;
        fpsAccumulator = 0f;
        fpsFrames = 0;
        fpsText.text = Mathf.RoundToInt(fpsCurrent).ToString();
    }

    private void Update()
    {
        fpsAccumulator += (1 / Time.unscaledDeltaTime);
        fpsFrames++;
    }
}
