using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    public Transform target;
	public bool grabcam = false;
	
	void Awake(){
		if(grabcam)target = Camera.main.transform;
	}

    // Update is called once per frame
    void LateUpdate()
    {
		transform.LookAt(target);
		transform.Rotate(0,180,0);
    }
}
