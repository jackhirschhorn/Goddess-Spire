using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{
	
	public Animator anim;
	public float rotspeed = 1f;
	public bool is_sprinting = false;
	public Transform camera;
	
	void Awake(){
		
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

	void LateUpdate(){
		camera.position = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		if(rotass != Vector2.zero){	
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0), 90f/Quaternion.Angle(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0))*rotspeed *(is_sprinting?2.5f:1)*Time.deltaTime);
			anim.SetBool((is_sprinting?"sprint":"walk"), true);
		} else {
			anim.SetBool("walk", false);
			anim.SetBool("sprint", false);
		}
    }
	
	public void sprinttoggle(InputAction.CallbackContext context){
		if(context.performed){
			is_sprinting = !is_sprinting;
			if(!is_sprinting)anim.SetBool("sprint", false);
		}
	}
	
	public Vector2 rotass = new Vector2(0,0);
	public void move(InputAction.CallbackContext context){
		rotass = context.ReadValue<Vector2>();
	}
	
	public void confirm(InputAction.CallbackContext context){
		if(context.performed){	
		
		}
	}
	
	public void cancel(InputAction.CallbackContext context){
		if(context.performed){
		
		}
	}
	
	public void jump(InputAction.CallbackContext context){
		if(context.performed){
		
		}
	}
	
	public void select1(InputAction.CallbackContext context){
		if(context.performed){
			
		}
	}
	
	public void select2(InputAction.CallbackContext context){
		if(context.performed){
			
		}
	}
	
	public void select3(InputAction.CallbackContext context){
		if(context.performed){
			
		}
	}
	
	public void select4(InputAction.CallbackContext context){
		if(context.performed){
			
		}
	}
	
	public void select5(InputAction.CallbackContext context){
		if(context.performed){
			
		}
	}
}
