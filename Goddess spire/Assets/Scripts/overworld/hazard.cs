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
		if(col.transform.GetComponent<playercontroller>() && col.transform.GetComponent<playercontroller>().canmove){
			col.transform.GetComponent<playercontroller>().hazardmode(dam, damtype, sink);
			Transform clone = Instantiate(fx);
			clone.position = col.transform.position;
			afx.Play();
		} 
		// npc logic
		// item logic
	}
}
