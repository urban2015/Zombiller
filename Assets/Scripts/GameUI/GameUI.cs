using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class GameUI : MonoBehaviour
    {
        [Header("GameUI References")]
        [SerializeField] private Text hp;
        [SerializeField] private Text clibAmmo;
        [SerializeField] private Text ammo;
        [SerializeField] private Text weaponName;
        [SerializeField] private Text zombieLeft;

        int _weaponIndex = 0;
        Weapon _weapon;

        [Header("Prefabs Reference")]
        [SerializeField] private HealthComponent hpComponent;
        [SerializeField] private WeaponManager gunManager;

        void Start(){
            _weaponIndex = gunManager.selectedWeapon;
            _weapon = gunManager.weaponHandlerObject.transform.GetChild(_weaponIndex).transform.GetComponent<Weapon>();
        }
        void Update()
        {
            hp.text =  (hpComponent != null ? hpComponent.Health.Current + " HP" : "null"); //edit: checks to make sure the player isn't null when we update the player health text

            if (_weaponIndex != gunManager.selectedWeapon || _weapon == null){
                _weaponIndex = gunManager.GetWeaponIndex();
                _weapon = gunManager.weaponHandlerObject.transform.GetChild(_weaponIndex).transform.GetComponent<Weapon>();
            }

            weaponName.text = _weapon.weaponName;
            clibAmmo.text = _weapon.currentClibAmmo.ToString();
            ammo.text = _weapon.currentAmmo.ToString();

            zombieLeft.text = NumberOfZombies.EnemiesInScene.ToString();
        }
    }
}