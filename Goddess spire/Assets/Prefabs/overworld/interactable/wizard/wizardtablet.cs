using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class wizardtablet : interactable
{
    public wizardtablet(){
		butt = "ability";
	}
	
	public override void interact(InputAction.CallbackContext context){
		if(context.performed){
			
		}
	}
	
	public AudioSource sfx;
	public void playfx(){		
		sfx.Play();
		//pfx.Play();
	}
}
