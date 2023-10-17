using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactable : MonoBehaviour
{
    public Transform indicator;
	public bool on = false;
	
	void OnTriggerEnter(Collider col){
		if(col.transform.GetComponent<playercontroller>()){
			on = true;
			if(indicator != null)indicator.gameObject.SetActive(true);
			Debug.Log(overworldmanager.OM);
			InputAction action = overworldmanager.OM.PI.actions.FindAction("confirm", true);
			action.performed += interact;
		}
	}
	
	void OnTriggerExit(Collider col){
		if(col.transform.GetComponent<playercontroller>()){
			on = false;
			if(indicator != null)indicator.gameObject.SetActive(false);
			InputAction action = overworldmanager.OM.PI.actions.FindAction("confirm", true);
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
