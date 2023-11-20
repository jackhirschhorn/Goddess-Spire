using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadetocamera : MonoBehaviour
{
    
	public List<Renderer> rends = new List<Renderer>();
	public LayerMask lm;
	
    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit[] hits = Physics.SphereCastAll(Camera.main.transform.position, 5f, overworldmanager.OM.pc.transform.position - Camera.main.transform.position, Vector3.Distance( overworldmanager.OM.pc.transform.position, Camera.main.transform.position)-5f, lm);
		foreach(RaycastHit h in hits){
			if(h.collider.GetComponent<Renderer>()){
				if(rends.Contains(h.collider.GetComponent<Renderer>())){
					foreach(Material m in h.collider.GetComponent<Renderer>().materials){
						m.SetFloat("_DitherThreshold",0.2f);
					}
				} else {					
					rends.Add(h.collider.GetComponent<Renderer>());
					foreach(Material m in h.collider.GetComponent<Renderer>().materials){
						m.SetFloat("_DitherThreshold",0.2f);
					}
				}
			}
		}
		for(int i = 0; i < rends.Count; i++){
			bool trub = true;
			foreach(Material m in rends[i].materials){
				m.SetFloat("_DitherThreshold",m.GetFloat("_DitherThreshold")+Time.fixedDeltaTime);
				if(m.GetFloat("_DitherThreshold") < 1){
					trub = false;
				}
			}
			if(trub){
				rends.RemoveAt(i);
				i--;
			}
		}
	}
}
