using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 0.5f;
    private Material _material;

    private Vector2 _offSet;

    void Start() 
    {
        _material = GetComponent<Renderer>().material;
        _offSet = new Vector2(0f, _scrollSpeed);
    }

    void Update()
    {
        _material.mainTextureOffset += _offSet * Time.deltaTime;
    }

}
