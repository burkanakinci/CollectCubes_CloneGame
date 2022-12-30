using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlayerCollectorState : IState
{
    private PlayerCollector m_Collector;
    public RunPlayerCollectorState(PlayerCollector _collector)
    {
        m_Collector = _collector;
    }

    public void Enter()
    {
        GameManager.Instance.InputManager.OnSwiped += UpdateSwipeValue;
    }
    public void LogicalUpdate()
    {
    }
    public void PhysicalUpdate()
    {
        m_Collector.SetCollectorVelocity();

    }
    public void Exit()
    {
        GameManager.Instance.InputManager.OnSwiped -= UpdateSwipeValue;
    }

    private Vector3 m_CollectorLookPos;
    private void UpdateSwipeValue(Vector2 _swipeValue)
    {
        m_CollectorLookPos = (m_Collector.transform.position) +
        (Vector3.right * _swipeValue.x) +
        (Vector3.forward * _swipeValue.y);

        m_Collector.SetCollectorTargetPosition(m_CollectorLookPos);
        m_Collector.SetCollectorRotationValue();
    }
}
