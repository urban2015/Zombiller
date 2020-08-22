using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    // Bullet spawnpoint
    public Transform gunPoint;
    public GameObject bulletPrefab;
    // Force added to bullet
    public float bulletspeed = 15f;
    // Delay until bullet deletes
    public float delay = 5f;
    //Time Between Shoots
    [SerializeField] private float rateOfFire = .5f;
    [SerializeField] private bool auto = true;
    private float waitTime;
    //Ammo Related 
    public static int playerAmmo = 10;

    void Update()
    {
        // Shoot when fire button is pressed
        if (auto){
            if (Input.GetButton("Fire1"))
            {
                HandleShooting();
            }
        } else {
            if (Input.GetButtonDown("Fire1"))
            {
                HandleShooting();
            }
        }
    }

    void Shoot()
    {
        // Instantiate bullet at the spawnpoint and add an upwards force
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.AddForce(gunPoint.forward * bulletspeed, ForceMode.Impulse);
        // Destroy the bullet after the delay
        Object.Destroy(bullet, delay);
    }

    void HandleShooting(){
        if(waitTime < Time.time && playerAmmo > 0)
        {
            Shoot();
            playerAmmo--;
            waitTime = Time.time + rateOfFire;
        }
    }
}