using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemenu : MonoBehaviour
{
    public int menustate = 0;
	public int menusubstate = 0;
	public int optionsmenustate = 0;
	public Animator anim;
	public static gamemenu GM;
	public bool menuon = false;
	
	void Awake(){
		GM = this;
	}
	// Start is called before the first frame update
    void Start()
    {
        menustate = 3;
    }

    // Update is called once per frame
    void Update()
    {
		
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
		}
		/*for(int i2 = 1; i2 < 5; i2++){
			transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(false);
			if(i2 == i)transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(true);
		}*/
	}
	
	public void updateoptionsmenutab(int i){
		optionsmenustate = i;
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
			} else {
				Time.timeScale = 1;
				transform.GetChild(0).gameObject.SetActive(false);
			}
		} else {
			menusubstate = 0;
			anim.SetInteger("submenutarg", 0);
			transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
			transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
		}
		
	}
}
