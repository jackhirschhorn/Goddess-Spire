using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;

public class gamemenu : MonoBehaviour
{
    public int menustate = 0;
	public int menusubstate = 0;
	public int optionsmenustate = 0;
	public Animator anim;
	public static gamemenu GM;
	public bool menuon = false;
	
	//itemscolor
	public Color itemscolor1;
	public Color itemscolor2;
	
	//journalcolors
	public Color journalcolor1;
	public Color journalcolor2;
	
	//controllers
	public InputActionAsset inputAsset;
	
	//audio
	public Slider master, sfx, music, voices, menu;
	public AudioMixer amc;
	
	public string[] classindex = new string[]{"summon","barbarian","martial artist","paladin","ranger","rogue","bard","wizard","cleric","druid"};
	
	void Awake(){
		GM = this;
		string rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))inputAsset.LoadBindingOverridesFromJson(rebinds);
	}
	// Start is called before the first frame update
    void Start()
    {
        menustate = 1;
		master.value = PlayerPrefs.GetFloat("MasterVolume", 0.6f);
		sfx.value = PlayerPrefs.GetFloat("SfxVolume", 0.6f);
		music.value = PlayerPrefs.GetFloat("MusicVolume", 0.6f);
		voices.value = PlayerPrefs.GetFloat("VoicesVolume", 0.6f);
		menu.value = PlayerPrefs.GetFloat("MenuVolume", 0.6f);
		amc.SetFloat("Master", (master.value != 0?Mathf.Log10(master.value) * 20:0));
		amc.SetFloat("sfx", (sfx.value != 0?Mathf.Log10(sfx.value) * 20:0));
		amc.SetFloat("music", (music.value != 0?Mathf.Log10(music.value) * 20:0));
		amc.SetFloat("voices", (voices.value != 0?Mathf.Log10(voices.value) * 20:0));
		amc.SetFloat("menu", (menu.value != 0?Mathf.Log10(menu.value) * 20:0));
    }

    // Update is called once per frame
    void LateUpdate()
    {
		if(menusubstate == 1 && optionsmenustate == 4){
			amc.SetFloat("Master", (master.value != 0?Mathf.Log10(master.value) * 20:0));
			amc.SetFloat("sfx", (sfx.value != 0?Mathf.Log10(sfx.value) * 20:0));
			amc.SetFloat("music", (music.value != 0?Mathf.Log10(music.value) * 20:0));
			amc.SetFloat("voices", (voices.value != 0?Mathf.Log10(voices.value) * 20:0));
			amc.SetFloat("menu", (menu.value != 0?Mathf.Log10(menu.value) * 20:0));
			PlayerPrefs.SetFloat("MasterVolume", master.value);
			PlayerPrefs.SetFloat("SfxVolume", sfx.value);
			PlayerPrefs.SetFloat("MusicVolume", music.value);
			PlayerPrefs.SetFloat("VoicesVolume", voices.value);
			PlayerPrefs.SetFloat("MenuVolume", menu.value);
		}
    }
	
	public void updatemenutab(int i){
		menustate = i;
		anim.SetInteger("lastmenutarg", anim.GetInteger("menutarg"));
		anim.SetInteger("menutarg", i);
		transform.GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(false);
		for(int i2 = 1; i2 < 5; i2++){
			transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(false);
			if(i2 == i)transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(true);
		}
		if(menustate == 1)partymenuupdate();
	}
	
	public void updatesubmenutab(int i){
		if(i != 5 && menusubstate == 5){
			anim.SetBool("mapon", false);
		}
		menusubstate = i;
		anim.SetInteger("submenutarg", i);
		if(menusubstate == 1){
			transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
			transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
		} else if(menusubstate <= 4){
			for(int i2 = 0; i2 < 3; i2++){
				transform.GetChild(0).GetChild(0).GetChild(2).GetChild(i2).GetChild(0).GetComponent<Image>().color = (i2+2 == menusubstate?itemscolor1:itemscolor2);
			}
		} else if(menusubstate == 5){
			anim.SetBool("mapon", true);
			transform.GetChild(1).position = Camera.main.transform.position + Camera.main.transform.forward;
			
		} else if(menusubstate == 6){
			transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<Image>().color = journalcolor1;
			transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(2).GetComponent<Image>().color = journalcolor2;
			transform.GetChild(0).GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(false);
		} else if(menusubstate == 7){
			transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<Image>().color = journalcolor2;
			transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(2).GetComponent<Image>().color = journalcolor1;
			transform.GetChild(0).GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(false);
			transform.GetChild(0).GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);
		}  else if(menusubstate == 8){
			transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
		}  else if(menusubstate == 9){
			transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
			transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(true);
		} else if (menusubstate <= 14){
			transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(true);
			combatantdata tempcd = overworldmanager.OM.pc.transform.GetComponent<combatantdataholder>().cd[menusubstate-10];
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<Image>().sprite = tempcd.icon;
			
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = tempcd.name;
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = classindex[tempcd.clas+1];
			
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.chp + "/" + tempcd.statblock.hp;
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.cmana + "/" + tempcd.statblock.mana;
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.cstam + "/" + tempcd.statblock.stam;
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.atk + "";
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.def + "";
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(5).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.spd + "";
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(6).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.mag + "";
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(7).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.res + "";
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(8).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.lck + "";
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(9).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.lvl + "";
			transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(10).GetComponent<TextMeshProUGUI>().text = tempcd.statblock.xp + "/" + tempcd.statblock.nextxp;

			
		}
		/*for(int i2 = 1; i2 < 5; i2++){
			transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(false);
			if(i2 == i)transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(true);
		}*/
	}
	
	public void updateoptionsmenutab(int i){
		optionsmenustate = i;
		anim.SetInteger("lastopsmenutarg", anim.GetInteger("optsmenutarg"));
		anim.SetInteger("optsmenutarg", i);
		for(int i2 = 1; i2 < 5; i2++){
			transform.GetChild(0).GetChild(1).GetChild(0).GetChild(i2).gameObject.SetActive(false);
			if(i2 == i)transform.GetChild(0).GetChild(1).GetChild(0).GetChild(i2).gameObject.SetActive(true);
		}
		refreshbindingswindow();
	}
	
	public void callmenu(){
		if(menusubstate == 0){
			menuon = !menuon;
			if(menuon){
				Time.timeScale = 0;
				transform.GetChild(0).gameObject.SetActive(true);
				anim.SetInteger("menuon", 1);
			} else {
				Time.timeScale = 1;
				transform.GetChild(0).gameObject.SetActive(false);
				anim.SetInteger("menuon", 0);
			}
			if(menustate == 1)partymenuupdate();
		} else if (menusubstate == 5) {
			updatesubmenutab(0);
		} else if (menusubstate == 8 || menusubstate == 9){
			transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
			transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(false);
		} else {
			menusubstate = 0;
			anim.SetInteger("submenutarg", 0);
			transform.GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(false);
			transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
			StartCoroutine(turnoffsubmenu());
		}
		
	}
	
	public IEnumerator turnoffsubmenu(){
		yield return new WaitForSeconds(1f);
		transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
		transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
		transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(false);
		transform.GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(false);
	}
	
	public void savebindings(){
		string rebinds = inputAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
	}
	
	public void refreshbindingswindow(){
		string rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))inputAsset.LoadBindingOverridesFromJson(rebinds);
		InputAction actionToRebind = new InputAction();
		var directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Left");;
		for(int i = 0; i <= 11; i++){
			switch (i){
			case 0: //confirm
				actionToRebind = inputAsset.FindAction("confirm", true);
				break;
			case 1: //cancel
				actionToRebind = inputAsset.FindAction("cancel", true);
				break;
			case 2: //ability
				actionToRebind = inputAsset.FindAction("ability", true);
				break;
			case 3: //left
				actionToRebind = inputAsset.FindAction("move", true);
				directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Left");
				break;
			case 4: //dodge
				actionToRebind = inputAsset.FindAction("select 1", true);
				break;
			case 5: //defend
				actionToRebind = inputAsset.FindAction("select 2", true);
				break;
			case 6: //up
				actionToRebind = inputAsset.FindAction("move", true);
				directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Up");
				break;
			case 7: //down
				actionToRebind = inputAsset.FindAction("move", true);
				directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Down");
				break;
			case 8: //jump
				actionToRebind = inputAsset.FindAction("jump", true);
				break;
			case 9: //menu
				actionToRebind = inputAsset.FindAction("menu", true);
				break;
			case 10: //sprint
				actionToRebind = inputAsset.FindAction("sprint", true);
				break;
			case 11: //right
				actionToRebind = inputAsset.FindAction("move", true);
				directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Right");
				break;
			default:
				break;
			}
			transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = (actionToRebind.name.Equals("move")?actionToRebind.GetBindingDisplayString(directionbinding.bindingIndex):actionToRebind.GetBindingDisplayString());
		}
	}
	
	public void bindkey(int i){
		transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(i).GetComponent<Animator>().SetBool("blink",true);
		StartCoroutine(bindkey2(i));
	}
	
	
	public IEnumerator bindkey2(int i){
		yield return new WaitForEndOfFrame();
		transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
		InputAction actionToRebind = new InputAction();
		var directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Left");;
		switch (i){
			case 0: //confirm
				actionToRebind = inputAsset.FindAction("confirm", true);
				break;
			case 1: //cancel
				actionToRebind = inputAsset.FindAction("cancel", true);
				break;
			case 2: //ability
				actionToRebind = inputAsset.FindAction("ability", true);
				break;
			case 3: //left
				actionToRebind = inputAsset.FindAction("move", true);
				directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Left");
				break;
			case 4: //dodge
				actionToRebind = inputAsset.FindAction("select 1", true);
				break;
			case 5: //defend
				actionToRebind = inputAsset.FindAction("select 2", true);
				break;
			case 6: //up
				actionToRebind = inputAsset.FindAction("move", true);
				directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Up");
				break;
			case 7: //down
				actionToRebind = inputAsset.FindAction("move", true);
				directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Down");
				break;
			case 8: //jump
				actionToRebind = inputAsset.FindAction("jump", true);
				break;
			case 9: //menu
				actionToRebind = inputAsset.FindAction("menu", true);
				break;
			case 10: //sprint
				actionToRebind = inputAsset.FindAction("sprint", true);
				break;
			case 11: //right
				actionToRebind = inputAsset.FindAction("move", true);
				directionbinding = inputAsset.FindAction("move", true).ChangeCompositeBinding("2D Vector").NextPartBinding("Right");
				break;
			default:
				break;
		}
		actionToRebind.Disable();
		InputActionRebindingExtensions.RebindingOperation rebindOperation;
		if(actionToRebind.name.Equals("move")){
			rebindOperation = actionToRebind.PerformInteractiveRebinding(directionbinding.bindingIndex)
			.WithControlsExcluding("Mouse")
			.Start();
			actionToRebind.Enable();
		} else {
			rebindOperation = actionToRebind.PerformInteractiveRebinding()
			.WithControlsExcluding("Mouse")
			.Start();
			actionToRebind.Enable();
		}
		yield return new WaitUntil(() => rebindOperation.completed);
		rebindOperation.Dispose();
		transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = (actionToRebind.name.Equals("move")?actionToRebind.GetBindingDisplayString(directionbinding.bindingIndex):actionToRebind.GetBindingDisplayString());
		transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(i).GetComponent<Animator>().SetBool("blink",false);
		
	}
	
	public void partymenuupdate(){
		for(int i = 0; i < 5; i++){
			if(overworldmanager.OM.pc.transform.GetComponent<combatantdataholder>().cd[i] != null){
				transform.GetChild(0).GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
				transform.GetChild(0).GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>().sprite = overworldmanager.OM.pc.transform.GetComponent<combatantdataholder>().cd[i].icon;
			} else {
				transform.GetChild(0).GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>().color = Color.grey;
			}
		}
	}
	
	public void showface(int i){
		updatesubmenutab(i+10);
	}
	
	public void hideface(){
		transform.GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(false);	
	}
	
}
