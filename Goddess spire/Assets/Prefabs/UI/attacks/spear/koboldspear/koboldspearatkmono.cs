using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koboldspearatkmono : MonoBehaviour
{
    public int stage = 0;
	public int damage = 0;
	public int pierce = 0;
	public Animator anim;
	public Combatant target;
	
	public void Update(){
		if(stage == 0){
			//determine damage and pierce with action command
			damage = 2;
			pierce = 1;
			stage = 1;
		} else if(stage == 1){
			anim.SetInteger("stage",2);
			//move from start to target
		}
	}
}
