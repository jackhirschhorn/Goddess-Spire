using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathredirect : animredirect
{
    
	
	public override void directed(float f){
		Transform clone = Instantiate(BattleMaster.pl[17]);
		clone.position = transform.position;
		Destroy(transform.parent.parent.gameObject);
	}
	
	public deathredirect() {
		
	}
}
