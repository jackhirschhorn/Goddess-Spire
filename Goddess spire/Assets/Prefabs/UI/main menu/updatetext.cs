using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class updatetext : MonoBehaviour
{
    public TextMeshProUGUI txt;
	public combatantdataholder cdh;
	
	public void updateclasstext(int i){
		txt.text = cdh.cd[i].getclasname();
	}
}
