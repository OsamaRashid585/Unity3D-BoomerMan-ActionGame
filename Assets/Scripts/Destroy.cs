using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitToDestory());
    }
    IEnumerator WaitToDestory()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
