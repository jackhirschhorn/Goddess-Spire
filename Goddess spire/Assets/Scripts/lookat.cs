using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
		transform.LookAt(target);
		transform.Rotate(0,180,0);
    }
}
