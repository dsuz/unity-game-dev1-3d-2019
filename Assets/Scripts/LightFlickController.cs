using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 光をゆらめかせるコンポーネント
/// </summary>
[RequireComponent(typeof(Light))]
public class LightFlickController : MonoBehaviour
{
    private Light m_light;
    float m_defaultRange;

    private void Start()
    {
        m_light = GetComponent<Light>();
        m_defaultRange = m_light.range;
    }

    private void Update()
    {
        float a = Mathf.PerlinNoise(Time.time * 0.1f, 0) * m_defaultRange;
        float b = Mathf.PerlinNoise(Time.time * 0.9f, 0) * m_defaultRange / 2;
        m_light.range = a + b;
    }
}
