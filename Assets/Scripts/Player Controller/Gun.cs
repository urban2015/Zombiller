using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Gun : MonoBehaviour
{
    public enum FireType{Single, Auto, Burst};
    //public enum GunType{Normal, Shotgun};
    public FireType fireType;
    //public GunType gunType;
    public string weaponName;
    public int clibSize, maxAmmo, currentAmmo = -1, currentClibAmmo = -1;
    [HideInInspector]public int burstAmount = 3; 
    [HideInInspector]public float burstDelay = 1;
    public float reloadTime, damage, fireRate;
    [Header("Crosshair")]
    public Sprite normalCrosshair; 
    public Sprite reloadingCrosshair, outOfAmmoCrosshair;
    [Header("Custom")]
    [SerializeField]private Vector3 customPosition;
    [SerializeField]private Vector3 customRoation;
    [HideInInspector]public Transform rayCastPoint;
    [HideInInspector]public SpriteRenderer crosshairRenderer;
    [HideInInspector]public bool isReloading = false;
    bool canShoot = true, canBurst = true;
    [Header("Animation Clibs")]
    public AnimationClip animIdle;
    public AnimationClip animWalk, animRun;
    
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
        if (fireType == FireType.Burst && Input.GetButtonDown("Fire1") && canBurst){
            StartCoroutine(BurstTimer());
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
        if (currentAmmo > 0){
            isReloading = true;
            StartCoroutine(ReloadTimer());
        }
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

    IEnumerator BurstTimer(){
        canBurst = false;
        for (int i = 0; i < burstAmount; i++){
            canShoot = false;
            Shoot();
            StartCoroutine(FireRateTimer());
            yield return new WaitForSeconds(fireRate);
        }
        yield return new WaitForSeconds(burstDelay);
        canBurst = true;
    }

}

//EDITOR
#if UNITY_EDITOR
 [CustomEditor(typeof(Gun))]
 public class Gun_Editor : Editor
 {
     public override void OnInspectorGUI()
     {
         DrawDefaultInspector(); // for other non-HideInInspector fields
 
         Gun script = (Gun)target;
 
         // draw checkbox for the bool
         if (script.fireType == Gun.FireType.Burst) // if bool is true, show other fields
         {
            EditorGUILayout.LabelField("Custom Options", EditorStyles.boldLabel); 
            script.burstAmount = EditorGUILayout.IntField("Burst Amount", script.burstAmount);
            script.burstDelay = EditorGUILayout.FloatField("Burst Delay", script.burstDelay);
         }
     }
 }
 #endif