using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Collider m_attackRange;

    void Start()
    {
        if (m_attackRange.gameObject.activeSelf)
        {
            AttackEnd();
        }
    }

    void AttackBegin()
    {
        m_attackRange.gameObject.SetActive(true);
    }

    void AttackEnd()
    {
        m_attackRange.gameObject.SetActive(false);
    }
}
