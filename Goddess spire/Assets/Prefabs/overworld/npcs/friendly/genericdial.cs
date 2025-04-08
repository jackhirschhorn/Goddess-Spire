using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="genericdial")]
public class genericdial : dialtree
{
    public override string startdial(){
		dialpointer = 0;
		return dials[dialpointer];
	}
	
	
	
}
