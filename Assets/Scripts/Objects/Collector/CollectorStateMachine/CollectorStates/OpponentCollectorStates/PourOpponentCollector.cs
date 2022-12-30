using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourOpponentCollector : IState
{
    private OpponentCollector m_Collector;
    public PourOpponentCollector(OpponentCollector _collector)
    {
        m_Collector = _collector;
    }

    public void Enter()
    {
        m_Collector.SetCollectorTargetPosition(GameManager.Instance.Entities.OpponentCollectMagnet.transform.position);
    }
    public void LogicalUpdate()
    {
        m_Collector.SetCollectorRotationValue();
        if (m_Collector.CollectedCubesCount == 0)
        {
            m_Collector.CollectorStateMachine.ChangeState((int)OpponentCollectorStates.CollectOpponentCollector);
        }
    }
    public void PhysicalUpdate()
    {
        m_Collector.SetCollectorVelocity();
    }
    public void Exit()
    {

    }
}
