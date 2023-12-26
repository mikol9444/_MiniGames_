using System;
using System.Collections;
using System.Collections.Generic;
using Essentials;
using UnityEngine;

public class _11_FroggerController : MonoBehaviour
{
    public float jumpHeight = 2f; // Adjust the jump height as needed
    public float jumpDuration = 1f; // Adjust the jump duration as needed

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTime;
    [SerializeField] private ParticleSystem explosionParticles;
    ExampleInputListener listener;

    private bool isJumping = false;

    private void Start()
    {
        listener = FindObjectOfType<ExampleInputListener>();
        // Set initial position
        startPosition = transform.position;
        endPosition = startPosition;

    }
    private void OnEnable()
    {
        _11_Swiper swiper = FindObjectOfType<_11_Swiper>();
        swiper.controller = this;
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
        if (isJumping) return;
        if (dir.x < 0 && pos.x < 0.1f) return;
        if (dir.x > 0 && pos.x >= 11.9f) return;
        if (dir.y < 0 && pos.z < 0.1f) return;
        if (pos.z > 11.9f) return;
        int posX = Mathf.RoundToInt(pos.x);
        if (Mathf.RoundToInt(pos.z) == 11)
        {
            bool b = (pos.x == 0 || pos.x == 2 || pos.x == 3 || pos.x == 5 || pos.x == 6 || pos.x == 8 || pos.x == 9 || pos.x == 11 || pos.x == 12);
            if (b && dir.y > 0)
            {
                return;
            }
            else if(!b && dir.y>0)
            {
                FindObjectOfType<_11_Spawner>().SpawnPlayer(new Vector3(6, 0, 0), Quaternion.identity);
                Invoke(nameof(DisableThis),1.0f);
            }
        }

        // Start the jump
        isJumping = true;
        startTime = Time.time;
        startPosition = transform.position;
        endPosition = new Vector3(startPosition.x + dir.x, 0.553f, startPosition.z + dir.y);
    }
    public void DisableThis(){
        enabled=false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            explosionParticles.gameObject.SetActive(true);
            FindObjectOfType<Popup>().OnActivate("YOU ARE DEAD\nRESTART?");
            Debug.LogWarning("YOU ARE DEAD");
            this.enabled = false;
            Destroy(gameObject, 1f);
            AudioManager_Test.Instance.PlaySound("DEATH");
            AudioManager_Test.Instance.PlaySound("BEEP");
        }
    }
}
