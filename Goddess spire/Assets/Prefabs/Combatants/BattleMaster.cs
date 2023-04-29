using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMaster : MonoBehaviour
{
    public static List<Combatant> combatants = new List<Combatant>();
    public List<Combatant> combatantsass = new List<Combatant>();
	
	public List<Combatant> initiative = new List<Combatant>();
	public int roundturn = 0;
	
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
		int cur = 255;
		while(cur >= 0){
			foreach(Combatant c in combatants){
				if(c.statblock.get_spd() == cur){
					initiative.Add(c);
				} else if(c.statblock.get_spd()-50 == cur){
					initiative.Add(c);
				} else if(c.statblock.get_spd()-100 == cur){
					initiative.Add(c);					
				}
			}
			cur--;
		}
	}
	
	public void next_turn(){
		roundturn++;
		if(roundturn >= initiative.Count){
			roundturn = 0;
		}
		Debug.Log(initiative[roundturn]);
	}
	
}
