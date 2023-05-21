using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CO_test : combatoption
{
    public CO_test(){
		iconid = 0;
		background = new Color(1,1,1,1);
		nme = "test option";
		explain = "this is a test option";
		cost = 0;
		costype = 0;//0 = mana, 1 = stam, 2 = hp
	}
	
	public override void dothething(){
		Debug.Log("this is you, doing the thing");
	}
	
	public override void demothething(){
		Debug.Log("this is a preview of the thing you will do");
	}
	
	public override void nevermind(){
		Debug.Log("nevermind");
	}
}
