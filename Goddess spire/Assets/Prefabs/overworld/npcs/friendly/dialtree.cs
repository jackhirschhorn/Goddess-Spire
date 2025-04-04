using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialtree 
{
    public int[] flags = new int[]{};
	
	public string[] dials = new string[]{};
	public int dialpointer = 0;
	public npctalker talker;
	
	public virtual string startdial(){
		return dials[dialpointer];
	}
	
	public virtual string advancedial(){
		dialpointer++;
		if(dialpointer >= dials.Length){
			closedial();
			return "";
		}
		return dials[dialpointer];
	}
	
	public virtual void closedial(){
		talker.closedial();
	}
	
}
