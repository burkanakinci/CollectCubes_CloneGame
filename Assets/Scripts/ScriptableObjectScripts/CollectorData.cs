using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "CollectorData", menuName = "Collector Data")]
public class CollectorData : ScriptableObject
{

    #region Datas
    [SerializeField] private float m_CollectorMovementSpeed = 5.0f;
    [SerializeField] private float m_CollectorRotationLerpValue = 5.0f;
    #endregion

    #region ExternalAccess
    public float CollectorMovementSpeed => m_CollectorMovementSpeed;
    public float CollectorRotationLerpValue => m_CollectorRotationLerpValue;
    #endregion
}
