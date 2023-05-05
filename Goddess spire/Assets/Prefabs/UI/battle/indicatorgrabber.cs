using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class indicatorgrabber : MonoBehaviour
{
    public Combatant comb;
	
	
	public void indicate(){
		comb.indicator.SetActive(true);
	}
	
	public void un_indicate(){
		comb.indicator.SetActive(false);
	}
}
