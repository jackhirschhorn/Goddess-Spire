using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartStopper : MonoBehaviour
{
	public int density = 1000;
	public int size = 10; 
	void Start(){
		StartCoroutine(dothethingpartstopper());
	}
	
	public IEnumerator dothethingpartstopper(){
		ParticleSystem sus =  transform.GetComponent<ParticleSystem>();
	    ParticleSystem.ShapeModule suss = sus.shape;
		ParticleSystem.EmissionModule suse = sus.emission;
		ParticleSystem.Burst burst = suse.GetBurst(0);
		burst.count = density;
		suse.SetBurst(0,burst);
		suss.radius = size;
		sus.Play();
		yield return new WaitForEndOfFrame();
		sus.Pause();
	}
	
	/*void Awake(){
		Debug.Log("ye");
		ParticleSystem sus =  transform.GetComponent<ParticleSystem>();
	    ParticleSystem.ShapeModule suss = sus.shape;
		ParticleSystem.EmissionModule suse = sus.emission;
		ParticleSystem.Burst burst = suse.GetBurst(0);
		burst.count = density;
		suse.SetBurst(0,burst);
		suss.radius = size;
		sus.Play();
	}
	
	void Start(){
		ParticleSystem sus =  transform.GetComponent<ParticleSystem>();
		sus.Pause();
	}*/
	
}
