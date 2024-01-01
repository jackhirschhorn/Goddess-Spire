using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class firestrikeui : MonoBehaviour
{
	public int atk = 0;
	public int pierce = 0;
	public int stage = 0;
	public Animator[] fires = new Animator[0];
	public TextMeshProUGUI[] texts = new TextMeshProUGUI[0];
	public char[] buttons = new char[]{'A','S','D','Q','W','E'};
	public int[] rans = new int[0];
	public int substage = 0;
	public float timer = 0;
	public RectTransform img;
	public AudioSource adso;
	
	
    // Start is called before the first frame update
    void Start()
    {
		rans[0] = Random.Range(0,6);
        texts[0].text = ""+buttons[rans[0]];
		texts[0].enabled = false;
        rans[1] = Random.Range(0,6);
        texts[1].text = ""+buttons[rans[1]];
		texts[1].enabled = false;
		rans[2] = Random.Range(0,6);
        texts[2].text = ""+buttons[rans[2]];
		texts[2].enabled = false;
		rans[3] = Random.Range(0,6);
        texts[3].text = ""+buttons[rans[3]];
		texts[3].enabled = false;
    }

	public void OnConfirm2(InputAction.CallbackContext context){ //e
		if(context.performed){
			if(stage == 1){
				if(rans[substage] == 5){				
					fires[substage].SetBool("expand",false);
					substage += 1;
					BattleMaster.makesound(15);
				} else {
					stage = 2;
					atk = substage;
					pierce = substage;
				}
			}
		}
	}

	public void OnCancel2(InputAction.CallbackContext context){ //q
		if(context.performed){
			if(stage == 1){
				if(rans[substage] == 3){				
					fires[substage].SetBool("expand",false);
					substage += 1;
					BattleMaster.makesound(15);
				} else {
					stage = 2;
					atk = substage;
					pierce = substage;
				}
			}
		}
	}
	
	bool vecreset = true;
	
	public void OnMove2(InputAction.CallbackContext context){ //WASD
		Vector2 vec = context.ReadValue<Vector2>();
		if(vec == Vector2.zero)vecreset = true;
		if(vecreset){
			if(stage == 1){
				if(vec.y > 0){//w
					if(rans[substage] == 4){				
						fires[substage].SetBool("expand",false);
						substage += 1;
						BattleMaster.makesound(15);
					} else {
						stage = 2;
						atk = substage;
						pierce = substage;
					}
					vecreset = false;
				}
				if(vec.y < 0){//s
					if(rans[substage] == 1){				
						fires[substage].SetBool("expand",false);
						substage += 1;
						BattleMaster.makesound(15);
					} else {
						stage = 2;
						atk = substage;
						pierce = substage;
					}
					vecreset = false;
				}
				if(vec.x > 0){//d
					if(rans[substage] == 2){				
						fires[substage].SetBool("expand",false);
						substage += 1;
						BattleMaster.makesound(15);
					} else {
						stage = 2;
						atk = substage;
						pierce = substage;
					}
					vecreset = false;
				}
				if(vec.x < 0){//a
					if(rans[substage] == 0){				
						fires[substage].SetBool("expand",false);
						substage += 1;
						BattleMaster.makesound(15);
					} else {
						stage = 2;
						atk = substage;
						pierce = substage;
					}
					vecreset = false;
				}
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        if(stage == 0){			
		} else if (stage == -1){
			Destroy(this.gameObject);
		} else if (stage == 1){
			adso.enabled = true;
			if(substage == 4){
				stage = 2;
				atk = substage;
				pierce = substage;
			}
			texts[substage].enabled = true;
			fires[substage].SetBool("expand",true);
			timer += Time.deltaTime/2f;
			adso.pitch = 0.5f + (timer*1.2f);		
			img.anchoredPosition = new Vector3(timer,0,0);
			if(timer >= 1){
				stage = 2;				
				atk = substage;
				pierce = substage;
			}
		} else if (stage == 2){
			
		}
    }
}
