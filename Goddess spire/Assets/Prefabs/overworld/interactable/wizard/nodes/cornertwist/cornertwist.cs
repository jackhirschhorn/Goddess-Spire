using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cornertwist : wizardtabletnode
{
    //rotates to connect nodes
	// 0 1 2
	// 3 * 4
	// 5 6 7
	public int connectstate = 0; //0 = 3-6+1-4, 1 == 0-5+2-7, ect, total 4
	int[,] connectmatrix = {
		{3,6,1,4},
		{0,5,2,7},
		{1,3,4,6},
		{2,0,7,5}};
	
	void Start(){
		for(int i = 0; i < 4; i++){
			if(nodes[connectmatrix[connectstate,i]] != null)
				powerin[connectmatrix[connectstate,i]] = nodes[connectmatrix[connectstate,i]].powerout[invertdirection(connectmatrix[connectstate,i])];
		}
	}
	
	public override void FixedUpdate(){
		base.FixedUpdate();
		for(int i = 0; i < 3; i += 2){
			if(nodes[connectmatrix[connectstate,0+i]] != null && nodes[connectmatrix[connectstate,1+i]] != null){
				if(powerin[connectmatrix[connectstate,0+i]] > powerin[powerin[connectmatrix[connectstate,1+i]]]){
					//both directions are outputting, one is larger
					powerout[connectmatrix[connectstate,1+i]] = powerin[connectmatrix[connectstate,0+i]]; 
					nodes[connectmatrix[connectstate,1+i]].connect(powerout[connectmatrix[connectstate,1+i]],connectmatrix[connectstate,1+i]);
					powerout[connectmatrix[connectstate,0+i]] = 0;
					nodes[connectmatrix[connectstate,0+i]].connect(powerout[connectmatrix[connectstate,0+i]],connectmatrix[connectstate,0+i]);
				} else {
					powerout[connectmatrix[connectstate,0+i]] = powerin[connectmatrix[connectstate,1+i]];
					nodes[connectmatrix[connectstate,0+i]].connect(powerout[connectmatrix[connectstate,0+i]],connectmatrix[connectstate,0+i]);
					powerout[connectmatrix[connectstate,1+i]] = 0;
					nodes[connectmatrix[connectstate,1+i]].connect(powerout[connectmatrix[connectstate,1+i]],connectmatrix[connectstate,1+i]);
				}
			}
		}
		//if(nodesout[0])nodes[0].connect(power);
		
	}
	
	public RectTransform mg;
	public AudioSource sfx;
	public void rotate(){
		if(!transform.GetChild(2).GetComponent<Animator>().GetBool("play")){
			connectstate++;
			if(connectstate == 4)connectstate = 0;
			mg.eulerAngles = mg.eulerAngles + new Vector3(0,0,-45);
			sfx.Play();
			transform.GetChild(2).GetComponent<Animator>().SetBool("play",true);
			//update connections
			for(int i2 = 0; i2 < 8; i2++){
				powerin[i2] = 0;
				powerout[i2] = 0;
				if(nodes[i2] != null){
					nodes[i2].connect(0,i2);
				}
			}
			for(int i = 0; i < 4; i++){
				if(nodes[connectmatrix[connectstate,i]] != null)
					powerin[connectmatrix[connectstate,i]] = nodes[connectmatrix[connectstate,i]].powerout[invertdirection(connectmatrix[connectstate,i])];
				
			}
		}
	}
}
