using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class threehitcombomono : animredirect
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
	public bool missed = true;
	public bool missed2 = false;
	
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
			if(anim.GetInteger("stage") != 2)BattleMaster.makesoundtokill(1);
			
			anim.SetInteger("stage",2);
			if(Vector3.Distance(anim.transform.position, target.transform.position) > 2f){
				timer += (speed * Time.deltaTime)/Vector3.Distance(anim.transform.parent.position, target.transform.position);
				anim.transform.position = Vector3.Lerp(new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z), new Vector3(target.transform.position.x,anim.transform.position.y,target.transform.position.z), timer);  
			} else {
				stage = 2;
				anim.SetInteger("stage",3);
				start = anim.transform.position;
				BattleMaster.killsound();
			}
		} else if (stage == 2){
			damage = comb.weapondamage();
			pierce = comb.pierce();
			if(Input.GetKeyDown(KeyCode.E)){
				stage = 4;
				missed2 = true;
				anim.SetInteger("stage",4);
				BattleMaster.makesound(10);
			}
		} else if (stage == 3){
			didit = false;
			if(Input.GetKeyDown(KeyCode.E)){
				target.take_damage(damage,pierce,1);
				missed = false;
				BattleMaster.makesound(9);
			}
		} else if (stage == 4){
			if(clone != null)Destroy(clone.gameObject);
			if(missed){
				//miss
				target.take_damage(damage,pierce,1);
				stage = 11;
				timer = -1.9f;
				anim.SetInteger("stage",4);
			} else {
				didit = false;
				stage = 5;
				missed = true;
			}
		} else if (stage == 5){
			if(Input.GetKeyDown(KeyCode.E)){
				stage = 7;
				missed2 = true;
				anim.SetInteger("stage",4);
			}
		} else if (stage == 6){
			didit = false;
			if(Input.GetKeyDown(KeyCode.E)){
				target.take_damage(damage,pierce,1);
				missed = false;
			}
		} else if (stage == 7){
			if(clone != null)Destroy(clone.gameObject);
			if(missed){
				//miss
				target.take_damage(damage,pierce,1);
				stage = 11;
				timer = -1.9f;
				anim.SetInteger("stage",4);
			} else {
				didit = false;
				stage = 8;
				missed = true;
			}
		} else if (stage == 8){
			if(Input.GetKeyDown(KeyCode.E)){
				stage = 10;
				missed2 = true;
				anim.SetInteger("stage",4);
			}
		} else if (stage == 9){
			didit = false;
			if(Input.GetKeyDown(KeyCode.E)){
				target.take_damage(damage+2,pierce+2,0);
				missed = false;
			}
		} else if (stage == 10){
			if(clone != null)Destroy(clone.gameObject);
			if(missed){
				//miss
				target.take_damage(damage,pierce,0);
				stage = 11;
				timer = 0f;
				anim.SetInteger("stage",4);
			} else {
				stage = 11;
				timer = 0f;
			}
		} else if (stage == 11){
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
	
	public bool didit = false;
	public override void directed(float f){
		if(!didit && !missed2){
			didit = true;	
			timer = 0f;
			if(stage == 2 || stage == 5 || stage == 8){
				clone = Instantiate(BattleMaster.pl[22]);
				clone.position = target.transform.position + new Vector3(-2f,3f,0);
				BattleMaster.makesound(9);
			}
			stage += 1;		
		}		
	}
}
