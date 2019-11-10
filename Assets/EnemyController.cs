using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵を制御するコンポーネント
/// </summary>
[RequireComponent(typeof(NavMeshAgent), typeof(Collider))]
public class EnemyController : MonoBehaviour
{
    /// <summary>NavMeshAgent で追いかける対象</summary>
    [SerializeField] Transform m_target;
    /// <summary>target を追跡する間隔（秒）</summary>
    [SerializeField] float m_followTargetInterval = 1f;
    /// <summary>制御する Animator</summary>
    [SerializeField] Animator m_anim;
    /// <summary>やられてから画面から消えるまでの秒数</summary>
    [SerializeField] float m_expirationPeriodAfterDeath = 5f;
    NavMeshAgent m_agent;
    float m_timer;


    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();

        // target が設定されていなければプレイヤーを target にする
        if (m_target == null)
        {
            m_target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // ターゲットを追いかける
        if (m_target)
        {
            m_agent.SetDestination(m_target.position);
        }
        else
        {
            // ターゲットが何もない時はエラーを出力する
            Debug.LogError("Target is null!");
        }
    }

    void Update()
    {
        // 一定時間おきに目標を設定し直す
        m_timer += Time.deltaTime;

        if (m_timer > m_followTargetInterval)
        {
            m_timer = 0;
            m_agent.SetDestination(m_target.position);
        }

        // アニメーターの Speed パラメータに移動速度をセットする
        if (m_anim)
        {
            m_anim.SetFloat("Speed", m_agent.velocity.magnitude);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 攻撃されたら
        if (other.gameObject.tag == "Attack")
        {
            m_agent.isStopped = true;   // 追跡を辞める
            GetComponent<Collider>().enabled = false;   // 当たり判定は消す

            // やられモーションに移行する
            if (m_anim)
            {
                m_anim.applyRootMotion = true;  // やられモーションは root motion を使うのでここで有効にする
                m_anim.SetTrigger("Dead");
            }

            // 一定時間後にオブジェクトを破棄する
            Destroy(this.gameObject, m_expirationPeriodAfterDeath);
        }
    }
}
