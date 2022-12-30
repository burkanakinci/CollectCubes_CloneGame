using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlayerCollectorState : IState
{
    private PlayerCollector m_Collector;
    public WinPlayerCollectorState(PlayerCollector _collector)
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
