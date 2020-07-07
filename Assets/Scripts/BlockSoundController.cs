using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSoundController : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        source.Play();

        yield return new WaitForSeconds(source.clip.length);

        Destroy(gameObject);
    }
}
