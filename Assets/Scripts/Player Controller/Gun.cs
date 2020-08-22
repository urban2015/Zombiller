using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum FireType{Single, Auto};
    //public enum GunType{Normal, Shotgun};
    public FireType fireType;
    //public GunType gunType;
    public string weaponName;
    public int clibSize, maxAmmo, currentAmmo = -1, currentClibAmmo = -1;
    public float reloadTime, damage, fireRate;
    [SerializeField]private Vector3 customPosition;
    [SerializeField]private Vector3 customRoation;
    [HideInInspector]public Transform rayCastPoint;
    bool isReloading = false, canShoot = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if (currentAmmo < 0) 
            currentAmmo = maxAmmo;

        if (currentClibAmmo < 0)
            currentClibAmmo = clibSize;

        //adapt to hand 
        transform.localPosition = customPosition;
        transform.localEulerAngles = customRoation;


    }

    // Update is called once per frame
    void Update()
    {
        //Handle input
        if (   (fireType == FireType.Auto && Input.GetButton("Fire1") && canShoot)
            || (fireType == FireType.Single && Input.GetButtonDown("Fire1") && canShoot)){

            Shoot();
            canShoot = false;
            StartCoroutine(FireRateTimer());
        }

        if (Input.GetButtonDown("Reload") && !isReloading && currentClibAmmo != clibSize){
            currentAmmo += currentClibAmmo;
            currentClibAmmo = 0;
            Reload();
        }
    }

    public void Shoot(){
        if (currentClibAmmo > 0){
            currentClibAmmo--;
            RaycastHit hit;
            if (Physics.Raycast(rayCastPoint.transform.position, rayCastPoint.transform.forward, out hit, 50)){
                Target target = hit.transform.GetComponent<Target>();
                if (target != null){
                    target.TakeDamage(damage);
                }
            }

            if (currentClibAmmo == 0 && !isReloading){
                Reload();
            }

        } else {
            if (!isReloading)
                Reload();   
        }
    }

    public void Reload(){
        isReloading = true;
        StartCoroutine(ReloadTimer());
    }

    void OnEnable() {
        if (currentClibAmmo == 0){
            Reload();
        }    
    }
    IEnumerator FireRateTimer(){
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    IEnumerator ReloadTimer(){
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        if (currentAmmo >= clibSize){
            currentAmmo -= clibSize;
            currentClibAmmo = clibSize;
        } else {
            currentClibAmmo = currentAmmo;
            currentAmmo = 0;
        }
    }

}
