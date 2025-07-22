using UnityEngine;

public class Weapon : MonoBehaviour
{
  
    public WeaponData weaponData;

    
    public Transform muzzlePoint;

    
    public void Shoot()
    {
       

        if (weaponData.bulletPrefab != null && muzzlePoint != null)
        {
            Instantiate(weaponData.bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        }

       
        if (weaponData.fireSound != null)
        {
            
            AudioSource.PlayClipAtPoint(weaponData.fireSound, transform.position);
        }

      
        if (weaponData.muzzleFlashEffect != null && muzzlePoint != null)
        {
            Instantiate(weaponData.muzzleFlashEffect, muzzlePoint.position, muzzlePoint.rotation);
        }
    }
}