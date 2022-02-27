using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    #region "Variables"
    //////////SerializeField////////////
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _laserSpeed = 1f;
    [SerializeField] private float _minShootRate, _maxShootRate;
    [SerializeField] private Material _matWhite;
    [Range(10,100)]
    [SerializeField] private int _dropChance = 25;
    

    ///////Private /////////////////
    private int _health = 0;
    private float _shootRate;
    private ObjectPool _objectPoolLaser;
    private ObjectPool _objectPoolExplosion;
    private SpriteRenderer _spriteRenderer;
    private Material _matDefault;
    private AudioSource _audioSource; //palys laser sound
    private Score _score;
    private GameObject PickUpsPool;
    private int _numOfPickUps;
    #endregion

    void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController"); // get the Pools in GameController
        ObjectPool[] objectPools = gameController.GetComponentsInChildren<ObjectPool>(); 
        _objectPoolLaser = objectPools[0];
        _objectPoolExplosion = objectPools[1]; //this is hard coded I admit it 

        _score = GameObject.FindObjectOfType<Score>();

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _matDefault = _spriteRenderer.material;                                //sets the default material

        _audioSource = GetComponent<AudioSource>();   

        PickUpsPool = GameObject.FindGameObjectWithTag("PickUpsPool");
        if(PickUpsPool == null) { Debug.LogError("You need to add the PickUpsPool to the scene and tag it");}
        _numOfPickUps = PickUpsPool.transform.childCount; //gets all pick ups  
    }

    void OnEnable()
    {
        _health = _maxHealth;
        _shootRate = Random.Range(_minShootRate, _maxShootRate);
        StartCoroutine(ShootContinuously());  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _spriteRenderer.material = _matWhite; //makes a flashing effect
        Invoke("ResetMaterial", 0.2f);

        
        if(other.tag != "Player")
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if(damageDealer != null)
            ProcessHit(damageDealer);
        }
        else
        {
            other.gameObject.GetComponent<Player>().Death();
            Death();
        }  
    }

    void ResetMaterial()
    {
        _spriteRenderer.material = _matDefault;
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.GetDamage();

        if(_health <= 0) Death();
        
    }

    private void Death()
    {
        
        GameObject explosion = _objectPoolExplosion.GetPooledObject();
        if(explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        }

        _score.AddScore(_maxHealth);

        if(Random.Range(0,100) < _dropChance)
        DropPickUp();

        gameObject.SetActive(false);
         
    }
   
    private void DropPickUp()
    {
        int indexOfPickUp = Random.Range(0,_numOfPickUps);
        ObjectPool[] objectPools = PickUpsPool.GetComponentsInChildren<ObjectPool>();
        GameObject pickUp = objectPools[indexOfPickUp].GetPooledObject();
        if(pickUp != null)
        {
            pickUp.transform.position = transform.position;
            pickUp.transform.rotation = transform.rotation;
            pickUp.SetActive(true); 
        }
        else
        {
            DropPickUp();
        }
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
