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
	public GameObject battlemaster;
	public RenderTexture battoltex;
	
	
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
	
	public void gotobattle(List<combatantdata> enemy, List<combatantdata> player){
		battoltex.Release();
		battoltex.width = Screen.width;
		battoltex.height = Screen.height;
		battoltex.Create();
		Camera.main.targetTexture = battoltex;
		Camera.main.Render();
		Camera.main.targetTexture = null;
		battlemaster.SetActive(true);
		foreach(combatantdata cd in player){
			BattleMaster.BM.addbattoler(cd,true);
		}
		foreach(combatantdata cd in enemy){
			BattleMaster.BM.addbattoler(cd,false);
		}
		BattleMaster.BM.begin();
		this.gameObject.SetActive(false);
	}
}
