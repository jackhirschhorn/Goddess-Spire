using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

//[CreateAssetMenu(fileName ="firebolt")]
public class firebolt : combatoption
{
    public firebolt(){
		background = Color.white;
		nme = "firebolt";
		explain = "Pick a target, then press [E] to keep the heat up! don't overdo it, or else!";
		cost = 2;
		costype = 0;//0 = mana, 1 = stam, 2 = hp
	}
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		tempac = anim.runtimeAnimatorController as AnimatorController;
		anim.runtimeAnimatorController = ac as RuntimeAnimatorController;
		anim.SetInteger("stage",1);
		anim.gameObject.AddComponent(typeof(fireboltmono));
		anim.transform.GetComponent<fireboltmono>().anim = anim;
		anim.transform.GetComponent<fireboltmono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<fireboltmono>().tempac = tempac;
	}
	
	public override void nevermind(){
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
		anim.transform.GetComponent<fireboltmono>().stage = -1;
	}
	
	public override void dothething(){
		anim.transform.GetComponent<fireboltmono>().stage = 1;
	}
}
