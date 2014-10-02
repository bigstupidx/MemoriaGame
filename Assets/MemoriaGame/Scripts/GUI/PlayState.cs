//
//  PlayState.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;


public class PlayState : FSMState {

    public GameObject play;
    public float timeAlpha = 1.0f;


    protected override void Awake () {
        base.Awake();
        stateID = StateID.PlayID;
        AddTransition(Transition.PlayToPause, StateID.PauseID);
    }

    public override void DoBeforeEntering() { 
        TweenAlpha.Begin(play,timeAlpha,1);
    }

    public override void DoBeforeLeaving() {
       // TweenAlpha.Begin(play,timeAlpha,0);
    }

    protected override void Reason() {

    }

    public void ChangueToPause(){
        ManagerPause.Instance.Pause = true;
        fsm.PerformTransition(Transition.PlayToPause);
    }


}

