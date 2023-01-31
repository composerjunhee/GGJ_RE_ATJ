using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D         m_body2d;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get movement direction (-1 left, 1 right)
        float inputRawX = Input.GetAxisRaw("Horizontal");
        float inputRawY = Input.GetAxisRaw("Vertical");
        // if (inputRawX != 0.0f)
        // {
        //     // Player position
        //     position = transform.Translate 
        // }
    }
}