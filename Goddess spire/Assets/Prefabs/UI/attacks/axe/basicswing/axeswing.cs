using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="axeswing")]
public class axeswing : combatoption
{
    public axeswing(){
		background = Color.white;
		nme = "Axe Chop";
		explain = "Press and hold [Space] to charge and release for an elemental blast. grows in power as you charge. don't hold it too long!";
		cost = 2;
		costype = 0;//0 = mana, 1 = stam, 2 = hp
	}
	
	public override void demothething(){
		
	}
	
	public override void nevermind(){
		
	}
	
	public override void dothething(){
		
	}
	
	
}
