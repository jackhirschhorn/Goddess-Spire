using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partystatic : combatantdataholder
{
    public static partystatic prty;
	
	public override void Awake(){
		base.Awake();
		prty = this;
	}
}
