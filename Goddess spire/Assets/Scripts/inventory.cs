using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
	public static inventory inv;
	public List<itemscript> invitems = new List<itemscript>();
	
	void Awake(){
		inv = this;
		List<itemscript> invitemsass = new List<itemscript>();
		foreach(itemscript its in invitems){
			invitemsass.Add(Instantiate(its));
		}
		invitems = invitemsass;
	}
}
