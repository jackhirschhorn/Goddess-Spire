using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemypath : navigatorlist
{
	public float detectrange = 3f;
	
    public virtual void FixedUpdate(){
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
				overworldmanager.OM.gotobattle();
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
}
