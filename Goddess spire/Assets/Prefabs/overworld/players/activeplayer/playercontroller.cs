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
	public List<AudioClip> sounds = new List<AudioClip>();
	public GameObject footsteps;
	public AudioSource jumpgrunt;
	public AudioSource land;
	
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
    void FixedUpdate()
    {
		if(rotass != Vector2.zero){	
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0), 90f/Quaternion.Angle(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0))*rotspeed *(is_sprinting?2.5f:1)*Time.deltaTime);
			anim.SetBool((is_sprinting?"sprint":"walk"), true);
			footsteps.GetComponent<AudioSource>().clip = sounds[(is_sprinting?0:1)];
			if(!footsteps.GetComponent<AudioSource>().isPlaying)footsteps.GetComponent<AudioSource>().Play();
			footsteps.SetActive(true);
			transform.GetComponent<Rigidbody>().velocity += ((new Vector3(rotass.x,0,rotass.y)*(is_sprinting?2.9f:1.9f)*50f*Time.fixedDeltaTime));
			transform.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(transform.GetComponent<Rigidbody>().velocity.x,rotass.x*(is_sprinting?2.9f:1.9f)*5f,rotass.x*(is_sprinting?2.9f:1.9f)*5f),transform.GetComponent<Rigidbody>().velocity.y,Mathf.Clamp(transform.GetComponent<Rigidbody>().velocity.z,rotass.y*(is_sprinting?2.9f:1.9f)*5f,rotass.y*(is_sprinting?2.9f:1.9f)*5f));
		} else {
			footsteps.SetActive(false);
			anim.SetBool("walk", false);
			anim.SetBool("sprint", false);
			transform.GetComponent<Rigidbody>().velocity = new Vector3(0,transform.GetComponent<Rigidbody>().velocity.y,0);
		}
		if(playland && Physics.SphereCast(transform.position, 0.4f, -Vector3.up, out RaycastHit hit, 1.21f, jumplayers) && !anim.GetBool("jump")){
			land.Play();
			playland = false;
		}
		if(jumppower > 0){
			transform.GetComponent<Rigidbody>().AddForce(new Vector3(0,jumppower,0));
			jumppower = Mathf.Clamp(jumppower - (Time.fixedDeltaTime*jumppowerdecay),0,5f);
		} else {
			transform.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * 2f * Time.fixedDeltaTime;
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
	
	bool playland = false;
	float jumppower = 0f;
	float jumppowerdecay = 1f;
	public LayerMask jumplayers;
	
	public void jump(InputAction.CallbackContext context){
		if(context.performed && Physics.SphereCast(transform.position, 0.4f, -Vector3.up, out RaycastHit hit, 1.21f, jumplayers)){
			jumppower = 500f;
			jumppowerdecay = 15f;
			anim.SetBool("jump", true);
			playland = true;
			jumpgrunt.Play();
		}
		if(context.canceled){
			jumppowerdecay = 50f;
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
