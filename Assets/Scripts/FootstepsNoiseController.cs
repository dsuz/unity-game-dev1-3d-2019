using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepsNoiseController : MonoBehaviour
{
    [SerializeField] AudioClip[] m_footStepNoises;
    AudioSource m_audio;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    void PlayRandomFootStepNoise()
    {
        m_audio.PlayOneShot(m_footStepNoises[Random.Range(0, m_footStepNoises.Length)]);
    }
}
