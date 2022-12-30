using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{

    #region Datas
    public List<Pixel> CubeColors;
    public bool UseTimer;
    public int TimeSecondCount;
    public bool UseOpponent;
    public List<Vector3> ObstaclePositions;
    public int ImageWidth;
    #endregion
}
