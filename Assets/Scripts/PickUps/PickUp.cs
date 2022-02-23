using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    ////////////////Serialize field//////////////
    [SerializeField]private AudioClip _audioClip; //plays a sound on pick up

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            var player = other.GetComponent<Player>();
            GetPower(player);
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            gameObject.SetActive(false);
        }   
    }

    protected virtual void GetPower(Player player)
    {
        return;
    }
}
