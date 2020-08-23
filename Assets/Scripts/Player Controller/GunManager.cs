using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField]private GameObject[] weaponsPrefabs;
    public GameObject weaponHandlerObject, rayCastPoint;
    public int[] currentWeapons = {0, 1};
    [HideInInspector] public int selectedWeapon = 0;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate weapon prefabs
        foreach (GameObject weapon in weaponsPrefabs){
            if (weapon.transform.GetComponent<Gun>() != null){
                GameObject obj = Instantiate(weapon, weaponHandlerObject.transform);
                obj.GetComponent<Gun>().rayCastPoint = rayCastPoint.transform;
            }
        }

        //checking if current weapons have a valid index
        i = 0;
        foreach (int weaponIndex in currentWeapons){
            if (weaponIndex < 0){
                currentWeapons[i] = 0;
            } else if (weaponIndex > weaponHandlerObject.transform.childCount - 1){
                currentWeapons[i] = weaponHandlerObject.transform.childCount - 1;
            }
            i++;
        }

        SelectWeapon(selectedWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0){
            NextWeapon();
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0){
            PreviousWeapon();
        }
    }

    public void SelectWeapon(int WeaponIndex){
        //checking if index out of range and loop through current weapons
        if (WeaponIndex < 0){
            selectedWeapon = currentWeapons.Length - 1;
        } else if (WeaponIndex > currentWeapons.Length - 1){
            selectedWeapon = 0;
        } else {
            selectedWeapon = WeaponIndex;
        }

        //enabling only selected weapon
        i = 0;
        foreach (Transform weapon in weaponHandlerObject.transform){
            if (i != currentWeapons[selectedWeapon]){
                weapon.gameObject.SetActive(false);
            } else {
                weapon.gameObject.SetActive(true);
            }
            i++;
        }

    }

    public void NextWeapon(){
        SelectWeapon(selectedWeapon + 1);
    }

    public void PreviousWeapon(){
        SelectWeapon(selectedWeapon - 1);
    }
}
