using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="combatantdata")]
public class combatantdata : ScriptableObject
{
	public string name = "";
	public int clas = -1; //-1 no class, 0 barbarian, 1 KI master, 2 paladin, 3 ranger, 4 phantom, 5 bard, 6 wizard, 7 cleric, 8 druid
    public bool strong = false;
	public brain AI;
	public bool humanoid = false;
	public bool ismaincharacter = false;
	public int phenotype = 0;
    public Stats statblock;
	public Sprite icon;
	
	public List<combatoption> class_CO = new List<combatoption>();
	public List<combatoption> weapon_CO = new List<combatoption>();
	
	
	public Color red;
	public Color blue;
	public Color green;
	public float height = 1.5f;
	
	public AnimatorController anim;
	public int idleanim = 0;
	public Transform model;
	
	//inventory
	public equipment[] equipmend = new equipment[12];
	
	public void reconstruct(Combatant c){
		phenotype = c.phenotype;
		humanoid = c.humanoid;
		strong = c.strong;
		statblock = c.statblock;
		class_CO = c.class_CO;
		weapon_CO = c.weapon_CO;
		red = c.red;
		blue = c.blue;
		green = c.green;
		height = c.height;
	}
}
