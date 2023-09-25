using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thermometer : MonoBehaviour
{
	public int stage = 0;
	public RectTransform img;
	public RectTransform img2;
	public AudioSource adso;
	public float timer = 0;
	public float charge = 0;
	public int damage = 0;
	public Combatant comb;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stage == 0){
			
		} else if (stage == -1){
			Destroy(this.gameObject);
		} else if (stage == 1){
			adso.enabled = true;
			timer += Time.deltaTime;
			charge -= Time.deltaTime/2f;
			charge = Mathf.Clamp01(charge);
			img.anchoredPosition = new Vector3(0,charge,0);
			img2.anchoredPosition = new Vector3(0,timer,0);
			adso.pitch = 0.5f + (charge*1.2f);		
			if(Input.GetKeyDown(KeyCode.E)){
				charge += 0.199f;
			}
			if(timer >= 2f){
				stage = 2;
				damage = (int)Mathf.Floor(charge * 5);
			}
			if(charge >= 1f){
				stage = 3;
				comb.take_damage(comb.magicdamage(3)+4, 0, 3);
			}
		} 
    }
}