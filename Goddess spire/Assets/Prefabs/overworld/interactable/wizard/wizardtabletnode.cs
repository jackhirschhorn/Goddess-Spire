using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizardtabletnode : MonoBehaviour
{
    public int power = 0;
	public wizardtabletnode[] nodes = new wizardtabletnode[8];
	public int[] powerin = new int[8];
	public int[] powerout = new int[8];	
	// 0 1 2
	// 3 * 4
	// 5 6 7
	public int invertdirection(int i){
		return 7-i;
	}
	
	public virtual bool connect(int p,int d){ //power and direction sending TO
		if(p >= power)return true;
		return false;
	}
}
