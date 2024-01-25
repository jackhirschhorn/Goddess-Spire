using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class gamemenu : MonoBehaviour
{
    public int menustate = 0;
	public int menusubstate = 0;
	public int optionsmenustate = 0;
	public Animator anim;
	public static gamemenu GM;
	public bool menuon = false;
	
	//itemscolor1
	public Color itemscolor1;
	public Color itemscolor2;
	
	//controllers
	public InputActionAsset inputAsset;
	
	//audio
	public Slider master, sfx, music, voices, menu;
	public AudioMixer amc;
	
	void Awake(){
		GM = this;
	}
	// Start is called before the first frame update
    void Start()
    {
        menustate = 3;
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
		for(int i2 = 1; i2 < 5; i2++){
			transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(false);
			if(i2 == i)transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(true);
		}
	}
	
	public void updatesubmenutab(int i){
		menusubstate = i;
		anim.SetInteger("submenutarg", i);
		if(menusubstate == 1){
			transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
			transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
		} else if(menusubstate <= 4){
			for(int i2 = 0; i2 < 3; i2++){
				transform.GetChild(0).GetChild(0).GetChild(2).GetChild(i2).GetChild(0).GetComponent<Image>().color = (i2+2 == menusubstate?itemscolor1:itemscolor2);
			}
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
		} else {
			menusubstate = 0;
			anim.SetInteger("submenutarg", 0);
			transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
		}
		
	}
	
}
