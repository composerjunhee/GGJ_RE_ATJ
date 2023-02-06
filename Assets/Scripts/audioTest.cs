using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTest : MonoBehaviour
{
    private AudioSource theAudio;
    [SerializeField]private int time = 5;
    [SerializeField]private float timer = 0.0f;
    [SerializeField]private bool played = false;

    [SerializeField] private AudioClip[] clip;

    void Start()
    {
        theAudio = GetComponent<AudioSource>();
    }

    public void PlaySE()
    {
        if (timer > time)
        {
            int _temp = Random.Range(0, 5);
            theAudio.clip = clip[_temp];
            theAudio.Play();
            timer = 0.0f;
            played = true;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!played)
        {
            PlaySE();
            played = false;
        }
    }
}
