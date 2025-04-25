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
    public State state = State.Wandering;
    public GameObject Player;
    private Vector3 PlayerPos;
    public Range range;
    public enum Type { 
        Tiny,
        Mele,
        Ranged,
        ThrowStab
    }
    public Type type;


    // Start is called before the first frame update
    void Start(){
        PlayerPos = Player.transform.position;
        range = transform.gameObject.GetComponent<Range>();
    }

    // Update is called once per frame
    void Update(){
        PlayerPos = Player.transform.position;
        ChangeState();
    }

    void ChangeState() {
        float distance = Vector3.Distance(transform.position, PlayerPos);
        if (distance <= range.TooClose && range.HasTooClose) {
            state = State.TooClose;
        }else if (distance <= range.Mele && range.HasMele) {
            state = State.Mele;
        }else if (distance <= range.CloseShooting && range.HasCloseShooting) {
            state = State.CloseShooting;
        }else if (distance <= range.MidShooting && range.HasCloseShooting) {
            state = State.MidShooting;
        }else if (distance <= range.FarShooting && range.HasFarShooting) {
            state = State.FarShooting;
        }else if (distance <= range.Agro && range.HasAgro) {
            state = State.Agro;
        }else if (state == State.Agro || state == State.FarShooting || state == State.MidShooting || state == State.CloseShooting || state == State.Mele || state == State.TooClose){
            if (range.CanDeagro) {
                state = State.Wandering;
            }
        }else {
            state = State.Wandering;
        }
    }
}
