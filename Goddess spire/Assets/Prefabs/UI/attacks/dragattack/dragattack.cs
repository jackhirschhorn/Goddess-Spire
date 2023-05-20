using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dragattack : MonoBehaviour
{
	public bool dragging = false;
	public LayerMask lm;
	public LayerMask lm2;
	public int dam;
	public int damtype;
	public float speed = 1;
	public float dragdist = 1;
	public float timer = 1;
	public float timerspeed = 1;
	public ParticleSystem part;	
	private ParticleSystem.ShapeModule shap;
	private ParticleSystem.EmissionModule emis;
	public Transform mover;
	
	public void Awake(){
		shap = part.shape;
		emis = part.emission;
	}
	
	public dragattack(int i, int i2){
		dam = i;
		damtype = i;
	}
	
    public void activate(){
		transform.GetChild(0).GetComponent<Button>().enabled = true;
	}
	
	public void startdrag(){
		dragging = true;
	}
	
	public void FixedUpdate(){
		if(dragging){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 100, lm)){
				//Debug.Log(Vector3.Lerp(hit.point,transform.position,speed/Vector3.Distance(hit.point,transform.position)));
				//Vector3.Lerp(hit.point,transform.position,speed/Vector3.Distance(hit.point,transform.position)); // drag behind
				mover.position = Vector3.Lerp(mover.position,hit.point,speed/Vector3.Distance(hit.point,mover.position));
			}
			ray = new Ray(Camera.main.transform.position, mover.position-Camera.main.transform.position);
			if(Physics.Raycast(ray, out hit, 100, lm2)){
				//Debug.Log(hit.transform.parent.GetComponent<Combatant>());
				hit.transform.parent.GetComponent<Combatant>().take_damage(dam,damtype); //attack handler? uh
				//spawn the thing
				Transform Clone = Instantiate(BattleMaster.pl[1]);
				Clone.parent = transform.parent;
				Clone.position = transform.position;
				Clone = Instantiate(BattleMaster.pl[2]);
				Clone.parent = transform.parent;
				Clone.position = mover.position;
				BattleMaster.attackcallback(0);
				Destroy(this.gameObject);
			}
			timer -= Time.fixedDeltaTime*timerspeed;
			//Debug.Log((int)(timer*12));
			//Debug.Log(emis.GetBurst(0));
			ParticleSystem.Burst burst = emis.GetBurst(0);
			burst.count = (int)(timer*12);
			emis.SetBurst(0,burst);
			shap.arc = (int)(timer*12)*30;
			if(timer <= 0){
				//spawn the thing
				Transform Clone = Instantiate(BattleMaster.pl[1]);
				Clone.parent = transform.parent;
				Clone.position = transform.position;
				BattleMaster.attackcallback(0);
				Destroy(this.gameObject);
			}
			
		}
	}
}
