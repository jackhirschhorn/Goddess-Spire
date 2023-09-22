using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koboldspearatkmono : attackmono
{
    public int stage = 0;
	public Animator anim;
	public Combatant target;
	public float speed = 9;
	public Vector3 start = new Vector3(0,0,0);
	public float timer = 0;
	public Combatant comb;
	
	Transform clone = null;
	
	public void Start(){
		comb = anim.transform.parent.parent.GetComponent<Combatant>();
		if(comb.isPC){
			clone = Instantiate(BattleMaster.pl[3]);
			clone.parent = BattleMaster.BM.combatmenu.parent;
			clone.position = new Vector3(0,0,0);
			clone.GetComponent<spearattackindicator>().atk = this;
			clone.GetComponent<spearattackindicator>().comb = comb;
		}
	}
	
	public void Update(){
		if(comb.isPC){
			if(stage == 0){			
			} else if(stage == 1){
				if(clone.GetComponent<spearattackindicator>()){
					clone.GetComponent<spearattackindicator>().stage = 1;
				} else {
					Destroy(clone.gameObject);
					stage = 2;
				}
				
			} else if(stage == 2){
				anim.SetInteger("stage",2);
				BattleMaster.makesoundtokill(1);
				//move from start to target
				target = BattleMaster.BM.meleetarg((anim.transform.parent.parent.GetComponent<Combatant>().isPC?false:true));
				stage = 3;
			} else if(stage == 3){
				if(Vector3.Distance(anim.transform.position, target.transform.position) > 1f){
					timer += (speed * Time.deltaTime)/Vector3.Distance(anim.transform.parent.position, target.transform.position);
					anim.transform.position = Vector3.Lerp(new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z), new Vector3(target.transform.position.x,anim.transform.position.y,target.transform.position.z), timer); 
				} else {
					BattleMaster.killsound();
					stage = 4;
					anim.SetInteger("stage",3);
					start = anim.transform.position;
					target.take_damage(damage,pierce,0);
					timer = 0;
				}
			} else if(stage == 4){
				timer += Time.deltaTime*1.9f;
				anim.transform.position = Vector3.Lerp(start, new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z), timer); 
				if(timer >= 1){
					anim.transform.position = new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z);
					anim.SetInteger("stage",-1);
					anim.SetInteger("atkanim",0);
					BattleMaster.attackcallback(0);
					BattleMaster.makesound(2);
					Destroy(this);
				}
			} else if (stage == -1){
				anim.SetInteger("stage",-1);
				anim.SetInteger("atkanim",0);
				Destroy(clone.gameObject);
				Destroy(this);
			}
		} else {
			if (stage == 1){
				timer += Time.deltaTime;
				if(timer >= 0.75f){
					anim.SetInteger("stage",2);
					BattleMaster.makesoundtokill(1);
					target = BattleMaster.BM.meleetarg((anim.transform.parent.parent.GetComponent<Combatant>().isPC?false:true));
					stage = 2;	
					timer = 0;
				}					
			} else if (stage == 2){
				if(Vector3.Distance(anim.transform.position, target.transform.position) > 1f){
					timer += (speed * Time.deltaTime)/Vector3.Distance(anim.transform.parent.position, target.transform.position);
					anim.transform.position = Vector3.Lerp(new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z), new Vector3(target.transform.position.x,anim.transform.position.y,target.transform.position.z), timer); 
				} else {
					BattleMaster.killsound();
					stage = 3;
					anim.SetInteger("stage",3);
					start = anim.transform.position;
					target.take_damage(comb.weapondamage() + 2,comb.pierce() + 2,0);
					timer = 0;
				}
			} else if (stage == 3){
				timer += Time.deltaTime*1.9f;
				anim.transform.position = Vector3.Lerp(start, new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z), timer); 
				if(timer >= 1){
					anim.transform.position = new Vector3(anim.transform.parent.position.x,anim.transform.position.y,anim.transform.parent.position.z);
					anim.SetInteger("stage",-1);
					anim.SetInteger("atkanim",0);
					BattleMaster.attackcallback(0);
					BattleMaster.makesound(2);
					Destroy(this);
				}
			}
		}
	}
}
