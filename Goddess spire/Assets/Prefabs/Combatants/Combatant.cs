using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Combatant : MonoBehaviour
{
	public int phenotype = 0;
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
	
	public Animator anim;
	public int idleanim = 0;
	
	public void Awake(){
		enemyHP = Instantiate(enemyHP);
		enemyHP.parent = transform;
		enemyHP.position = transform.position + new Vector3(0,height,0);
		HP = enemyHP.GetChild(0) as RectTransform;
		hpt = enemyHP.GetChild(1).GetComponent<TextMeshProUGUI>();
		//debug();
		anim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		if(idleanim != 0){
			anim.SetInteger("weapon",idleanim);
		}
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
	
	public void take_damage(int i, int i2, int i3){
		//damage reduction logic goes here
		//i = damage, i2 = pierce, i3 = damagetype;
		//damagetype, 0 = pierce, 1 = slash, 2 = bash, 3 = fire, 4 = ice, 5 = lightning, 6 = acid, 7 = wind, 8 = light, 9 = dark, 10 = arcane
		int totdam = i;
		totdam = (int)Mathf.Floor(i * statblock.resistances[i3]);
		totdam -= (i3 >= 3?statblock.res:statblock.def);
		totdam = (totdam <= 0?0:totdam);
		statblock.chp = Mathf.Max(statblock.chp-totdam,0);
		if(statblock.chp == 0){
			die();
		}
		if(totdam == 0){
			//Debug.Log("took no damage!");
			//no damage swoosh
			Transform clone = Instantiate(BattleMaster.pl[20]);
			clone.position = transform.position;
			clone.GetComponent<swooshcontroller>().damtype = -1;
			clone.GetComponent<swooshcontroller>().dothething();
		} else {
			anim.SetBool("hurt",true);		
			Transform clone = Instantiate(BattleMaster.pl[6+i3]);
			clone.position = transform.position;
			clone.GetComponent<swooshcontroller>().dam = totdam;
			clone.GetComponent<swooshcontroller>().damtype = i3;
			clone.GetComponent<swooshcontroller>().dothething();
		}
		
	}
	
	public void debug(){
		class_CO.Add(new Magic_Missile());
	}
	
	public int weapondamage(){
		return statblock.atk; //add weapon damage with items
	}
	
	public int magicdamage(int i){
		return statblock.mag; //per damage type?
	}
	
	public int pierce(){
		return 0;
	}
	
	public void die(){
		anim.SetBool("dead",true);
		transform.GetChild(0).GetComponent<Collider>().enabled = false;
		BattleMaster.isdead(this);
		anim.gameObject.AddComponent(typeof(deathredirect));
	}
	
	public void resetanim(RuntimeAnimatorController rac){
		StartCoroutine(resetanimIE(rac));
	}
	
	public IEnumerator resetanimIE(RuntimeAnimatorController rac){
		anim.SetBool("exit",true);
		yield return new WaitForSeconds(0.25f);
		anim.runtimeAnimatorController = rac;
		if(idleanim != 0){
			anim.SetInteger("weapon",idleanim);
		}
	}
}
