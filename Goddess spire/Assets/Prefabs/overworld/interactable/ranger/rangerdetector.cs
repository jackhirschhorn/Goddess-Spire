using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangerdetector : MonoBehaviour
{
	public Animator anim;
	public AudioSource sfx;
    public void shimmer(){
		//delay shimmer based on distance from player
		//might need to mess with the shader? use width as an offset?
		StartCoroutine(shimmer1());
	}
	
	public IEnumerator shimmer1(){
		float temp = Vector3.Distance(transform.position, overworldmanager.OM.pc.transform.position);
		float temp2 = 0;
		while(temp+temp2 > 0){
			temp = Vector3.Distance(transform.position, overworldmanager.OM.pc.transform.position);
			temp2 -= Time.deltaTime*5;
			yield return new WaitForEndOfFrame();
		}
		if(temp < 6){
			anim.SetBool("play",true);	
			sfx.Play();
		}			
	}
	
}
