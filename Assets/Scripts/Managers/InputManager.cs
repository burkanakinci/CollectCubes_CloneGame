using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputManager : CustomBehaviour
{
    #region Fields
    [SerializeField] private float m_MinimumSwipeDistanceInViewportPoint;
    private Vector2 m_FirstMousePos;
    private Vector2 m_MouseChange;
    private Vector2 m_SwipeValue;
    private bool m_IsUIOverride;
    private float m_ScreenWidth;
    #endregion

    #region Actions
    public event Action<Vector2> OnSwiped;
    #endregion

    public override void Initialize()
    {
        m_ScreenWidth = Screen.width;
        m_SwipeValue = Vector2.zero;

        GameManager.Instance.OnResetToMainMenu += OnResetToMainMenu;
        GameManager.Instance.OnGameStart += OnStartGame;
    }
#if UNITY_EDITOR
    private void Update()
    {
        UpdateUIOverride();
        UpdateInput();
    }

    public void UpdateInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!m_IsUIOverride)
            {
                TouchControlsDown();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (!m_IsUIOverride)
            {
                TouchControls();

                OnSwiped?.Invoke(m_SwipeValue);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (!m_IsUIOverride)
            {
                TouchControlsUp();

                OnSwiped?.Invoke(m_SwipeValue);
            }
        }

    }

    public void UpdateUIOverride()
    {
        m_IsUIOverride = EventSystem.current.IsPointerOverGameObject(0);
    }
    public void TouchControlsDown()
    {
        m_FirstMousePos = Input.mousePosition;
    }
    public void TouchControls()
    {

        m_SwipeValue = Vector2.zero;

        m_MouseChange.x = Input.mousePosition.x - m_FirstMousePos.x;
        m_MouseChange.y = Input.mousePosition.y - m_FirstMousePos.y;

        if ((Mathf.Abs(m_MouseChange.x) > m_MinimumSwipeDistanceInViewportPoint) ||
        ((Mathf.Abs(m_MouseChange.y) > m_MinimumSwipeDistanceInViewportPoint)))
        {
            m_SwipeValue = (m_MouseChange / m_ScreenWidth);
            m_FirstMousePos = Input.mousePosition;
        }

    }
    public void TouchControlsUp()
    {
        m_SwipeValue = Vector2.zero;
        m_FirstMousePos = Input.mousePosition;
    }

#endif

    private void OnStartSwiped(Vector2 _swipeValue)
    {
        GameManager.Instance.GameStart();
    }

    #region Events
    private void OnStartGame()
    {
        OnSwiped -= OnStartSwiped;
    }

    private void OnResetToMainMenu()
    {
        OnSwiped += OnStartSwiped;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnResetToMainMenu -= OnResetToMainMenu;
        GameManager.Instance.OnGameStart -= OnStartGame;
    }

    #endregion
}
