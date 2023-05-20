using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Missile : combatoption
{
	public Magic_Missile(){
		iconid = 3;
		background = new Color(1,1,1,1);
		nme = "Magic Missile";
	}
	
	public Transform clone;
	
	public override void dothething(){
		clone.GetComponent<dragattack>().activate();
	}
	
	public override void demothething(){
		clone = GameObject.Instantiate(BattleMaster.pl[0]);
		clone.parent = BattleMaster.attackst;
		clone.position = Vector3.zero;
	}
	
	public override void nevermind(){
		GameObject.Destroy(clone.gameObject);
	}
}
