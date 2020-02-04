using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃のコライダー（トリガー）の有効/無効を切り替える
/// Animator と同じ GameObject に追加し、Animation Event から関数を呼び出すことを前提に作られている。
/// </summary>
[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour
{
    /// <summary>攻撃範囲のコライダー</summary>
    [SerializeField] Collider[] m_attackRangeArray;

    [SerializeField] ParticleSystem[] m_vfxArray;
    [SerializeField] AudioClip[] m_sfxArray;
    AudioSource m_audio;
    PlayerController m_player;

    void Start()
    {
        m_player = this.transform.parent.GetComponent<PlayerController>();
        m_audio = GetComponent<AudioSource>();

        // コライダーが有効になっていたら無効にする
        for (int i = 0; i < m_attackRangeArray.Length; i++)
        {
            if (m_attackRangeArray[i].gameObject.activeSelf)
            {
                EndAttack(i);
            }
        }
    }

    /// <summary>
    /// 攻撃を開始する時に呼ぶ
    /// コライダーが有効になる
    /// </summary>
    void BeginAttack(int i)
    {
        //m_attackRangeArray[i].gameObject.SetActive(true);
        m_attackRangeArray[i].enabled = true;
    }

    /// <summary>
    /// 攻撃を終了する時に呼ぶ
    /// コライダーが無効になる
    /// パラメータに負の値を指定すると全ての攻撃範囲が無効になる
    /// </summary>
    void EndAttack(int i)
    {
        if (i < 0)
        {
            foreach(var r in m_attackRangeArray)
            {
                r.enabled = false;
            }
        }
        else
        {
            m_attackRangeArray[i].enabled = false;
        }
    }

    void Effect(int i)
    {
        if (m_sfxArray[i])
        {
            m_audio.PlayOneShot(m_sfxArray[i]);
        }

        if (m_vfxArray[i])
        {
            m_vfxArray[i].Play();
        }
    }

    void FreezePlayer()
    {
        if (m_player)
        {
            m_player.Freeze(true);
        }
    }

    void DefrostPlayer()
    {
        if (m_player)
        {
            m_player.Freeze(false);
        }
    }
}


