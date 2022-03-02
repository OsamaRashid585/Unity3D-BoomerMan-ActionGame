using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float _movSpeed = 0.05f;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {

    }
    void FixedUpdate()
    {
        var player = FindObjectOfType<Player>().GetComponent<Transform>();
        var direction = (player.position - transform.position).normalized;
        _rb.AddForce(direction * _movSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

}
