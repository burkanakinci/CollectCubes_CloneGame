using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleOpponentCollector : IState
{
    private OpponentCollector m_Collector;
    public IdleOpponentCollector(OpponentCollector _collector)
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
