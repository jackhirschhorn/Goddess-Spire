using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="fade")]
public class fade : combatoption
{
    public fade(){
		background = Color.white;
		nme = "fade";
		explain = "{PLACEHOLDER} Turn invisible for one turn, evading all attacks.";
		cost = 3;
		costype = 0;//0 = mana, 1 = stam, 2 = hp
	}
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		tempac = anim.runtimeAnimatorController as AnimatorController;
		anim.runtimeAnimatorController = ac as RuntimeAnimatorController;
		anim.SetInteger("stage",1);
		anim.gameObject.AddComponent(typeof(fademono));
		anim.transform.GetComponent<fademono>().anim = anim;
		anim.transform.GetComponent<fademono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<fademono>().tempac = tempac;
	}
	
	public override void nevermind(){
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
		anim.transform.GetComponent<fademono>().stage = -1;
	}
	
	public override void dothething(){
		anim.transform.GetComponent<fademono>().stage = 1;
	}
}
