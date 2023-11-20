using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class structure : MonoBehaviour
{
	public bool fadeout = false;
	public float fadetim = 1f;
	public float delay = 1f;
	
	public virtual void FixedUpdate(){
		if(fadetim < 1f && !fadeout){
			fadetim += Time.fixedDeltaTime*3;
			foreach(Renderer r in transform.GetComponentsInChildren<Renderer>()){
				foreach(Material m in r.materials){
					m.SetFloat("_DitherThreshold",fadetim);
				}
			}
		} else if (fadetim > 0.2f && fadeout){
			fadetim -= Time.fixedDeltaTime*3;
			if(fadetim < 0.2f)fadetim = 0.2f;
			foreach(Renderer r in transform.GetComponentsInChildren<Renderer>()){
				foreach(Material m in r.materials){
					m.SetFloat("_DitherThreshold",fadetim);
				}
			}
		}
		
	}
	
}
