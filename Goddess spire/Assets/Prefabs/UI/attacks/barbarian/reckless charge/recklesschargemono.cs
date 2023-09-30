using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class recklesschargemono : MonoBehaviour
{
    public Animator anim;
	public Combatant comb;
	public int stage = 0;
	public Transform clone;
	public Combatant target;
	public float timer = 0;
	public float speed = 9;
	public Vector3 start = new Vector3(0,0,0);
	public int damage = 0;
	public int pierce = 0;
	public AnimatorController tempac;
	public bool timedhit = false;
	public bool hit = false;
	public bool tooearly = false;
	public bool firstframe = true;
	
	// Start is called before the first frame update
    void Start()
    {
        target = BattleMaster.BM.meleetarg(!comb.isPC);
    }

    // Update is called once per frame
    void Update()
    {
        if(stage == 0){
		} else if (stage == -1){
			Destroy(this);
		} else if (stage == 1){
			if(anim.GetInteger("stage") < 2)BattleMaster.makesoundtokill(1);
			
			anim.SetInteger("stage",2);
			
			if(timer >= 1){
				anim.SetInteger("stage",3);
				if(!hit){
					damage = comb.statblock.atk + (timedhit?2:0);
					pierce = 0;
					target.take_damage(damage,pierce,2);
					if(timedhit){
						target.apply_status(new prone(target, target.transform.GetChild(0).GetChild(0).GetComponent<Animator>()), 30);
					}
					//target knocked prone;
					hit = true;
					Destroy(clone.gameObject);
				}
			}
			if(Vector3.Distance(anim.transform.position, target.transform.position) < 2f && timer < 1){
				if(Input.GetKeyDown(KeyCode.E) && !tooearly && !timedhit){
					timedhit = true;					
					BattleMaster.makesound(9);
				}
				if(clone == null){			
					clone = Instantiate(BattleMaster.pl[22]);
					clone.position = target.transform.position + new Vector3(-2f,3f,0);
				}
			} else if (!firstframe) {
				if(Input.GetKeyDown(KeyCode.E) && !tooearly){
					tooearly = true;
					BattleMaster.makesound(10);
				}
			}
			if((Vector3.Distance(anim.transform.position, target.transform.position) < 1.5f && timer > 1) || timer < 1){
				timer += (speed * Time.deltaTime)/Vector3.Distance(anim.transform.parent.position, target.transform.position);
				anim.transform.position = Vector3.LerpUnclamped(new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z), new Vector3(target.transform.position.x,anim.transform.position.y,target.transform.position.z), timer);  
			} else {
				stage = 2;
				anim.SetInteger("stage",4);
				start = anim.transform.position;
				BattleMaster.killsound();
				timer = -0.2f*1.9f;
			}
			firstframe = false;
		} else if (stage == 2){
			timer += Time.deltaTime*1.9f;
			anim.transform.position = Vector3.Lerp(start, new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z), timer); 
			if(timer >= 1){
				anim.transform.position = new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z);
				BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
				BattleMaster.attackcallback(0);
				BattleMaster.makesound(2);
				Destroy(this);
			}
		}
    }
}
