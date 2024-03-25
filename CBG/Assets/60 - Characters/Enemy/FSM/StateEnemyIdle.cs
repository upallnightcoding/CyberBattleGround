using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyIdle : FiniteState
{
    public static string TITLE = "IDLE";

    private EnemyCntrl enemyCntrl = null;

    public StateEnemyIdle(EnemyCntrl enemyCntrl) : base(TITLE)
    {
        this.enemyCntrl = enemyCntrl;
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }

    public override string OnUpdate(float dt)
    {
        return (null);
    }
}
