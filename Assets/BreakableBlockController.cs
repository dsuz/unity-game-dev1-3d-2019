using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlockController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Collider col = GetComponent<Collider>();
            col.enabled = false;

            ParticleSystem ps = GetComponent<ParticleSystem>();
            ps.Play();
            Destroy(this.gameObject, ps.main.duration);

            Renderer r = GetComponent<Renderer>();
            r.enabled = false;
        }
    }
}
