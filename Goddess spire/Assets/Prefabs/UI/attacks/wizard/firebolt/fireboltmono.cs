using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(stage == 0){
		} else if(stage == -1){
			clone.GetComponent<thermometer>().stage = -1;
			Destroy(this);
		} else if (stage == 1){
			if(clone2 == null)clone2 = Instantiate(BattleMaster.pl[26]);
			if(Input.GetKeyDown(KeyCode.A)){
				curtarg -= 1;
				if(curtarg < 0)curtarg = targs.Count-1;
			} else if (Input.GetKeyDown(KeyCode.D)){
				curtarg += 1;
				if(curtarg >= targs.Count)curtarg = 0;				
			}
			clone2.position = targs[curtarg].transform.position;
			if(Input.GetKeyDown(KeyCode.E) && !first){
				target = targs[curtarg];
				stage = 2;
				BattleMaster.makesound(10);
				Destroy(clone2.gameObject);
				
			}
			first = false;
		} else if (stage == 2){
			if(clone.GetComponent<thermometer>().stage == 0)clone.GetComponent<thermometer>().stage = 1;
			if(clone.GetComponent<thermometer>().stage == 2)stage = 3;
			if(clone.GetComponent<thermometer>().stage == 3)stage = 4;
		} else if (stage == 3){
				target.take_damage(clone.GetComponent<thermometer>().damage+comb.magicdamage(3), 0, 3);
				Destroy(clone.gameObject);
				BattleMaster.attackcallback(0);	
				Destroy(this);
		} else if (stage == 4){
				Destroy(clone.gameObject);
				BattleMaster.attackcallback(0);
				Destroy(this);
			
		}
    }
}
