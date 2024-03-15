using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Player : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            source.PlayOneShot(clip);
        }
    }
}
