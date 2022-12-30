using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : CustomBehaviour
{
    #region Fields
    [SerializeField] protected Rigidbody m_CollectorRB;
    [SerializeField] protected CollectorData m_CollectorData;
    protected Vector3 m_StartPosition;
    protected Quaternion m_StartRotation;
    public CollectorStateMachine CollectorStateMachine;
    #endregion
    public override void Initialize()
    {
        m_StartPosition = transform.position;
        m_StartRotation = transform.rotation;
    }

    private Quaternion m_CollectorRotation;
    private Vector3 m_TempLookPos;
    private Vector3 m_TempVelocity;
    public void SetCollectorTargetPosition(Vector3 _targetPos)
    {
        m_TempVelocity = _targetPos - transform.position;
        m_TempVelocity = (m_TempVelocity.normalized * m_CollectorData.CollectorMovementSpeed);
    }
    public void SetCollectorRotationValue()
    {
        m_TempLookPos = m_CollectorRB.velocity;
        m_TempLookPos.y = 0.0f;

        if (m_TempLookPos != Vector3.zero)
        {
            m_CollectorRotation = Quaternion.LookRotation(m_TempLookPos);
        }
        transform.rotation = Quaternion.Slerp(
            (transform.rotation),
            m_CollectorRotation,
            (m_CollectorData.CollectorRotationLerpValue * Time.deltaTime)
            );

    }
    public void SetCollectorVelocity()
    {
        m_CollectorRB.velocity = m_TempVelocity;
    }

    protected void ResetCollector()
    {
        m_CollectorRB.velocity = Vector3.zero;
        transform.position = m_StartPosition;
        transform.rotation = m_StartRotation;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.OBSTACLE))
        {
            ResetCollector();
        }
    }
}
