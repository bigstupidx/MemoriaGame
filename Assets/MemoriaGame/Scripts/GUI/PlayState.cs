//
//  PlayState.cs
//
//  Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
using UnityEngine;
using System.Collections;


public class PlayState : FSMState
{

    public Animator play;
    public float timeAlpha = 1.0f;


    protected override void Awake ()
    {
        base.Awake ();
        stateID = StateID.PlayID;
        AddTransition (Transition.PlayToPause, StateID.PauseID);
    }

    public  void Start ()
    {
        play.SetBool ("In", true);

    }

    public override void DoBeforeEntering ()
    { 
        play.SetBool ("In", true);
        // TweenAlpha_2.Begin (play, timeAlpha, 1);
    }

    public override void DoBeforeLeaving ()
    {
        play.SetBool ("In", false);
    }

    protected override void Reason ()
    {

    }

    public void ChangueToPause ()
    {
        ManagerPause.Instance.Pause = true;
        fsm.PerformTransition (Transition.PlayToPause);
    }


}

