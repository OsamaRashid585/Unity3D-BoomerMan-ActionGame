using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float _count;
    private int _time = 3;
    private int _radius = 5;
    private bool _isExploded = true;
    private int _explosionPower = 2000;
    [SerializeField] private ParticleSystem _ExplosionEffect;

    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        _count = _time;
    }

    // Update is called once per frame
    void Update()
    {

        _count -= Time.deltaTime;
        if(_count <= 0 && _isExploded == true)
        {
            _isExploded = false;
            Explosion();
        }
    }

    private void Explosion()
    {
        Instantiate(_ExplosionEffect, transform.position, Quaternion.identity);
        var colliders =  Physics.OverlapSphere(transform.position, _radius);
        foreach(var collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(_explosionPower, transform.position, _radius);
            }
        }
        Destroy(gameObject);
    }
}
