using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class status
{
    public bool holdturn = false;
	public Combatant comb;
	public Animator anim;
	public bool removests = false;
	public int id = 0;

    public virtual void init(){
		
	}
	
	public virtual void onturnstart(){
		
	}
}
