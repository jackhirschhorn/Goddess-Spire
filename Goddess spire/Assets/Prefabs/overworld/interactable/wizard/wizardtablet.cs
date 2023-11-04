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
		if(context.performed && overworldmanager.OM.pc.classid == 6){
			playfx();
		}
	}
	
	public AudioSource sfx;
	public GameObject tabletgui;
	public void playfx(){	
		tabletgui.SetActive(!tabletgui.activeSelf);
		overworldmanager.OM.pc.canmove =!tabletgui.activeSelf; 
		sfx.Play();
		//pfx.Play();
	}
	
}
