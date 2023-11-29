using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class navnodebaker : MonoBehaviour
{
    public bool doit = false;
	public GameObject owm;
	
	void Update(){
		if(doit){
			owm.BroadcastMessage("findconnections_navnode");
			doit = false;
		}
	}
}
