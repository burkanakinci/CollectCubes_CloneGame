using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI m_LevelText;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        
        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnGameStart += OnGameStart;
    }
    private void OnResetToMainMenu()
    {
    }
    private void OnGameStart()
    {
        ShowPanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        m_LevelText.text = "Level : " + GameManager.Instance.PlayerManager.GetLevelNumber().ToString();
    }

    private void OnDestroy()
    {
    }

}
