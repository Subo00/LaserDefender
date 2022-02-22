using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ////////////SerializeField paramaters/////////
    [Header("Player")]
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _padding = 0.5f;
    [SerializeField] private int _health = 100;
    [Header("Projectile")]
    [SerializeField] private float _laserSpeed = 2f;
    [SerializeField] private float _shootRate = 0.5f;
    [SerializeField] private ObjectPool _laserPool;

    ////////////Private variables//////////////
    private float _xMin, _xMax;
    private float _yMin, _yMax;
    private Vector3 _mousePosition;
    private Rigidbody2D _rigidbody;
    private Vector2 _position = new Vector2(0f, 0f);
    private Coroutine _firingCorutine;
    private AudioSource _audioSource; //plays laser sound
    void Start()
    {
        SetUpMoveLimits();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void SetUpMoveLimits()
    {
        Camera gameCamera = Camera.main;
        _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + _padding;
        _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - _padding;

        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + _padding;
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - _padding;
    }

 
    void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

        float newXPos = Mathf.Clamp(_mousePosition.x, _xMin, _xMax);
        float newYPos = Mathf.Clamp(_mousePosition.y, _yMin, _yMax);

        Vector2 newPos = new Vector2(newXPos, newYPos);
        _position = Vector2.Lerp(transform.position, newPos, _moveSpeed);
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_position);
    }
    private void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
           _firingCorutine = StartCoroutine(ShootContinuously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firingCorutine);
        }
    }
    IEnumerator ShootContinuously()
    {
        while(true)
        {           
            GameObject laser = _laserPool.GetPooledObject();
            if(laser != null)
            {
                laser.transform.position = transform.position;
                laser.transform.rotation = transform.rotation;
                laser.SetActive(true);

                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,_laserSpeed);
            }
            _audioSource.Play();
            yield return new WaitForSeconds(_shootRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) {return;}
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.GetDamage();
        if(_health <= 0) Death();
    }

    public void Death()
    {
        DeathAnimation();
        Destroy(gameObject);
        FindObjectOfType<SceneController>().LoadGameOver();
    }

    private void DeathAnimation()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        ObjectPool objectPoolExplosion = gameController.GetComponentsInChildren<ObjectPool>()[1]; //pool 0 is for enemy bullets
        
        GameObject explosion = objectPoolExplosion.GetPooledObject();
        if(explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        }

    }

    public int GetHealth() { return _health; }
}
