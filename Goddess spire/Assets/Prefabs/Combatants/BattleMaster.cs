using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class BattleMaster : MonoBehaviour
{
	public static BattleMaster BM;
	public  List<battleplayerui> bpuiass = new List<battleplayerui>();
	public static List<battleplayerui> bpui = new List<battleplayerui>();
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
	public Quaternion[] cmrotpos;
	
	public static List<Transform> sndl = new List<Transform>(); //sound prefab list
	public List<Transform> sndlass = new List<Transform>();
	
	//public static List<itemscript> itms = new List<itemscript>(); // items
	//public List<itemscript> itmsass = new List<itemscript>();
	
	public static int lastitmsel = 0;
	
	
    public static List<Combatant> partyorder = new List<Combatant>();
    public List<Combatant> partyorderass = new List<Combatant>();
	
	public Animator screenwipe;
	
	public int encounterxp = 0;
	public List<itemscript> encounteritems = new List<itemscript>();
	
	public static void makesound(int i){
		Instantiate(sndl[i]);
	}
	
	public Transform snd;
	
	public static void makesoundtokill(int i){
		BM.snd = Instantiate(sndl[i]);
	}
	
	public static void killsound(){
		if(BM.snd)Destroy(BM.snd.gameObject);
	}
	
	public static int targettotal(bool b){
		int i = 0;
		combatants.RemoveAll(s => s == null);
		foreach(Combatant c in combatants){
			if(c.isPC == b && c.statblock.chp > 0){
				i++;
			}
		}
		return i;
	}
	
	public static List<Combatant> gettargs(bool b){
		List<Combatant> temp = new List<Combatant>();
		combatants.RemoveAll(s => s == null);
		foreach(Combatant c in combatants){
			if(c.isPC == b && c.statblock.chp > 0){
				temp.Add(c);
			}
		}
		return temp;
	}
	
	public static void isdead(Combatant b, bool b2){
		if(!b2)combatants.RemoveAll(s => s == b);
		BM.initiative.RemoveAll(s => s == b);
		BM.initiativetrackcheck();
		if(!b2)BM.encounterxp += b.statblock.xp;
		if(!b2)addencounteritem(b.statblock.deathdrops);
	}
	
	public static void addencounteritem(List<itemscript> l){
		foreach(itemscript i in l){
			addencounteritem(Instantiate(i));
		}
	}
	
	public static void addencounteritem(itemscript l){
		for(int i = 0; i < BM.encounteritems.Count;i++){
			if(BM.encounteritems[i].GetType() == l.GetType()){
				BM.encounteritems[i].count += l.count;
				return;
			}
		}
		BM.encounteritems.Add(l);
		
	}
	
	
	public static Combatant unitlist(bool b, int i){
		//true = player, false = enemy;
		//FIX THIS LATER
		combatants.RemoveAll(s => s == null);
		foreach(Combatant c in combatants){
			if(c.isPC == b && c.statblock.chp > 0){
				if(i == 0){
					return c;
				} else {
					i--;
				}
			}
		}
		return null;
	}
	
	public Combatant meleetarg(bool b){
		//true = player, false = enemy;
		RaycastHit hit;
		if(Physics.Raycast(Vector3.zero, (b? new Vector3(-1,0,0):new Vector3(1,0,0)),out hit,1000)){
			//Debug.Log(hit.transform.parent.GetComponent<Combatant>());
			return hit.transform.parent.GetComponent<Combatant>();
		}
		return null;
	}
	
	void Awake(){
		BM = this;
		bpui = bpuiass;
		//combatants = combatantsass;
		cmoi = cmoiass;
		sl = slass;
		pl = plass;
		sndl = sndlass;
		iconanims[1].SetBool("skip",true);
		iconanims[2].SetBool("skip",true);
		iconanims[3].SetBool("skip",true);
		attackst = attackstass;
		//itms = itmsass;
		//partyorder = partyorderass;
	}
	
	public Transform emptycombatant;
	public int pcsnum = 0;
	public int enemynum = 0;
	
	public void addbattoler(combatantdata cd, bool team){
		//team true = player
		Transform clone = Instantiate(emptycombatant);
		clone.parent = transform.GetChild(0);
		clone.localPosition = new Vector3((team?-1:1)*(2+(team?pcsnum*3:enemynum*3)),0,0);
		Transform clone2 = Instantiate(cd.model);
		clone2.parent = clone.GetChild(0);
		clone2.localPosition = Vector3.zero;
		clone2.localScale = new Vector3(clone2.localScale.x*(team?1:-1),clone2.localScale.y,clone2.localScale.z);
		clone.GetComponent<Combatant>().sync(cd,team);
		if(team){
			pcsnum++;
			partyorder.Add(clone.GetComponent<Combatant>());
			bpuilinker(clone.GetComponent<Combatant>());
		} else {
			enemynum++;
		}
		combatants.Add(clone.GetComponent<Combatant>());
		if(clone.GetComponent<Combatant>().statblock.chp <= 0)clone.GetComponent<Combatant>().die();
	}
	
	public void bpuilinker(Combatant c){
		for(int i = 4; i > -1; i--){
			if(bpui[i].linked == null){
				bpui[i].linked = c;
				bpui[i].awake();
				return;
			}
		}
	}
	
	//public void addreinforcement
	
	public void begin(){
		foreach(Combatant c in combatants){
			c.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetFloat("startspd", Random.Range(8,13)*0.1f);
			c.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("start", false);
		}
		
		initiative_calc();
		update_menu_memory();
	}
	
	bool OC = false;
	
	public void OnConfirm(InputAction.CallbackContext context){ //e
		if(gameObject.activeSelf)BroadcastMessage("OnConfirm2",context);
		if(context.performed){
			OC = true;
			if(!explained){
				explained = true;
				explainer.gameObject.SetActive(false);
				screenwipe.SetBool("start",true);
				foreach(Combatant c in combatants){
					c.transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("start", false);
				}
			} else if(!abilityactive && initiative[roundturn].isPC){
				if(!csubmenuon && !abilityselected){
					combatmenu.GetComponent<Animator>().SetBool("select", true);
					csubmenuon = true;
					csubmenu.SetActive(true);
					update_submenu_memory(true);
					SCM_icon_change();
				} else if(!abilityselected) {
					if(costcheck()){
						combatmenu.GetComponent<Animator>().SetBool("select", true);
						cur_sel_CO.demothething();
						abilityselected = true;
						csubmenuon = false;
						csubmenu.SetActive(false);
						update_submenu_memory(false);
						combatmenu.GetChild(0).gameObject.SetActive(false);
						explainer.gameObject.SetActive(true);
						explainertxt.text = cur_sel_CO.explain;
					}
				} else {
					combatmenu.GetComponent<Animator>().SetBool("select", true);
					cur_sel_CO.dothething();
					docosts(initiative[roundturn],cur_sel_CO.cost,cur_sel_CO.costype);
					abilityselected = false;
					abilityactive = true;
					explainer.gameObject.SetActive(false);
				}
			}
		}
	}
	
	public void OnCancel(InputAction.CallbackContext context){ //q
		BroadcastMessage("OnCancel2",context);
		if(!abilityactive && initiative[roundturn].isPC){
			
			if(abilityselected){
				combatmenu.GetComponent<Animator>().SetBool("unselect", true);
				cur_sel_CO.nevermind();
				abilityselected = false;
				csubmenuon = true;
				csubmenu.SetActive(true);
				update_submenu_memory(true);
				combatmenu.GetChild(0).gameObject.SetActive(true);
				explainer.gameObject.SetActive(false);
			} else if(csubmenuon){
				combatmenu.GetComponent<Animator>().SetBool("unselect", true);
				csubmenuon = false;
				csubmenu.SetActive(false);
				update_submenu_memory(false);
			}
		}
	}
	
	public Vector2 vec;	
	public void OnMove(InputAction.CallbackContext context){ //WASD
		BroadcastMessage("OnMove2",context);
		if(explained)vec = context.ReadValue<Vector2>();
	}
	
	public void OnSprint(InputAction.CallbackContext context){ //shift
		BroadcastMessage("OnSprint2",context);
		
	}
	
	public void OnSelect1(InputAction.CallbackContext context){ //1
		BroadcastMessage("OnSelect12",context);
		if(explained)partyorder[4].dodge();
	}
	
	public void OnSelect2(InputAction.CallbackContext context){ //1
		BroadcastMessage("OnSelect22",context);
		if(explained)partyorder[3].dodge();
	}
	
	public void OnSelect3(InputAction.CallbackContext context){ //1
		BroadcastMessage("OnSelect32",context);
		if(explained)partyorder[2].dodge();
	}
	
	public void OnSelect4(InputAction.CallbackContext context){ //1
		BroadcastMessage("OnSelect42",context);
		if(explained)partyorder[1].dodge();
	}
	
	public void OnSelect5(InputAction.CallbackContext context){ //1
		BroadcastMessage("OnSelect52",context);
		if(explained)partyorder[0].dodge();
	}
	
	public void OnJump(InputAction.CallbackContext context){ //space
		BroadcastMessage("OnJump2",context);
		
	}
	
	public void OnAbility(InputAction.CallbackContext context){ //f
		BroadcastMessage("OnAbility2",context);
		
	}
	
	public float wasdtimer = 0f;
	
	void Update(){
		if(!abilityactive && initiative[roundturn].isPC){
			wasdtimer -= Time.deltaTime;
			combatmenurotate();
			if(csubmenuon)subcombatmenurotate();
			if(vec == Vector2.zero)wasdtimer = -1f;
			if(wasdtimer < 0){
				if(vec.y > 0){//w
					if(csubmenuon){
						submenutarg -= 1;	
						wasdtimer = 0.35f;
					}
				}
				if(vec.y < 0){//s
					if(csubmenuon){
						submenutarg += 1;	
						wasdtimer = 0.35f;
					}
				}
				if(vec.x > 0){//d
					if(!csubmenuon && !abilityselected){
						menutarget += 1;	
						wasdtimer = 0.35f;
					}
				}
				if(vec.x < 0){//a
					if(!csubmenuon && !abilityselected){
						menutarget -= 1;	
						wasdtimer = 0.35f;
					}
				}
			}
		}
	}
	
	public void LateUpdate(){
		initiativetrack();
	}
	
	public void initiative_calc(){
		initiative.Clear();
		int temp = init_track_holder.GetChild(0).childCount;
		//reset_init_faces();
		//(init_track_holder as RectTransform).anchoredPosition = new Vector3(-70*roundturn+50,-50,0);
		int cur = 255;
		while(cur >= 0){
			foreach(Combatant c in combatants){
				if(c.statblock.chp > 0){
					if(c.statblock.get_spd() == cur){
						initiative.Add(c);
						//add_init_face(c,temp);
					} else if(c.statblock.get_spd()-50 == cur){
						initiative.Add(c);
						//add_init_face(c,temp);
					} else if(c.statblock.get_spd()-100 == cur){
						initiative.Add(c);	
						//add_init_face(c,temp);				
					}
				}
			}
			cur--;
		}		
		reverseorder();
		initiativetrackcheck();
		update_menu_memory();
		if(initiative[roundturn].isPC){
			abilityselected = false;
			csubmenuon = false;
			csubmenu.SetActive(false);
			combatmenu.GetChild(0).gameObject.SetActive(true);
			combatmenu.position = initiative[roundturn].transform.position + new Vector3(0,5,0);
			combatmenu.rotation = cmrotpos[initiative[roundturn].transform.GetSiblingIndex()];
			combatmenu.GetChild(0).localRotation = Quaternion.Euler(-38,-20,5);
			combatmenu.GetChild(0).Rotate(0,initiative[roundturn].BMM%4 == 0?0:(initiative[roundturn].BMM%4 == 1?90:(initiative[roundturn].BMM%4 == 2?180:270)),0);
			//Debug.Log(initiative[roundturn].BMM%4);
		} else {
			initiative[roundturn].runAIturn();
		}
	}
	
	public void reset_init_faces(){
		foreach (Transform child in init_track_holder.GetChild(0)) {
			Destroy(child.gameObject);
		}
	}
	
	public void add_init_face(Combatant c, int i){
		RectTransform clone = (Instantiate(init_head)) as RectTransform;
		clone.parent = init_track_holder.GetChild(0);
		clone.GetComponent<Image>().color = (c.isPC?Color.green:Color.red);
		clone.GetChild(0).GetChild(0).GetComponent<Image>().sprite = c.icon;
		clone.GetChild(0).GetChild(0).GetComponent<indicatorgrabber>().comb = c;
		clone.anchoredPosition = Vector3.zero + new Vector3(70*(init_track_holder.GetChild(0).childCount-i-1),0,0);
		clone.localScale = Vector3.one;
	}
	
	public void reverseorder(){
		for(int i=0;i<init_track_holder.GetChild(0).childCount;i++){
			init_track_holder.GetChild(0).GetChild(0).SetSiblingIndex((init_track_holder.GetChild(0).childCount-1)-i);
		}
	}
	
	public bool unset = true;
	
	public void initiativetrack(){
		if(unset){//create/mantain
			for(int i = 0; i <initiative.Count; i++){
				add_init_face(initiative[i],0);
			}
			unset = false;
		}
		//animate
	}
	
	public void initiativetrackcheck(){
		if(init_track_holder.GetChild(0).childCount != initiative.Count){
			unset = true;
			reset_init_faces();
			return;
		}
		for(int i = 0; i < initiative.Count; i++){
			if(init_track_holder.GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<indicatorgrabber>().comb != initiative[i]){
				unset = true;
				reset_init_faces();
				return;
			}
		}
	}
	
	public void next_turn(){
		if(checkendfight())return;
		initiative[roundturn].BMM = menutarget;
		//roundturn++;
		initiative.RemoveAt(0);
		if(initiative.Count == 0){
			initiative_calc();
			abilityactive = false;
			return;
		} else {
			init_track_holder.GetComponent<Animator>().SetBool("move",true);
		}
		//(init_track_holder as RectTransform).anchoredPosition = new Vector3(-70*roundturn+50,-50,0);
		initiativetrackcheck();
		update_menu_memory();
		abilityactive = false;
		if(initiative[roundturn].isPC){
			abilityselected = false;
			csubmenuon = false;
			csubmenu.SetActive(false);
			combatmenu.GetChild(0).gameObject.SetActive(true);
			explainer.gameObject.SetActive(false);
			combatmenu.position = initiative[roundturn].transform.position + new Vector3(0,5,0);
			combatmenu.rotation = cmrotpos[initiative[roundturn].transform.GetSiblingIndex()];
			combatmenu.GetChild(0).localRotation = Quaternion.Euler(-38,-20,5);
			combatmenu.GetChild(0).Rotate(0,initiative[roundturn].BMM%4 == 0?0:(initiative[roundturn].BMM%4 == 1?90:(initiative[roundturn].BMM%4 == 2?180:270)),0);
		} else {
			initiative[roundturn].runAIturn();
		}
	}
	
	public GameObject gameoverscreen;
	
	public bool checkendfight(){
		bool ded = true;
		foreach(Combatant c in partyorder){
			if(c.statblock.chp != 0)ded = false;
		}
		if(ded){//party loss
			gameover();
			return true;
		}
		ded = true;
		foreach(Combatant c in combatants){
			if(!c.isPC && c.statblock.chp != 0)ded = false;
		}
		if(ded){//party win
			endfight();
			return true;
		}
		return false;
	}
	
	public void gameover(){
		gameoverscreen.SetActive(true);
		//play gameover tune
		//add scene transition logic too later
	}
	
	public GameObject battleendscreen;
	public TextMeshProUGUI endscreenxp;
	
	public void endfight(){
		battleendscreen.SetActive(true);
		//play victory tune
		StartCoroutine(endfight1());
	}
	
	public Transform itmholder;
	public Transform levelupxp;
	public Transform levelupwxp;
	
	public IEnumerator endfight1(){	
		init_track_holder.parent.gameObject.SetActive(false);
		yield return new WaitForEndOfFrame();	
		endscreenxp.text = "" + encounterxp;
		yield return new WaitForSeconds(0.1f);
		int curxp = encounterxp;
		List<xptotaler> lstxp = new List<xptotaler>();
		foreach(Combatant c in combatants){
			if(c != null && c.ismaincharacter){
				Transform xpclone = Instantiate(levelupxp);
				xpclone.parent = transform.GetChild(2).GetChild(3);
				xpclone.position = c.transform.position + new Vector3(0,3,0);
				xpclone.GetComponent<xptotaler>().comb = c;
				lstxp.Add(xpclone.GetComponent<xptotaler>());
				xpclone = Instantiate(levelupwxp);
				xpclone.parent = transform.GetChild(2).GetChild(3);
				xpclone.position = c.transform.position + new Vector3(0,4,0);
				xpclone.GetComponent<xptotaler>().comb = c;
				//change icon to match weapon type, and apply weapon type
				lstxp.Add(xpclone.GetComponent<xptotaler>());
				if(c.clas == 3){
					xpclone = Instantiate(levelupwxp);
					xpclone.parent = transform.GetChild(2).GetChild(3);
					xpclone.position = c.transform.position + new Vector3(0,5,0);
					xpclone.GetComponent<xptotaler>().comb = c;
					lstxp.Add(xpclone.GetComponent<xptotaler>());
				}
			}
		}
		//OC = false;
		//yield return new WaitUntil(() => OC);
		//OC = false;
		StartCoroutine("showitems");
		while(curxp != 0){
			curxp--;
			endscreenxp.text = "" + curxp;
			//update player ones here
			foreach(xptotaler xpt in lstxp){
				xpt.xp++;
			}
			yield return new WaitForSeconds(0.1f);
		}
		// actually give the player the correct xp here
		List<Combatant> levelups = new List<Combatant>();
		List<int> levelupsnum = new List<int>();
		foreach(Combatant c in combatants){
			if(c != null && c.ismaincharacter){
				int num = c.addxp(encounterxp);
				if(num > 0){
					levelups.Add(c);
					levelupsnum.Add(num);
				}
			}
		}
		yield return new WaitForSeconds(0.2f);
		OC = false;
		yield return new WaitUntil(() => OC);
		OC = false;		
		foreach(Transform c in transform.GetChild(2).GetChild(3)){
			Destroy(c.gameObject);
		}
		battleendscreen.SetActive(false);
		lvlr.gameObject.SetActive(true);
		for(int i = 0; i < levelups.Count; i++){
			//zoom in, show stats screen change, play animation/soundbyte for the person
			cameraholder.position = levelups[i].transform.position + new Vector3(2,2,-8);
			lvlr.num = levelupsnum[i];
			lvlr.comb = levelups[i];
			lvlr.doer2();
			yield return new WaitForSeconds(2.2f);
			OC = false;
			yield return new WaitUntil(() => OC);
			OC = false;
			lvlr.reset();
		}
		lvlr.gameObject.SetActive(false);
		cameraholder.position = new Vector3(0,5,-35);
		//remove 'summons'
		for(int i = 0; i < combatants.Count; i++){
			if(combatants[i].issummon){
				combatants.RemoveAt(i);
				i--;
			}
		}		
		pcsnum = 0;
		enemynum = 0;
		initiative.Clear();
		abilityactive = false;
		init_track_holder.parent.gameObject.SetActive(true);
		overworldmanager.OM.backfrombattle(combatants);
		//cutback to overworld;
	}
	
	public IEnumerator showitems(){
		int offset = 50;
		foreach(itemscript its in encounteritems){
			Transform itsclone = Instantiate(pl[29]);
			itsclone.parent = itmholder;
			itsclone.localPosition = new Vector3(0,0-offset,0);
			itsclone.GetChild(0).GetComponent<Image>().sprite = its.icon;
			itsclone.GetChild(1).GetComponent<TextMeshProUGUI>().text = its.name + " X " + its.count;
			//add to total items;
			offset += 100;
			yield return new WaitForSeconds(0.2f);
		}
	}
	
	public Transform cameraholder;
	public levelerupui lvlr;
	
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
			
			if(curmenutarg%4 == 3){
				CO = (combatoption)inventory.inv.invitems[Mathf.Abs((submenucurtarg+i2)%inventory.inv.invitems.Count)].user;
				
			}
			
			optiontexts[i].image.sprite = CO.icon;
			optiontexts[i].background.color = CO.background;
			optiontexts[i].text.text = CO.nme;
			optiontexts[i].costback.color = (CO.costype == 0?Color.blue:(CO.costype == 1?Color.green:(CO.costype == 2?Color.red:Color.black)));
			optiontexts[i].cost.text = CO.cost+"";
			if(CO.iswand)optiontexts[i].cost.text = "0";
			if(CO.costype == 3)optiontexts[i].cost.text = inventory.inv.invitems[BattleMaster.lastitmsel].count + "";
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
	
	public bool costcheck(){
		if(cur_sel_CO.iswand) return true;
		switch(cur_sel_CO.costype){
			case 0: //mana
				return initiative[roundturn].statblock.cmana >= cur_sel_CO.cost;
			break;
			case 1: //stam
				return initiative[roundturn].statblock.cstam >= cur_sel_CO.cost;
			break;
			case 2: //hp
				return initiative[roundturn].statblock.chp >= cur_sel_CO.cost;
			break;
			case 3: //item
				return inventory.inv.invitems[BattleMaster.lastitmsel].count > 0;
			break;
		}
		return false;
	}
	
	public void docosts(Combatant c, int i, int i2){
		if(cur_sel_CO.iswand) return;
		switch(i2){
			case 0: //mana
				c.statblock.cmana -= i;
			break;
			case 1: //stam
				c.statblock.cstam -= i;
			break;
			case 2: //hp
				c.statblock.chp -= i;
			break;
			case 3: //item
				inventory.inv.invitems[BattleMaster.lastitmsel].count -= 1;
			break;
		}
		
	}
	
}
