using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラグドールをランダムに吹き飛ばし、dissove するコンポーネント
/// </summary>
public class DeadZombieController : MonoBehaviour
{
    [SerializeField] float m_forceMultiplier = 100f;
    [SerializeField] AnimationCurve m_dissolveCurve;
    [SerializeField] string m_dissolvePropertyNameOfShader = "_DissolveAmount";
    float m_timer;

    void Start()
    {
        BlowOff();
    }

    void Update()
    {
        Dissolve();
        m_timer += Time.deltaTime;
    }

    /// <summary>
    /// プレイヤーと反対方向に吹っ飛ばす
    /// 子の Rigidbody のうちどれか一つに力を加える
    /// </summary>
    void BlowOff()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dir = this.transform.position - player.transform.position;
        dir.y = 0;
        dir = dir.normalized + Vector3.up;

        Rigidbody[] rbArray = this.transform.GetComponentsInChildren<Rigidbody>();
        rbArray[Random.Range(0, rbArray.Length)].AddForce(dir * m_forceMultiplier, ForceMode.Impulse);
    }

    /// <summary>
    /// m_dissolveCurve で指定したカーブに基づいて配下の material を dissolve する
    /// 全ての material にはシェーダーに DissolveEmission を指定すること
    /// dissolve 率が 99% になった時点でオブジェクトを破棄する
    /// </summary>
    void Dissolve()
    {
        Renderer[] renArray = this.transform.GetComponentsInChildren<Renderer>();
        float value = m_dissolveCurve.Evaluate(m_timer);
        foreach (var r in renArray)
        {
            Material mat = r.material;
            if (mat.HasProperty(m_dissolvePropertyNameOfShader))
            {
                mat.SetFloat(m_dissolvePropertyNameOfShader, value);
            }
        }
        if (value > .99f)
        {
            Destroy(this.gameObject);
        }
    }
}
