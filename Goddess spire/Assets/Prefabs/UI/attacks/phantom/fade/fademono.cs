using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class fademono : MonoBehaviour
{
	
	public Animator anim;
	public Combatant comb;
	public int stage = 0;
	public AnimatorController tempac;
	public Transform clone;
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
			anim.SetInteger("stage",2);
			stage = 2;
			BattleMaster.attackcallback(0);
			clone = Instantiate(BattleMaster.pl[24]);
			clone.position = comb.transform.position + new Vector3(0.25f,-0.5f,0);
		} else if (stage == 2){
			if(BattleMaster.BM.initiative[BattleMaster.BM.roundturn] == comb){	
				BattleMaster.makesound(16);			
				anim.SetInteger("stage",3);
				stage = 3;
			}
		} else if (stage == 3){
			timer += Time.deltaTime;
			if(timer >= 1){
				BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
				Destroy(clone.gameObject);
				Destroy(this);				
			}
		}
    }
}
