using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClickSound : MonoBehaviour
{
    public AudioSource clickSound;
    public void ClickSound()
    {
        clickSound.Play();
    }
}
