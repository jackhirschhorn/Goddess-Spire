using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class firestrikemono : animredirect
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
		clone = Instantiate(BattleMaster.pl[23]);
		clone.position = target.transform.position + new Vector3(-2f,3f,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(stage == 0){
		} else if (stage == -1){
			clone.GetComponent<firestrikeui>().stage = -1;
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
				clone.GetComponent<firestrikeui>().stage = 1;
			}
		} else if (stage == 2){
			if(clone.GetComponent<firestrikeui>().stage == 2){
				anim.SetInteger("stage",4);
				stage = 3;
				damage = comb.weapondamage() + clone.GetComponent<firestrikeui>().atk*2;
				pierce = comb.pierce() + clone.GetComponent<firestrikeui>().pierce;
				clone.GetComponent<firestrikeui>().stage = -1;
			}
		} else if (stage == 3){
			
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
	
	public bool didit = false;
	public override void directed(float f){
		if(!didit){
			didit = true;
			stage = 4;
			target.take_damage(damage,pierce,3);
			anim.SetInteger("stage",5);
			timer = -1.9f;
		}		
	}
}
