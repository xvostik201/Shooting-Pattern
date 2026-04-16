using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/Data/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("--- Weapon Identification ---")]
    [Tooltip("Unique ID used to identify this weapon in the registry (e.g., 'M4A1', 'Glock17')")]
    public string WeaponName;

    [SerializeField] private float _weaponDamage = 1f;
    public float WeaponDamage => _weaponDamage;
    
    [Header("--- Primary Settings ---")]
    [Tooltip("How the recoil pattern is calculated")]
    [SerializeField] private WeaponRecoilType _recoilType;
    public WeaponRecoilType RecoilType => _recoilType;
    
    [Tooltip("Time between shots in seconds")]
    [Range(0.01f, 1f)]
    [SerializeField] private float _shootRate = 0.1f;
    public float ShootRate => _shootRate;

    [Header("--- Visual Polish ---")]
    [Tooltip("How fast the gun returns to the initial position")]
    [SerializeField] private float _returnSpeed = 10f;
    public float ReturnSpeed => _returnSpeed;

    [Tooltip("X = Punch Back force, Y = Upward tilt multiplier")]
    [SerializeField] private Vector2 _gunRecoilForce = new Vector2(-0.1f, 0.2f);
    public Vector2 GunRecoilForce => _gunRecoilForce;

    [Header("--- Recoil Logic ---")]
    [Tooltip("Time of inactivity before the bullet counter resets")]
    [SerializeField] private float _timeToResetSpread = 0.5f;
    public float TimeToResetSpread => _timeToResetSpread;

    [Tooltip("Legacy array-based pattern (used if RecoilType is Array)")]
    [SerializeField] private Vector2[] _spreadPattern;
    public Vector2[] SpreadPattern => _spreadPattern;

    [Tooltip("Modern curve-based pattern (used if RecoilType is Curve)")]
    [SerializeField] private AnimationCurve _spreadCurveX = AnimationCurve.Linear(0, 0, 10, 0);
    [SerializeField] private AnimationCurve _spreadCurveY = AnimationCurve.EaseInOut(0, 0, 10, 1);
    public AnimationCurve SpreadCurveX => _spreadCurveX;
    public AnimationCurve SpreadCurveY => _spreadCurveY;

    [Header("--- Random Spread (Noise) ---")]
    [SerializeField] private float _minXRandomSpread = -0.01f;
    [SerializeField] private float _maxXRandomSpread = 0.01f;
    [Space(5)]
    [SerializeField] private float _minYRandomSpread = -0.01f;
    [SerializeField] private float _maxYRandomSpread = 0.01f;

    public float MinXRandomSpread => _minXRandomSpread;
    public float MaxXRandomSpread => _maxXRandomSpread;
    public float MinYRandomSpread => _minYRandomSpread;
    public float MaxYRandomSpread => _maxYRandomSpread;

    [Header("--- Global Modifiers ---")]
    [Tooltip("Overall intensity multiplier for camera recoil")]
    [SerializeField] private float _recoilMultiplier = 1f;
    public float RecoilMultiplier => _recoilMultiplier;

    [Header("--- Development Tools ---")]
    [SerializeField] private bool _debugEnable = false;
    [SerializeField] private float _cubeLifetime = 2f;
    
    public bool DebugEnable => _debugEnable;
    public float CubeLifetime => _cubeLifetime;
}