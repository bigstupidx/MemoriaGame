//
// FSMSystem.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// FSMSystem class represents the Finite State Machine class.
///  It has a List with the States the NPC has and methods to add,
///  delete a state, and to change the current state the Machine is on.
/// </summary>
public class FSMSystem :FSMState
{
	private List<FSMState> states =  new List<FSMState>();
	
	// The only way one can change the state of the FSM is by performing a transition
	// Don't change the CurrentState directly
	private StateID currentStateID;
	public StateID CurrentStateID { get { return currentStateID; } }
	private FSMState currentState;
	public FSMState CurrentState { get { return currentState; } }

    /// <summary>
    /// Get the state with the identifier id
    /// If the FSM doestn have the state, return null
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public FSMState GetState(StateID id)
    {
        foreach (FSMState state in states)
        {
            if (state.ID == id)
            {
                return state;
            }
        }
        return null;
    }
	/// <summary>
	/// This method places new states inside the FSM,
	/// or prints an ERROR message if the state was already inside the List.
	/// First state added is also the initial state.
	/// </summary>
	public void AddState(FSMState s)
	{
		// Check for Null reference before deleting
		if (s == null)
		{
			Debug.LogError("FSM ERROR: Null reference is not allowed");
		}
		
		// First State inserted is also the Initial state,
		//   the state the machine is in when the simulation begins
		if (states.Count == 0)
		{
			states.Add(s);
			if(InitialState == s.ID){
				currentState = s;
				currentStateID = s.ID;
				currentState.enabled = true;
			}else{
				s.enabled = false;
			}
			return;
		}
		
		// Add the state to the List if it's not inside it
		foreach (FSMState state in states)
		{
			if (state.ID == s.ID)
			{
				Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() + 
				               " because state has already been added");
				return;
			}
		}
		states.Add(s);

		if(InitialState == s.ID){
			currentState = s;
			currentStateID = s.ID;
			s.enabled = true;
		}else{
			s.enabled = false;
		}

	}
	
	/// <summary>
	/// This method delete a state from the FSM List if it exists, 
	///   or prints an ERROR message if the state was not on the List.
	/// </summary>
	public void DeleteState(StateID id)
	{
		// Check for NullState before deleting
		if (id == StateID.NullStateID)
		{
			Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
			return;
		}
		
		// Search the List and delete the state if it's inside it
		foreach (FSMState state in states)
		{
			if (state.ID == id)
			{
				state.enabled = false;
				states.Remove(state);
				return;
			}
		}
		Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() + 
		               ". It was not on the list of states");
	}
	
	/// <summary>
	/// This method tries to change the state the FSM is in based on
	/// the current state and the transition passed. If current state
	///  doesn't have a target state for the transition passed, 
	/// an ERROR message is printed.
	/// </summary>
	public void PerformTransition(Transition trans)
	{
		// Check for NullTransition before changing the current state
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
			return;
		}
		
		// Check if the currentState has the transition passed as argument
		StateID id = currentState.GetOutputState(trans);
		if (id == StateID.NullStateID)
		{
			Debug.LogError("FSM ERROR: State " + currentStateID.ToString() +  " does not have a target state " + 
			               " for transition " + trans.ToString());
			return;
		}
		
		// Update the currentStateID and currentState		
		currentStateID = id;
		foreach (FSMState state in states)
		{
			if (state.ID == currentStateID)
			{
				// Do the post processing of the state before setting the new one
				currentState.DoBeforeLeaving();
				currentState.enabled = false;

				currentState = state;
				
				// Reset the state to its desired condition before it can reason or act
				currentState.DoBeforeEntering();
				currentState.enabled = true;

				break;
			}
		}
		
	} // PerformTransition()

	#region Initialization_Unity:
	 
	public StateID InitialState = StateID.NullStateID;
	public bool AutomaticAddState = false;

	protected override void Awake(){
		base.Awake();
#if UNITY_PSM
		if(states == null)
			states =  new List<FSMState>();
#endif
	}
	protected virtual void Start(){
		if(AutomaticAddState){
			FSMState[] fsmStates =  GetComponents<FSMState>();

			foreach (FSMState i in fsmStates ){
				if(i.GetInstanceID() != GetInstanceID())
					AddState(i);
			}
		}
	}

	public override void DoBeforeEntering() {
		currentStateID = InitialState;
		foreach (FSMState state in states)
		{
			if (state.ID == currentStateID)
			{
				
				currentState = state;
				
				// Reset the state to its desired condition before it can reason or act
				currentState.DoBeforeEntering();
				currentState.enabled = true;
				
				break;
			}
		}
	}

	public override void DoBeforeLeaving() { 

		foreach (FSMState state in states)
		{
			if (state.ID == currentStateID)
			{
				// Do the post processing of the state before setting the new one
				currentState.DoBeforeLeaving();
				currentState.enabled = false;
				break;
			}
		}

	} 

	#endregion
	
} //class FSMSystem