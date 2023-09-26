using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class defendmono : MonoBehaviour
{
    public Animator anim;
	public Combatant comb;
	public int stage = 0;
	public AnimatorController tempac;
	public float timer = 0;
	
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
			BattleMaster.makesound(16);
			stage = 2;
			BattleMaster.attackcallback(0);
		} else if (stage == 2){
			if(BattleMaster.BM.initiative[BattleMaster.BM.roundturn] == comb){	
				BattleMaster.makesound(9);
				stage = 3;
			}
		} else if (stage == 3){
			timer += Time.deltaTime;
			if(timer >= 1){
				if(comb.humanoid){
					//BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
					anim.SetBool("defend",false);
				} else {
					anim.SetInteger("atkanim",0);		
					anim.SetInteger("stage",0);
				}
				Destroy(this);				
			}
		}
    }
}
