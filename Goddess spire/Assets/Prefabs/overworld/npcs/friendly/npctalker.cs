using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class npctalker : interactable
{
    public Sprite icon;
	public dialtree dtass;
	public dialtree dt;
	public Color talkercolor;
	public bool talkin = false;
	
	void Start()
    {
		dt = Instantiate(dtass);
		dt.talker = this;
    }
	
	public override void interact(InputAction.CallbackContext context){
		if(on){
			if(!talkin){
				opendial();
			} else {
				advancedial();
			}
		}
	}
	
	public virtual void opendial(){
		//called from playercontroller, open UI and pause gameplay
		overworldmanager.OM.talkingUI.SetActive(true);
		overworldmanager.OM.talkingUI.GetComponent<Animator>().SetBool("movein",true);
		overworldmanager.OM.talkingUIicon.sprite = icon;
		overworldmanager.OM.talkingUItxt.text = dt.startdial();
		overworldmanager.OM.talkingUItxt.color = talkercolor;
		overworldmanager.OM.pc.canmove = false;
		talkin = true;
	}
	
	public virtual void closedial(){
		//called from either cancel button or dialtree, close UI and resume gameplay
		overworldmanager.OM.talkingUI.GetComponent<Animator>().SetBool("moveout",true);
		StartCoroutine("closedial2");
		overworldmanager.OM.pc.canmove = true;
		talkin = false;
	}
	
	public IEnumerator closedial2(){
		yield return new WaitForSeconds(0.3f);
		overworldmanager.OM.talkingUI.SetActive(false);
	}
	
	public virtual void advancedial(){
		//press A to continue speaking, just say the next string, increase dialtree dialpointer by 1
		overworldmanager.OM.talkingUItxt.text = dt.advancedial();
	}
	
	public virtual void choosedial(int i){
		// chosing what to say, increase dialtree by the decision #, apply any flags, display.
	}
	
	public virtual void advancetodial(int i){
		//press A to continue speaking, just say the next string, increase dialtree dialpointer by 1
		overworldmanager.OM.talkingUItxt.text = dt.advancetodial(i);
	}
	
}
