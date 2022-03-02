using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 0.5f;
    [SerializeField] private Texture2D[] textures;
    private Material _material;

    private Vector2 _offSet;

    void Start() 
    {
        _material = GetComponent<Renderer>().material;
        _offSet = new Vector2(0f, _scrollSpeed);

        if(textures.Length > 0)
        {
            int randIndex = Random.Range(0, textures.Length);
            _material.SetTexture("_MainTex", textures[randIndex]);
        }
        else 
        {
            Debug.LogError("You need to add textures to the script!");
        }
    }

    void Update()
    {
        _material.mainTextureOffset += _offSet * Time.deltaTime;
    }

}
