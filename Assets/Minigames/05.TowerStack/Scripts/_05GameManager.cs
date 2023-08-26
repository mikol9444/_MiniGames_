using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials;
public class _05GameManager : MonoBehaviour
{
    public bool spawnOnZ = true;
    private void OnEnable()
    {
        // InputManager._SprintEvent += OnSprint;
        InputManager.Instance._Fire1Event += OnFire;
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        InputManager.Instance._Fire1Event -= OnFire;
    }


    private void OnFire()
    {
        Debug.Log("Fire 1 Pressed");
        if (_05MovingPlatform.CurrentPlatform)
        {
            _05MovingPlatform.CurrentPlatform.Stop();

            Camera.main.transform.position += new Vector3(0, 0.1f, 0);
            spawnOnZ = !spawnOnZ;
            // _05MovingPlatform.LastPlatform = _05MovingPlatform.CurrentPlatform;
            // _05MovingPlatform.CurrentPlatform = platform.GetComponent<_05MovingPlatform>();
        }

    }

}
