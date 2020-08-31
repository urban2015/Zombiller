using UnityEngine;

public class AmmoCrate: MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            RessuplyAmmo(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void RessuplyAmmo(GameObject player) //When trigger player ammo goes up.
    {
        WeaponManager gunManager = player.GetComponent<WeaponManager>();
        Weapon gun = gunManager.weaponHandlerObject.transform.GetChild(gunManager.GetWeaponIndex()).GetComponent<Weapon>();
        gun.currentAmmo = gun.maxAmmo;
    }
}