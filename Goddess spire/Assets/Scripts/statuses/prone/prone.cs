using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prone : status
{
	public prone(Combatant c, Animator a){
		comb = c;
		anim = a;
		id = 0;
	}
	
	public prone(){
		id = 0;
	}
	
	public override void init(){
		anim.SetBool("prone",true);
	}
	
    public override void onturnstart(){
		anim.SetBool("prone",false);
		removests = true;
	}
}
