using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Update is called once per frame
    void Update()
    {
        if(stage == 0){			
		} else if (stage == -1){
			Destroy(this.gameObject);
		} else if (stage == 1){
			adso.enabled = true;
			texts[substage].enabled = true;
			fires[substage].SetBool("expand",true);
			if(Input.GetKeyDown(rans[substage] == 0?KeyCode.A:(rans[substage] == 1?KeyCode.S:(rans[substage] == 2?KeyCode.D:(rans[substage] == 3?KeyCode.Q:(rans[substage] == 4?KeyCode.W:KeyCode.E)))))){				
				fires[substage].SetBool("expand",false);
				substage += 1;
				BattleMaster.makesound(15);
			} else if(Input.anyKeyDown){
				stage = 2;
				atk = substage;
				pierce = substage;
			}
			if(substage == 4){
				stage = 2;
				atk = substage;
				pierce = substage;
			}
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
