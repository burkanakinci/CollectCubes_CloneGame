using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : Collector
{
    public override void Initialize()
    {
        base.Initialize();

        List<IState> playerCollectorStates = new List<IState>();
        playerCollectorStates.Add(new IdlePlayerCollectorState(this));
        playerCollectorStates.Add(new RunPlayerCollectorState(this));
        playerCollectorStates.Add(new WinPlayerCollectorState(this));
        playerCollectorStates.Add(new FailPlayerCollectorState(this));
        CollectorStateMachine = new CollectorStateMachine(this, playerCollectorStates);

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnGameStart += OnStartGame;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;
    }
    private void FixedUpdate()
    {
        CollectorStateMachine.PhysicalUpdate();
    }
    #region Events
    private void OnResetToMainMenu()
    {
        ResetCollector();
        CollectorStateMachine.ChangeState((int)PlayerCollectorStates.IdleCollectorState);
    }

    private void OnStartGame()
    {
        CollectorStateMachine.ChangeState((int)PlayerCollectorStates.RunCollectorState);
    }

    private void OnLevelFailed()
    {
        CollectorStateMachine.ChangeState((int)PlayerCollectorStates.FailCollectorState);
    }

    private void OnLevelCompleted()
    {
        CollectorStateMachine.ChangeState((int)PlayerCollectorStates.WinCollectorState);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnGameStart -= OnStartGame;
        GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;
        GameManager.Instance.OnLevelFailed -= OnLevelFailed;
    }
    #endregion


}
