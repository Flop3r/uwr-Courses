using System.Collections;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CheckParticleSystem());
    }

    IEnumerator CheckParticleSystem()
    {
        ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
        yield return new WaitUntil(() => !particleSystem.isPlaying);
        Destroy(gameObject);
    }
}
