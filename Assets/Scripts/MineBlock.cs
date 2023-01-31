using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBlock : MonoBehaviour
{
    private Vector2 position;
    private float rayDistance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get movement direction (-1 left, 1 right)
        float inputRaw = Input.GetAxisRaw("Horizontal");
        if (inputRaw != 0.0f)
        {
            // Player position
            position = transform.position;
            // Checks if there is a object next to the player on the horizontal axis 
            RaycastHit2D hit = Physics2D.Raycast(
                position + new Vector2(0.66f / 2 * inputRaw, 1.3125f / 2), // TODO: check player edges and use those to set the origin vectoryou
                Vector2.right * inputRaw,
                rayDistance);
            // Check if the object is minable
            if (hit.collider != null && hit.collider.tag == "Minable")
                Destroy(hit.collider.gameObject, 0);


            Debug.Log("Is block: " + hit.collider.name);
            Debug.Log("Position: " + position);
        }
    }
}