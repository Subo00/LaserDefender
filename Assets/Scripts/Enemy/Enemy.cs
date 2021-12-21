using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] private ObjectPool _objectPool;
     
    [SerializeField] private float _laserSpeed = 1f;
    
    [SerializeField] private float _minShootRate, _maxShootRate;
    private float _shootRate;
    void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        _objectPool = gameController.GetComponent<ObjectPool>(); 

        _shootRate = Random.Range(_minShootRate, _maxShootRate);
        StartCoroutine(ShootContinuously());       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.GetDamage();
        if(_health <= 0) Destroy(gameObject);
    }

    IEnumerator ShootContinuously()
    {
        while(true)
        {     
            yield return new WaitForSeconds(_shootRate);

            GameObject laser = _objectPool.GetPooledObject();
            if(laser != null)
            {
                laser.transform.position = transform.position;
                laser.transform.rotation = transform.rotation;
                laser.SetActive(true);
                                                                        //negative because it's going down the Y axis
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-_laserSpeed);
            }

            
        }
    }
}
