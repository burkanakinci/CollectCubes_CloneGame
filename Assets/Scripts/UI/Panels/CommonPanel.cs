using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommonPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI m_TimeCounterText;

    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnGameStart += OnGameStart;
        GameManager.Instance.OnLevelCompleted += OnLevelCompleted;
        GameManager.Instance.OnLevelFailed += OnLevelFailed;
    }

    private float m_TempPassingSecond;
    private bool m_UseTimer, m_GameStart;
    private void Update()
    {
        if (m_UseTimer && m_GameStart)
        {
            m_TempPassingSecond += Time.deltaTime;
            if (m_TempPassingSecond >= 1.0f)
            {
                m_TempPassingSecond -= 1.0f;
                m_TotalTimeCount--;
                if (m_TotalTimeCount <= 0)
                {
                    GameManager.Instance.LevelCompleted();
                    return;
                }
                SetTimeText();
            }
        }
    }
    private void SetTimeText()
    {
        m_TimeMinute = m_TotalTimeCount / 60;
        m_TimeSecond = m_TotalTimeCount - (m_TimeMinute * 60);
        m_TimeCounterText.text = m_TimeMinute + " : " + m_TimeSecond;
    }
    private int m_TimeSecond, m_TimeMinute, m_TotalTimeCount;
    #region Events
    private void OnResetToMainMenu()
    {
        base.ShowPanel();
        m_TempPassingSecond = 0.0f;
        m_GameStart = false;
        m_UseTimer = GameManager.Instance.LevelManager.LevelData.UseTimer;
        if (m_UseTimer)
        {
            m_TotalTimeCount = GameManager.Instance.LevelManager.LevelData.TimeSecondCount;
            m_TimeCounterText.gameObject.SetActive(true);

            SetTimeText();
        }
        else
        {
            m_TimeCounterText.gameObject.SetActive(false);
        }
    }

    private void OnGameStart()
    {
        m_GameStart = true;
    }
    private void OnLevelFailed()
    {
        m_UseTimer = false;
        base.HidePanel();
    }
    private void OnLevelCompleted()
    {
        m_UseTimer = false;
        base.HidePanel();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnGameStart -= OnGameStart;
        GameManager.Instance.OnLevelCompleted -= OnLevelCompleted;
        GameManager.Instance.OnLevelFailed -= OnLevelFailed;
    }
    #endregion
}
