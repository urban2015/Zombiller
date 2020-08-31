using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Weapon : MonoBehaviour
{
    public enum WeaponType{Normal, Melee};
    public WeaponType weaponType;
    public enum FireType{Single, Auto, Burst};
    public FireType fireType;
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
    [HideInInspector]private Vector3 meleeRaycastOffest = new Vector3(0, 0, 1);
    [HideInInspector]public Transform rayCastPoint;
    [HideInInspector]public SpriteRenderer crosshairRenderer;
    [HideInInspector]public bool isReloading = false;
    [HideInInspector]public int meleeRange;
    bool canShoot = true, canBurst = true, isMelee = false;
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

        isMelee = (weaponType == WeaponType.Melee);
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
        if (currentClibAmmo > 0 || isMelee){
            if (!isMelee){
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
                RaycastHit[] hitList = Physics.BoxCastAll(rayCastPoint.transform.position + new Vector3(0, 1,  -meleeRange / 2), new Vector3(meleeRange / 2, 1, meleeRange / 2), rayCastPoint.transform.forward, Quaternion.identity, meleeRange);

                foreach (var hit in hitList){
                    Target target = hit.transform.GetComponent<Target>();
                    if (target != null){
                        target.TakeDamage(damage);
                    }
                }
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
 [CustomEditor(typeof(Weapon))]
 public class Gun_Editor : Editor
 {
     public override void OnInspectorGUI()
     {
         DrawDefaultInspector(); // for other non-HideInInspector fields
 
         Weapon script = (Weapon)target;
 
         // draw custom options
         if (script.fireType == Weapon.FireType.Burst) // if bool is true, show other fields
         {
            EditorGUILayout.LabelField("Burst Options", EditorStyles.boldLabel); 
            script.burstAmount = EditorGUILayout.IntField("Burst Amount", script.burstAmount);
            script.burstDelay = EditorGUILayout.FloatField("Burst Delay", script.burstDelay);
         }

         if (script.weaponType == Weapon.WeaponType.Melee) // if bool is true, show other fields
         {
            EditorGUILayout.LabelField("Melee Options", EditorStyles.boldLabel); 
            script.meleeRange = EditorGUILayout.IntField("Melee Range", script.meleeRange);
         }

     }
 }
 #endif