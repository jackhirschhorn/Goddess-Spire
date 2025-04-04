using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericdial : dialtree
{
    public override string startdial(){
		dialpointer = 0;
		return dials[dialpointer];
	}
	
	
	
}
