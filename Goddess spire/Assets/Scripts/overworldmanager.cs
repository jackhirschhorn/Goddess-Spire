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
	public Transform camera;
	
	
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
		if(building.inroom){
			camera.localRotation = Quaternion.Slerp(camera.localRotation, Quaternion.Euler(building.inroom.euls), Time.fixedDeltaTime*(180f/Quaternion.Angle(camera.localRotation, Quaternion.Euler(building.inroom.euls))));
		} else {
			camera.localRotation = Quaternion.Slerp(camera.localRotation, Quaternion.Euler(new Vector3(0,0,0)), Time.fixedDeltaTime*(180f/Vector3.Angle(camera.forward, Vector3.forward)));
		}
		
	}
	
	public static void resetplants(){
		OM.BroadcastMessage("plantresetpower");
		OM.BroadcastMessage("plantupdatelinks");
		OM.BroadcastMessage("plantupdatepower");
	}
}
