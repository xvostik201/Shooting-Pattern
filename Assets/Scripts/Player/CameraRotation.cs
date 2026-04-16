using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraRotation : MonoBehaviour
{
    [SerializeField, Range(0.05f, 0.5f)] private float _mouseSensitivity;
    [SerializeField] private float _xRotationClamp = 90f;
    [SerializeField] private Transform _body;

    private float _xRotation = 0f;
    private float _yRotation = 0f;
    
    [Inject] private InputManager _inputManager;

    private void OnDisable()
    {
        _inputManager.OnLook -= Rotate;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _inputManager.OnLook += Rotate;
    }
    void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(-_xRotation, 0f, 0f);
        _body.localRotation = Quaternion.Euler(0f, -_yRotation, 0f);
    }

    private void Rotate(Vector2 vector2)
    {
        float mouseX = vector2.x * _mouseSensitivity;
        float mouseY =  vector2.y * _mouseSensitivity;
        
        _xRotation  += mouseY;
        _yRotation  -= mouseX;
        
        _xRotation = Mathf.Clamp(_xRotation, -_xRotationClamp, _xRotationClamp);
    }

    public void ApplyRecoil(Vector2 recoil)
    {
        _xRotation += recoil.y;
        _yRotation -= recoil.x;
    }
}
