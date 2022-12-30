using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooledObjects : PooledObject
{
    [SerializeField] private ParticleSystem m_PlayedParticle;

    public override void Initialize()
    {

    }
    public override void OnObjectSpawn()
    {
        m_PlayedParticle.Play();
        StartParticleIsAliveCoroutine();
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        m_PlayedParticle.Stop();
        base.OnObjectDeactive();
    }

    private Coroutine m_StartParticleIsAliveCoroutine;
    private void StartParticleIsAliveCoroutine()
    {
        if (m_StartParticleIsAliveCoroutine != null)
        {
            StopCoroutine(m_StartParticleIsAliveCoroutine);
        }

        m_StartParticleIsAliveCoroutine = StartCoroutine(ParticleIsAlive());
    }
    private IEnumerator ParticleIsAlive()
    {
        yield return new WaitUntil(() => (!m_PlayedParticle.IsAlive()));
        OnObjectDeactive();
    }
}
