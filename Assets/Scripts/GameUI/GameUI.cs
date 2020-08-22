using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("GameUI References")]
    [SerializeField] private Text HP;
    [SerializeField] private Text ClibAmmo;
    [SerializeField] private Text Ammo;
    [SerializeField] private Text WeaponName;
    [SerializeField] private Text ZombieLeft;

    int weaponIndex = 0;
    Gun weapon;

    [Header("Prefabs Reference")]
    [SerializeField] private HealthComponent hpComponent;
    [SerializeField] private GunManager gunManager;

    void Start(){
        weaponIndex = gunManager.selectedWeapon;
        weapon = gunManager.weaponHandlerObject.transform.GetChild(weaponIndex).transform.GetComponent<Gun>();
    }
    void Update()
    {
        HP.text =  (hpComponent != null ? hpComponent._health.Current + " HP" : "null"); //edit: checks to make sure the player isn't null when we update the player health text

        if (weaponIndex != gunManager.selectedWeapon || weapon == null){
            weaponIndex = gunManager.selectedWeapon;
            weapon = gunManager.weaponHandlerObject.transform.GetChild(weaponIndex).transform.GetComponent<Gun>();
        }

        WeaponName.text = weapon.weaponName;
        ClibAmmo.text = weapon.currentClibAmmo.ToString();
        Ammo.text = weapon.currentAmmo.ToString();

        ZombieLeft.text = NumberOfZombies.EnemiesInScene.ToString();
    }
}