using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public abstract class BaseWeapon : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] protected string _weaponId = "M4";
    
    [Header("Transform settings")]
    [SerializeField] protected Transform _shootPoint;
    [SerializeField] protected Transform _gunParent;

    protected float _shootTimer;
    protected int _currentBullet;
    protected bool _isShooting;
    
    protected Dictionary<Transform, IDamagable> _damagableCache;
    
    protected WeaponData _weaponData;
    
    [Inject] protected InputManager _inputManager;
    [Inject] protected WeaponRegistry _registry;
    [Inject] protected CameraRotation _camera;
    
    
    protected virtual void Start()
    {
        _weaponData = _registry.GetWeapon(_weaponId);
        if (_weaponData == null) return;
        
        _shootTimer = _weaponData.ShootRate;
        
        _inputManager.OnShootStart += StartShooting;
        _inputManager.OnShootEnd += StopShooting;
    }

    protected virtual void OnDisable()
    {
        _inputManager.OnShootStart -= StartShooting;
        _inputManager.OnShootEnd -= StopShooting;
    }

    protected virtual void StartShooting()
    {
        _isShooting = true;
    }

    protected virtual void StopShooting()
    {
        _isShooting = false;
    }

    protected virtual void Update()
    {
        _shootTimer += Time.deltaTime;

        // REPLACE DOTWEEN DO PUNCH 
        {
            // _gunParent.localPosition = Vector3.Lerp
            //     (_gunParent.localPosition, _gunReturnPoint.localPosition, _weaponData.ReturnSpeed * Time.deltaTime);
            // _gunParent.localRotation = Quaternion.RotateTowards
            //     (_gunParent.localRotation, _gunReturnPoint.localRotation, 
            //         _weaponData.ReturnSpeed * Time.deltaTime);}
        }


        
        if (_shootTimer >= _weaponData.TimeToResetSpread) _currentBullet = 0;

        if (_isShooting)
        {
            Shoot();
        }
    }

    protected abstract void Shoot();
    
    protected virtual void WeaponPunch(float spreadY)
    {
        float punchZ = _weaponData.GunRecoilForce.x;
        float punchY = _weaponData.GunRecoilForce.y * spreadY;

        _gunParent.DOKill(true);
        
        _gunParent.DOPunchPosition(new Vector3(0, punchY, punchZ), 0.2f, 1);
        _gunParent.DOPunchRotation(new Vector3(-punchY * 5f, 0f, 0f), 0.2f, 1);
        
        // DO PUNCH BETTEr
        // _gunParent.localPosition += new Vector3(0, punchY, punchZ);
        // _gunParent.localRotation *= Quaternion.Euler(-punchY * 5f, 0f, 0f);
    }
    
    private void WeaponPunch()
    {
        float punchZ = _weaponData.GunRecoilForce.x;
        float punchY = _weaponData.GunRecoilForce.y * _weaponData.SpreadCurveY.Evaluate(_currentBullet);
                
        _gunParent.localPosition += new Vector3(0,punchY ,
            punchZ);
                
        _gunParent.localRotation *= Quaternion.Euler(-punchY * 5f, 0f, 0f);
    }
    
    protected virtual void DebugCube(Vector3 impactPoint)
    {
        if (_weaponData.DebugEnable)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = impactPoint;
            cube.transform.localScale = new Vector3(0.035f, 0.035f, 0.035f); 
            cube.GetComponent<Renderer>().material.color = Color.red;
            cube.GetComponent<Collider>().isTrigger = true;
            Destroy(cube, _weaponData.CubeLifetime);
        }
    }
}
