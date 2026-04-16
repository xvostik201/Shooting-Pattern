using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    private Vector2 _currentMoveInput;
    
    [Inject] private InputManager _inputManager;

    private void Start()
    {
        _inputManager.OnMove += OnMoveInput;
    }

    private void OnDisable()
    {
        _inputManager.OnMove -= OnMoveInput;
    }

    private void Update()
    {
        if (_currentMoveInput.sqrMagnitude > 0.01f)
        {
            Vector3 direction = transform.right * _currentMoveInput.x + transform.forward * _currentMoveInput.y;
            transform.position += direction.normalized * _speed * Time.deltaTime;
        }
    }

    private void OnMoveInput(Vector2 input)
    {
        _currentMoveInput = input;
    }
}
