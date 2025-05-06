using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleRatAttack : MonoBehaviour{
    private EnemyStateInfo Self;
    private EnemyStateInfo.State State;
    public float MaxAttackPause;
    public float MinAttackPause;
    private float AttackTimer;
    private BoxCollider WeaponCollider;
    private Animator Anim;
    private bool animPlaying = false;

    // Start is called before the first frame update
    void Start(){
        Self = transform.gameObject.GetComponent<EnemyStateInfo>();
        AttackTimer = Random.Range(MinAttackPause, MaxAttackPause);
        print("Remember! switch out the name for the weapon object at the end of the path once the actual model is in place. It currently says 'Cube', which won't work if the object you're trying to grab isn't called 'Cube'");
        WeaponCollider = transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm").Find("mixamorig:RightHand").Find("Cube").GetComponent<BoxCollider>();
        Anim = transform.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        State = Self.state;

        if (State == EnemyStateInfo.State.Mele && animPlaying == false){
            if (AttackTimer <= 0){
                StartCoroutine(Attack());

                AttackTimer = Random.Range(MinAttackPause, MaxAttackPause);
            }else{
                AttackTimer -= Time.deltaTime;
            }
        }

    }

    IEnumerator Attack() {
        animPlaying = true;

        if (Self.ReadyToStart){
            int swingDir = Random.Range(0, 2);
            float waitTime;
            if (swingDir == 0){
                Anim.SetTrigger("SlashForward");
                waitTime = getAnimation(Anim, "Stable Sword Outward Slash").length;
            }else{
                Anim.SetTrigger("SlashBackwards");
                waitTime = getAnimation(Anim, "Stable Sword Inward Slash").length;
            }
            yield return new WaitForSeconds(1);
            WeaponCollider.enabled = true;
            yield return new WaitForSeconds(waitTime - 1);
            WeaponCollider.enabled = false;
            animPlaying = false;
        }
    }

    AnimationClip getAnimation(Animator anim, string name) {
        RuntimeAnimatorController controler = anim.runtimeAnimatorController;
        AnimationClip[] clips = controler.animationClips;

        AnimationClip clip = null;
        foreach (AnimationClip c in clips) {
            if (c.name == name) {
                clip = c;
            }
        }
        if (clip != null) {
            return clip;
        } else {
            Debug.LogError("Error: no animation found by that name (in ShootingAttack)");
            return clip;
        }
    }
}
