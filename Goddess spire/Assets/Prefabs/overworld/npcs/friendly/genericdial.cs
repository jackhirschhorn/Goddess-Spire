using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="genericdial")]
public class genericdial : dialtree
{
    public override string startdial(){
		dialpointer = 0;
		overworldmanager.OM.adddialoptions(new string[]{dials[dialpointer+1],dials[dialpointer+2]},new int[]{3,4}, talker);
		return dials[dialpointer];
	}
	
	public override string advancedial(){
		if(dialpointer == 0) return dials[dialpointer];
		dialpointer++;
		if(dialpointer >= 3){
			closedial();
			return "";
		}
		return dials[dialpointer];
	}
	
}
