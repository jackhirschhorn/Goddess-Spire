using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class itemscript : ScriptableObject
{
	public bool done = false;
	public int targtype = 0; //0 = self only, 1 = target, 2 = area
	public int count = 0;
	public ScriptableObject user;
	public bool mat = false; //material, not for combat
	public Sprite icon;
	public string name = "";
	public string desc = "";
	
    public virtual void useitem(){
		done = true;
	}
	
	public virtual void useitem(Vector3 v){
		done = true;		
	}
	
	public virtual void useitem(Combatant c){
		done = true;		
	}
	
	public virtual IEnumerator animwait(){
		yield return new WaitForEndOfFrame();
		done = true;
	}
}
