using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="equipment")]
public class equipment : itemscript
{
    public enum equiptype {
		amulet,
		helm,
		cloak,
		weapon,
		shield,
		armor,
		ring,
		belt,
		misc,
		foot
	}	
	public equiptype etype;
	public bool istwohand = false;
	public List<combatoption> combops = new List<combatoption>();
	
	public virtual void onequip(){
		
	}
	
	public virtual void onunequip(){
		
	}
	
	public virtual void checkpassive(){
		
	}
	
	
}
