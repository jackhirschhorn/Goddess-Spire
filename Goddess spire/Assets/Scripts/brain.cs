using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class brain : ScriptableObject
{
    public List<combatoption> CO = new List<combatoption>();
	
	public virtual combatoption pickoption(){
		if(statuscheck()){
			return null;
		}
		return CO[UnityEngine.Random.Range(0,CO.Count)];
	}
	
	public virtual bool statuscheck(){
		foreach(statusmono sm in BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetComponents<statusmono>()){
			if(sm.sts.id == 0){
				sm.sts.onturnstart();
				return true;
				
			}
		}
		return false;
	}
}
