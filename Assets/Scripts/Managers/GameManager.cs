using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{
    public static GameManager Instance { get; private set; }
    #region Fields
    public InputManager InputManager;
    public PlayerManager PlayerManager;
    public ObjectPool ObjectPool;
    public LevelManager LevelManager;
    public JsonConverter JsonConverter;
    public UIManager UIManager;
    public Entities Entities;
    #endregion

    #region Actions
    public event Action OnResetToMainMenu;
    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;
    public event Action OnGameStart;
    #endregion
    private void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Initialize();
    }
    public override void Initialize()
    {
        JsonConverter.Initialize();
        PlayerManager.Initialize();
        LevelManager.Initialize();
        ObjectPool.Initialize();
        UIManager.Initialize();
        InputManager.Initialize();
        Entities.Initialize();
    }
    private void Start()
    {
        ResetToMainMenu();
    }
    public void ResetToMainMenu()
    {
        OnResetToMainMenu?.Invoke();
    }
    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }
    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
    public void GameStart()
    {
        OnGameStart?.Invoke();
    }
}
