using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleMaster : MonoBehaviour
{
	public static BattleMaster BM;
    public static List<Combatant> combatants = new List<Combatant>();
    public List<Combatant> combatantsass = new List<Combatant>();
	
	public static List<Sprite> cmoi = new List<Sprite>(); //combatmenuoptionicons
	public List<Sprite> cmoiass = new List<Sprite>(); 
	
	public static List<Sprite> sl = new List<Sprite>(); //sprite list
	public List<Sprite> slass = new List<Sprite>(); 
	
	public static List<Transform> pl = new List<Transform>(); //prefab list
	public List<Transform> plass = new List<Transform>(); //prefab list
	
	public List<Combatant> initiative = new List<Combatant>();
	public int roundturn = 0;	
	public Transform init_track_holder;
	public Transform init_head;
	
	public Transform combatmenu;
	public int menutarget = 4000;
	public int curmenutarg = 4000;
	public Animator[] iconanims;
	
	public bool csubmenuon;
	public bool abilityselected;
	public bool abilityactive;
	public GameObject csubmenu;
	public Animator csubmenuanim;
	public int submenutarg = 3;
	public int submenucurtarg = 3;
	public menuoption[] optiontexts;
	public combatoption cur_sel_CO;
	
	public static Transform attackst;
	public Transform attackstass;
	
	public combatoption[] tactics;
	
	public Transform explainer;
	public TextMeshProUGUI explainertxt;
	public bool explained;
	
	
	void Awake(){
		BM = this;
		combatants = combatantsass;
		cmoi = cmoiass;
		sl = slass;
		pl = plass;
		iconanims[1].SetBool("skip",true);
		iconanims[2].SetBool("skip",true);
		iconanims[3].SetBool("skip",true);
		attackst = attackstass;
	}
	
	void Start(){
		initiative_calc();
		update_menu_memory();
	}
	
	void Update(){
		//if(Input.GetKeyDown(KeyCode.Space))next_turn();
		if(!explained){
			if(Input.GetKeyDown(KeyCode.E)){
				explained = true;
				explainer.gameObject.SetActive(false);
			}
		} else if(!abilityactive){
			if(Input.GetKeyDown(KeyCode.A) && !csubmenuon)menutarget -= 1;
			if(Input.GetKeyDown(KeyCode.D) && !csubmenuon)menutarget += 1;
			if(Input.GetKeyDown(KeyCode.W) && csubmenuon)submenutarg -= 1;
			if(Input.GetKeyDown(KeyCode.S) && csubmenuon)submenutarg += 1;
			if(Input.GetKeyDown(KeyCode.Q)){
				if(abilityselected){
					cur_sel_CO.nevermind();
					abilityselected = false;
					csubmenuon = true;
					csubmenu.SetActive(true);
					update_submenu_memory(true);
					combatmenu.gameObject.SetActive(true);
					explainer.gameObject.SetActive(false);
				} else if(csubmenuon){
					csubmenuon = false;
					csubmenu.SetActive(false);
					update_submenu_memory(false);
				}
			}
			if(Input.GetKeyDown(KeyCode.E)){
				if(!csubmenuon && !abilityselected){
					csubmenuon = true;
					csubmenu.SetActive(true);
					update_submenu_memory(true);
					SCM_icon_change();
				} else if(!abilityselected) {
					cur_sel_CO.demothething();
					abilityselected = true;
					csubmenuon = false;
					csubmenu.SetActive(false);
					update_submenu_memory(false);
					combatmenu.gameObject.SetActive(false);
					explainer.gameObject.SetActive(true);
					explainertxt.text = cur_sel_CO.explain;
				} else {
					cur_sel_CO.dothething();
					abilityselected = false;
					abilityactive = true;
					explainer.gameObject.SetActive(false);
				}
			}
			combatmenurotate();
			if(csubmenuon)subcombatmenurotate();
		}
	}
	
	public void initiative_calc(){
		initiative.Clear();
		reset_init_faces();
		int cur = 255;
		while(cur >= 0){
			foreach(Combatant c in combatants){
				if(c.statblock.get_spd() == cur){
					initiative.Add(c);
					add_init_face(c);
				} else if(c.statblock.get_spd()-50 == cur){
					initiative.Add(c);
					add_init_face(c);
				} else if(c.statblock.get_spd()-100 == cur){
					initiative.Add(c);	
					add_init_face(c);				
				}
			}
			cur--;
		}
	}
	
	public void reset_init_faces(){
		foreach (Transform child in init_track_holder.GetChild(0)) {
			GameObject.Destroy(child.gameObject);
		}
	}
	
	public void add_init_face(Combatant c){
		RectTransform clone = (Instantiate(init_head)) as RectTransform;
		clone.parent = init_track_holder.GetChild(0);
		clone.GetComponent<Image>().color = (c.isPC?Color.green:Color.red);
		clone.GetChild(0).GetComponent<Image>().sprite = c.icon;
		clone.GetChild(0).GetComponent<indicatorgrabber>().comb = c;
		clone.anchoredPosition = Vector3.zero + new Vector3(100*(init_track_holder.GetChild(0).childCount-1),0,0);
	}
	
	public void next_turn(){
		initiative[roundturn].BMM = menutarget;
		roundturn++;
		if(roundturn >= initiative.Count){
			roundturn = 0;
		} else {
			init_track_holder.GetComponent<Animator>().SetBool("move",true);
		}
		(init_track_holder as RectTransform).anchoredPosition = new Vector3(-100*roundturn+50,-50,0);
		update_menu_memory();
		abilityactive = false;
	}
	
	public void update_menu_memory(){
		menutarget = initiative[roundturn].BMM;
		curmenutarg = menutarget;
	}
	
	public void update_submenu_memory(bool b){
		if(b){
			submenutarg = initiative[roundturn].BSMM[curmenutarg%4];
			submenucurtarg = submenutarg;
		} else {
			initiative[roundturn].BSMM[curmenutarg%4] = submenutarg;
		}
	}
	
	public void combatmenurotate(){
		if(curmenutarg > menutarget){
			if(!combatmenu.GetComponent<Animator>().GetBool("left") && !combatmenu.GetComponent<Animator>().GetBool("right")) {				
				combatmenu.GetComponent<Animator>().SetBool("left", true);
				combatmenu.GetChild(0).Rotate(0,-90,0);
				curmenutarg--;
			}
		} else if(curmenutarg < menutarget) {	
			if(!combatmenu.GetComponent<Animator>().GetBool("right") && !combatmenu.GetComponent<Animator>().GetBool("left")) {
				combatmenu.GetComponent<Animator>().SetBool("right", true);
				combatmenu.GetChild(0).Rotate(0,90,0);
				curmenutarg++;
			}
		}
		for(int i = 0; i < 4; i++){
			if(curmenutarg%4 != i){
				iconanims[i].SetBool("darken",true);
				iconanims[i].SetBool("brighten",false);
			} else {
				iconanims[i].SetBool("brighten",true);
				iconanims[i].SetBool("darken",false);
			}
		}
	}
	
	public void subcombatmenurotate(){
		if(submenucurtarg > submenutarg){
			if(csubmenuanim.GetBool("up") || csubmenuanim.GetBool("down")){
				
			} else {
				csubmenuanim.SetBool("up",true);
				submenucurtarg--;
				SCM_icon_change();
			}
		} else if (submenucurtarg < submenutarg){
			if(csubmenuanim.GetBool("down") || csubmenuanim.GetBool("up")){
				
			} else {
				csubmenuanim.SetBool("down",true);
				submenucurtarg++;
				SCM_icon_change();
			}
		}
	}
	
	public void SCM_icon_change(){
		int i2 = -3;
		for(int i = 0; i < 7; i++){
			combatoption CO = new combatoption();
			
			if(curmenutarg%4 == 0){ //class
				if(submenucurtarg+i2 != 0){
					CO = initiative[roundturn].class_CO[Mathf.Abs((submenucurtarg+i2)%initiative[roundturn].class_CO.Count)];
					//Debug.Log((((submenucurtarg+i)%tactics.Length)));
				} else {
					CO = initiative[roundturn].class_CO[0];
				}
			}
			
			if(curmenutarg%4 == 1){ //weapon
				if(submenucurtarg+i2 != 0){
					CO = initiative[roundturn].weapon_CO[Mathf.Abs((submenucurtarg+i2)%initiative[roundturn].weapon_CO.Count)];
					//Debug.Log((((submenucurtarg+i)%tactics.Length)));
				} else {
					CO = initiative[roundturn].weapon_CO[0];
				}
			}
			
			if(curmenutarg%4 == 2){ //tactics
				if(submenucurtarg+i2 != 0){
					CO = tactics[Mathf.Abs((submenucurtarg+i2)%tactics.Length)];
					//Debug.Log((((submenucurtarg+i)%tactics.Length)));
				} else {
					CO = tactics[0];
				}
			}
			
			if(curmenutarg%4 == 3)CO = new CO_test(); //DEBUG, no inventory currently
			
			optiontexts[i].image.sprite = BattleMaster.cmoi[CO.iconid];
			optiontexts[i].background.color = CO.background;
			optiontexts[i].text.text = CO.nme;
			if(i2 == 0)cur_sel_CO = CO;
			i2++;
		}
	}
	
	public static void attackcallback(int i){
		switch(i){
			case 0:
				BM.next_turn();
			break;
		}
	}
	
}
