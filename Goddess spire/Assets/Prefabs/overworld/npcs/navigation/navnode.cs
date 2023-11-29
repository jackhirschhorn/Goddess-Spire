using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class navnode : MonoBehaviour
{
	public Dictionary<navnode,float> connections = new Dictionary<navnode,float>();
	public LayerMask lm;
	//call in editor, reset nav, test against every node in scene with a raycast, if path is clear record connection and distance.
	//debug mode that shows connections with debug.line or whatever
    public void findconnections_navnode(){
		connections = new Dictionary<navnode,float>();
		navnode[] tempnodes = GameObject.FindObjectsOfType<navnode>();
		foreach(navnode n in tempnodes){
			if(n != this && connections.Count < 4){
				if(Physics.Linecast(n.transform.position,transform.position, lm)){
					
				} else {
					connections.Add(n, Vector3.Distance(n.transform.position,transform.position));
				}
			}
		}
	}
}
