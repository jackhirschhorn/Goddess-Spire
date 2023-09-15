using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class katanacutmono : MonoBehaviour
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
	public bool missed = false;
	
    // Start is called before the first frame update
    void Start()
    {
        target = BattleMaster.BM.meleetarg(!comb.isPC);
    }

    // Update is called once per frame
    void Update()
    {
        if(stage == 0){
		} else if(stage == -1){			
			Destroy(this);
		} else if(stage == 1){
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
				BattleMaster.makesound(11);
				timer = Random.Range(1,6);
			}
		} else if(stage == 2){
			//sound
			timer -= Time.deltaTime;
			if(timer <= 0 && clone == null){
				clone = Instantiate(BattleMaster.pl[22]);
				clone.position = target.transform.position + new Vector3(-2f,5f,0);				
				BattleMaster.makesound(14);
			}
			if(Input.GetKeyDown(KeyCode.E)){
				if(timer > 0){
					//miss animation?
					damage = 0;
					pierce = 0;
					stage = 3;
					anim.SetInteger("stage",5);
					missed = true;
				} else if (timer <= 0 && timer >= -0.3f){
					damage = comb.weapondamage() + 5;
					pierce = 5;
					stage = 3;
					anim.SetInteger("stage",4);
				} else if (timer <= -0.3f && timer >= -0.6f){
					damage = comb.weapondamage() + 2;
					pierce = 2;
					stage = 3;
					anim.SetInteger("stage",4);
				} else if (timer <= -0.6f && timer >= -1f){
					damage = comb.weapondamage();
					pierce = 0;
					stage = 3;
					anim.SetInteger("stage",4);
				} else if (timer <= -1f){
					damage = 0;
					pierce = 0;
					stage = 3;
					anim.SetInteger("stage",5);
					missed = true;
				}
			}
		} else if (stage == 3){
			if(clone != null)Destroy(clone.gameObject);
			if(!missed)target.take_damage(damage,pierce,1);
			anim.SetInteger("stage",6);
			timer = (missed?-1.9f:-2.3f);
			stage = 4;
		} else if (stage == 4){
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
