using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion2 : AbstractStateBehavior<CompanionState>
{
    void Start()
    {
        ChangeState(CompanionState.Idle); 
    }
}
