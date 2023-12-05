using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class detailplacercaller : MonoBehaviour
{
    public bool doit = false;
	public bool undoit = false;
	public GameObject owm;
	
	void Update(){
		if(doit){
			owm.BroadcastMessage("detailplacer_place");
			doit = false;
		}
		if(undoit){
			owm.BroadcastMessage("detailplacer_clear");
			undoit = false;
		}
	}
}
