using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : AbstractStateBehavior<PlayerState>
{
    void Start()
    {
        ChangeState(PlayerState.Idle); 
    }
}
