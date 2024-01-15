using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemenu : MonoBehaviour
{
    public int menustate = 0;
	public int menusubstate = 0;
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
		anim.SetInteger("menutarg", i);
		for(int i2 = 1; i2 < 5; i2++){
			transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(false);
			if(i2 == i)transform.GetChild(0).GetChild(0).GetChild(i2).gameObject.SetActive(true);
		}
	}
	
	public void callmenu(){
		menuon = !menuon;
		if(menuon){
			Time.timeScale = 0;
			transform.GetChild(0).gameObject.SetActive(true);
		} else {
			Time.timeScale = 1;
			transform.GetChild(0).gameObject.SetActive(false);
		}
		
	}
}
