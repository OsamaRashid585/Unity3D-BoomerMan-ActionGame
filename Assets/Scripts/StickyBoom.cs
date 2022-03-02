using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBoom : MonoBehaviour
{
    public Transform pos;
    public int _time = 1;
    public float _count;

    private void Start()
    {
        _count = _time;
    }
    private void Update()
    {
        _count -= Time.deltaTime;
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            if(_count <= 0)
            {
                var offset = new Vector3(1, 0, 0);
                transform.position = collision.gameObject.transform.position + offset;
            }
        }
    }
}
