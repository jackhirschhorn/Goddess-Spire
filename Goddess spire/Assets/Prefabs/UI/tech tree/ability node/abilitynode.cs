using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilitynode : MonoBehaviour
{
    //ability
	public int tier = 0;
	public bool learned = false;
	public ParticleSystem parts;
	public LineRenderer lr;
	public abilitynode parnt;
	
	public void LateUpdate(){
		if(learned){
			if(!parts.isPlaying)parts.Play();
			if(parnt != null){
				lr.SetPosition(0, new Vector3(0,0,-10));				
				lr.SetPosition(1, ((parnt.transform as RectTransform).anchoredPosition-(transform as RectTransform).anchoredPosition));
				lr.SetPosition(1, lr.GetPosition(1) + new Vector3(0,0,-10));
			}
		} else {
			if(parts.isPlaying)parts.Stop();
		}
	}
}
