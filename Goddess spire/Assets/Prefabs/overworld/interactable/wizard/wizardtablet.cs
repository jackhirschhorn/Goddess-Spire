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
	public AudioSource sfx2;
	public GameObject tabletgui;
	public Animator anim;
	public void playfx(){	
		if(!tabletgui.activeSelf){
			tabletgui.SetActive(!tabletgui.activeSelf);
			overworldmanager.OM.pc.canmove =!tabletgui.activeSelf; 
			sfx.Play();
			anim.SetBool("play",true);
			//pfx.Play();
		} else {
			sfx2.Play();
			anim.SetBool("play",false);
		}
	}
	
	public void endanim(){
		tabletgui.SetActive(false);
		overworldmanager.OM.pc.canmove =true; 
	}
	
}
