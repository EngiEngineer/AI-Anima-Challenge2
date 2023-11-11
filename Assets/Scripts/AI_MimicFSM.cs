using Assets.SimpleFSM.Demo.Scripts.States.Idle;
using Assets.SimpleFSM.Demo.Scripts.States.Patrol;
using RobustFSM.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_MimicFSM : MonoFSM<AI_Mimic>
{
    public override void AddStates()
    {
        //add the states
        AddState<IdleMainState>();
        AddState<PatrolMainState>();
        AddState<AttackState>();

        //set the initial state
        SetInitialState<AttackState>();
    }
}
