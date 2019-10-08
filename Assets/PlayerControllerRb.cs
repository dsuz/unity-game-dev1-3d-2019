using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rigidbody を使ってプレイヤーを動かすコンポーネント
/// 入力を受け取り、それに従ってオブジェクトを動かす
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerRb : MonoBehaviour
{
    /// <summary>力を加えて動かすか/速度を直接操作するか</summary>
    [SerializeField] MovingType m_movingType = MovingType.AddForce;
    /// <summary>動く速さ</summary>
    [SerializeField] float m_movingSpeed = 5f;
    /// <summary>ジャンプ力</summary>
    [SerializeField] float m_jumpPower = 5f;
    Rigidbody m_rb;
    Animator m_anim;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 dir = (Vector3.forward * v + Vector3.right * h).normalized;

        // 入力があれば、そちらの方向に動かす
        if (dir != Vector3.zero)
        {
            this.transform.forward = dir;
            if (m_movingType == MovingType.AddForce)
            {
                m_rb.AddForce(this.transform.forward * m_movingSpeed);
            }
            else if (m_movingType == MovingType.Velocity)
            {
                m_rb.velocity = this.transform.forward * m_movingSpeed;
            }
        }

        // Animator Controller のパラメータをセットする
        m_anim.SetFloat("Speed", m_rb.velocity.magnitude);

        // ジャンプの入力を取得し、押されていたらジャンプする
        if (Input.GetButtonDown("Jump"))
        {
            m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);
            // Animator Controller のパラメータをセットする
            m_anim.SetTrigger("Jump");
        }
    }
}

public enum MovingType
{
    AddForce,
    Velocity,
}