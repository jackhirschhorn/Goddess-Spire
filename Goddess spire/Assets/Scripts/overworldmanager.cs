using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

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
	public GameObject talkingUI;
	public Image talkingUIicon;
	public TextMeshProUGUI talkingUItxt;
	public GameObject talkingUIbuttonholder;
	public GameObject talkingUIbutton;
	public npctalker lasttalker;
	
	
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
	
	public combatantdataholder senttocombat;
	
	public void gotobattle(List<combatantdata> enemy, combatantdataholder player){
		senttocombat = player;
		battoltex.Release();
		battoltex.width = Screen.width;
		battoltex.height = Screen.height;
		battoltex.Create();
		Camera.main.targetTexture = battoltex;
		Camera.main.Render();
		Camera.main.targetTexture = null;
		battlemaster.SetActive(true);
		foreach(combatantdata cd in player.cd){
			BattleMaster.BM.addbattoler(cd,true);
		}
		foreach(combatantdata cd in enemy){
			BattleMaster.BM.addbattoler(cd,false);
		}
		BattleMaster.BM.begin();
		this.gameObject.SetActive(false);
	}
	
	public void backfrombattle(List<Combatant> player){
		//animation
		//
		//Combatant to combatantdata
		for(int i = 0; i < player.Count; i++){
			senttocombat.cd[i].reconstruct(player[i]);
		}		
		foreach(Transform c in battlemaster.transform.GetChild(0)){
			Destroy(c.gameObject);
		}
		BattleMaster.combatants.Clear();
		battlemaster.SetActive(false);
		this.gameObject.SetActive(true);
	}
	
	public void firststrike(combatantdata cd, combatoption co){
		battlemaster.GetComponent<BattleMaster>().firststrike(cd,co);
	}
	
	public void battlestate(int i){
		if(i>0)battlemaster.GetComponent<BattleMaster>().battlestateenemy = i;
		if(i<0)battlemaster.GetComponent<BattleMaster>().battlestateally = i;
	}
	
	public void adddialoptions(string[] strings,int[] ints, npctalker npcspeaker){
		lasttalker = npcspeaker;
		for(int i = 0; i < strings.Length; i++){
			Transform clone = Instantiate(talkingUIbutton,talkingUIbuttonholder.transform).transform;
			int temp = ints[i];
			clone.GetComponent<Button>().onClick.AddListener(() => advancetodial(temp));
			clone.GetChild(0).GetComponent<TextMeshProUGUI>().text = strings[i];
			clone.GetComponent<RectTransform>().anchoredPosition = clone.GetComponent<RectTransform>().anchoredPosition + new Vector2(0,-200*i);
		}
	}
	
	public void advancetodial(int i){
		foreach (Transform child in talkingUIbuttonholder.transform) {
			GameObject.Destroy(child.gameObject);
		}
		lasttalker.advancetodial(i);
	}
}
