using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOpponentCollector : IState
{
    private OpponentCollector m_Collector;
    public CollectOpponentCollector(OpponentCollector _collector)
    {
        m_Collector = _collector;
    }

    private Vector3 m_TargetPos;
    private bool m_CanMovement;
    public void Enter()
    {
        m_CanMovement = false;

    }
    public void LogicalUpdate()
    {
        if (m_CanMovement)
        {
            m_Collector.SetCollectorRotationValue();

            if (m_Collector.CollectedCubesCount > 0)
            {
                m_Collector.CollectorStateMachine.ChangeState((int)OpponentCollectorStates.PourOpponentCollector);
            }
        }
    }
    public void PhysicalUpdate()
    {

        if (!m_CanMovement)
        {
            SetTargetPosition();
        }
        else
        {
            m_Collector.SetCollectorVelocity();
        }

    }
    public void Exit()
    {
    }

    private RaycastHit m_TargetCubeHit;
    private int m_CubeLayerMask = 1 << (int)ObjectsLayer.Cube;
    private void SetTargetPosition()
    {
        m_Collector.CubeHitTransform.eulerAngles = new Vector3(0.0f, Random.Range(90.0f, 270.0f), 0.0f);


        if (Physics.Raycast(m_Collector.CubeHitTransform.position, m_Collector.CubeHitTransform.forward, out m_TargetCubeHit, 55.0f, m_CubeLayerMask))
        {
            Debug.DrawRay(m_Collector.CubeHitTransform.position, m_Collector.CubeHitTransform.forward * 55.0f, Color.yellow);
            m_TargetPos = m_TargetCubeHit.transform.position;
            m_Collector.SetCollectorTargetPosition(m_TargetPos);
            m_CanMovement = true;

        }
        else
        {
            Debug.DrawRay(m_Collector.CubeHitTransform.position, m_Collector.CubeHitTransform.forward * 55.0f, Color.yellow);
        }
    }
}
