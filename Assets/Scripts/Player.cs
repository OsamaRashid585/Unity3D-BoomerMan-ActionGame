using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody _rb;
    [SerializeField] private GameObject _blackBoom;
    [SerializeField] private GameObject _stickyBoom;
    private Vector3 _boomSpawnPos;
    private bool _isMultipulBoom;
    private bool _isStickyBoomPickUp;
    private bool _isThrough;
    private float _seconds;
    private float _throughPower = 5;
    public float playerSpeed;

    void Awake()
    {
        _isThrough = true;
        _isMultipulBoom = false;
        _isStickyBoomPickUp = false;

    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _boomSpawnPos = transform.position;
        _boomSpawnPos += new Vector3(0, 3, 0);
        Invoke("SpawnStickyBoom", 1);
        Invoke("SpawnBlackBoom", 1);
        Movement();
        MultiBomChecker();
        SpawnStickyBoom();
        SpawnBlackBoom();
    }

    private void Movement()
    {
        var inpx = Input.GetAxis("Horizontal") * playerSpeed;
        var inpz = Input.GetAxis("Vertical") * playerSpeed;
        _rb.velocity = new Vector3(inpx, _rb.velocity.y, inpz);
    }
    private void MultiBomChecker()
    {
        if (_isMultipulBoom == true)
        {
            _seconds = 0.5f;
        }
        else
        {
            _seconds = 2f;
        }
    }
    void SpawnStickyBoom()
    {
        if (Input.GetKey(KeyCode.S) && _isStickyBoomPickUp == true && _isThrough == true)
        {
           var boom =  Instantiate(_stickyBoom, _boomSpawnPos, Quaternion.identity);
            var rb = boom.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * _throughPower, ForceMode.Impulse);
            _isThrough = false;
            StartCoroutine(WaitForNextBoom(0.5f));
            StartCoroutine(StickyBoomWait());
        }

    }
    void SpawnBlackBoom()
    {
        if (Input.GetKey(KeyCode.Space) && _isThrough == true)
        {
            var boom =  Instantiate(_blackBoom, _boomSpawnPos, Quaternion.identity);
            var rb = boom.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * _throughPower, ForceMode.Impulse);
            _isThrough = false;
            StartCoroutine(WaitForNextBoom(_seconds));

        }

    }
    IEnumerator WaitForNextBoom(float s)
    {
        yield return new WaitForSeconds(s);
        _isThrough = true;
    }

    IEnumerator StickyBoomWait()
    {
        yield return new WaitForSeconds(1.5f);
        _isStickyBoomPickUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MultiBoomPickUp"))
        {
            _isMultipulBoom = true;
            Destroy(other.gameObject);
            StartCoroutine(WaitForMultiBoom());
        }
        if (other.gameObject.CompareTag("StickyBoomPickUp"))
        {
            _isStickyBoomPickUp = true;
            Destroy(other.gameObject);
        }
    }
    IEnumerator WaitForMultiBoom()
    {
        yield return new WaitForSeconds(10);
        _isMultipulBoom = false;
    }
}
