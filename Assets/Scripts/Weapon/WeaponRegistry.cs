using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponRegistry", menuName = "ScriptableObject/WeaponRegistry")]
public class WeaponRegistry : ScriptableObject
{
    public List<WeaponData> AllWeapons;

    public WeaponData GetWeapon(string ID) => AllWeapons.Find(x => x.WeaponName == ID);
}
