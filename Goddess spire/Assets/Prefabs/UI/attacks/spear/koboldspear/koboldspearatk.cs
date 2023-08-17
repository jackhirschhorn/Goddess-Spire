using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="koboldspearatk")]
public class koboldspearatk : combatoption
{
	public koboldspearatk(){
		iconid = 4;
		background = Color.white;
		nme = "Spear Charge";
		explain = "Press [A] or [D] to tug the cursor into the green zone";
		cost = 0;
		costype = 1;//0 = mana, 1 = stam, 2 = hp
	}
	
	Animator anim;
	
	public override void dothething(){
		
	}
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		anim.SetInteger("atkanim",1);
		anim.SetInteger("stage",1);
	}
	
	public override void nevermind(){
		anim.SetInteger("atkanim",0);
		anim.SetInteger("stage",0);
	}
}
