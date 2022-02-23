using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Score _score;
    private Transform _playerTransform;
    void OnEnable()
    {
    }
    void Start()
    {
        _score = GameObject.FindObjectOfType<Score>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        transform.position = _playerTransform.position;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) {return;}
        int scoreToAdd = damageDealer.GetDamage();
        //damageDealer.gameObject.SetActive(false);
        _score.AddScore(scoreToAdd);
    }
}
