using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D	m_body2d;
    private Vector2		positions;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		positions = transform.position;
        // Get movement direction (-1 left, 1 right)
        float inputRawX = Input.GetAxisRaw("Horizontal");
        float inputRawY = Input.GetAxisRaw("Vertical");
        if (inputRawX != 0.0f)
        {
            // Move player horizontaly
            transform.Translate(new Vector2(inputRawX * Time.deltaTime * 6, 0));
        }
		if (inputRawY != 0.0f)
        {
            // Move player horizontaly
            transform.Translate(new Vector2(0, inputRawY * Time.deltaTime * 6));
        }
    }
}