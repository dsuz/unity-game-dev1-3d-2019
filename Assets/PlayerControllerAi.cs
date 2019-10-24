using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // Navmesh Agent を使うために必要

public class PlayerControllerAi : MonoBehaviour
{
    [SerializeField] Transform m_target;
    NavMeshAgent m_agent;
    Vector3 m_cachedTargetPosition;

    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_cachedTargetPosition = m_target.position;
        m_agent.SetDestination(m_cachedTargetPosition);
    }
    
    void Update()
    {
        if (Vector3.Distance(m_cachedTargetPosition, m_target.position) > 0.1f)
        {
            m_cachedTargetPosition = m_target.position;
            m_agent.SetDestination(m_cachedTargetPosition);
        }
    }
}
