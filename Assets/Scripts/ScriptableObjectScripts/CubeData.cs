using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;


[CreateAssetMenu(fileName = "CubeData", menuName = "Cube Data")]
public class CubeData : ScriptableObject
{

    #region Datas
    [SerializeField] private float m_ChangeCubeColorTweenDuration;
    [SerializeField] private Ease m_ChangeCubeColorTweenEase;
    [SerializeField] private float m_CollectedTweenDuration;
    [SerializeField] private Color m_CollectedColor;
    [SerializeField] private Ease m_CollectedTweenEase;
    [SerializeField] private float m_CollectedJumpPower;
    [SerializeField] private float m_ExitCollectedDuration;
    #endregion

    #region ExternalAccess
    public float ChangeCubeColorTweenDuration => m_ChangeCubeColorTweenDuration;
    public Ease ChangeCubeColorTweenEase => m_ChangeCubeColorTweenEase;
    public float CollectedTweenDuration => m_CollectedTweenDuration;
    public Color CollectedColor => m_CollectedColor;
    public Ease CollectedTweenEase => m_CollectedTweenEase;
    public float CollectedJumpPower => m_CollectedJumpPower;
    public float ExitCollectedDuration=>m_ExitCollectedDuration;
    #endregion
}
