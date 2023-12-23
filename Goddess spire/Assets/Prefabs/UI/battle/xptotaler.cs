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
	bool xp2 = true;
	public Image icon;
	public int weapontype = -1;
	

    // Update is called once per frame
    void Update()
    {
		barscroll.transform.localPosition = new Vector3(200*((comb.statblock.xp+xp+0f)/(comb.statblock.nextxp+0f)),0,0);
		if(comb.statblock.nextxp-(comb.statblock.xp+xp) == 0){
			if(xp2){
				//level up animation
				transform.GetComponent<Animator>().SetBool("levelup",true);
				txt.characterSpacing = 0;
				txt.text = "LEVEL UP!";//replace later once we have a proper xp curve
				xp2 = false;
			}
		} else {
			txt.characterSpacing = 20;
			txt.text = (comb.statblock.nextxp-(comb.statblock.xp+xp)) + "";
			xp2 = true;
		}
    }
}
