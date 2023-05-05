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
	
	void Awake(){
		combatants = combatantsass;
	}
	
	void Start(){
		initiative_calc();
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space))next_turn();
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
	
}
