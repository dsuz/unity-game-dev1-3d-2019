using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rigidbody を使ってプレイヤーを動かすコンポーネント
/// 入力を受け取り、それに従ってオブジェクトを動かす
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    /// <summary></summary>
    [SerializeField] ControlType m_controlType = ControlType.Turn;
    /// <summary>動く速さ</summary>
    [SerializeField] float m_movingSpeed = 5f;
    /// <summary>ターンの速さ</summary>
    [SerializeField] float m_turnSpeed = 3f;
    /// <summary>ジャンプ力</summary>
    [SerializeField] float m_jumpPower = 5f;
    /// <summary>接地判定の際、足元からどれくらいの距離を「接地している」と判定するかの長さ</summary>
    [SerializeField] float m_isGroundedLength = 0.2f;
    Rigidbody m_rb;
    /// <summary>キャラクターの Animator</summary>
    [SerializeField] Animator m_anim;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // ControlType と入力に応じてキャラクターを動かす
        if (m_controlType == ControlType.Turn)
        {
            if (h != 0)
            {
                this.transform.Rotate(this.transform.up, h * m_turnSpeed);
            }

            Vector3 velo = this.transform.forward * m_movingSpeed * v;
            velo.y = m_rb.velocity.y;
            m_rb.velocity = velo;
        }
        else if (m_controlType == ControlType.Move)
        {
            Vector3 dir = Vector3.forward * v + Vector3.right * h;
            if (dir == Vector3.zero)
            {
                m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
            }
            else
            {
                dir = Camera.main.transform.TransformDirection(dir);
                dir.y = 0;
                this.transform.forward = dir;

                Vector3 velo = this.transform.forward * m_movingSpeed;
                velo.y = m_rb.velocity.y;
                m_rb.velocity = velo;
            }
        }

        // Animator Controller のパラメータをセットする
        if (m_anim)
        {
            if (Input.GetButtonDown("Fire1") && IsGrounded())
            {
                m_anim.SetTrigger("Attack");
            }

            Vector3 velocity = m_rb.velocity;
            velocity.y = 0f;
            m_anim.SetFloat("Speed", velocity.magnitude);

            if (m_rb.velocity.y <= 0f && IsGrounded())
            {
                m_anim.SetBool("IsGrounded", true);
            }
            else if (!IsGrounded())
            {
                m_anim.SetBool("IsGrounded", false);
            }
        }

        // ジャンプの入力を取得し、接地している時に押されていたらジャンプする
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);

            // Animator Controller のパラメータをセットする
            if (m_anim)
            {
                m_anim.SetBool("IsGrounded", false);
            }
        }
    }

    /// <summary>
    /// 地面に接触しているか判定する
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
    {
        // Physics.Linecast() を使って足元から線を張り、そこに何かが衝突していたら true とする
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center;   // start: 体の中心
        Vector3 end = start + Vector3.down * (col.center.y + col.height / 2 + m_isGroundedLength);  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
        return isGrounded;
    }
}

public enum ControlType
{
    /// <summary>初代バイオハザードのようなラジコン操作</summary>
    Turn,
    /// <summary>カメラを基準とした方向に移動する</summary>
    Move,
}