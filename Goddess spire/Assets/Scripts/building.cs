using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : structure
{
    public GameObject flr, clng, wall1,wall2,wall3,frnt;
	
	public static building inroom;
	
	public void OnTriggerEnter(Collider col){
		if(col.GetComponent<playercontroller>()){
			if(clng.GetComponent<Renderer>()){			
				foreach(Material m in clng.GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",0.2f);
				}
			} else {
				foreach(Material m in clng.transform.GetChild(0).GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",0.2f);
				}
			}
			if(frnt.GetComponent<Renderer>()){
				foreach(Material m in frnt.GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",0.2f);
				}
			} else {
				foreach(Material m in frnt.transform.GetChild(0).GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",0.2f);
				}
			}
			inroom = this;
		} 
	}
	
	public void OnTriggerExit(Collider col){
		if(col.GetComponent<playercontroller>()){
			if(clng.GetComponent<Renderer>()){
				foreach(Material m in clng.GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",1f);
				}
			} else {
				foreach(Material m in clng.transform.GetChild(0).GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",1f);
				}
			}
			if(frnt.GetComponent<Renderer>()){
				foreach(Material m in frnt.GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",1f);
				}
			} else {
				foreach(Material m in frnt.transform.GetChild(0).GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",1f);
				}
			}
			if(inroom == this) inroom = null;
		}
	}
}
