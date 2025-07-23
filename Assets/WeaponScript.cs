using UnityEngine;

public class Weapon : MonoBehaviour
{
  
    public WeaponData weaponData;

    
    public Transform muzzlePoint;

    
   // In Weapon.cs
public void Shoot()
{
    Debug.Log("Shoot() method called on " + gameObject.name);
    if (weaponData.bulletPrefab != null && muzzlePoint != null)
    {
        Debug.Log("Bullet Shot");
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - muzzlePoint.position).normalized;
        
        // Instantiate the bullet
        GameObject bulletObject = Instantiate(weaponData.bulletPrefab, muzzlePoint.position, Quaternion.identity);

        
        // Rotate the bullet to face the direction it's moving
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletObject.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Get the Bullet script and set its damage
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = weaponData.damage;
        }
        

        // Set the bullet's velocity
        Rigidbody2D bulletRb = bulletObject.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = direction * 10f; // You can use a variable for bullet speed here
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