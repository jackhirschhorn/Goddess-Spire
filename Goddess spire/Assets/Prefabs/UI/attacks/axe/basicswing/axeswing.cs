using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="axeswing")]
public class axeswing : combatoption
{
    public axeswing(){
		background = Color.white;
		nme = "Axe Chop";
		explain = "Rapidly Press [E] to increase damage!";
		cost = 0;
		costype = 0;//0 = mana, 1 = stam, 2 = hp
	}
	
	public AnimatorController ac;
	public AnimatorController tempac;
	Animator anim;
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		tempac = anim.runtimeAnimatorController as AnimatorController;
		anim.runtimeAnimatorController = ac as RuntimeAnimatorController;
		anim.SetInteger("stage",1);
		anim.gameObject.AddComponent(typeof(axeswingmono));
		anim.transform.GetComponent<axeswingmono>().anim = anim;
		anim.transform.GetComponent<axeswingmono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<axeswingmono>().tempac = tempac;
	}
	
	public override void nevermind(){
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
		anim.transform.GetComponent<axeswingmono>().stage = -1;
	}
	
	public override void dothething(){
		anim.transform.GetComponent<axeswingmono>().stage = 1;
	}
	
	
}
