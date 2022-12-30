using UnityEngine;
using System.Collections.Generic;

public class CollectorStateMachine
{
    private IState m_CurrentState;
    private List<IState> m_States;

    public CollectorStateMachine(Collector _collector, List<IState> _states)
    {
        m_States = new List<IState>();
        m_States.AddRange(_states);
    }

    public void LogicalUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.LogicalUpdate();
        }
    }
    public void PhysicalUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.PhysicalUpdate();
        }
    }
    public void ChangeState(int _state, bool _changeForce = false)
    {
        if (m_States[(int)_state] != m_CurrentState || _changeForce)
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.Exit();
            }

            m_CurrentState = m_States[(int)_state];
            m_CurrentState.Enter();
        }
    }

    protected bool EqualCurrentState(int _state)
    {
        return (m_CurrentState == m_States[_state]);
    }
}
