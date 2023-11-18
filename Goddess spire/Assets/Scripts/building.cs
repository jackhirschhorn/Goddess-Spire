using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
    public GameObject flr, clng, wall1,wall2,wall3,frnt;
	
	public static building inroom;
	
	public void OnTriggerEnter(Collider col){
		if(col.GetComponent<playercontroller>()){
			foreach(Material m in clng.GetComponent<Renderer>().materials){
				m.SetFloat("_DitherThreshold",0.2f);
			}
			foreach(Material m in frnt.GetComponent<Renderer>().materials){
				m.SetFloat("_DitherThreshold",0.2f);
			}
			inroom = this;
		} 
	}
	
	public void OnTriggerExit(Collider col){
		if(col.GetComponent<playercontroller>()){
			foreach(Material m in clng.GetComponent<Renderer>().materials){
				m.SetFloat("_DitherThreshold",1f);
			}
			foreach(Material m in frnt.GetComponent<Renderer>().materials){
				m.SetFloat("_DitherThreshold",1f);
			}
			if(inroom == this) inroom = null;
		}
	}
}
