using UnityEngine;

public class AmmoCrate: MonoBehaviour
{
    [SerializeField] private int ammoCrateAmmount = 20;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            RessuplyAmmo(ammoCrateAmmount);
            Destroy(gameObject);
        }
    }

    private void RessuplyAmmo(int ammountToRessuply) //When trigger player ammo goes up.
    {
        shooting.playerAmmo += ammountToRessuply;
    }
}