using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class torch : interactable
{
	public bool lit = false;
	
    public override void interact(InputAction.CallbackContext context){
		if(on && !lit){
			lit = true;
			transform.GetChild(1).gameObject.SetActive(true);
			indicator.gameObject.SetActive(false);
			indicator = null;
		}
	}
}
