using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemypath : navigatorlist
{
	public float detectrange = 3f;
	public bool isundead = false;
	public int state = 0; //0 = normal, 1 = stun, 2 = fear
	public float stateduration = 0;
	
    public virtual void FixedUpdate(){
		stateduration -= Time.fixedDeltaTime;
		if(stateduration < 0)state = 0;
		if(target == null){//find target
			target = connections[0].transform;
			targ = 0;
		}
		if(Vector3.Distance(overworldmanager.OM.pc.transform.position,transform.position) < detectrange){
			target = overworldmanager.OM.pc.transform;
		}
		//lose interest after certain time
		
		if(target == overworldmanager.OM.pc.transform){
			if(Vector3.Distance(overworldmanager.OM.pc.transform.position,transform.position) < 2){
				//combat load
				if(state != 0)overworldmanager.OM.battlestate(state);
				overworldmanager.OM.gotobattle(transform.GetComponent<combatantdataholder>().cd, target.GetComponent<combatantdataholder>());
				gameObject.SetActive(false);
			}
		}
		
		timer -= Time.fixedDeltaTime;
		if(timer < 0){
			transform.LookAt(new Vector3(target.position.x,transform.position.y,target.position.z));
			cc.velocity = transform.forward*8;
			if(target == overworldmanager.OM.pc.transform){
				
			} else {
				if(Vector3.Distance(transform.position,target.position) < 5.5f){
					timer = connectionswait[targ];
					targ++;
					if(targ > connections.Count-1)targ = 0;
					target = connections[targ].transform;
				}
			}
		}
	}
	
	public void enterbattle(){
		overworldmanager.OM.gotobattle(transform.GetComponent<combatantdataholder>().cd, target.GetComponent<combatantdataholder>());
		gameObject.SetActive(false);
	}
}
