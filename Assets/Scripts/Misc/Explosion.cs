using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float timeDuration = 0.7f;
    private AudioSource _audioSource;

    void Start()
    {
        
    }
    void OnEnable()
    {
        StartCoroutine(Deactivate());
    }
    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(timeDuration);
        gameObject.SetActive(false);
    }
}
