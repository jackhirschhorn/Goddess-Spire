using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paladinsmite : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
		if(col.transform.GetComponent<enemypath>()){
			if(col.transform.GetComponent<enemypath>().isundead){
				col.transform.GetComponent<enemypath>().state = 2;
				col.transform.GetComponent<enemypath>().stateduration = 10f;
			} else {
				col.transform.GetComponent<enemypath>().state = 1;
				col.transform.GetComponent<enemypath>().stateduration = 2f;
			}
		}
	}
}
