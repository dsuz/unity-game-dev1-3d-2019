using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃が当たった時に音を鳴らすコンポーネント
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AttackAudioController : MonoBehaviour
{
    /// <summary>攻撃が敵に当たった時の音</summary>
    [SerializeField] AudioClip[] m_attackSounds;
    /// <summary>攻撃が敵以外のものに当たった時の音</summary>
    [SerializeField] AudioClip[] m_clipNoises;
    AudioSource m_audio;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (m_attackSounds.Length > 0)
            {
                m_audio.PlayOneShot(m_attackSounds[Random.Range(0, m_attackSounds.Length)]);
            }
        }
        else
        {
            if (m_clipNoises.Length > 0)
            {
                m_audio.PlayOneShot(m_clipNoises[Random.Range(0, m_clipNoises.Length)]);
            }
        }
    }
}
