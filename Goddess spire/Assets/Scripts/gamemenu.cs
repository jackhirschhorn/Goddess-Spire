using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemenu : MonoBehaviour
{
    public int menustate = 0;
	public int menusubstate = 0;
	public Animator anim;
	// Start is called before the first frame update
    void Start()
    {
        menustate = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))anim.SetInteger("menutarg",(anim.GetInteger("menutarg")+1)%5);
    }
}
