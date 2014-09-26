//
//  PauseState.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;


public class PauseState : FSMState {

    public GameObject pause;

    public float timeAlpha = 0.5f;

    protected override void Awake () {
        base.Awake();

        stateID = StateID.PauseID;
        AddTransition(Transition.PlayToPause, StateID.PlayID);


    }

    public override void DoBeforeEntering() { 

        TweenAlpha.Begin(pause,timeAlpha,1);


    }
    public override void DoBeforeLeaving() {

        TweenAlpha.Begin(pause,timeAlpha,0);


    }
    protected override void Reason() {

    }

    public  void Resume() {
        ManagerPause.Instance.Pause = false;
        fsm.PerformTransition(Transition.PlayToPause);
    }




}

