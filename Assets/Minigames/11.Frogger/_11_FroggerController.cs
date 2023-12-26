using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _11_FroggerController : MonoBehaviour
{
public float jumpHeight = 2f; // Adjust the jump height as needed
    public float jumpDuration = 1f; // Adjust the jump duration as needed

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTime;

    private bool isJumping = false;

    private void Start()
    {
        // Set initial position
        startPosition = transform.position;
        endPosition = startPosition;
    }

    private void Update()
    {
        // Update jump position if jumping
        if (isJumping)
        {
            float elapsed = Time.time - startTime;
            float t = Mathf.Clamp01(elapsed / jumpDuration);

            // Apply quadratic easing function
            t = Mathf.Pow(t, 2);

            // Update position using Lerp
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // Check if the jump is complete
            if (t >= 1.0f)
            {
                isJumping = false;
            }
        }
    }

    public void Jump(Vector2 dir)
    {
        Vector3 pos = transform.position;
        if(isJumping)return;
        if(dir.x<0 && pos.x<0.1f)return;
        if(dir.x>0 && pos.x>=11.9f)return;
        if(dir.y<0 && pos.z<0.1f)return;
        if(dir.y>0 && pos.z>12.9f)return; 
        // Start the jump
        isJumping = true;
        startTime = Time.time;
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x+dir.x,0.553f,startPosition.z+dir.y);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Enemy")){
            Debug.LogWarning("YOU ARE DEAD");
        }
    }
}
