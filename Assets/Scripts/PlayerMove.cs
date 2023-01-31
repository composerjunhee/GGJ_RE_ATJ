using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public Animator			animator;
	public SpriteRenderer	spriteRenderer;
    public Rigidbody2D		m_body2d;
    private Vector2			positions;

	private Vector3 m_Velocity = Vector3.zero;
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
			Debug.Log(m_body2d.velocity);
			
			if (inputRawX == -1)
				spriteRenderer.flipX = false;
			if (inputRawX == 1)
				spriteRenderer.flipX = true;
            // Move player horizontaly
            // transform.Translate(new Vector2(inputRawX * Time.deltaTime * 6, 0));

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(inputRawX * 10f, m_body2d.velocity.y);
			// And then smoothing it out and applying it to the character
			m_body2d.velocity = Vector3.SmoothDamp(m_body2d.velocity, targetVelocity, ref m_Velocity, 0.1f); // Like skating on ice.. No good

        }
		if (inputRawY != 0.0f)
        {
            // Move player horizontaly
			if (inputRawY == 1)
				m_body2d.AddForce(new Vector2(0f, 6f)); // Can fly
            // transform.Translate(new Vector2(0, inputRawY * Time.deltaTime * 6));
        }
		animator.SetFloat("Speed", Mathf.Abs(m_body2d.velocity.x));
    }
}