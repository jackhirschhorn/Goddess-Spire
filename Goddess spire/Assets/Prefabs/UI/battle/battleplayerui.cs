using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class battleplayerui : MonoBehaviour
{
    public RectTransform hp, mana, stam;
	public TextMeshProUGUI hpt, manat, stamt;
	public Image head;
	public Combatant linked;
	
	void Awake(){
		if(linked != null){
			head.sprite = linked.icon;
			hpt.text = linked.statblock.chp + "/" + linked.statblock.hp;
			manat.text = linked.statblock.cmana + "/" + linked.statblock.mana;
			stamt.text = linked.statblock.cstam + "/" + linked.statblock.cstam;
			hp.offsetMin = new Vector2(100*((linked.statblock.chp*1f)/(linked.statblock.hp*1f)), hp.offsetMin.y);
			mana.offsetMin = new Vector2(100*((linked.statblock.cmana*1f)/(linked.statblock.mana*1f)), hp.offsetMin.y);
			stam.offsetMin = new Vector2(100*((linked.statblock.cstam*1f)/(linked.statblock.stam*1f)), hp.offsetMin.y);
		} else {
			transform.gameObject.SetActive(false);
		}
	}
	
	void LateUpdate(){
		if(linked != null){
			transform.gameObject.SetActive(true);
			head.sprite = linked.icon;		
			hpt.text = linked.statblock.chp + "/" + linked.statblock.hp;
			manat.text = linked.statblock.cmana + "/" + linked.statblock.mana;
			stamt.text = linked.statblock.cstam + "/" + linked.statblock.cstam;
			hp.offsetMin = new Vector2(100*((linked.statblock.chp*1f)/(linked.statblock.hp*1f)), hp.offsetMin.y);
			mana.offsetMin = new Vector2(100*((linked.statblock.cmana*1f)/(linked.statblock.mana*1f)), hp.offsetMin.y);
			stam.offsetMin = new Vector2(100*((linked.statblock.cstam*1f)/(linked.statblock.stam*1f)), hp.offsetMin.y);
		} else {
			transform.gameObject.SetActive(false);
		}
	}
}
