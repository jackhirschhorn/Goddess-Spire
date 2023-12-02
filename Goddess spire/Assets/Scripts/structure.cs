using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class structure : MonoBehaviour
{
	public structure overrider; //calling nextfloor
	public structure overrider2; // this floor
	public bool fadeout = false;
	public float fadetim = 1f;
	public float delay = 1f;
	public structure nextfloor;
	
	public virtual void FixedUpdate(){
		if (fadetim > 0.2f && (fadeout || overrider != null && overrider2 == null)){
			fadetim -= Time.fixedDeltaTime*3;
			if(fadetim < 0.2f)fadetim = 0.2f;
			foreach(Renderer r in transform.GetComponentsInChildren<Renderer>()){
				foreach(Material m in r.materials){
					m.SetFloat("_DitherThreshold",fadetim);
				}
			}
		} else if(fadetim < 1f && !fadeout){			
			fadetim += Time.fixedDeltaTime*3;
			foreach(Renderer r in transform.GetComponentsInChildren<Renderer>()){
				foreach(Material m in r.materials){
					m.SetFloat("_DitherThreshold",fadetim);
				}
			}
		}
		
	}
	
	public virtual void snap(){
		if(fadetim < 1){
			fadetim = 1.1f;
			fadeout = false;
			foreach(Renderer r in transform.GetComponentsInChildren<Renderer>()){
				foreach(Material m in r.materials){
					m.SetFloat("_DitherThreshold",fadetim);
				}
			}
		}
	}
	
	
}
