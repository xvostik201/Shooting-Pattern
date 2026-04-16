using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private WeaponRegistry _weaponRegistry;
    [SerializeField] private CameraRotation _cameraRotation;

    public override void InstallBindings()
    {
        Container.BindInstance(_weaponRegistry).AsSingle();
        Container.BindInstance(_cameraRotation).AsSingle();
    }
}
