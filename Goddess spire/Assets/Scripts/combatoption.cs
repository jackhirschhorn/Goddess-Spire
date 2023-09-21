using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


[Serializable]
public class combatoption : ScriptableObject
{
    public Sprite icon;
	public Color background = Color.white;
	public string nme = "";
	public string explain = "";
	public int cost;
	public int costype = 0;//0 = mana, 1 = stam, 2 = hp	
	public AnimatorController ac;
	public AnimatorController tempac;
	public Animator anim;
	public bool iswand = false;
		
	
	public virtual void dothething(){
		
	}
	
	public virtual void demothething(){
		
	}
	
	public virtual void nevermind(){
		
	}
}
