using UnityEngine;

public class M4 : BaseWeapon
{
    protected override void Shoot()
    {
        if (_shootTimer >= _weaponData.ShootRate)
        {
            _shootTimer = 0;
    
            Ray targetRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Vector3 targetPoint = Physics.Raycast(targetRay, out RaycastHit hit, 100f, Physics.DefaultRaycastLayers,
                QueryTriggerInteraction.Ignore)
                ? hit.point
                : targetRay.GetPoint(100f);
    
            float spreadX, spreadY;
    
            if (_weaponData.RecoilType == WeaponRecoilType.Array && _weaponData.SpreadPattern.Length > 0)
            {
                int index = Mathf.Min(_currentBullet, _weaponData.SpreadPattern.Length - 1);
                spreadX = _weaponData.SpreadPattern[index].x;
                spreadY = _weaponData.SpreadPattern[index].y;
            }
            else
            {
                spreadX = _weaponData.SpreadCurveX.Evaluate(_currentBullet);
                spreadY = _weaponData.SpreadCurveY.Evaluate(_currentBullet);
            }
    
            float finalX = spreadX + Random.Range(_weaponData.MinXRandomSpread, _weaponData.MaxXRandomSpread);
            float finalY = spreadY + Random.Range(_weaponData.MinYRandomSpread, _weaponData.MaxYRandomSpread);
            Vector3 spreadOffset = new Vector3(finalX, finalY, 0);
    
            Vector3 direction = (targetPoint - _shootPoint.position).normalized;
            Vector3 finalDir = (_currentBullet > 0) ? (direction + spreadOffset).normalized : direction;
    
            if (Physics.Raycast(_shootPoint.position, finalDir, out RaycastHit finalHit, 100f, Physics.DefaultRaycastLayers,
                    QueryTriggerInteraction.Ignore))
            {
                if(!_damagableCache.TryGetValue(finalHit.transform, out IDamagable damagable))
                {
                    damagable = finalHit.transform.GetComponentInParent<IDamagable>() 
                                ?? finalHit.transform.GetComponentInChildren<IDamagable>();
                    
                    _damagableCache[finalHit.transform] = damagable;
                }
                damagable?.TakeDamage(_weaponData.WeaponDamage);

                DebugCube(finalHit.point);
            }
            else
            {
                DebugCube(_shootPoint.position + (finalDir * 100));
            }

            
           
            _camera.ApplyRecoil(new Vector2(spreadX, spreadY) * _weaponData.RecoilMultiplier);
    
            WeaponPunch(spreadY);
    
            _currentBullet++;
        }
    }
}
