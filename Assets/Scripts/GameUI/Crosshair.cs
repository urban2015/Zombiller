using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField]GunManager gunManager;
    Image crosshairImage;
    Gun currentWeapon;
    public Sprite defaultCrosshair, defaultReloadingCrosshair, defaultOutOfAmmoCrosshair;
    int weaponIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        crosshairImage = transform.GetComponent<Image>();
        EnableCrosshair();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;

        //update current weapon if changed
        if (weaponIndex != gunManager.selectedWeapon || currentWeapon == null){
            weaponIndex = gunManager.selectedWeapon;
            currentWeapon = gunManager.weaponHandlerObject.transform.GetChild(weaponIndex).GetComponent<Gun>();
        }

        //check what crosshair to show
        if (currentWeapon.currentClibAmmo == 0){
            if(currentWeapon.currentAmmo == 0){
                if (currentWeapon.outOfAmmoCrosshair != null){
                    crosshairImage.overrideSprite = currentWeapon.outOfAmmoCrosshair;
                } else {
                    crosshairImage.overrideSprite = defaultOutOfAmmoCrosshair;
                }
            } else {
                if (currentWeapon.reloadingCrosshair != null){
                        crosshairImage.overrideSprite = currentWeapon.reloadingCrosshair;
                } else {
                        crosshairImage.overrideSprite = defaultReloadingCrosshair;
                } 
            }
        } else if (currentWeapon.normalCrosshair != null){
            crosshairImage.overrideSprite = currentWeapon.normalCrosshair;
        } else {
            crosshairImage.overrideSprite = defaultCrosshair;
        }

    }

    void EnableCrosshair(){
        Cursor.visible = false;
        crosshairImage.enabled = true;
    }

    void DisableCrosshair(){
        Cursor.visible = true;
        crosshairImage.enabled = false;
    }
}
