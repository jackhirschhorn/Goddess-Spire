using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powergem : wizardtabletnode
{
	//don't have their power reduced when split
	void Awake(){
		for(int i = 0; i < 8; i++){
			powerout[i] = power;
		}
	}
	
	public override void FixedUpdate(){
		base.FixedUpdate();
		for(int i = 0; i < 8; i++){
			if(nodes[i] != null){
			   nodes[i].connect(power, i);
			}
		}
	}
}
