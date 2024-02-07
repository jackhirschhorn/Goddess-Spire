using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundplayer : MonoBehaviour
{
	public AudioSource sus;
	
	public AudioClip[] clips = new AudioClip[0];
	
	public int lastplayed = 0;
	
	public int varmin, varmax;
	
	public void Play(){
		lastplayed = Random.Range(0,clips.Length);
		sus.clip = clips[lastplayed];
		sus.pitch = Random.Range(varmin,varmax+1)*0.01f;
		sus.Play();
	}
	
	public void Play(int i){
		if(i == -1){
			sus.clip = clips[lastplayed];
		} else {
			sus.clip = clips[i];
		}
		sus.pitch = Random.Range(varmin,varmax+1)*0.01f;
		sus.Play();
	}
	
}
