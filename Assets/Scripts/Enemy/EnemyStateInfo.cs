using System;
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
    public Boolean ReadyToStart = false;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
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

    #region getting info on what's enabled
    public List<State> getRangeOptions() {
        List<State> StateOptions = new List<State>();
        if (range.HasAgro) {
            StateOptions.Add(State.Agro);
        }
        if (range.HasFarShooting) {
            StateOptions.Add(State.FarShooting);
        }
        if (range.HasMidShooting) {
            StateOptions.Add(State.MidShooting);
        }
        if (range.HasCloseShooting) {
            StateOptions.Add(State.CloseShooting);
        }
        if (range.HasMele) {
            StateOptions.Add(State.Mele);
        }
        if (range.HasTooClose) {
            StateOptions.Add(State.TooClose);
        }
        return StateOptions;
    }

    public State getNextState(State curState) {
        List<State> options = getRangeOptions();

        int index = options.IndexOf(curState);
        if(index != -1 && index < options.Count -1) {
            return options[index + 1];
        }else if (index == -1) {
            print("Error: State not enabled (EnemyStateInfo:getNextState)");
            return curState;
        }else if (index >= options.Count - 1) {
            print("Error: Tried to get next state on last enabled state (EnemyStateInfo:getNextState)");
            return curState;
        }else {
            print("Error: Reaching this shouldn't be possible (EnemyStateInfo:getNextState)");
            return curState;
        }
    }

    public State getPrevState(State curState){
        List<State> options = getRangeOptions();

        int index = options.IndexOf(curState);
        if (index != -1 && index != 0){
            return options[index - 1];
        }else if (index == -1){
            print("Error: State not enabled (EnemyStateInfo:getPrevState)");
            return curState;
        }else if (index == 0){
            print("Error: Tried to get prev state on first enabled state (EnemyStateInfo:getPrevState)");
            return curState;
        }else{
            print("Error: Reaching this shouldn't be possible (EnemyStateInfo:getPrevState)");
            return curState;
        }
    }

    #endregion
}
