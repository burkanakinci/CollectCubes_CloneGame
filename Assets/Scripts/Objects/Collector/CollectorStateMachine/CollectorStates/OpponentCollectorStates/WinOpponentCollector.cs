using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOpponentCollector : IState
{
    private OpponentCollector m_Collector;
    public WinOpponentCollector(OpponentCollector _collector)
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
