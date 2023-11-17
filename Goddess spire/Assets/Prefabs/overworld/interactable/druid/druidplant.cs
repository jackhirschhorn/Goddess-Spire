using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class druidplant : interactable
{
    public druidplant(){
		butt = "ability";
	}
	
	public override void interact(InputAction.CallbackContext context){
		if(context.performed && overworldmanager.OM.pc.classid == 8){
			markthis();
		}
	}
	
	public GameObject linkcanvasin;
	public GameObject linkcanvasout;
	public int linkstate = 0; // 0 = none, 1 = out, 2 = in;
	public druidplant linkto;
	public LineRenderer linelink;
	public Transform linker;
	public int linkedto = 0;
	public int power;
	public Color flowerpower = new Color(25,180,25,255); //default to green
	public int adjpower; //after adjustments
	public Color adjflowerpower = new Color(25,180,25,255);
	public static druidplant linkin;
	public static druidplant linkout;
	
	public int powerrequire;
	public Color flowerpowerrequire = new Color(25,180,25,255);
		
	public virtual void markthis(){
		linkstate++;
		if(druidplant.linkout != null && druidplant.linkout != this && linkstate == 1){
			linkstate++;
		}
		if(linkstate == 1){
			if(druidplant.linkin == this)druidplant.linkin = null;
			druidplant.linkout = this;
		} else if(linkstate == 2){
			if(druidplant.linkout == this)druidplant.linkout = null;
			druidplant.linkin = this;
		} else if(linkstate > 2){
			linkstate = 0;			
			druidplant.linkin = null;
		}
		if(druidplant.linkin != null && druidplant.linkout != null){
			druidplant.linkout.linkto = druidplant.linkin;
			if(druidplant.linkin.linkto = druidplant.linkout)druidplant.linkin.linkto = null;
			druidplant.linkout.linkstate = 0;
			druidplant.linkin.linkstate = 0;
			druidplant.linkin = null;
			druidplant.linkout = null;
			overworldmanager.resetplants();
		}
	}
	
	public Image mg;
	public Image mg2;
	
	public void Awake(){
		plantresetpower();
		plantupdatepower();
		mg2.color = flowerpowerrequire;
		mg2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = powerrequire +"";
	}
	
	
	
	public virtual void plantresetpower(){
		adjpower = power;
		adjflowerpower = flowerpower;
		mg.color = adjflowerpower;
		mg.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = adjpower +"";
		linkedto = 0;
	}
	
	public virtual void plantupdatelinks(){
		if(linkto != null) linkto.linkedto++;
	}
	
	
	public virtual void plantupdatepower(){ // need to fix, only takes 1 power input
		if(linkedto == 0){ //has something pointing at this
			if(linkto != null){
				linkto.adjpower += adjpower;
				linkto.adjflowerpower = Color.Lerp(adjflowerpower,linkto.adjflowerpower, 0.5f);
				linkto.mg.color = linkto.adjflowerpower;
				linkto.mg.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = linkto.adjpower +"";
				linkto.plantupdatepower();
				//linkto.linkedto--;
			}
		} else {
			linkedto--;
		}
	}
	
	public AudioSource sfx1;
	public AudioSource sfx2;
	
	public virtual void dothething(){
		if(!transform.GetChild(0).GetComponent<Animator>().GetBool("bloom")){
			sfx1.Play();
		}
		transform.GetChild(0).GetComponent<Animator>().SetBool("bloom",true);
	}
	
	public virtual void undothething(){
		if(transform.GetChild(0).GetComponent<Animator>().GetBool("bloom")){
			sfx2.Play();
		}
		transform.GetChild(0).GetComponent<Animator>().SetBool("bloom",false);
	}
	
	public virtual bool checklink(){
		if(on){
			return true;
		} else if(linkto != null) {
			return linkto.checklink();
		} else {
			return false;
		}
		
	}
	
	public virtual void FixedUpdate(){
		if(linkto != null && (on || linkto.checklink())){
			linelink.enabled = true;
			linelink.material.SetColor("_Color",adjflowerpower);
			linelink.SetPosition(0,linker.position);
			linelink.SetPosition(1,linkto.linker.position);
		} else {
			linelink.enabled = false;
		}
		if(adjpower >= powerrequire && comparecolor(adjflowerpower,flowerpowerrequire)){
			dothething();
		} else {
			undothething();
		}
		if(linkstate == 0){
			linkcanvasin.SetActive(false);
			linkcanvasout.SetActive(false);			
		} else if (linkstate == 1){
			linkcanvasin.SetActive(false);
			linkcanvasout.SetActive(true);
		} else if (linkstate == 2){			
			linkcanvasin.SetActive(true);
			linkcanvasout.SetActive(false);
		}
	}
	
	public bool comparecolor(Color c1, Color c2){
		return (Mathf.Approximately(c1.r,c2.r) && Mathf.Approximately(c1.g,c2.g) && Mathf.Approximately(c1.b,c2.b));
	}
	
}
