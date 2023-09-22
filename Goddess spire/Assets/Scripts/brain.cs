using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brain : ScriptableObject
{
    public List<combatoption> CO = new List<combatoption>();
	
	public virtual combatoption pickoption(){
		return CO[Random.Range(0,CO.Count)];
	}
}
