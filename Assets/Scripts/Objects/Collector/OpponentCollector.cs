using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCollector : Collector
{
    public Transform CubeHitTransform;
    private List<Cube> m_CollectedCubes;
    public int CollectedCubesCount => m_CollectedCubes.Count;
    public override void Initialize()
    {
        base.Initialize();

        m_CollectedCubes = new List<Cube>();

        List<IState> playerCollectorStates = new List<IState>();
        playerCollectorStates.Add(new IdleOpponentCollector(this));
        playerCollectorStates.Add(new CollectOpponentCollector(this));
        playerCollectorStates.Add(new PourOpponentCollector(this));
        playerCollectorStates.Add(new WinOpponentCollector(this));
        playerCollectorStates.Add(new FailOpponentCollector(this));
        CollectorStateMachine = new CollectorStateMachine(this, playerCollectorStates);

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnGameStart += OnStartGame;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;
    }
    private void Update()
    {
        CollectorStateMachine.LogicalUpdate();
    }
    private void FixedUpdate()
    {
        CollectorStateMachine.PhysicalUpdate();
    }

    public void ManageCollectedCubeList(ListOperation _operation, Cube _cube)
    {

        if (_operation == ListOperation.Adding)
        {
            if (!m_CollectedCubes.Contains(_cube))
            {
                m_CollectedCubes.Add(_cube);
            }
        }
        else if (_operation == ListOperation.Subtraction)
        {
            if (m_CollectedCubes.Contains(_cube))
            {
                m_CollectedCubes.Remove(_cube);
            }
        }
    }
    #region Events
    private void OnResetToMainMenu()
    {
        ResetCollector();
        CollectorStateMachine.ChangeState((int)OpponentCollectorStates.IdleCollectorState);
        m_CollectedCubes.Clear();
    }

    private void OnStartGame()
    {

        CollectorStateMachine.ChangeState((int)OpponentCollectorStates.CollectOpponentCollector);
    }

    private void OnLevelFailed()
    {
        CollectorStateMachine.ChangeState((int)OpponentCollectorStates.FailCollectorState);
    }

    private void OnLevelCompleted()
    {
        CollectorStateMachine.ChangeState((int)OpponentCollectorStates.WinCollectorState);
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
