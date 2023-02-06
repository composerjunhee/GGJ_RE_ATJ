using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
	public Transform followTransform;
    private Vector3 originalCamera;
    void Start()
    {
        originalCamera = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

	void LateUpdate()
	{
        Vector3 currentPos = this.transform.position;
        Vector3 endPos = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);
        if (followTransform.position.y < -5)
		{
            //this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(currentPos, endPos, 20f);
        }
        else
            this.transform.position = Vector3.Lerp(this.transform.position, originalCamera, 15f);
	}
}
