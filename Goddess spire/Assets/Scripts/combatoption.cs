using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class combatoption
{
    public int iconid = 0;
	public Color background = Color.white;
	public string nme = "";
	public string explain = "";
	public int cost;
	public int costype = 0;//0 = mana, 1 = stam, 2 = hp
	
	public virtual void dothething(){
		
	}
	
	public virtual void demothething(){
		
	}
	
	public virtual void nevermind(){
		
	}
}
