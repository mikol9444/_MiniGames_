using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _02StartScene : MonoBehaviour
{
    [SerializeField] private float playerSpeed=15f;
    Rigidbody rb;
    

    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        rb.velocity = Vector3.right * playerSpeed;
    }
}
