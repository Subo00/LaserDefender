using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage = 100;

    public int GetDamage(){ return _damage; }
    public void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine("HandleCollision");
    }

    IEnumerator HandleCollision()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

}
