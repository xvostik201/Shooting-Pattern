using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerActionMap _playerActionMap; 

    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action OnShootStart;
    public event Action OnShootEnd;

    private void OnEnable()
    {
        _playerActionMap = new PlayerActionMap();
        _playerActionMap.Enable();
        
        _playerActionMap.Player.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        _playerActionMap.Player.Move.canceled += _ => OnMove?.Invoke(Vector2.zero);
        
        _playerActionMap.Player.Look.performed += ctx => OnLook?.Invoke(ctx.ReadValue<Vector2>());
        
        _playerActionMap.Weapon.Shoot.performed += _ => OnShootStart?.Invoke();
        _playerActionMap.Weapon.Shoot.canceled += _ => OnShootEnd?.Invoke();
    }

}
