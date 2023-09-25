using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


[CreateAssetMenu(fileName ="useitem")]
public class useitem : combatoption
{
    public useitem(){
		background = Color.white;
		nme = "REPLACE THIS TEXT WITH ITEM NAME";
		explain = "REPLACE THIS TEXT WITH ITEM USE";
		cost = 1;
		costype = 3;//0 = mana, 1 = stam, 2 = hp, 3 = item
	}
	
	public itemscript its;
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		tempac = anim.runtimeAnimatorController as AnimatorController;
		if(BattleMaster.BM.initiative[BattleMaster.BM.roundturn].humanoid)anim.runtimeAnimatorController = ac as RuntimeAnimatorController;
		anim.SetInteger("stage",1);
		if(!BattleMaster.BM.initiative[BattleMaster.BM.roundturn].humanoid)anim.SetInteger("atkanim",-2);
		anim.gameObject.AddComponent(typeof(useitemmono));
		anim.transform.GetComponent<useitemmono>().anim = anim;
		anim.transform.GetComponent<useitemmono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<useitemmono>().tempac = tempac;
		anim.transform.GetComponent<useitemmono>().its = its;
	}
	
	public override void nevermind(){
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
		anim.transform.GetComponent<useitemmono>().stage = -1;
	}
	
	public override void dothething(){
		anim.transform.GetComponent<useitemmono>().stage = 1;
	}
}
