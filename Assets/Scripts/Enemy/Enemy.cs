using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //////////SerializeField////////////
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _laserSpeed = 1f;
    [SerializeField] private float _minShootRate, _maxShootRate;
    [SerializeField] private Material _matWhite;

    ///////Private /////////////////
    private int _health = 0;
    private float _shootRate;
    private ObjectPool _objectPoolLaser;
    private ObjectPool _objectPoolExplosion;
    private SpriteRenderer _spriteRenderer;
    private Material _matDefault;
    private AudioSource _audioSource; //palys laser sound
    private Score _score;
    void Awake()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController"); // get the Pools in GameController
        ObjectPool[] objectPools = gameController.GetComponentsInChildren<ObjectPool>(); 
        _objectPoolLaser = objectPools[0];
        _objectPoolExplosion = objectPools[1];

        _health = _maxHealth;

        _score = GameObject.FindObjectOfType<Score>();

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _matDefault = _spriteRenderer.material;                                //sets the default material

        _audioSource = GetComponent<AudioSource>();

        _shootRate = Random.Range(_minShootRate, _maxShootRate);
        StartCoroutine(ShootContinuously());       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _spriteRenderer.material = _matWhite; //makes a flashing effect
        Invoke("ResetMaterial", 0.2f);

        //add a state when enemy hits player 

        //other.gameObject.SetActive(false);

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.GetDamage();

        if(_health <= 0) Death();
        
    }

    void Death()
    {
        
        GameObject explosion = _objectPoolExplosion.GetPooledObject();
        if(explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        }

        _score.AddScore(_maxHealth);

        Destroy(gameObject);
         
    }
   
    void ResetMaterial()
    {
        _spriteRenderer.material = _matDefault;
    }
    IEnumerator ShootContinuously()
    {
        while(true)
        {     
            yield return new WaitForSeconds(_shootRate);

            GameObject laser = _objectPoolLaser.GetPooledObject();
            if(laser != null)
            {
                laser.transform.position = transform.position;
                laser.transform.rotation = transform.rotation;
                laser.SetActive(true);
                                                                        //negative because it's going down the Y axis
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-_laserSpeed);
            }
            _audioSource.Play();
            
        }
    }
}
