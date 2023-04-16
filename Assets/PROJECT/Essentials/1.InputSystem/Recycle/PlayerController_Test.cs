using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Test : MonoBehaviour
{
    InputManager inputManager;
    public float speed = 5f;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }
    private void Update()
    {
        Vector3 movementVector = new Vector3(inputManager.move.x, 0f, inputManager.move.y);
        transform.position += movementVector * speed * Time.deltaTime;
    }
}
