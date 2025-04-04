using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npctalker : MonoBehaviour
{
    public Image icon;
	public dialtree dt;
	
	public virtual void opendial(){
		//called from playercontroller, open UI and pause gameplay
	}
	
	public virtual void closedial(){
		//called from either cancel button or dialtree, close UI and resume gameplay
	}
	
	public virtual void advancedial(){
		//press A to continue speaking, just say the next string, increase dialtree dialpointer by 1
	}
	
	public virtual void choosedial(int i){
		// chosing what to say, increase dialtree by the decision #, apply any flags, display.
	}
}
