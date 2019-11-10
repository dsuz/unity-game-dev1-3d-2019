using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 三角関数でオブジェクトを動かすコンポーネント
/// </summary>
public class MovingObjectController : MonoBehaviour
{
    /// <summary>振り幅(x)</summary>
    public float m_amplitude_x = 1f;
    /// <summary>振り幅(y)</summary>
    public float m_amplitude_y = 0f;
    /// <summary>振り幅(z)</summary>
    public float m_amplitude_z = 0f;
    /// <summary>動く速さ</summary>
    public float m_speed = 2.0f;
    private float m_timer;
    private Vector3 m_initialPosition;

    void Start()
    {
        m_initialPosition = transform.position;
    }

    void Update()
    {
        // オブジェクトを回す
        m_timer += Time.deltaTime * m_speed;
        float posX = Mathf.Sin(m_timer) * m_amplitude_x;
        float posY = Mathf.Sin(m_timer) * m_amplitude_y;
        float posZ = Mathf.Cos(m_timer) * m_amplitude_z;

        Vector3 pos = m_initialPosition;
        pos = pos + new Vector3(posX, posY, posZ);
        transform.position = pos;
    }
}
