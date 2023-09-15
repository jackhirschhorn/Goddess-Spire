using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="recklesscharge")]
public class recklesscharge : combatoption
{
    public recklesscharge(){
		background = Color.white;
		nme = "Reckless Charge";
		explain = "Press [E] right before striking the enemy for bonus damage and a chance to knock them prone!";
		cost = 0;
		costype = 1;//0 = mana, 1 = stam, 2 = hp
	}
	
	public AnimatorController ac;
	public AnimatorController tempac;
	Animator anim;
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		tempac = anim.runtimeAnimatorController as AnimatorController;
		anim.runtimeAnimatorController = ac as RuntimeAnimatorController;
		anim.SetInteger("stage",1);
		anim.gameObject.AddComponent(typeof(recklesschargemono));
		anim.transform.GetComponent<recklesschargemono>().anim = anim;
		anim.transform.GetComponent<recklesschargemono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<recklesschargemono>().tempac = tempac;
	}
	
	public override void nevermind(){
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
		anim.transform.GetComponent<recklesschargemono>().stage = -1;
	}
	
	public override void dothething(){
		anim.transform.GetComponent<recklesschargemono>().stage = 1;
	}
}
