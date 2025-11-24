using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class decalraycaster : MonoBehaviour
{
	public DecalProjector dp;
	public LayerMask lm;
	private RaycastHit hit;
	
    // Update is called once per frame
    void FixedUpdate()
    {
        Physics.Raycast(transform.position, -Vector3.up, out hit, 100, lm);
		dp.pivot = new Vector3(0,0,hit.distance-0.03f);
    }
}
