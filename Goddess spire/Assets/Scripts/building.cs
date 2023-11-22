using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : structure
{
    public GameObject flr, clng, wall1,wall2,wall3,frnt;
	
	public static building inroom;
	public GameObject lighting;
	
	public bool fadeout2 = false;
	public float fadetim2 = 1f;
	
	public override void FixedUpdate(){
		base.FixedUpdate();
		if(!fadeout2 && fadetim2 < 1f){
			fadetim2 += Time.fixedDeltaTime*3;
			if(clng.GetComponent<Renderer>()){			
				foreach(Material m in clng.GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			} else {
				foreach(Material m in clng.transform.GetChild(0).GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			}
			if(frnt.GetComponent<Renderer>()){
				foreach(Material m in frnt.GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			} else {
				foreach(Material m in frnt.transform.GetChild(0).GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			}
		} else if(fadeout2 && fadetim2 > 0.2f){
			fadetim2 -= Time.fixedDeltaTime*3;
			if(fadetim2 < 0.2f)fadetim2 = 0.2f;
			if(clng.GetComponent<Renderer>()){			
				foreach(Material m in clng.GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			} else {
				foreach(Material m in clng.transform.GetChild(0).GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			}
			if(frnt.GetComponent<Renderer>()){
				foreach(Material m in frnt.GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			} else {
				foreach(Material m in frnt.transform.GetChild(0).GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			}
		}
	}
	
	public void OnTriggerEnter(Collider col){
		if(col.GetComponent<playercontroller>()){
			fadeout2 = true;
			inroom = this;
			lighting.SetActive(true);
		} 
	}
	
	public void OnTriggerExit(Collider col){
		if(col.GetComponent<playercontroller>()){
			fadeout2 = false;
			if(inroom == this) inroom = null;			
			lighting.SetActive(false);
		}
	}
}
