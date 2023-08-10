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
	
	public List<combatoption> class_CO = new List<combatoption>();
	public List<combatoption> weapon_CO = new List<combatoption>();
	
	public int BMM = 4000; //battleMenuMemory;
	public int[] BSMM = {0,0,0,0}; //battleSubMenuMemories;
	
	public Color red;
	public Color blue;
	public Color green;
	public float height = 1.5f;
	
	public void Awake(){
		enemyHP = Instantiate(enemyHP);
		enemyHP.parent = transform;
		enemyHP.position = transform.position + new Vector3(0,height,0);
		HP = enemyHP.GetChild(0) as RectTransform;
		hpt = enemyHP.GetChild(1).GetComponent<TextMeshProUGUI>();
		debug();
	}
	
	public void LateUpdate(){
		if(show_HP){
			enemyHP.gameObject.SetActive(true);
			hpt.text = statblock.chp + "/" + statblock.hp;
			HP.offsetMin = new Vector2(200*((statblock.chp*1f)/(statblock.hp*1f)), HP.offsetMin.y);
			
		} else {
			enemyHP.gameObject.SetActive(false);
		}
	}
	
	public void take_damage(int i, int i2){
		//damage reduction logic goes here
		statblock.chp -= i;
		if(i == 0)Debug.Log("took no damage!");
		
	}
	
	public void debug(){
		class_CO.Add(new Magic_Missile());
	}
	
}
