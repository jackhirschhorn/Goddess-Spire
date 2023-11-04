using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicator : fixture
{
    public GameObject item;
	public wizardtabletnode node; //probably need a go between later
	
	void FixedUpdate(){
		if(node.power > 0){
			item.SetActive(true);
		} else{
			item.SetActive(false);
		}
	}
}
