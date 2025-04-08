using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class npctalker : interactable
{
    public Image icon;
	public dialtree dt;
	
	public override void interact(InputAction.CallbackContext context){
		if(on)opendial();
	}
	
	public virtual void opendial(){
		//called from playercontroller, open UI and pause gameplay
		Debug.Log("OPEN UI");
	}
	
	public virtual void closedial(){
		//called from either cancel button or dialtree, close UI and resume gameplay
	}
	
	public virtual void advancedial(){
		//press A to continue speaking, just say the next string, increase dialtree dialpointer by 1
	}
	
	public virtual void choosedial(int i){
		// chosing what to say, increase dialtree by the decision #, apply any flags, display.
	}
}
