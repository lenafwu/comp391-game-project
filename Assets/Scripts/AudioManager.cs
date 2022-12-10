using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource jumpSource;
    [SerializeField] AudioClip jumpSound;

    public void JumpSFX()
    {
        jumpSource.PlayOneShot(jumpSound);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
