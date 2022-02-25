using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 10f;

    [SerializeField] private float _padding = 0.5f;
    
    /////////////Private variables////////////////
    private Vector3 _mousePosition;
    private Vector2 _position = new Vector2(0f, 0f);
    private float _xMin, _xMax;
    private float _yMin, _yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveLimits();
    }

     private void SetUpMoveLimits()
    {
        Camera gameCamera = Camera.main;
        _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x + _padding;
        _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - _padding;

        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y + _padding;
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y - _padding;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

        float newXPos = Mathf.Clamp(_mousePosition.x, _xMin, _xMax);
        float newYPos = Mathf.Clamp(_mousePosition.y, _yMin, _yMax);

        Vector2 newPos = new Vector2(newXPos, newYPos);
        _position = Vector2.Lerp(transform.position, newPos, _moveSpeed);
    }

    private void FixedUpdate()
    {
        transform.position = _position;
    }
}
