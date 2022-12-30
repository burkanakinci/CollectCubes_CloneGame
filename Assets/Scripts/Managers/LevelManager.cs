
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : CustomBehaviour
{
    #region Fields
    private LevelData m_LevelData;
    private int m_CurrentLevelNumber;
    private int m_ActiveLevelDataNumber;
    private int m_MaxLevelDataCount;
    private int m_TotalCubeCount, m_PlayerCollectedCubeCount, m_OpponentCollectedCubeCount;
    #endregion

    #region ExternalAccess
    public LevelData LevelData => m_LevelData;
    #endregion

    #region Actions
    public event Action OnCleanSceneObject;
    #endregion


    public override void Initialize()
    {

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;

        m_MaxLevelDataCount = Resources.LoadAll("LevelData", typeof(LevelData)).Length;
    }

    private void GetLevelData()
    {
        m_ActiveLevelDataNumber = (m_CurrentLevelNumber <= m_MaxLevelDataCount) ? (m_CurrentLevelNumber) : ((int)(UnityEngine.Random.Range(1, (m_MaxLevelDataCount + 1))));
        m_LevelData = Resources.Load<LevelData>("LevelData/" + m_ActiveLevelDataNumber + "LevelData");
    }
    #region SpawnSceneObject

    private Cube m_TempSpawnedCube;
    private void SpawnSceneObjects()
    {
        m_TotalCubeCount = m_LevelData.CubeColors.Count;
        SpawnPixelArtCubes();
        SpawnObstacles();
    }

    private Vector3 m_TempImagePivot;
    private void SpawnPixelArtCubes()
    {
        m_TempImagePivot = (GameManager.Instance.Entities.PixelArtCubesParent.position) - ((Vector3.right * m_LevelData.ImageWidth / 2.0f + Vector3.forward * m_LevelData.ImageWidth / 2.0f));

        for (int _pixelCount = 0; _pixelCount < m_TotalCubeCount; _pixelCount++)
        {
            m_TempSpawnedCube = GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.CUBE),
                ((m_TempImagePivot) +
                    ((Vector3.right * m_LevelData.CubeColors[_pixelCount].PixelWidthIndex) +
                    (Vector3.forward * m_LevelData.CubeColors[_pixelCount].PixelHeightIndex))),
                (Quaternion.identity),
                (GameManager.Instance.Entities.PixelArtCubesParent)
            ).GetGameObject().GetComponent<Cube>();

            m_TempSpawnedCube.SetCubeColor(m_LevelData.CubeColors[_pixelCount].PixelColor);
        }

    }

    private void SpawnObstacles()
    {
        for (int _obstacleCount = 0; _obstacleCount < m_LevelData.ObstaclePositions.Count; _obstacleCount++)
        {
            GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.OBSTACLE),
                (m_LevelData.ObstaclePositions[_obstacleCount]),
                (Quaternion.identity),
                null
            );
        }
    }
    #endregion

    public void IncreaseCollectedCubeCount(CollectorType _collectorType)
    {
        if (_collectorType == CollectorType.PlayerCollector)
        {
            m_PlayerCollectedCubeCount++;
        }
        else if (_collectorType == CollectorType.OpponentCollector)
        {
            m_OpponentCollectedCubeCount++;
        }

        if ((m_PlayerCollectedCubeCount + m_OpponentCollectedCubeCount) >= m_TotalCubeCount)
        {
            GameManager.Instance.LevelCompleted();
        }
        GameManager.Instance.Entities.SetCollectedTexts(m_PlayerCollectedCubeCount, m_OpponentCollectedCubeCount);
    }

    #region Events
    private void OnResetToMainMenu()
    {
        OnCleanSceneObject?.Invoke();

        m_PlayerCollectedCubeCount = 0;
        m_OpponentCollectedCubeCount = 0;

        m_CurrentLevelNumber = GameManager.Instance.PlayerManager.GetLevelNumber();

        GetLevelData();
        SpawnSceneObjects();
    }
    private void OnLevelCompleted()
    {
    }
    private void OnLevelFailed()
    {
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;
        GameManager.Instance.OnLevelFailed -= OnLevelFailed;
    }
    #endregion
}
