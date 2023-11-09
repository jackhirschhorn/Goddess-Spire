using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class wizardtabletnode : MonoBehaviour
{
    public int power = 0;
	public wizardtabletnode[] nodes = new wizardtabletnode[8];
	public int[] powerin = new int[8];
	public int[] powerout = new int[8];	
	public LineRenderer[] lrs = new LineRenderer[8];
	public Transform[] anchors = new Transform[8];
	// 0 1 2 square
	// 3 * 4
	// 5 6 7
	public int invertdirection(int i){
		return 7-i;
	}
	// 0 1  hex
	//2 * 3
	// 4 5
	public int inverthex(int i){
		return 5-i;
	}
	
	public virtual void FixedUpdate(){
		for(int i = 0; i < 8; i++){
			if(nodes[i] != null && powerout[i] > 0){
				lrs[i].SetPosition(0,anchors[i].position); 
				lrs[i].SetPosition(1,nodes[i].anchors[invertdirection(i)].position); 
				lrs[i].material.SetFloat("_speed",powerout[i]*-0.1f);
				lrs[i].enabled = true;
				anchors[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = powerout[i] + "";
			} else {
				lrs[i].enabled = false;
				anchors[i].GetChild(0).GetComponent<TextMeshProUGUI>().text =  "";
			}
		}
	}
	
	public virtual bool connect(int p,int d){ //power and direction sending TO
		if(p >= power)return true;
		return false;
	}
}
