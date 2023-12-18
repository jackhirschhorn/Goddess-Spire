using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class xptotaler : MonoBehaviour
{
    public int xp;
	public Image barscroll;
	public TextMeshProUGUI txt;
	public Combatant comb;
	
	

    // Update is called once per frame
    void Update()
    {
        barscroll.transform.localPosition = new Vector3(200*((comb.statblock.nextxp+1f)/(comb.statblock.xp+1f)),0,0);
		txt.text = (comb.statblock.nextxp-(comb.statblock.xp+xp)) + "";
		if(comb.statblock.nextxp-(comb.statblock.xp+xp) <= 0){
			//level up animation
			transform.GetComponent<Animator>().SetBool("levelup",true);
			txt.text = "LEVEL UP!";//replace later once we have a proper xp curve
		}
    }
}
