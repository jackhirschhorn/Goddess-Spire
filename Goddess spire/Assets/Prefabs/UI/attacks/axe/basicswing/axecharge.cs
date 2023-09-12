using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axecharge : MonoBehaviour
{
	public RectTransform img;
	public float charge = 0f;
	public float timer = 0f;
	public RectTransform timerimg;
	public int stage = 0;
	public ParticleSystem ps;
	public AudioSource adso;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stage == 0){
			if(Input.GetKeyDown(KeyCode.E))stage = 1;
		} else if (stage == -1){
			Destroy(this.gameObject);
		} else if (stage == 1){
			adso.gameObject.SetActive(true);
			if(Input.GetKeyDown(KeyCode.E)){
				img.anchoredPosition = new Vector3((1f*charge),0,0);
				charge = Mathf.Clamp01(charge+(Random.Range(1,5)*0.03f));
			} else {				
				img.anchoredPosition = Vector3.Lerp(img.anchoredPosition, new Vector3((1f*charge),0,0),0.5f);
			}
			adso.pitch = 0.5f + (charge*1.2f);			
			timer += Time.deltaTime;
			if(timer >= 2)stage = 2;
			if(charge >= 1)ps.Play();
			timerimg.anchoredPosition = new Vector3((-1.2f+(1.2f*(timer/2f))),0,0);
		} else if (stage == 2){
			
		}
    }
}
