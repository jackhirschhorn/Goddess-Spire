using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class levelerupui : MonoBehaviour
{
	public Combatant comb;
	public int num;
	
	public void reset(){
		for(int i = 0; i < 10; i++){
			transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
		}
	}
	
	public void doer2(){
		StartCoroutine("doer");
	}
	
	public IEnumerator doer(){
		for(int i = 0; i < 10; i++){
			string txt = "";
			txt += (i==0?comb.statblock.hp:(i==1?comb.statblock.stam:(i==2?comb.statblock.mana:(i==3?comb.statblock.atk:(i==4?comb.statblock.def:(i==5?comb.statblock.mag:(i==6?comb.statblock.res:(i==7?comb.statblock.spd:(i==8?comb.statblock.lck:comb.statblock.lvl)))))))));
			transform.GetChild(1).GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = txt;
		}
		comb.lvlup(num);
		for(int i = 0; i < 10; i++){
			string txt = "";
			txt += (i==0?comb.statblock.hp:(i==1?comb.statblock.stam:(i==2?comb.statblock.mana:(i==3?comb.statblock.atk:(i==4?comb.statblock.def:(i==5?comb.statblock.mag:(i==6?comb.statblock.res:(i==7?comb.statblock.spd:(i==8?comb.statblock.lck:comb.statblock.lvl)))))))));
			transform.GetChild(1).GetChild(i).GetChild(3).GetComponent<TextMeshProUGUI>().text = txt;
		}
		for(int i = 0; i < 10; i++){
			//play a sound;
			yield return new WaitForSeconds(0.2f);
			transform.GetChild(1).GetChild(i).gameObject.SetActive(true);			
		}
	}
}
