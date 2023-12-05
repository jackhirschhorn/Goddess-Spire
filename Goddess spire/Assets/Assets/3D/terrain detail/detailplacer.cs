using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class detailplacer : MonoBehaviour
{
	public int numparts = 0;
	public Transform part;
	public float radius;
	public LayerMask lm;
	
	
    public void detailplacer_place(){
		RaycastHit hit = new RaycastHit();
		for(int i = 0; i < numparts; i++){
			if(Physics.Raycast(transform.position + new Vector3(Random.Range(-radius*100,radius*100)*0.01f,0,Random.Range(-radius*100,radius*100)*0.01f), -Vector3.up, out hit, 1000, lm)){
				Transform clone = Instantiate(part);
				clone.parent = this.transform;
				clone.position = hit.point + new Vector3(0,0.5f,0);
			}
		}
	}
	
	public void detailplacer_clear(){
		while(transform.childCount > 0)
		{
			DestroyImmediate(transform.GetChild(0).gameObject);
		}
	}
}
