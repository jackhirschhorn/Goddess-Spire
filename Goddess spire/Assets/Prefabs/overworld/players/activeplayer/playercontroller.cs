using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
	
	public Animator anim;
	public float rotspeed = 1f;
	public bool is_sprinting = false;
	public Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	void LateUpdate(){
		camera.position = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			is_sprinting = !is_sprinting;
			if(!is_sprinting)anim.SetBool("sprint", false);
		}
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
			anim.SetBool((is_sprinting?"sprint":"walk"), true);
		} else {
			anim.SetBool("walk", false);
			anim.SetBool("sprint", false);
		}
		Vector2 rotass = new Vector2(0,0);
		if(Input.GetKey(KeyCode.W)){
			rotass[1]++;
		}
		if(Input.GetKey(KeyCode.S)){
			rotass[1]--;
		}
		if(Input.GetKey(KeyCode.A)){
			rotass[0]++;
		}
		if(Input.GetKey(KeyCode.D)){
			rotass[0]--;
		}
		if(rotass != Vector2.zero){			
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,(rotass[0] == -1 && rotass[1] == 0?0:(rotass[0] == -1 && rotass[1] == -1?45:(rotass[0] == 0 && rotass[1] == -1?90:(rotass[0] == 1 && rotass[1] == -1?135:(rotass[0] == 1 && rotass[1] == 0?180:(rotass[0] == 1 && rotass[1] == 1?225:(rotass[0] == 0 && rotass[1] == 1?270:315))))))),0),90f/Quaternion.Angle(transform.localRotation,Quaternion.Euler(0,(rotass[0] == -1 && rotass[1] == 0?0:(rotass[0] == -1 && rotass[1] == -1?45:(rotass[0] == 0 && rotass[1] == -1?90:(rotass[0] == 1 && rotass[1] == -1?135:(rotass[0] == 1 && rotass[1] == 0?180:(rotass[0] == 1 && rotass[1] == 1?225:(rotass[0] == 0 && rotass[1] == 1?270:315))))))),0))*rotspeed *(is_sprinting?2.5f:1)*Time.deltaTime);
		}
    }
}
