using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class kimasterbreaker : interactable
{
	public AudioSource sfx;
	public ParticleSystem pfx;
	public bool broken = false;
	
	public kimasterbreaker(){
		butt = "ability";
	}
    
	public override void interact(InputAction.CallbackContext context){
		if(context.performed){
			if(overworldmanager.OM.pc.classid == 1 && !broken){
				overworldmanager.OM.pc.anim.SetBool("kistrike",true);
				broken = true;
				sfx.Play();
				pfx.Play();
				indicator.gameObject.SetActive(false);
				indicator = null;
				transform.GetComponent<Animator>().SetBool("break",true);
			}
		}
	}
	
}
