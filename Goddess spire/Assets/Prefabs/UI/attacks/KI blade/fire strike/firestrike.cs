using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="firestrike")]
public class firestrike : combatoption
{
    public firestrike(){
		background = Color.white;
		nme = "Fire Strike";
		explain = "Press the indicated buttons before the timer is up!";
		cost = 3;
		costype = 1;//0 = mana, 1 = stam, 2 = hp
	}
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		tempac = anim.runtimeAnimatorController as AnimatorController;
		anim.runtimeAnimatorController = ac as RuntimeAnimatorController;
		anim.SetInteger("stage",1);
		anim.gameObject.AddComponent(typeof(firestrikemono));
		anim.transform.GetComponent<firestrikemono>().anim = anim;
		anim.transform.GetComponent<firestrikemono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<firestrikemono>().tempac = tempac;
	}
	
	public override void nevermind(){
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
		anim.transform.GetComponent<firestrikemono>().stage = -1;
	}
	
	public override void dothething(){
		anim.transform.GetComponent<firestrikemono>().stage = 1;
	}
}
