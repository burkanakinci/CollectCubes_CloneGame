using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : CustomBehaviour, IPooledObject
{
    private string m_PooledTag;
    private int m_ObjectLayer;
    public void SetPooledTag(string _pooledTag)
    {
        m_PooledTag = _pooledTag;
        m_ObjectLayer = this.gameObject.layer;
    }
    public override void Initialize()
    {

    }
    public virtual void OnObjectSpawn()
    {
        this.gameObject.layer = m_ObjectLayer;
        GameManager.Instance.LevelManager.OnCleanSceneObject += OnObjectDeactive;
    }
    public virtual void OnObjectDeactive()
    {
        GameManager.Instance.LevelManager.OnCleanSceneObject -= OnObjectDeactive;
        this.transform.SetParent(null);
        GameManager.Instance.ObjectPool.AddObjectPool(m_PooledTag, this);
        this.gameObject.SetActive(false);
    }
    public CustomBehaviour GetGameObject()
    {
        return this;
    }
}
