using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="katanacut")]
public class katanacut : combatoption
{
    public katanacut(){
		background = Color.white;
		nme = "Unsheath";
		explain = "Press [E] when the indicator appears. Press to early or too late and you'll miss!";
		cost = 0;
		costype = 1;//0 = mana, 1 = stam, 2 = hp
	}
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		tempac = anim.runtimeAnimatorController as AnimatorController;
		anim.runtimeAnimatorController = ac as RuntimeAnimatorController;
		anim.SetInteger("stage",1);
		anim.gameObject.AddComponent(typeof(katanacutmono));
		anim.transform.GetComponent<katanacutmono>().anim = anim;
		anim.transform.GetComponent<katanacutmono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<katanacutmono>().tempac = tempac;
	}
	
	public override void nevermind(){
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
		anim.transform.GetComponent<katanacutmono>().stage = -1;
	}
	
	public override void dothething(){
		anim.transform.GetComponent<katanacutmono>().stage = 1;
	}
}
