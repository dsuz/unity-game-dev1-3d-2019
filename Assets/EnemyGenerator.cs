using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵を生成するコンポーネント
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    /// <summary>生成する敵の元となる GameObject</summary>
    [SerializeField] GameObject m_enemyPrefab;
    /// <summary>敵の出現ポイント</summary>
    [SerializeField] Transform m_spawnPoint;
    /// <summary>敵を生成する間隔（秒）</summary>
    [SerializeField] float m_generateInterval = 3f;
    float m_timer;

    void Update()
    {
        // 一定時間ごとに敵を生成する
        m_timer += Time.deltaTime;

        if (m_timer > m_generateInterval)
        {
            m_timer = 0f;
            var go = Instantiate(m_enemyPrefab, m_spawnPoint.position, m_enemyPrefab.transform.rotation);

            // オブジェクトが無効になっていたら有効にする
            if (!go.activeSelf)
            {
                go.SetActive(true);
            }
        }
    }
}
