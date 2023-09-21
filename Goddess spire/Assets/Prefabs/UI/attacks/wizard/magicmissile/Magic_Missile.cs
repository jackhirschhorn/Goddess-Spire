using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="magicmissile")]
public class Magic_Missile : combatoption
{
	public Magic_Missile(){
		//iconid = 3;
		background = new Color(1,1,1,1);
		nme = "Magic Missile";
		explain = "Click on the magic circle and drag the missile into the target.";
		cost = 2;
		costype = 0;//0 = mana, 1 = stam, 2 = hp
	}
	
	public Transform clone;
	
	public override void dothething(){
		clone.GetComponent<dragattack>().activate();
	}
	
	public override void demothething(){
		clone = GameObject.Instantiate(BattleMaster.pl[0]);
		clone.parent = BattleMaster.attackst;
		clone.position = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.position + new Vector3(2f,3f,0);
		clone.GetComponent<dragattack>().dam = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].magicdamage(11);
		clone.GetComponent<dragattack>().damtype = 10;
	}
	
	public override void nevermind(){
		GameObject.Destroy(clone.gameObject);
	}
}
