using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersink : wizardtabletnode
{
    //links to outside object, which points to this
	void FixedUpdate(){
		power = 0;
		foreach(int i in powerin){
			power = (i > power?i:power);
		}
	}
	
	public override bool connect(int p, int d){
		powerin[invertdirection(d)] = p;
		return true;
	}
}
