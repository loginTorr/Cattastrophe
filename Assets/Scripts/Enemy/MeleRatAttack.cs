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

    // Start is called before the first frame update
    void Start(){
        Self = transform.gameObject.GetComponent<EnemyStateInfo>();
        AttackTimer = Random.Range(MinAttackPause, MaxAttackPause);
        print("Remember! switch out the name for the weapon object at the end of the path once the actual model is in place. It currently says 'Cube', which won't work if the object you're trying to grab isn't called 'Cube'");
        WeaponCollider = transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm").Find("mixamorig:RightHand").Find("Cube").GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update(){
        State = Self.state;
        
        if (AttackTimer <= 0) {
            StartCoroutine(Attack());

            AttackTimer = Random.Range(MinAttackPause, MaxAttackPause);
        } else{
            AttackTimer -= Time.deltaTime;
        }

    }

    IEnumerator Attack() {
        WeaponCollider.enabled = true;
        print("remember! to replace the 2 in the number of seconds to wait with the length of the attack animation");
        yield return new WaitForSeconds(2);
        WeaponCollider.enabled = false;
    }
}
