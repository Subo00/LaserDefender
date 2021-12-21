using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.SetActive(false);
    }
}
