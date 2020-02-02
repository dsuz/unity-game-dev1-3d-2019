using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 光をゆらめかせるコンポーネント
/// </summary>
[RequireComponent(typeof(Light))]
public class LightFlickController : MonoBehaviour
{
    [SerializeField] bool m_flickIntensity = true;
    [SerializeField] bool m_flickRange = true;

    private Light m_light;
    float m_defaultRange;
    float m_defaultIntensity;

    private void Start()
    {
        m_light = GetComponent<Light>();
        m_defaultRange = m_light.range;
        m_defaultIntensity = m_light.intensity;
    }

    private void Update()
    {
        if (m_flickRange)
        {
            float a = Mathf.PerlinNoise(Time.time * 0.1f, 0) * m_defaultRange;
            float b = Mathf.PerlinNoise(Time.time * 0.9f, 0) * m_defaultRange / 2;
            m_light.range = a + b;
        }

        if (m_flickIntensity)
        {
            float c = Mathf.PerlinNoise(Time.time * 0.1f, 0) * m_defaultIntensity;
            float d = Mathf.PerlinNoise(Time.time * 0.9f, 0) * m_defaultIntensity / 2;
            m_light.intensity = c + d;
        }
    }
}
