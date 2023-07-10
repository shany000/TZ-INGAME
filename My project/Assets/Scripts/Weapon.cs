using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 51)]
public class Weapon : ScriptableObject
{
    public GameObject WeaponModel;

    public float Cooldown;

    public int AttackPower;

    public Color ShootColor;
}
