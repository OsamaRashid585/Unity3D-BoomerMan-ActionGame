using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spwnmanager : MonoBehaviour
{
    [SerializeField] private GameObject _stickyBoomPickUpPrefab;
    [SerializeField] private GameObject _multiBoomPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    private int _waveCoutn = 6;

    private void Update()
    {
        Invoke("SpawnStickyBoomPickUp", 0);
        Invoke("SpawnMultiBoomPickUp", 0);
        Invoke("SpwanEnemy", 0);
    }
    private void SpawnStickyBoomPickUp()
    {
        var StickyBoomCount = GameObject.FindGameObjectsWithTag("StickyBoomPickUp").Length;
        if (StickyBoomCount <= 0)
        {
            var pos = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));
            Instantiate(_stickyBoomPickUpPrefab, pos, Quaternion.identity);
        }
    }
    private void SpawnMultiBoomPickUp()
    {
        var multiBoomPickUpCount = GameObject.FindGameObjectsWithTag("MultiBoomPickUp").Length;
        if (multiBoomPickUpCount <= 0)
        {
            var pos = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));
            Instantiate(_multiBoomPrefab, pos, Quaternion.identity);
        }
    }

    private void SpwanEnemy()
    {
        var enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount <= 0 && _waveCoutn != 0)
        {
            var pos = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));
            Instantiate(_enemyPrefab, pos, Quaternion.identity);
            _waveCoutn--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
        else
        {
            SceneManager.LoadScene(0);

        }
    }
}
