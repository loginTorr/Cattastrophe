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
    private enum Direction { 
        Advance,
        Retreat,
        Wander
    }
    private Direction CurDirection;

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
        agent.SetDestination(target.position);
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
        if (State != EnemyStateInfo.State.Wandering && State != EnemyStateInfo.State.TooClose) {
            CurDirection = Direction.Advance;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        } else if (State == EnemyStateInfo.State.TooClose && CurDirection == Direction.Retreat) {
            CurDirection = Direction.Retreat;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = AgroSpeed;
        }else if (State == EnemyStateInfo.State.Wandering){
            CurDirection = Direction.Wander;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = WanderSpeed;
        }
    }

    void PathfindAsMele() { 

    }

    void PathfindAsRanged() { 

    }

    void PathfindAsThrowStab() { 

    }

    #endregion

    void MoveTarget() { 
        if(CurDirection == Direction.Advance) {
            target.position = new Vector3(PlayerTransform.position.x, target.position.y, PlayerTransform.position.z);
        }else if(CurDirection == Direction.Retreat) {
            Vector3 directionAndDistance = transform.position - PlayerTransform.position;
            Vector3 direction = Vector3.Normalize(directionAndDistance);
            target.position = target.position + new Vector3(direction.x, 0, direction.z);
        }else if (CurDirection == Direction.Wander) {
            if (WanderTimer <= 0){
                float MoveDecision = Random.Range(0.0f, 1.0f);
                if (MoveDecision <= WanderMoveChance){
                    Vector3 randomDirectionNotNormalized = new Vector3(Random.Range(-1.0f, 1.0f), transform.position.y, Random.Range(-1.0f, 1.0f));
                    Vector3 randomDirectionNormalized = Vector3.Normalize(randomDirectionNotNormalized);
                    float randomDistance = Random.Range(0.0f, MaxWanderDistance);
                    Vector3 randomDirectionAndDistance = new Vector3(randomDirectionNormalized.x * randomDistance, transform.position.y, randomDirectionNormalized.z * randomDistance);
                    target.position = new Vector3(target.position.x + randomDirectionAndDistance.x, target.position.y, target.position.z + randomDirectionAndDistance.z);
                }
                WanderTimer = 2.0f;
            }else {
                WanderTimer -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            CurDirection = Direction.Retreat;
        }
    }
}
