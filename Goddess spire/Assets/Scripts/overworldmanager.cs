using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class overworldmanager : MonoBehaviour
{
	public static overworldmanager OM;
    
	public InputActionMap IA;
	public PlayerInput PI;
	public playercontroller pc;
	public GameObject buildinglos;
	
	
	void Awake(){
		OM = this;
	}
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	bool lastframecheckbuilding = true;
	
	void FixedUpdate(){	
		if(lastframecheckbuilding != building.inroom){
			if(building.inroom){
				buildinglos.GetComponent<Animator>().Play("Base Layer.fadein",-1, 0);
			} else {
				buildinglos.GetComponent<Animator>().Play("Base Layer.fadeout",-1, 0);
			}
		}
		lastframecheckbuilding = building.inroom;
		
	}
	
	public static void resetplants(){
		OM.BroadcastMessage("plantresetpower");
		OM.BroadcastMessage("plantupdatelinks");
		OM.BroadcastMessage("plantupdatepower");
	}
}
