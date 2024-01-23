using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class Combatant : MonoBehaviour
{
	public int clas = -1;//-1 no class, 0 barbarian, 1 KI master, 2 paladin, 3 ranger, 4 phantom, 5 bard, 6 wizard, 7 cleric, 8 druid
	public bool strong = false;
	public brain AI;
	public bool humanoid = false;
	public int phenotype = 0;
    public Stats statblock;
	public bool isPC = false;
	public bool ismaincharacter = false;
	public bool issummon = false;
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
	
	public void OnConfirm2(InputAction.CallbackContext context){ //e
	
	}
	public void OnCancel2(InputAction.CallbackContext context){ //q
		
	}
	public void OnMove2(InputAction.CallbackContext context){ //WASD
	}
	public void OnSprint2(InputAction.CallbackContext context){ //shift
		
	}
	public void OnSelect12(InputAction.CallbackContext context){ //1
		
	}
	public void OnSelect22(InputAction.CallbackContext context){ //1
	
	}
	public void OnSelect32(InputAction.CallbackContext context){ //1
		
	}
	public void OnSelect42(InputAction.CallbackContext context){ //1
	
	}
	public void OnSelect52(InputAction.CallbackContext context){ //1
	
	}
	public void OnJump2(InputAction.CallbackContext context){ //space
	
	}
	public void OnAbility2(InputAction.CallbackContext context){ //f
	
	}
	
	public int addxp(int i){
		statblock.xp += i;
		int num = 0;
		if(statblock.xp >= statblock.nextxp){
			//statblock.lvl++; level up when called
			statblock.xp -= statblock.nextxp;
			statblock.nextxp += 5*statblock.lvl; //???
			num++;
		}
		return num;
	}
	
	public void lvlup(int i){
		//do we need number of levels?
		//add stats correctly
		statblock.lvl += i;
	}
	
	public void sync(combatantdata cd, bool team){
		show_HP = !team;
		isPC = team;
		clas = cd.clas;
		
		strong = cd.strong;
		AI = cd.AI;
		humanoid = cd.humanoid;
		ismaincharacter = cd.ismaincharacter;
		phenotype = cd.phenotype;
		statblock = cd.statblock;
		icon = cd.icon;
	
		foreach(combatoption co in cd.class_CO){
			class_CO.Add(Instantiate(co));
		}
		
		foreach(combatoption co in cd.weapon_CO){
			weapon_CO.Add(Instantiate(co));
		}
	
	
		red = cd.red;
		blue = cd.blue;
		green = cd.green;
		height = cd.height;
	
		anim = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		
		anim.runtimeAnimatorController = (cd.anim as RuntimeAnimatorController);
		idleanim = cd.idleanim;
		
		
		statblock.start();
		enemyHP = Instantiate(enemyHP);
		enemyHP.parent = transform;
		enemyHP.position = transform.position + new Vector3(0,height,0);
		HP = enemyHP.GetChild(0) as RectTransform;
		hpt = enemyHP.GetChild(1).GetComponent<TextMeshProUGUI>();
		//debug();
		if(idleanim != 0){
			anim.SetInteger("weapon",idleanim);
		}
	}
	
	/*public void Awake(){
		statblock.start();
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
	}*/
	
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
		if(dodgestate == 1){ // full evade
			if(humanoid){				
					anim.SetBool("dodge",true);
					anim.SetBool("defend",(transform.GetChild(0).GetChild(0).GetComponent<defendmono>())?true:false);

			} else {				
				anim.SetInteger("stage",1);
				anim.SetInteger("atkanim",(transform.GetChild(0).GetChild(0).GetComponent<defendmono>())?-1:0);
				anim.SetBool("dodge",true);
			}
			//miss/dodge swoosh
			Transform clone = Instantiate(BattleMaster.pl[28]);
			clone.position = transform.position;
			clone.GetComponent<swooshcontroller>().damtype = -1;
			clone.GetComponent<swooshcontroller>().dothething();
			dodgestate = 0;
			if(isPC){
				BattleMaster.BM.resetdodgePC();
			}
			return;
		}
		int totdam = i;
		totdam = (int)Mathf.Floor(i * statblock.resistances[i3]);
		totdam -= (i3 >= 3?statblock.res:statblock.def);
		if(transform.GetChild(0).GetChild(0).GetComponent<defendmono>()){
			totdam -= 1; // might be dynamic later. % based?
		}
		if(dodgestate == 2)totdam -= 1;
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
			if(dodgestate != 2)anim.SetBool("hurt",true);		
			Transform clone = Instantiate(BattleMaster.pl[6+i3]);
			clone.position = transform.position;
			clone.GetComponent<swooshcontroller>().dam = totdam;
			clone.GetComponent<swooshcontroller>().damtype = i3;
			clone.GetComponent<swooshcontroller>().dothething();
			dodgestate = -1;			
			StartCoroutine(dodge2());
		}
		
	}
	
	public void heal(int i){
		statblock.chp += i;
		if(statblock.chp >= statblock.hp)statblock.chp = statblock.hp;
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
		StartCoroutine(die1());
	}
	
	public IEnumerator die1(){	
		yield return new WaitForEndOfFrame();
		anim.SetBool("dead",true);
		transform.GetChild(0).GetComponent<Collider>().enabled = false;
		BattleMaster.isdead(this, isPC);
		if(!isPC){
			anim.gameObject.AddComponent(typeof(deathredirect));
		}
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
	
	public void runAIturn(){
		StartCoroutine(runAIturn1());
	}
	
	public IEnumerator runAIturn1(){
		combatoption curop = AI.pickoption();
		if(curop == null){
			BattleMaster.attackcallback(0);			
			yield return new WaitForEndOfFrame();
		} else {
			curop.demothething();
			yield return new WaitForEndOfFrame();
			curop.dothething();
		}
	}
	
	public void block(){
		StartCoroutine(block1());
	}
	
	public IEnumerator block1(){
		if(dodgestate != 0)yield break;
		dodgestate = 2; //too early, block
		if(humanoid){
			anim.SetBool("defend",true);
		} else {
			anim.SetInteger("stage",1);
			anim.SetInteger("atkanim",-1);
		}	
		yield return new WaitForSeconds(1f);
		if(dodgestate == 2){
			dodgestate = 0; //lockout
			if(!transform.GetChild(0).GetChild(0).GetComponent<defendmono>()){
				if(humanoid){
					anim.SetBool("defend",false);
				} else {				
					anim.SetInteger("stage",0);
					anim.SetInteger("atkanim",0);
				}
			}
		}
	}
	
	public void dodge(){
		StartCoroutine(dodge1());
	}
	
	public int dodgestate = 0;
	
	public IEnumerator dodge1(){
		if(dodgestate == -1){//knock prone for dodging late
			yield break;
		} else if (dodgestate == 0){ //from neutral
			dodgestate = 1; // actual dodge
		} else {
			yield break;
		}
		yield return new WaitForSeconds(0.05f);
		/*
		if(dodgestate == 1){
			dodgestate = 2; //too early, block
			if(humanoid){
				anim.SetBool("defend",true);
			} else {
				anim.SetInteger("stage",1);
				anim.SetInteger("atkanim",-1);
			}	
		}
		yield return new WaitForSeconds(1f);
		*/
		if(dodgestate == 1){
			dodgestate = 3; //lockout
			if(!transform.GetChild(0).GetChild(0).GetComponent<defendmono>()){
				if(humanoid){
					anim.SetBool("defend",false);
				} else {				
					anim.SetInteger("stage",0);
					anim.SetInteger("atkanim",0);
				}
			}
		}
		yield return new WaitForSeconds(2f);
		if(!transform.GetChild(0).GetChild(0).GetComponent<defendmono>()){
			if(humanoid){
				anim.SetBool("defend",false);
				anim.SetBool("dodge",false);
			} else {				
				anim.SetInteger("stage",0);
				anim.SetInteger("atkanim",0);
			}
		}
		dodgestate = 0;
		
	}
	
	public IEnumerator dodge2(){
		yield return new WaitForSeconds(3f);		
		if(dodgestate == -1)dodgestate = 0; 
		anim.SetBool("dodge",false);
		if(humanoid){
			anim.SetBool("defend",(transform.GetChild(0).GetChild(0).GetComponent<defendmono>())?true:false);
		} else {
			anim.SetInteger("atkanim",(transform.GetChild(0).GetChild(0).GetComponent<defendmono>())?-1:0);
		}
	}
	
	public bool apply_status(status s, int i){
		int res = statblock.lck;
		for(int i2 = 0; i2 < statblock.statusresistance.Count; i2++){
			res += (int)((int)statblock.statusresistance[i2][0] == s.id?statblock.statusresistance[i2][1]:0);
		}
		int chance = i - res;
		int randchance = UnityEngine.Random.Range(0,100);
		if(randchance <= chance){
			statusmono sm = (statusmono)gameObject.AddComponent(typeof(statusmono));
			sm.comb = this;
			sm.sts = s;
			sm.init();
			return true;
		}
		return false;
	}		
	
}
