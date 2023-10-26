using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactable : fixture
{
    public Transform indicator;
	public bool on = false;
	public string butt = "";
	
	void OnTriggerEnter(Collider col){
		if(col.transform.GetComponent<playercontroller>()){
			on = true;
			if(indicator != null)indicator.gameObject.SetActive(true);
			InputAction action = overworldmanager.OM.PI.actions.FindAction(butt, true);
			action.performed += interact;
		}
	}
	
	void OnTriggerExit(Collider col){
		if(col.transform.GetComponent<playercontroller>()){
			on = false;
			if(indicator != null)indicator.gameObject.SetActive(false);
			InputAction action = overworldmanager.OM.PI.actions.FindAction(butt, true);
			action.performed -= interact;
		}
	}
	
	/*void FixedUpdate(){
		on = false;
		if(indicator != null)indicator.gameObject.SetActive(false);
	}*/
	
	
	
	public virtual void interact(InputAction.CallbackContext context){
		if(on)Debug.Log("interacted!");
	}
}
