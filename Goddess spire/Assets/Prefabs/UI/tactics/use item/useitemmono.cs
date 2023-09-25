using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class useitemmono : MonoBehaviour
{
    public Animator anim;
	public Combatant comb;
	public int stage = 0;
	public AnimatorController tempac;
	public float timer = 0;
	public itemscript its;
	public bool once = true;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if(stage == 0){
			
		} else if (stage == -1){
			Destroy(this);
		} else if (stage == 1){
			timer += Time.deltaTime;
			//BattleMaster.makesound(16);
			if(timer >= 0.5f){
				stage = 2;
				timer = 0;
			}			
		} else if (stage == 2){
			if(its.done){	
				stage = 3;
			} else if(once) {
				if(its.targtype == 0){
					its.useitem();
					StartCoroutine(its.animwait());
				} else if(its.targtype == 0) { //target combatant
					//finish
				} else { // target area
					//finish
				}
				once = false;
			}
		} else if (stage == 3){
			its.done = false;
			timer += Time.deltaTime;
			if(timer >= 0.5f){
				if(comb.humanoid){
					BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
				} else {
					anim.SetInteger("atkanim",0);		
					anim.SetInteger("stage",0);
				}			
				BattleMaster.attackcallback(0);
				Destroy(this);				
			}
		}
    }
}
