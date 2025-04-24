using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startchareditor : MonoBehaviour
{
    public combatantdataholder cdh;
	
	public void increasecharclass(int i){
		cdh.cd[i].clas++;
		if(cdh.cd[i].clas == 9)cdh.cd[i].clas = 0;
	}
	
	public void decreasecharclass(int i){
		cdh.cd[i].clas--;
		if(cdh.cd[i].clas == -1)cdh.cd[i].clas = 8;
	}
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
