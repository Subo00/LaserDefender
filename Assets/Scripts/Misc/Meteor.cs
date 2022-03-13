using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    [SerializeField] private int _maxHealth = 50;
    [SerializeField] private Material _matWhite;
    [SerializeField] private float _rotSpeed = 50.0f;

    private ObjectPool _objectPoolExplosion;

    private Rigidbody2D _rb;
    private Material _matDefault;
    private SpriteRenderer _spriteRenderer;
    private int _health;

    public Rigidbody2D GetRigidbody()
    {
        return _rb;
    }
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _matDefault = _spriteRenderer.material;

        var gameController = GameObject.FindGameObjectWithTag("GameController"); // get the Pools in GameController
        ObjectPool[] objectPools = gameController.GetComponentsInChildren<ObjectPool>();
        _objectPoolExplosion = objectPools[2];
    }

    void OnEnable()
    {
        _health = _maxHealth;
    }

    private void FixedUpdate() 
    {
        _rb.MoveRotation(_rb.rotation + _rotSpeed * Time.fixedDeltaTime);
    }

    #region "Interaction"
    private void OnTriggerEnter2D(Collider2D other) 
    {
        _spriteRenderer.material = _matWhite; //makes a flashing effect
        Invoke("ResetMaterial", 0.2f);

        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().ProcessHit(_health);
            Death();
        }
        else if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().ProcessHit(_health);
            Death();
        }
        else
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if(damageDealer != null)
            ProcessHit(damageDealer.GetDamage());
        }    
    }
    void ResetMaterial()
    {
        _spriteRenderer.material = _matDefault;
    }

    public void ProcessHit(int damage)
    {
        _health -= damage;

        if(_health <= 0) Death();
        
    }

    private void Death()
    {
        //add explosion
        GameObject explosion = _objectPoolExplosion.GetPooledObject();
        if(explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        }
        //add sfx

        gameObject.SetActive(false);
    }
    #endregion
}
