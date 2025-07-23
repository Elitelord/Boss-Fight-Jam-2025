using UnityEngine;

public class Weapon : MonoBehaviour
{
  
    public WeaponData weaponData;

    
    public Transform muzzlePoint;

    
    public void Shoot()
    {
       
        Debug.Log("Shoot() method called on " + gameObject.name);
        if (weaponData.bulletPrefab != null && muzzlePoint != null)
        {
            Debug.Log("Bullet Shot");
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPos - muzzlePoint.position).normalized;
            
            GameObject bullet = Instantiate(weaponData.bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = direction * 10f;
            // bulletRb.linearVelocity = direction * bulletSpeed;
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