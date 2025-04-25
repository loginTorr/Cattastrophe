using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float AgroSpeed;
    public float WanderSpeed;
    private EnemyStateInfo Self;
    private EnemyStateInfo.State State;
    private EnemyStateInfo.Type Type;

    // Start is called before the first frame update
    void Start(){
        Self = transform.gameObject.GetComponent<EnemyStateInfo>();
        State = Self.state;
        Type = Self.type;
    }

    // Update is called once per frame
    void Update(){
        State = Self.state;
    }
}
