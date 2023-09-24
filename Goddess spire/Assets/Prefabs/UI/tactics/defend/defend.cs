using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


[CreateAssetMenu(fileName ="defend")]
public class defend : combatoption
{
    public defend(){
		background = Color.white;
		nme = "Defend";
		explain = "Increase your Defence and Resistance by [1]";
		cost = 0;
		costype = 1;//0 = mana, 1 = stam, 2 = hp
	}
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		tempac = anim.runtimeAnimatorController as AnimatorController;
		if(BattleMaster.BM.initiative[BattleMaster.BM.roundturn].humanoid)anim.runtimeAnimatorController = ac as RuntimeAnimatorController;
		anim.SetInteger("stage",1);
		if(!BattleMaster.BM.initiative[BattleMaster.BM.roundturn].humanoid)anim.SetInteger("atkanim",-1);
		anim.gameObject.AddComponent(typeof(defendmono));
		anim.transform.GetComponent<defendmono>().anim = anim;
		anim.transform.GetComponent<defendmono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<defendmono>().tempac = tempac;
	}
	
	public override void nevermind(){
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
		anim.transform.GetComponent<defendmono>().stage = -1;
	}
	
	public override void dothething(){
		anim.transform.GetComponent<defendmono>().stage = 1;
	}
}
