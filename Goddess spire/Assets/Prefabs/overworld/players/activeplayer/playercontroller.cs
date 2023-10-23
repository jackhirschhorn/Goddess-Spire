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
	public AudioSource footsteps;
	public AudioSource jumpgrunt;
	public AudioSource land;	
	public Rigidbody cc;
	public float lastangle = 0;
	
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
			cc.isKinematic = false;
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0), 90f/Quaternion.Angle(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0))*rotspeed *(is_sprinting?2.5f:1)*Time.deltaTime);
			anim.SetBool((is_sprinting?"sprint":"walk"), true);
			footsteps.clip = sounds[(is_sprinting?0:1)];
			cc.velocity += ((new Vector3(rotass.x,0,rotass.y)*(is_sprinting?2.9f:1.9f)*50f*Time.fixedDeltaTime));
			cc.velocity = new Vector3(Mathf.Clamp(cc.velocity.x,rotass.x*(is_sprinting?2.9f:1.9f)*5f,rotass.x*(is_sprinting?2.9f:1.9f)*5f),cc.velocity.y,Mathf.Clamp(cc.velocity.z,rotass.y*(is_sprinting?2.9f:1.9f)*5f,rotass.y*(is_sprinting?2.9f:1.9f)*5f));
			if(jumppower > 0){
				
			} else if(Physics.SphereCast(transform.position, 0.49f, -Vector3.up, out RaycastHit hit, 1.21f, jumplayers)){
				if(groundangle(hit.normal,1)){ //fix this to be right decector
					if(lastangle < 150f){
						cc.velocity = new Vector3(0,0,0);
						cc.isKinematic = true;
					} else {
						cc.velocity = -Physics.gravity.y*Vector3.Cross(hit.normal,Vector3.Cross(hit.normal,Vector3.up));
					}
				} else {
					cc.velocity = Vector3.ProjectOnPlane(cc.velocity,hit.normal);
					lastangle = Vector3.Angle(Vector3.Cross(hit.normal,Vector3.Cross(hit.normal,Vector3.up)),Vector3.up);
				}
			}
		} else {
			footsteps.Stop();
			anim.SetBool("walk", false);
			anim.SetBool("sprint", false);
			transform.GetComponent<Rigidbody>().velocity = new Vector3(0,transform.GetComponent<Rigidbody>().velocity.y,0);
			RaycastHit hit = new RaycastHit();
			if(!playland && Physics.SphereCast(transform.position, 0.49f, -Vector3.up, out hit, 1.21f, jumplayers) && groundangle(hit.normal)){
				cc.isKinematic = true;
			} else {
				cc.isKinematic = false;
				if(Vector3.Angle(Vector3.Cross(hit.normal,Vector3.Cross(hit.normal,Vector3.up)),Vector3.up) >149f)cc.velocity = -Physics.gravity.y*Vector3.Cross(hit.normal,Vector3.Cross(hit.normal,Vector3.up));
			}
		}
		if(playland && Physics.SphereCast(transform.position, 0.49f, -Vector3.up, out RaycastHit hit2, 1.21f, jumplayers) && !anim.GetBool("jump")){
			land.Play();
			playland = false;
			anim.SetBool("landed", true);
			lastangle = Vector3.Angle(Vector3.Cross(hit2.normal,Vector3.Cross(hit2.normal,Vector3.up)),Vector3.up);
		}
		if(jumppower > 0){
			footsteps.Stop();
			transform.GetComponent<Rigidbody>().AddForce(jumpangle*jumppower);
			jumppower = Mathf.Clamp(jumppower - (Time.fixedDeltaTime*jumppowerdecay),0,5f);
		} else {
			transform.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * 2f * Time.fixedDeltaTime;
		}
    }
	
	public bool groundangle(Vector3 v){
		return (((Vector3.Angle( v, new Vector3(cc.velocity.normalized.x,0,cc.velocity.normalized.z))) - 90)> 50f);
	}
	
	public bool groundangle(Vector3 v, int i){
		if(Vector3.Angle(Vector3.Cross(v,Vector3.Cross(v,Vector3.up)),Vector3.up) <149f)return false;
		return (((Vector3.Angle( v, new Vector3(cc.velocity.normalized.x,0,cc.velocity.normalized.z))) - 90)> Mathf.Clamp(Vector3.Angle(Vector3.Cross(v,Vector3.Cross(v,Vector3.up)),Vector3.ProjectOnPlane(cc.velocity,v))-120,0,50));
	}
	
	public bool groundangle(Vector3 v, bool b){ //forjumping
		if(Vector3.Angle(Vector3.Cross(v,Vector3.Cross(v,Vector3.up)),Vector3.up) >149f)return true;
		return (((Vector3.Angle( v, transform.right)) - 90)> 50f);
	}
	
	public void playstep(){
		footsteps.Play();
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
	Vector3 jumpangle = new Vector3(0,1,0);
	
	public void jump(InputAction.CallbackContext context){
		if(context.performed && Physics.SphereCast(transform.position, 0.49f, -Vector3.up, out RaycastHit hit, 1.21f, jumplayers) && !groundangle(hit.normal,true)){
			//jumpangle = hit.normal;
			jumppower = 500f;
			jumppowerdecay = 15f;
			anim.SetBool("jump", true);
			anim.SetBool("landed", false);
			playland = true;
			jumpgrunt.Play();
			cc.isKinematic = false;
			cc.velocity = new Vector3(cc.velocity.x, 0, cc.velocity.z);
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
