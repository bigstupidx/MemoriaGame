//
// FSMState.cs
//
// Author:
//       Luis Alejandro Vieira <lavz24@gmail.com>
//
// Copyright (c) 2014 
//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// This class represents the States in the Finite State System.
/// Each state has a Dictionary with pairs (transition-state) showing
/// which state the FSM should be if a transition is fired while this state
/// is the current state.
/// Method Reason is used to determine which transition should be fired .
/// Method Act has the code to perform the actions the NPC is supposed do if it's on this state.
/// </summary>
public class FSMState : MonoBehaviour
{
	private Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
	protected StateID stateID;
	protected FSMSystem fsm;

	public StateID ID { get { return stateID; } }
	
	public void AddTransition(Transition trans, StateID id)
	{
		// Check if anyone of the args is invalid
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
			return;
		}
		
		if (id == StateID.NullStateID)
		{
			Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
			return;
		}
		
		// Since this is a Deterministic FSM,
		//   check if the current transition was already inside the map
		if (map.ContainsKey(trans))
		{
			Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() + 
			               "Impossible to assign to another state");
			return;
		}
		
		map.Add(trans, id);
	}
	
	/// <summary>
	/// This method deletes a pair transition-state from this state's map.
	/// If the transition was not inside the state's map, an ERROR message is printed.
	/// </summary>
	public void DeleteTransition(Transition trans)
	{
		// Check for NullTransition
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("FSMState ERROR: NullTransition is not allowed");
			return;
		}
		
		// Check if the pair is inside the map before deleting
		if (map.ContainsKey(trans))
		{
			map.Remove(trans);
			return;
		}
		Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() + 
		               " was not on the state's transition list");
	}
	
	/// <summary>
	/// This method returns the new state the FSM should be if
	///    this state receives a transition and 
	/// </summary>
	public StateID GetOutputState(Transition trans)
	{
		// Check if the map has this transition
		if (map.ContainsKey(trans))
		{
			return map[trans];
		}
		return StateID.NullStateID;
	}
	
	/// <summary>
	/// This method is used to set up the State condition before entering it.
	/// It is called automatically by the FSMSystem class before assigning it
	/// to the current state.
	/// </summary>
	public virtual void DoBeforeEntering() { }
	
	/// <summary>
	/// This method is used to make anything necessary, as reseting variables
	/// before the FSMSystem changes to another one. It is called automatically
	/// by the FSMSystem before changing to a new state.
	/// </summary>
	public virtual void DoBeforeLeaving() { } 

	/// <summary>
	/// This method is used to check transition for changue states
	/// </summary>
	protected virtual void Reason() { }
	/// <summary>
	/// This method is used to a normal update
	/// </summary>
	protected virtual void UpdateChild() { }
    /// <summary>
    /// thu methos is used to a lateupdate
    /// </summary>
    protected virtual void LateUpdateChild() { }
    /// <summary>
    /// this method is used to a fixedupdate
    /// </summary>
	protected virtual void FixedUpdateChild() { }

	protected virtual void Awake(){
#if UNITY_PSM
		if(map == null)
			map = new Dictionary<Transition, StateID>();
#endif
		fsm = GetComponent<FSMSystem>();
	}
	
    void Update(){
		Reason();
		UpdateChild();
	}
    void LateUpdate()
    {
        LateUpdateChild();
    }
	void FixedUpdate(){
		FixedUpdateChild();
	}




	
} // class FSMState
