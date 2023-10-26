using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barbarianbreaker : MonoBehaviour
{
	public bool broken = false;
	public AudioSource sfx;
	public ParticleSystem pfx1;	
	public ParticleSystem pfx2;
	
	void OnTriggerEnter(Collider col){
		if(!broken && col.transform.GetComponent<playercontroller>() && col.transform.GetComponent<playercontroller>().classid == 0 && col.transform.GetComponent<Animator>().GetBool("barbcharge")){
			broken = true;
			sfx.Play();
			transform.GetComponent<Animator>().SetBool("break", true);
			if(Vector3.Dot(col.transform.right,transform.right) > 0){
				pfx1.Play();
			} else {
				pfx2.Play();
			}
		}
	}
}
