using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.InputSystem;

public class fireboltmono : MonoBehaviour
{
	public Animator anim;
	public Combatant comb;
	public int stage = 0;
	public AnimatorController tempac;
	public Transform clone;
	public Combatant target;
	public Transform clone2;
	public List<Combatant> targs = new List<Combatant>();
	public int curtarg = 0;
	public bool first = true;
	
    // Start is called before the first frame update
    void Start()
    {
        clone = Instantiate(BattleMaster.pl[25]);
		clone.position = comb.transform.position + new Vector3(1.5f,4f,0);
		clone.GetComponent<thermometer>().comb = comb;
		targs = BattleMaster.gettargs(!comb.isPC);
		clone.parent = BattleMaster.BM.transform;
    }
	
	bool ddown,adown = false;

	public void OnMove2(InputAction.CallbackContext context){ //WASD
		Vector2 vec = context.ReadValue<Vector2>();
		if(vec.y > 0){//w
		
		}
		if(vec.y < 0){//s
		
		}
		if(vec.x > 0){//d
			ddown = true;
			adown = false;
		}
		if(vec.x < 0){//a
			ddown = false;
			adown = true;
		}
		if(vec == Vector2.zero){
			ddown = false;
			adown = false;
			wasdtimer = -1;
		}
	}
	
	public void OnConfirm2(InputAction.CallbackContext context){ //e
		if(stage == 1){
			if(context.performed){
				if(!first){
					target = targs[curtarg];
					stage = 2;
					BattleMaster.makesound(10);
					Destroy(clone2.gameObject);				
				}
			}
		}
	}

	float wasdtimer = 0f;
    // Update is called once per frame
    void Update()
    {
        if(stage == 0){
		} else if(stage == -1){
			clone.GetComponent<thermometer>().stage = -1;
			Destroy(this);
		} else if (stage == 1){
			if(clone2 == null)clone2 = Instantiate(BattleMaster.pl[26]);
			wasdtimer -= Time.deltaTime;
			if(wasdtimer < 0){
				if(adown){
					curtarg -= 1;
					if(curtarg < 0)curtarg = targs.Count-1;
					wasdtimer = 0.35f;
				} else if (ddown){
					curtarg += 1;
					if(curtarg >= targs.Count)curtarg = 0;
					wasdtimer = 0.35f;				
				}
			}
			clone2.position = targs[curtarg].transform.position;
			first = false;
		} else if (stage == 2){
			if(clone.GetComponent<thermometer>().stage == 0)clone.GetComponent<thermometer>().stage = 1;
			if(clone.GetComponent<thermometer>().stage == 2)stage = 3;
			if(clone.GetComponent<thermometer>().stage == 3)stage = 4;
		} else if (stage == 3){
				target.take_damage(clone.GetComponent<thermometer>().damage+comb.magicdamage(3), 0, 3);
				BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
				Destroy(clone.gameObject);
				BattleMaster.attackcallback(0);	
				Destroy(this);
		} else if (stage == 4){			
				comb.take_damage(comb.magicdamage(3)+4, 0, 3);
				BattleMaster.BM.initiative[BattleMaster.BM.roundturn].resetanim(tempac as RuntimeAnimatorController);
				Destroy(clone.gameObject);
				BattleMaster.attackcallback(0);
				Destroy(this);
			
		}
    }
}
