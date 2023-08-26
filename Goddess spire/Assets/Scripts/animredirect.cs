using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animredirect : MonoBehaviour
{
    public virtual void redirect(float f){
		Component[] objs = GetComponents(typeof(animredirect));
		foreach(Component c in objs){
			(c as animredirect).directed(f);
		}
	}
	
	public virtual void directed(float f){
		
	}
	
	public animredirect(){
		
	}
}
