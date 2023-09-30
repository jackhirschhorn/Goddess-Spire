using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusmono : MonoBehaviour
{
	
	public bool holdturn = false;
	public Combatant comb;
	public status sts;
	
	public void init(){
		sts.init();
	}
	
    // Update is called once per frame
    void LateUpdate()
    {
		if(sts.removests)Destroy(this);
        if(BattleMaster.BM.initiative[BattleMaster.BM.roundturn] == comb){	
			if(!holdturn){
				sts.onturnstart();
			}
			holdturn = true;
		} else {
			holdturn = false;
		}
    }
}
