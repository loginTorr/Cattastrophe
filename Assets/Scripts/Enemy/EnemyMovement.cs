using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float AgroSpeed;
    public float WanderSpeed;
    public float WanderMoveChance;
    public float MaxWanderDistance;
    private EnemyStateInfo Self;
    private EnemyStateInfo.State State;
    private EnemyStateInfo.Type Type;
    public Transform target;
    private float WanderTimer = 0.0f;
    private Transform PlayerTransform;
    private NavMeshAgent agent;
    private Direction driftDirection = Direction.Advance;
    public float FarShootingAdvanceChance;
    public float FarShootingRetreatChance;
    public float MidShootingAdvanceChance;
    public float MidShootingRetreatChance;
    public float CloseShootingAdvanceChance;
    public float CloseShootingRetreatChance;
    private enum Direction { 
        Advance,
        Retreat,
        Wander,
        Still
    }
    private Direction CurDirection;
    private Direction lastDirection;
    private enum StrafingDirection { 
        Clockwise,
        CounterClockwise
    }
    private StrafingDirection CurStrafingDirection = StrafingDirection.Clockwise;

    // Start is called before the first frame update
    void Start(){
        Self = transform.gameObject.GetComponent<EnemyStateInfo>();
        State = Self.state;
        Type = Self.type;
        PlayerTransform = Self.Player.transform;

        if (target == null) {
            return;
        }
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update(){
        State = Self.state;

        Pathfind();
        MoveTarget();
        Strafe();
        agent.SetDestination(target.position);
        RealignGaze();
    }

    void Strafe() {
        if (State != EnemyStateInfo.State.Wandering){
            float turn = Random.Range(0f, 1f);
            if (turn <= 0.2f)
            {
                if (CurStrafingDirection == StrafingDirection.Clockwise)
                {
                    CurStrafingDirection = StrafingDirection.CounterClockwise;
                }
                else
                {
                    CurStrafingDirection = StrafingDirection.Clockwise;
                }
            }

            Vector3 NotNormalizedDirection;
            if (CurStrafingDirection == StrafingDirection.Clockwise)
            {
                NotNormalizedDirection = PlayerTransform.position - transform.position;
            }
            else
            {
                NotNormalizedDirection = transform.position - PlayerTransform.position;
            }

            Vector3 direction = Vector3.Normalize(NotNormalizedDirection);
            Vector3 perpNotNormal = Vector3.Cross(direction, Vector3.up);
            Vector3 perp = Vector3.Normalize(perpNotNormal);

            target.position = target.position + perp;
        }
    }

    #region Pathfind

    void Pathfind() {
        if (Type == EnemyStateInfo.Type.Tiny) {
            PathfindAsTiny();
        }else if (Type == EnemyStateInfo.Type.Mele) {
            PathfindAsMele();
        }else if (Type == EnemyStateInfo.Type.Ranged) {
            PathfindAsRanged();
        }else if (Type == EnemyStateInfo.Type.ThrowStab) {
            PathfindAsThrowStab();
        }else {
            Debug.LogError("Error: Enemy Type is not valid Type (how did you manage this? it's an enum???)");
        }
    }

    void PathfindAsTiny() {
        if (State == EnemyStateInfo.State.Agro) {
            CurDirection = Direction.Advance;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        } else if (State == EnemyStateInfo.State.TooClose && CurDirection == Direction.Retreat) {
            CurDirection = Direction.Retreat;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        } else if (State == EnemyStateInfo.State.Wandering) {
            CurDirection = Direction.Wander;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = WanderSpeed;
        }
    }

    void PathfindAsMele(){
        if(State == EnemyStateInfo.State.Agro) {
            CurDirection = Direction.Advance;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        }else if (State == EnemyStateInfo.State.Mele){
            CurDirection = Direction.Still;
        }else if (State == EnemyStateInfo.State.TooClose) {
            CurDirection = Direction.Retreat;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        }else if (State == EnemyStateInfo.State.Wandering) {
            CurDirection = Direction.Wander;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = WanderSpeed;
        }
    }

    void PathfindAsRanged() { 
        if(State == EnemyStateInfo.State.Agro) {
            CurDirection = Direction.Advance;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        }else if (State == EnemyStateInfo.State.FarShooting) {
            float turn = Random.Range(0f, 1f);
                if(CurDirection == Direction.Advance) {
                    if(turn <= FarShootingRetreatChance){
                        CurDirection = Direction.Retreat;
                    }
                }else{
                    if(turn <= FarShootingAdvanceChance){
                        CurDirection = Direction.Advance;
                    }
                }
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        }else if (State == EnemyStateInfo.State.MidShooting) { 
            float turn = Random.Range(0f, 1f);
                if(CurDirection == Direction.Advance) {
                    if(turn <= MidShootingRetreatChance){
                        CurDirection = Direction.Retreat;
                    }
                }else{
                    if(turn <= MidShootingAdvanceChance){
                        CurDirection = Direction.Advance;
                    }
                }
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        }else if (State == EnemyStateInfo.State.CloseShooting) { 
            float turn = Random.Range(0f, 1f);
                if(CurDirection == Direction.Advance) {
                    if(turn <= CloseShootingRetreatChance){
                        CurDirection = Direction.Retreat;
                    }
                }else{
                    if(turn <= CloseShootingAdvanceChance){
                        CurDirection = Direction.Advance;
                    }
                }
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        }else if (State == EnemyStateInfo.State.TooClose) {
            CurDirection = Direction.Retreat;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        }
        else if (State == EnemyStateInfo.State.Wandering){
            CurDirection = Direction.Wander;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = WanderSpeed;
        }
    }

    void PathfindAsThrowStab() { 

    }

    #endregion

    void MoveTarget() {
        if (CurDirection == Direction.Advance) {
            target.position = new Vector3(PlayerTransform.position.x, target.position.y, PlayerTransform.position.z);
            lastDirection = Direction.Advance;
            if(gameObject.CompareTag("Raton")){
                gameObject.GetComponent<Animator>().SetBool("agro", true);
                gameObject.GetComponent<Animator>().SetBool("backwards", false);
            }
        } else if (CurDirection == Direction.Retreat) {
            Vector3 directionAndDistance = transform.position - PlayerTransform.position;
            Vector3 direction = Vector3.Normalize(directionAndDistance);
            target.position = transform.position + new Vector3(direction.x * 5, 0, direction.z * 5);
            lastDirection = Direction.Retreat;
            if(gameObject.CompareTag("Raton")){
                gameObject.GetComponent<Animator>().SetBool("agro", true);
                gameObject.GetComponent<Animator>().SetBool("backwards", true);
            }
        } else if (CurDirection == Direction.Wander) {
            if (WanderTimer <= 0) {
                float MoveDecision = Random.Range(0.0f, 1.0f);
                if (MoveDecision <= WanderMoveChance) {
                    float x = Random.Range(-1.0f, 1.0f);
                    float z = Random.Range(-1.0f, 1.0f);
                    Vector3 randomDirectionNotNormalized = new Vector3(x, transform.position.y, z);
                    Vector3 randomDirectionNormalized = Vector3.Normalize(randomDirectionNotNormalized);
                    float randomDistance = Random.Range(0.0f, MaxWanderDistance);
                    Vector3 randomDirectionAndDistance = randomDirectionNormalized * randomDistance;
                    target.position = new Vector3(transform.position.x + randomDirectionAndDistance.x, transform.position.y, transform.position.z + randomDirectionAndDistance.z);
                    if(gameObject.CompareTag("Raton")){
                        gameObject.GetComponent<Animator>().SetBool("agro", false);
                        gameObject.GetComponent<Animator>().SetBool("backwards", false);
                    }
                }
                WanderTimer = 2.0f;
            } else {
                WanderTimer -= Time.deltaTime;
            }
            lastDirection = Direction.Wander;
        } else if (CurDirection == Direction.Still){
            Vector3 notNormalizedToward = PlayerTransform.position - transform.position;
            Vector3 toward = Vector3.Normalize(notNormalizedToward);
            Vector3 notNormalizedAway = transform.position - PlayerTransform.position;
            Vector3 away = Vector3.Normalize(notNormalizedAway);

            
            if(Random.Range(0f, 1f) <= 0.10f) { 
                if (driftDirection == Direction.Advance) {
                    driftDirection = Direction.Retreat;
                    if(gameObject.CompareTag("Raton")){
                        gameObject.GetComponent<Animator>().SetBool("agro", true);
                        gameObject.GetComponent<Animator>().SetBool("backwards", false);
                    }
                } else if (driftDirection == Direction.Retreat) {
                    driftDirection = Direction.Advance;
                    if(gameObject.CompareTag("Raton")){
                        gameObject.GetComponent<Animator>().SetBool("agro", true);
                        gameObject.GetComponent<Animator>().SetBool("backwards", true);
                    }
                }
            }

            Vector3 drift;
            if(driftDirection == Direction.Advance) {
                drift = toward;
            }else{
                drift = away;
            }
            target.position = transform.position + new Vector3(drift.x * 5, 0f, drift.z * 5);
            lastDirection = Direction.Still;
        }
    }

    void RealignGaze() { 
        if(State != EnemyStateInfo.State.Wandering) {
            transform.LookAt(new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z));
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            CurDirection = Direction.Retreat;
        }
    }

    private void OnCollisionStay(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            CurDirection = Direction.Retreat;
        }
    }
}
