using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="lowpotionitm")]
public class lowpotion : itemscript
{
    public lowpotion(){
		targtype = 0;
	}
		Transform clone;
	public override void useitem(){
		clone = GameObject.Instantiate(BattleMaster.pl[27]);
		clone.position = BattleMaster.BM.initiative[BattleMaster.BM.roundturn].transform.position + new Vector3(0.8f,2f,0);
		//play item sound
		BattleMaster.BM.initiative[BattleMaster.BM.roundturn].heal(5);
	}
	
	public override IEnumerator animwait(){
		yield return new WaitForSeconds(1f);
		GameObject.Destroy(clone.gameObject);
		done = true;
	}
}
