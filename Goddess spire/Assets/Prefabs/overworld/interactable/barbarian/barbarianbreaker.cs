using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barbarianbreaker : MonoBehaviour
{
	public bool broken = false;
	public AudioSource sfx;
	void OnTriggerEnter(Collider col){
		Debug.Log(col.transform.GetComponent<Animator>().GetBool("barbcharge"));
		if(col.transform.GetComponent<playercontroller>() && col.transform.GetComponent<playercontroller>().classid == 0 && col.transform.GetComponent<Animator>().GetBool("barbcharge")){
			broken = true;
			sfx.Play();
			transform.GetComponent<Animator>().SetBool("break", true);
		}
	}
}
