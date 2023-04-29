using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stats 
{
    public int atk,spd,def,mag,res,lck,hp,chp,stam,cstam,mana,cmana;
	
	public int get_spd(){
		if(spd>255)return 255;
		return spd;
	}
}
