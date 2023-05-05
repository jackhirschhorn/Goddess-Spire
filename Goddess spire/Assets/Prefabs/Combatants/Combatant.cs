using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Combatant : MonoBehaviour
{
    public Stats statblock;
	public bool isPC = false;
	public Sprite icon;
	public GameObject indicator;
	public Transform enemyHP;
	public RectTransform HP;
	public TextMeshProUGUI hpt;
	public bool show_HP;
	
	public void Awake(){
		enemyHP = Instantiate(enemyHP);
		enemyHP.parent = transform;
		enemyHP.position = transform.position + new Vector3(0,1.5f,0);
		HP = enemyHP.GetChild(0) as RectTransform;
		hpt = enemyHP.GetChild(1).GetComponent<TextMeshProUGUI>();
	}
	
	public void LateUpdate(){
		if(show_HP){
			enemyHP.gameObject.SetActive(true);
			hpt.text = statblock.chp + "/" + statblock.hp;
			HP.offsetMin = new Vector2(100*((statblock.chp*1f)/(statblock.hp*1f)), HP.offsetMin.y);
			
		} else {
			enemyHP.gameObject.SetActive(false);
		}
	}
	
}
