using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazard : MonoBehaviour
{
	public Transform fx;
	public AudioSource afx;
	public int dam;
	public int damtype;
	public bool sink = false;
    
	void OnTriggerEnter(Collider col){
		if(col.transform.GetComponent<playercontroller>() && !overworldmanager.OM.pc.transform.GetComponent<Animator>().GetBool("hazard") && (col.transform.GetComponent<playercontroller>().canmove || overworldmanager.OM.pc.transform.GetComponent<Animator>().GetBool("pray"))){
			col.transform.GetComponent<playercontroller>().hazardmode(dam, damtype, sink);
			Transform clone = Instantiate(fx);
			clone.position = col.transform.position;
			overworldmanager.OM.pc.transform.GetComponent<Animator>().Play("atklayer.blank",-1, 0);
			afx.Play();
		} 
		// npc logic
		// item logic
	}
}
