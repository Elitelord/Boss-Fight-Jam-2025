using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon System/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public string weaponName;
    public string description;

    [Header("Stats")]
    public float damage = 10f;
    public float fireRate = 0.5f; 
    public int maxAmmo = 30;

    [Header("Prefabs & Effects")]
    public GameObject bulletPrefab;
    public GameObject muzzleFlashEffect;
    public AudioClip fireSound;
}