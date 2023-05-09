using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMaster : MonoBehaviour
{
    public static List<Combatant> combatants = new List<Combatant>();
    public List<Combatant> combatantsass = new List<Combatant>();
	
	public List<Combatant> initiative = new List<Combatant>();
	public int roundturn = 0;	
	public Transform init_track_holder;
	public Transform init_head;
	
	public Transform combatmenu;
	public int menutarget = 4000;
	public int curmenutarg = 4000;
	public Animator[] iconanims;
	
	void Awake(){
		combatants = combatantsass;
		iconanims[1].SetBool("skip",true);
		iconanims[2].SetBool("skip",true);
		iconanims[3].SetBool("skip",true);
	}
	
	void Start(){
		initiative_calc();
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space))next_turn();
		if(Input.GetKeyDown(KeyCode.A))menutarget -= 1;
		if(Input.GetKeyDown(KeyCode.D))menutarget += 1;
		combatmenurotate();
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
		roundturn++;
		if(roundturn >= initiative.Count){
			roundturn = 0;
		} else {
			init_track_holder.GetComponent<Animator>().SetBool("move",true);
		}
		(init_track_holder as RectTransform).anchoredPosition = new Vector3(-100*roundturn+50,-50,0);
	}
	
	public void combatmenurotate(){
		if(curmenutarg > menutarget){
			if(combatmenu.GetComponent<Animator>().GetBool("leftdone")){
				combatmenu.GetComponent<Animator>().SetBool("leftdone", false);
			} else if(!combatmenu.GetComponent<Animator>().GetBool("left")) {				
				combatmenu.GetComponent<Animator>().SetBool("left", true);
				combatmenu.Rotate(0,-90,0);
				curmenutarg--;
			}
		} else if(curmenutarg < menutarget) {	
			if(combatmenu.GetComponent<Animator>().GetBool("rightdone")){
				combatmenu.GetComponent<Animator>().SetBool("rightdone", false);
			} else if(!combatmenu.GetComponent<Animator>().GetBool("right")) {
				combatmenu.GetComponent<Animator>().SetBool("right", true);
				combatmenu.Rotate(0,90,0);
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
	
}
