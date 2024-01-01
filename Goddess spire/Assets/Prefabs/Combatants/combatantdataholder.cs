using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatantdataholder : MonoBehaviour
{
    public List<combatantdata> cdass = new List<combatantdata>();
	public List<combatantdata> cd = new List<combatantdata>();
	
	public virtual void Awake(){
		foreach(combatantdata c in cdass){
			cd.Add(Instantiate(c));
		}
	}
}
