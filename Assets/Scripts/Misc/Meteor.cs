using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector2 newForce; ///used to be changed later via script 



    [SerializeField] private int _maxHealth = 50;
    [SerializeField] private Material _matWhite;
    [SerializeField] private float _rotSpeed = 50.0f;

    private Rigidbody2D _rb;
    private Material _matDefault;
    private SpriteRenderer _spriteRenderer;
    private int _health;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _matDefault = _spriteRenderer.material;   
    }

    void OnEnable()
    {
        _health = _maxHealth;
        newForce = new Vector2(0f,Random.Range(1f,10f)); 
        _rb.AddForce(newForce, ForceMode2D.Impulse);
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
        //add sfx

        gameObject.SetActive(false);
    }
    #endregion
}
