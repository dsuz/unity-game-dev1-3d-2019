using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトを生成するコンポーネント
/// </summary>
public class ObjectGenerator : MonoBehaviour
{
    /// <summary>生成する元となる GameObject</summary>
    [SerializeField] GameObject m_source;
    /// <summary>出現ポイント</summary>
    [SerializeField] Transform m_spawnPoint;
    /// <summary>生成する間隔（秒）</summary>
    [SerializeField] float m_generateInterval = 3f;
    [SerializeField] bool m_generateOnAwake = true;
    [SerializeField] int m_count;
    float m_timer;
    bool m_isActive;
    int m_counter;

    void Awake()
    {
        m_isActive = m_generateOnAwake;
        m_timer = m_generateInterval;
    }

    void Update()
    {
        if (!m_isActive) return;
        if (m_count > 0 && m_counter > m_count - 1)
        {
            m_isActive = false;
            m_counter = 0;
            return;
        }

        // 一定時間ごとに敵を生成する
        m_timer += Time.deltaTime;

        if (m_timer > m_generateInterval)
        {
            m_timer = 0f;
            var go = Instantiate(m_source, m_spawnPoint.position, m_spawnPoint.rotation);
            m_counter++;

            // オブジェクトが無効になっていたら有効にする
            if (!go.activeSelf)
            {
                go.SetActive(true);
            }
        }
    }

    public void StartGenerate()
    {
        m_isActive = true;
        m_timer = m_generateInterval;
    }

    public void Pause()
    {
        m_isActive = false;
    }

    public void Stop()
    {
        m_isActive = false;
        m_counter = 0;
    }
}
