using UnityEngine;

public class HealthCrate: MonoBehaviour
{
    [SerializeField] private int healthCrateAmmount = 20;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            RessuplyHealth(healthCrateAmmount);
            Destroy(gameObject);
        }
    }

    private void RessuplyHealth(int ammountToRessuply) //When trigger player ammo goes up.
    {
        HealthComponent.HealthPackHpToAdd += ammountToRessuply;
    }
}