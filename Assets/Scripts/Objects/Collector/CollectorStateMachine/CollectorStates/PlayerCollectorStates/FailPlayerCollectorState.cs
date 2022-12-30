using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailPlayerCollectorState : IState
{
    private PlayerCollector m_Collector;
    public FailPlayerCollectorState(PlayerCollector _collector)
    {
        m_Collector = _collector;
    }

    public void Enter()
    {
    }
    public void LogicalUpdate()
    {
    }
    public void PhysicalUpdate()
    {

    }
    public void Exit()
    {

    }
}
