using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entities : CustomBehaviour
{
    public Transform PixelArtCubesParent;
    public OpponentCollector OpponentCollector;
    public PlayerCollector PlayerCollector;
    public GameObject PlayerCollectMagnet;
    public GameObject OpponentCollectMagnet;
    public TextMeshPro OpponentCollectedCount, PlayerCollectedCount;
    public override void Initialize()
    {
        PlayerCollector.Initialize();
        OpponentCollector.Initialize();

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnGameStart += OnGameStart;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;
    }

    private void SetOpponentVisibility()
    {
        OpponentCollector.gameObject.SetActive(GameManager.Instance.LevelManager.LevelData.UseOpponent);
        OpponentCollectMagnet.SetActive(GameManager.Instance.LevelManager.LevelData.UseOpponent);
        OpponentCollectedCount.gameObject.SetActive(GameManager.Instance.LevelManager.LevelData.UseOpponent);
    }

    public void SetCollectedTexts(int _playerCollectedCount,int _opponentCollectedCount)
    {
        OpponentCollectedCount.text = _opponentCollectedCount.ToString();
        PlayerCollectedCount.text = _playerCollectedCount.ToString();
    }

    #region Events
    private void OnResetToMainMenu()
    {
        OpponentCollectedCount.text = "0";
        PlayerCollectedCount.text = "0";
        SetOpponentVisibility();
        PlayerCollectMagnet.SetActive(false);
    }
    private void OnGameStart()
    {
        PlayerCollectMagnet.SetActive(true);
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
        GameManager.Instance.OnGameStart -= OnGameStart;
        GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;
        GameManager.Instance.OnLevelFailed -= OnLevelFailed;
    }

    #endregion
}