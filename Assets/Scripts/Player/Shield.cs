using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Score _score;
    void Awake() //awake because the shield will be disabled on player in Start()
    {
        _score = GameObject.FindObjectOfType<Score>();
    }

    void OnTriggerEnter2D(Collider2D other) //collects lasers and adds them as points
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) {return;}
        int scoreToAdd = damageDealer.GetDamage();
        _score.AddScore(scoreToAdd);
    }
}
