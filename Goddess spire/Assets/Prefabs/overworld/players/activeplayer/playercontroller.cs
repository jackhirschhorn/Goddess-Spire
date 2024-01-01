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
	public AudioSource faceplant;		
	public Rigidbody cc;
	public float lastangle = 0;
	public PhysicMaterial jumpmat;
	public PhysicMaterial standmat;
	public bool canmove = true;
	public Vector3 safepoint;
	public int classid = 0; //0 barbarian, 1 KI master, 2 paladin, 3 ranger, 4 phantom, 5 bard, 6 wizard, 7 cleric, 8 druid
	public fixture targetfixture;
	public Animator rangervision;
	
	void Awake(){
		
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

	void LateUpdate(){
		camera.position = transform.position;
	}

	Vector3 lastvel = Vector3.zero;

    // Update is called once per frame
    void FixedUpdate()
    {
		if(canmove && !anim.GetBool("kistrike")){
			if(rotass != Vector2.zero){
				cc.isKinematic = false;
				transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0), 90f/Quaternion.Angle(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0))*rotspeed *(is_sprinting?2.5f:1)*Time.deltaTime);
				anim.SetBool((is_sprinting?"sprint":"walk"), true);
				footsteps.clip = sounds[(is_sprinting?0:1)];
				cc.velocity += ((new Vector3(rotass.x,0,rotass.y)*(is_sprinting?2.9f:1.9f)*50f*Time.fixedDeltaTime));
				cc.velocity = new Vector3(Mathf.Clamp(cc.velocity.x,rotass.x*(is_sprinting?2.9f:1.9f)*5f,rotass.x*(is_sprinting?2.9f:1.9f)*5f),cc.velocity.y,Mathf.Clamp(cc.velocity.z,rotass.y*(is_sprinting?2.9f:1.9f)*5f,rotass.y*(is_sprinting?2.9f:1.9f)*5f));
				if(jumppower > 0){
					
				} else if(Physics.SphereCast(transform.position, 0.49f, -Vector3.up, out RaycastHit hit, 1.21f, jumplayers,QueryTriggerInteraction.Ignore)){
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
				anim.SetBool("barbcharge", false);
				cc.velocity = new Vector3(0,cc.velocity.y,0);
				RaycastHit hit = new RaycastHit();
				Physics.SphereCast(transform.position, 0.49f, -Vector3.up, out hit, 1.21f, jumplayers,QueryTriggerInteraction.Ignore);
				if(!playland && hit.transform != null && !groundangle(hit.normal,true)){
					//cc.isKinematic = true;
					cc.velocity = Vector3.zero;
				} else {
					cc.isKinematic = false;
					if(Vector3.Angle(Vector3.Cross(hit.normal,Vector3.Cross(hit.normal,Vector3.up)),Vector3.up) >149f){
						cc.velocity = -Physics.gravity.y*Vector3.Cross(hit.normal,Vector3.Cross(hit.normal,Vector3.up));
					}
				}
			}
			if(!playland && Physics.SphereCast(transform.position, 0.4f, -Vector3.up, out RaycastHit hit3, 1.21f, jumplayers,QueryTriggerInteraction.Ignore)){
				if(hit3.transform.GetComponent<Rigidbody>()){
					lastvel = hit3.transform.GetComponent<Rigidbody>().velocity;
				} else {
					lastvel = Vector3.zero;
					safepoint = transform.position;
				}
			}
			if(playland && Physics.SphereCast(transform.position, 0.49f, -Vector3.up, out RaycastHit hit2, 1.21f, jumplayers,QueryTriggerInteraction.Ignore) && !anim.GetBool("jump")){
				land.Play();
				playland = false;
				anim.SetBool("landed", true);
				//transform.GetComponent<Collider>().material = standmat;
				lastangle = Vector3.Angle(Vector3.Cross(hit2.normal,Vector3.Cross(hit2.normal,Vector3.up)),Vector3.up);
			}
			if(jumppower > 0){
				anim.SetBool("barbcharge", false);
				footsteps.Stop();
				transform.GetComponent<Rigidbody>().AddForce(jumpangle*jumppower);
				jumppower = Mathf.Clamp(jumppower - (Time.fixedDeltaTime*jumppowerdecay),0,5f);
			} else {
				transform.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * 2f * Time.fixedDeltaTime;
			}
			cc.velocity += lastvel;
		} else {
			transform.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * 2f * Time.fixedDeltaTime;
		}
    }
	
	public bool isgrounded(){
		return Physics.SphereCast(transform.position, 0.40f, -Vector3.up, out RaycastHit hit, 1.21f, jumplayers,QueryTriggerInteraction.Ignore);
	}
	
	public AudioSource kistrikeas;
	
	public void playkistrike(){
		kistrikeas.Play();
	}
	
	public bool iskistriking = false;
	
	public void startkifirststrike(){
		iskistriking = true;
	}
	
	public void endkifirststrike(){
		iskistriking = false;
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
	
	public void playfaceplant(){
		faceplant.Play();
	}
	
	public void sprinttoggle(InputAction.CallbackContext context){
		if(context.performed){
			is_sprinting = !is_sprinting;
			if(!is_sprinting){
				anim.SetBool("sprint", false);				
				anim.SetBool("barbcharge", false);
			}
		}
	}
	
	public Vector2 rotass = new Vector2(0,0);
	public void move(InputAction.CallbackContext context){
		rotass = Quaternion.AngleAxis(-camera.eulerAngles.y, Vector3.forward) * context.ReadValue<Vector2>();
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
		if(context.performed && Physics.SphereCast(transform.position, 0.49f, -Vector3.up, out RaycastHit hit, 1.21f, jumplayers,QueryTriggerInteraction.Ignore) && !groundangle(hit.normal,true)){
			//jumpangle = hit.normal;
			jumppower = 500f;
			jumppowerdecay = 15f;
			anim.SetBool("jump", true);
			anim.SetBool("landed", false);
			playland = true;
			jumpgrunt.Play();
			cc.isKinematic = false;
			cc.velocity = new Vector3(cc.velocity.x, 0, cc.velocity.z);
			//transform.GetComponent<Collider>().material = jumpmat;
		}
		if(context.canceled){
			jumppowerdecay = 50f;
		}
	}
	
	public void hazardmode(int dam,int damtype, bool sink){
		//deal damage to party members
		if(sink){
			StartCoroutine(sinkhazard());
		} else {
			StartCoroutine(spikehazard());
		}
	}
	
	public IEnumerator spikehazard(){
		cc.isKinematic = true;
		canmove = false;
		anim.SetBool("spike",true);
		anim.SetBool("hazard",true);
		anim.SetBool("walk", false);
		anim.SetBool("sprint", false);
		anim.SetBool("barbcharge", false);
		anim.SetBool("hazardfall",true);
		if(!playland)safepoint -= transform.right;
		while(anim.GetBool("hazard")){
			yield return new WaitForEndOfFrame();
		}
		//anim.SetBool("fall",true); // add launch animation?
		transform.position = safepoint;
		do {
			yield return new WaitForEndOfFrame();
		} while(anim.GetBool("hazardfall"));
		canmove = true;
		cc.isKinematic = false;
	}
	
	public IEnumerator sinkhazard(){
		cc.isKinematic = true;
		canmove = false;
		anim.SetBool("sink",true);
		anim.SetBool("hazard",true);
		anim.SetBool("walk", false);
		anim.SetBool("sprint", false);	
		anim.SetBool("barbcharge", false);
		anim.SetBool("hazardfall",true);
		if(!playland)safepoint -= transform.right;
		while(anim.GetBool("hazard")){
			yield return new WaitForEndOfFrame();
		}
		//anim.SetBool("fall",true); // add launch animation?
		transform.position = safepoint;
		do {
			yield return new WaitForEndOfFrame();
		} while(anim.GetBool("hazardfall"));
		canmove = true;
		cc.isKinematic = false;
	}
	
	public AudioSource rangersonar;
	public AudioSource prayer;
	public ParticleSystem prayerps;
	
	public void ability(InputAction.CallbackContext context){
		if(classid == 0){ //barbarian
			if(context.performed){
				anim.SetBool("barbcharge", true);
				is_sprinting = true;
			} else if (context.canceled){
				anim.SetBool("barbcharge", false);
			}
		} else if (classid == 1){ //ki master
			//handled by breakers
			try{
				StartCoroutine(kimasterstrike());
			} catch {
				
			}
		} else if (classid == 2){ //paladin
			
		} else if (classid == 3){ //ranger
			if(context.performed && !rangervision.GetBool("play")){
				//anim;
				rangerdetector[] randets = Object.FindObjectsOfType<rangerdetector>();
				foreach(rangerdetector rd in randets){
					rd.shimmer();
				}
				rangervision.SetBool("play", true);
				rangersonar.Play();
			}
		} else if (classid == 4){ //rogue
			
		} else if (classid == 5){ //bard
		
		} else if (classid == 6){ //wizard
			if(context.performed){
				//connect to a tablet
			}
		} else if (classid == 7){ //cleric
			if(context.performed){
				canmove = false; 
				cancelcanmove = true;
				anim.SetBool((overworldmanager.OM.pc.is_sprinting?"sprint":"walk"), false); 
				anim.SetBool("pray",true);
				prayer.Play();
				prayerps.Play();
				footsteps.Stop();
				anim.SetBool("walk", false);
				anim.SetBool("sprint", false);
				anim.SetBool("barbcharge", false);
				if(isgrounded()){
					cc.velocity = new Vector3(0,0,0);
				}/* else {
					cc.velocity = new Vector3(0,cc.velocity.y,0);
				}*/ //leap of faith?
			}
			if(context.canceled){
				anim.SetBool("pray",false);
				cancelcanmove = false;
				StartCoroutine(delayedcanmove(0.5f));
				prayer.Stop();
				prayerps.Stop();
			}
		} else if (classid == 8){ //druid
			
		}
	}
	
	public LayerMask entitiesonly;
	public combatoption firestrike;
	
	public IEnumerator kimasterstrike(){
		yield return new WaitForEndOfFrame();
		if(!anim.GetBool("kistrike")){
			anim.SetBool("kistrike",true);
			canmove = false;
			yield return new WaitUntil(() => iskistriking);
			RaycastHit hit;
			while(iskistriking){
				if(Physics.SphereCast(transform.position,1.2f,transform.right,out hit,3f,entitiesonly)){
					if(hit.transform.GetComponent<enemypath>()){
						Debug.Log("first strike!");
						//DEBUG
						combatantdata cd = new combatantdata();
						foreach(combatantdata c in transform.GetComponent<combatantdataholder>().cd){
							if(c.clas == 1)cd = c;
						}
						//END DEBUG
						overworldmanager.OM.firststrike(cd, firestrike);
						hit.transform.GetComponent<enemypath>().enterbattle();
						canmove = true;
						yield break;
					}
				}
				yield return new WaitForEndOfFrame();
			}
			yield return new WaitUntil(() => !anim.GetBool("kistrike"));
			canmove = true;
		}
	}
	
	public void select1(InputAction.CallbackContext context){
		if(context.performed){
			//debug
			classid++;
			Debug.Log(classid);
		}
	}
	
	public void select2(InputAction.CallbackContext context){
		if(context.performed){
			//debug
			classid--;
			Debug.Log(classid);
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
	
	public bool cancelcanmove = false;
	
	public IEnumerator delayedcanmove(float f){
		yield return new WaitForSeconds(f);
		if(!cancelcanmove)canmove = true;
	}
}
