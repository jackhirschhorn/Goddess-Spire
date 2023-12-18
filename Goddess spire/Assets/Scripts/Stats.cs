using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Stats 
{
    public int atk,spd,def,mag,res,lck,hp,chp,stam,cstam,mana,cmana, lvl, xp, nextxp;
	public float[] resistances = new float[]{1,1,1,1,1,1,1,1,1,1,1};
	[SerializeField]
	public List<Vector2> statusresistance = new List<Vector2>(); //x = id, y = modifier, 0 = prone
	public List<itemscript> deathdrops = new List<itemscript>();
	public int[] weaponxp = new int[]{0,0,0,0,0,0,0,0,0,0,0,0};
	public int[] weaponnextxp = new int[]{0,0,0,0,0,0,0,0,0,0,0,0};
	//0 sword,1 axe,2 bludgeon,3 pierce,4 dagger,5 katana,6 bows, 7 crossbow/gun,8 wand,9 book,10 staff,11 instrument 
	
	public void start(){
		
	}
	
	public int get_spd(){
		if(spd>255)return 255;
		return spd;
	}
}
