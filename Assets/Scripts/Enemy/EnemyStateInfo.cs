using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateInfo : MonoBehaviour
{
    public enum State{
        TooClose,
        Mele,
        CloseShooting,
        MidShooting,
        FarShooting,
        Agro,
        Wandering
    }
    public State state;
    public GameObject Player;
    private Vector3 PlayerPos;
    private Range range;
    public enum Type { 
        Tiny,
        Mele,
        Ranged,
        ThrowStab,
        Boss
    }
    public Type type;


    // Start is called before the first frame update
    void Start(){
        PlayerPos = Player.transform.position;
        range = Player.GetComponent<Range>();
    }

    // Update is called once per frame
    void Update(){
        PlayerPos = Player.transform.position;
        ChangeState();
    }

    void ChangeState() {
        float distance = Vector3.Distance(transform.position, PlayerPos);
        if (distance <= range.TooClose) {
            state = State.TooClose;
        }else if (distance <= range.Mele) {
            state = State.Mele;
        }else if (distance <= range.CloseShooting) {
            state = State.CloseShooting;
        }else if (distance <= range.MidShooting) {
            state = State.MidShooting;
        }else if (distance <= range.FarShooting) {
            state = State.FarShooting;
        }else if (distance <= range.Agro) {
            state = State.Agro;
        }else {
            state = State.Wandering;
        }
    }
}
