using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShootingRatAttack : MonoBehaviour{
    private EnemyStateInfo Self;
    private EnemyStateInfo.State State;
    public float MaxAttackPause;
    public float MinAttackPause;
    public GameObject projectilePrefab;
    private float AttackTimer;
    private GameObject Player;
    private Transform ProjectileSpawnPt;
    public float speed;
    private Animator Anim;
    private bool AnimRunning;

    // Start is called before the first frame update
    void Start(){
        Self = transform.gameObject.GetComponent<EnemyStateInfo>();
        Player = Self.Player;
        AttackTimer = Random.Range(MinAttackPause, MaxAttackPause);
        Anim = transform.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update(){
        State = Self.state; 
        ProjectileSpawnPt = transform.Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:RightShoulder").Find("mixamorig:RightArm").Find("mixamorig:RightForeArm").Find("mixamorig:RightHand").Find("ProjectileSpawnPt").GetComponent<Transform>();

        if (Self.ReadyToStart){
            if (AnimRunning == false){
                if (State == EnemyStateInfo.State.FarShooting || State == EnemyStateInfo.State.MidShooting || State == EnemyStateInfo.State.CloseShooting){
                    if (AttackTimer <= 0){
                        //StartCoroutine(Shoot());
                        ShootAnim();

                        AttackTimer = Random.Range(MinAttackPause, MaxAttackPause);
                    }else{
                        AttackTimer -= Time.deltaTime;
                    }
                }
            }
        }
    }

    void ShootAnim() {
        Self.midAttack = true;
        AnimRunning = true;
        Anim.SetTrigger("GrabGear");
    }

    public void ShootFunc() {
    //IEnumerator Shoot() {
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = ProjectileSpawnPt.position;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 AtPlayer = Player.transform.position - ProjectileSpawnPt.position;

        // speed = distance/time => speed * time = distance => time = distance/speed
        float t = (Vector3.Distance(ProjectileSpawnPt.position, Player.transform.position)) / speed;
        float g = Physics.gravity.magnitude;
        float verticalOffset = 0.5f * g * (t * t);
        Vector3 DirectionNotNormalized = new Vector3(AtPlayer.x, (Player.transform.position.y * t) + verticalOffset + (Player.transform.lossyScale.y * 0.5f), AtPlayer.z);
        Vector3 Direction = Vector3.Normalize(DirectionNotNormalized) * speed;

        rb.velocity = Direction;
        AnimRunning = false;
        Self.midAttack = false;
    }
}
