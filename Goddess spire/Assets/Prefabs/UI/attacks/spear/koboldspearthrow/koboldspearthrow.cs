using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="koboldspearthrow")]
public class koboldspearthrow : combatoption
{
   public koboldspearthrow(){
		//iconid = 5;
		background = Color.white;
		nme = "Spear Throw";
		explain = "Press [A] or [D] to aim, then hold [W] to increase power.";
		cost = 2;
		costype = 1;//0 = mana, 1 = stam, 2 = hp
	}
	
	
	public override void dothething(){
		anim.transform.GetComponent<koboldspearthrowmono>().stage = 1;
		BattleMaster.makesoundtokill(3);
	}
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		anim.SetInteger("atkanim",2);
		anim.SetInteger("stage",1);
		anim.gameObject.AddComponent(typeof(koboldspearthrowmono));
		anim.transform.GetComponent<koboldspearthrowmono>().anim = anim;
		anim.transform.GetComponent<koboldspearthrowmono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
	}
	
	public override void nevermind(){
		anim.SetInteger("atkanim",0);
		anim.SetInteger("stage",0);
		anim.transform.GetComponent<koboldspearthrowmono>().stage = -1;
	}
}
