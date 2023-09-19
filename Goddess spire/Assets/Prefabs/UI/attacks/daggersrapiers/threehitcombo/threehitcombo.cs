using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="threehitcombo")]
public class threehitcombo : combatoption
{
    public threehitcombo(){
		background = Color.white;
		nme = "Three hit combo";
		explain = "Press [E] as you strike to keep the combo going!";
		cost = 0;
		costype = 1;//0 = mana, 1 = stam, 2 = hp
	}
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		tempac = anim.runtimeAnimatorController as AnimatorController;
		anim.runtimeAnimatorController = ac as RuntimeAnimatorController;
		anim.SetInteger("stage",1);
		anim.gameObject.AddComponent(typeof(threehitcombomono));
		anim.transform.GetComponent<threehitcombomono>().anim = anim;
		anim.transform.GetComponent<threehitcombomono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<threehitcombomono>().tempac = tempac;
	}
	
	public override void nevermind(){
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
		anim.transform.GetComponent<threehitcombomono>().stage = -1;
	}
	
	public override void dothething(){
		anim.transform.GetComponent<threehitcombomono>().stage = 1;
	}
}
