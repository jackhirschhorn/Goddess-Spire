using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartStopper : MonoBehaviour
{
	public int density = 1000;
	public int size = 10; 
	void Awake(){
		ParticleSystem sus =  transform.GetComponent<ParticleSystem>();
	    ParticleSystem.ShapeModule suss = sus.shape;
		ParticleSystem.EmissionModule suse = sus.emission;
		suse.rateOverTime = density;
		suss.radius = size;
		sus.Play();
	}
	
    // Start is called before the first frame update
    void Start()
    {
       ParticleSystem sus =  transform.GetComponent<ParticleSystem>();
	   sus.Pause();
    }
}