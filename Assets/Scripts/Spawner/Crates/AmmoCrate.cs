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
        GunManager gunManager = player.GetComponent<GunManager>();
        Gun gun = gunManager.weaponHandlerObject.transform.GetChild(gunManager.GetWeaponIndex()).GetComponent<Gun>();
        gun.currentAmmo = gun.maxAmmo;
    }
}