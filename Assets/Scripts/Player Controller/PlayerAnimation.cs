using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]AnimationClip animIdle, animWalk, animRun;
    public enum Anim{Idle, Walk, Run};
    public Anim currentAnim = Anim.Idle;
    public Animator playerAnimator;
    GunManager gunManager;
    public AnimatorOverrideController aoc;
    int weaponIndex, newIndex;

    // Start is called before the first frame update
    void Start()
    {
        gunManager = GetComponent<GunManager>();
        setAnim();
        weaponIndex = gunManager.GetWeaponIndex();
        updateAnimationClibs();
        aoc = new AnimatorOverrideController(playerAnimator.runtimeAnimatorController);
    }

    // Update is called once per frame
    void Update()
    {
        newIndex = gunManager.GetWeaponIndex();
        if (weaponIndex != newIndex){
            updateAnimationClibs();
            weaponIndex = newIndex;
        }
        setAnim();
    }

    void updateAnimationClibs(){
        Gun gun = gunManager.weaponHandlerObject.transform.GetChild(gunManager.GetWeaponIndex()).GetComponent<Gun>();
        AnimationClip gunIdle = gun.animIdle, gunWalk = gun.animWalk, gunRun = gun.animRun;
        
        if (gunIdle == null){
            gunIdle = animIdle;
        }
        
        if (gunWalk == null){
            gunWalk = animWalk;
        }

        if (gunRun == null){
            gunRun = animRun;
        }

        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();

        anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[0], gunIdle));
        anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[1], gunRun));
        anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[2], gunWalk));
        
        aoc.ApplyOverrides(anims);


        playerAnimator.runtimeAnimatorController = aoc;

    }

   void setAnim(){
        if (currentAnim == Anim.Idle){
            playerAnimator.SetInteger("State", 0);
        } else if (currentAnim == Anim.Walk){
            playerAnimator.SetInteger("State", 1);
        } else {
            playerAnimator.SetInteger("State", 2);
        }
    }
}
