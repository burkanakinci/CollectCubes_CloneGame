using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cube : PooledObject
{
    [SerializeField] private Rigidbody m_CubeRB;
    [SerializeField] private CubeData m_CubeData;
    [SerializeField] private MeshRenderer m_CubeRenderer;
    private MaterialPropertyBlock m_CubeMPB;

    public override void Initialize()
    {
        base.Initialize();
        m_CubeMPB = new MaterialPropertyBlock();

        m_SetColorTweenID = GetInstanceID() + "m_SetColorTweenID";
        m_JumpTweenID = GetInstanceID() + "m_JumpTweenID";
    }

    public override void OnObjectSpawn()
    {
        m_CubeRB.velocity = Vector3.zero;
        m_CubeMPB.SetColor("_Color", Color.black);
        m_CubeRenderer.SetPropertyBlock(m_CubeMPB);
        ChangeCubeRBKinematic(false, true);
        this.gameObject.layer = (int)ObjectsLayer.Cube;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        KillAllTween();
        StopAllCoroutine();
        base.OnObjectDeactive();
    }

    private void ChangeCubeRBKinematic(bool _isKinematic, bool _useGravity)
    {
        m_CubeRB.isKinematic = _isKinematic;
        m_CubeRB.useGravity = _useGravity;
    }

    public void SetCubeColor(Color _color, bool _useTween = true)
    {
        if (_useTween)
        {
            m_TargetColor = _color;
            m_StartColor = m_CubeMPB.GetColor("_Color");
            StartCubeColorTween();
        }
        else
        {
            m_CubeMPB.SetColor("_Color", _color);
            m_CubeRenderer.SetPropertyBlock(m_CubeMPB);
        }
    }

    private string m_SetColorTweenID;
    private float m_ColorLerpValue;
    private Color m_TargetColor, m_StartColor;
    private void StartCubeColorTween()
    {
        m_ColorLerpValue = 0.0f;

        DOTween.Kill(m_SetColorTweenID);
        DOTween.To(() => m_ColorLerpValue, x => m_ColorLerpValue = x, 1.0f, m_CubeData.ChangeCubeColorTweenDuration).
                        OnUpdate(() => UpdateCubeColor()).
                        SetEase(m_CubeData.ChangeCubeColorTweenEase).
                        SetId(m_SetColorTweenID);
    }

    private Color m_TempCubeColor;
    private void UpdateCubeColor()
    {
        m_TempCubeColor = Color.Lerp(m_StartColor, m_TargetColor, m_ColorLerpValue);
        m_CubeMPB.SetColor("_Color", m_TempCubeColor);
        m_CubeRenderer.SetPropertyBlock(m_CubeMPB);
    }

    private string m_JumpTweenID;
    private Vector3 m_CollectedTargetPos;
    private void StartCollectedTween()
    {
        DOTween.Kill(m_JumpTweenID);

        transform.DOJump(m_CollectedTargetPos, m_CubeData.CollectedJumpPower, 1, m_CubeData.CollectedTweenDuration).
        OnComplete(() => CollectedTweenComplete()).
        SetEase(m_CubeData.CollectedTweenEase).
        SetId(m_JumpTweenID);
    }

    private void CollectedTweenComplete()
    {
        GameManager.Instance.LevelManager.IncreaseCollectedCubeCount(m_CollectedCollectorType);
        GameManager.Instance.ObjectPool.SpawnFromPool(
            (PooledObjectTags.CONFETTİ_VFX),
            (transform.position),
            (Quaternion.identity),
            null);
        OnObjectDeactive();
    }

    private Coroutine m_ExitCollectedCoroutine;
    private void StartCubeExitCollectedCoroutine()
    {
        if (m_ExitCollectedCoroutine != null)
        {
            StopCoroutine(m_ExitCollectedCoroutine);
        }

        m_ExitCollectedCoroutine = StartCoroutine(ExitCollectedCoroutine());
    }

    private IEnumerator ExitCollectedCoroutine()
    {
        yield return new WaitForSecondsRealtime(m_CubeData.ExitCollectedDuration);

        this.gameObject.layer = (int)ObjectsLayer.Cube;
    }

    private CollectorType m_CollectedCollectorType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.COLLECT_MAGNET_AREA))
        {
            this.gameObject.layer = (int)ObjectsLayer.Default;
            ChangeCubeRBKinematic(false, false);
            m_CollectedTargetPos = other.transform.position;
            SetCubeColor(m_CubeData.CollectedColor);
            GameManager.Instance.Entities.OpponentCollector.ManageCollectedCubeList(ListOperation.Subtraction, this);
            m_CollectedCollectorType = other.GetComponent<Magnet>().CollectorType;
            StartCollectedTween();
        }
        else if (other.CompareTag(ObjectTags.COLLECTED_CUBE_ENTER_TRIGGER))
        {
            if (other.transform.parent.CompareTag(ObjectTags.PLAYER_COLLECTOR))
            {
                this.gameObject.layer = (int)ObjectsLayer.PlayerCollectedCube;
            }
            else if (other.transform.parent.CompareTag(ObjectTags.OPPONENT_COLLECTOR))
            {
                GameManager.Instance.Entities.OpponentCollector.ManageCollectedCubeList(ListOperation.Adding, this);
                this.gameObject.layer = (int)ObjectsLayer.OpponentCollectedCube;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ObjectTags.COLLECTED_CUBE_EXIT_TRIGGER))
        {
            if (other.transform.parent.CompareTag(ObjectTags.OPPONENT_COLLECTOR))
            {
                GameManager.Instance.Entities.OpponentCollector.ManageCollectedCubeList(ListOperation.Subtraction, this);
            }
            StartCubeExitCollectedCoroutine();
        }
    }

    private void KillAllTween()
    {
        DOTween.Kill(m_SetColorTweenID);
        DOTween.Kill(m_JumpTweenID);
    }

    private void StopAllCoroutine()
    {
        if (m_ExitCollectedCoroutine != null)
        {
            StopCoroutine(m_ExitCollectedCoroutine);
        }
    }
}
