using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
	public Transform followTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void LateUpdate()
	{
		this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);
	}
}
