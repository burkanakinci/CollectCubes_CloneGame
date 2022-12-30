using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailOpponentCollector : IState
{
    private OpponentCollector m_Collector;
    public FailOpponentCollector(OpponentCollector _collector)
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
