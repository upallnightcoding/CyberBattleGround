using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStateIdle : FiniteState
{
    private static string TITLE = "Idle";

    private EnemiesCntrl enemyCntrl = null;

    public RobotStateIdle(EnemiesCntrl enemyCntrl) : base(TITLE)
    {
        this.enemyCntrl = enemyCntrl;
    }

    public override void OnEnter()
    {
        enemyCntrl.SetSpeed(0.0f);
    }

    public override string OnUpdate(float dt)
    {
        return (null);
    }

    public override void OnExit()
    {
        
    }
}
