using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : structure
{
    public GameObject flr, clng, wall1,wall2,wall3,frnt;
	public bool sndwall = false;
	
	public static building inroom;
	public GameObject lighting;
	
	public bool fadeout2 = false;
	public float fadetim2 = 1f;
	public Vector3 euls;
	
	void Awake(){
		if(transform.parent.GetComponent<structure>().nextfloor != null)nextfloor = transform.parent.GetComponent<structure>().nextfloor;
	}
	
	public override void FixedUpdate(){
		base.FixedUpdate();
		if(fadeout2 && inroom != this)inroom = this;
		if(!fadeout2 && fadetim2 < 1f){
			fadetim2 += Time.fixedDeltaTime*3;
			if(inroom == this) inroom = null;
			if(fadetim2 >= 1f){
				lighting.SetActive(false);
				if(transform.parent.GetComponent<structure>().overrider2 == this)transform.parent.GetComponent<structure>().overrider2 = null;
			}
			if(clng.GetComponent<Renderer>()){			
				foreach(Material m in clng.GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			} else {
				foreach(Material m in clng.transform.GetChild(0).GetComponent<Renderer>().materials){
					m.SetFloat("_DitherThreshold",fadetim2);
				}
			}
			if(sndwall){
				if(wall1.GetComponent<Renderer>()){			
					foreach(Material m in wall1.GetComponent<Renderer>().materials){
						m.SetFloat("_DitherThreshold",fadetim2);
					}
				} else {
					foreach(Material m in wall1.transform.GetChild(0).GetComponent<Renderer>().materials){
						m.SetFloat("_DitherThreshold",fadetim2);
					}
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
			if(fadetim2 < 0.2f){
				fadetim2 = 0.2f;
				if(nextfloor != null)nextfloor.overrider = this;			
			}
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
			if(sndwall){
				if(wall1.GetComponent<Renderer>()){			
					foreach(Material m in wall1.GetComponent<Renderer>().materials){
						m.SetFloat("_DitherThreshold",fadetim2);
					}
				} else {
					foreach(Material m in wall1.transform.GetChild(0).GetComponent<Renderer>().materials){
						m.SetFloat("_DitherThreshold",fadetim2);
					}
				}
			}
		}
	}
	
	public void OnTriggerEnter(Collider col){
		if(col.GetComponent<playercontroller>() && col.material.dynamicFriction == 0){
			fadeout2 = true;
			inroom = this;
			lighting.SetActive(true);			
			if(transform.parent.GetComponent<structure>().overrider != null)transform.parent.GetComponent<structure>().snap();
			//transform.parent.GetComponent<structure>().overrider = null;
			transform.parent.GetComponent<structure>().overrider2 = this;
		} 
	}
	
	public void OnTriggerExit(Collider col){
		if(col.GetComponent<playercontroller>() && col.material.dynamicFriction == 0){
			fadeout2 = false;
			if(nextfloor != null && nextfloor.overrider == this)nextfloor.overrider = null;
			 //	needs to recheck so it doesn't fade out at wrong time	
		}
	}
}
