using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="koboldspit")]
public class koboldspit : combatoption
{
    public koboldspit(){
		//iconid = 6;
		background = Color.white;
		nme = "Elemental spit";
		explain = "Press and hold [Space] to charge and release for an elemental blast. grows in power as you charge. don't hold it too long!";
		cost = 2;
		costype = 0;//0 = mana, 1 = stam, 2 = hp
	}
		
	Animator anim;
	public List<Color> cols = new List<Color>();
	
	public override void dothething(){
		anim.transform.GetComponent<koboldspitmono>().stage = 1;
	}
	
	public override void demothething(){
		anim = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		anim.SetInteger("atkanim",3);
		anim.SetInteger("stage",1);
		anim.gameObject.AddComponent(typeof(koboldspitmono));
		anim.transform.GetComponent<koboldspitmono>().anim = anim;
		anim.transform.GetComponent<koboldspitmono>().comb = BattleMaster.BM.initiative[BattleMaster.BM.roundturn];
		anim.transform.GetComponent<koboldspitmono>().cols = cols;
	}
	
	public override void nevermind(){
		anim.SetInteger("atkanim",0);
		anim.SetInteger("stage",0);
		anim.transform.GetComponent<koboldspitmono>().stage = -1;
	}
}
