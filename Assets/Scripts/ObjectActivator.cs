using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObjectActivator : MonoBehaviour
{
    [SerializeField] GameObject[] m_objects;
    [SerializeField] bool m_activate = true;

    private void OnTriggerEnter(Collider other)
    {
        foreach (var o in m_objects)
        {
            if (o)
            {
                o.SetActive(m_activate);
            }
        }
    }
}
