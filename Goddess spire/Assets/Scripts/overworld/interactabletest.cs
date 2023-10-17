using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactabletest : interactable
{
	public ParticleSystem ps;
    public override void interact(InputAction.CallbackContext context){
		if(on){
			Debug.Log("interacted!");
			ps.Play();
		}
	}
}
