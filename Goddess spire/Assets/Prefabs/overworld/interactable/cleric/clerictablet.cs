using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class clerictablet : interactable
{
    public int[] inputs = new int[0]; //0 = up 1 = left 2 = down 3 = right
	public int inputindex = 0;
	public bool on2 = false;
	public bool finished = false;
	InputAction move;
	Vector2 rotass = new Vector2(0,0);
	public bool inputready = true;
	
	public clerictablet(){
		butt = "ability";
	}
	
	void Start(){
		move = overworldmanager.OM.PI.actions.FindAction("move", true);
		
	}
	
	public override void interact(InputAction.CallbackContext context){
		if(context.performed && overworldmanager.OM.pc.classid == 7){
			on2 = true;
			move.performed += moveput;
		} 
		if(context.canceled){
			on2 = false;
			move.performed -= moveput;
		}
	}
	
	public void moveput(InputAction.CallbackContext context){
		rotass = context.ReadValue<Vector2>();
	}
	
	void FixedUpdate(){
		if(on && on2 && !finished){
			/*if(!inputready){ // may only need for controller?
				if(rotass.magnitude < 0.2f){
					inputready = true;
				}
			} else {*/
				if(rotass.y > 0.5f){ //up
					if(inputs[inputindex] == 0){
						inputindex++;
						inputready = false;
						rotass = Vector2.zero;
						//play +sound?
					} else {
						inputindex = 0;
						inputready = false;
						//play -sound?
					}
				} else if(rotass.y < -0.5f){ //down
					if(inputs[inputindex] == 2){
						inputindex++;
						inputready = false;
						rotass = Vector2.zero;
						//play +sound?
					} else {
						inputindex = 0;
						inputready = false;
						//play -sound?
					}
				} else if(rotass.x > 0.5f){ //right
					if(inputs[inputindex] == 3){
						inputindex++;
						inputready = false;
						rotass = Vector2.zero;
						//play +sound?
					} else {
						inputindex = 0;
						inputready = false;
						//play -sound?
					}
				} if(rotass.x < -0.5f){ //left
					if(inputs[inputindex] == 1){
						inputindex++;
						inputready = false;
						rotass = Vector2.zero;
						//play +sound?
					} else {
						inputindex = 0;
						inputready = false;
						//play -sound?
					}
				}
				if(inputindex >= inputs.Length){
					finished = true;
					dothething();
					//do a thing if needed					
				}
			//}
		}
	}
	
	public virtual void dothething(){
		
	}
}
