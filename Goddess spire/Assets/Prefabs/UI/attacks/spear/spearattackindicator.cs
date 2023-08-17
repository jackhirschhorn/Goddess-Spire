using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearattackindicator : MonoBehaviour
{
    public RectTransform cursor;
	public int stage = 0;
	public RectTransform RTtimer;
	public float timer = 0;
	public float timesec = 3;
	public attackmono atk;
	public Combatant comb;
	public int pull = 1;
	public float totalpull = 0;
	
	
	void Start(){
		cursor.localPosition = new Vector2(Random.Range(-100,101),cursor.localPosition.y);
		pull = (Random.Range(0,2) == 0?-1:1);
	}
	
    // Update is called once per frame
    void Update()
    {
        if(stage == 1){
			timer += Time.deltaTime;
			if(timer%0.2f < 0.03f) pull = (Random.Range(0,2) == 0?-1:1); 
			totalpull = (Input.GetKey(KeyCode.A)?-2f:(Input.GetKey(KeyCode.D))?2f:0);
			totalpull += pull;
			cursor.localPosition = new Vector2(Mathf.Clamp(cursor.localPosition.x+(totalpull*1.5f),-100,100),cursor.localPosition.y);
			RTtimer.offsetMin = new Vector2(0-((105f*timer)/timesec),RTtimer.offsetMin.y);
			RTtimer.localPosition = new Vector2(0,0);
			if(timer >= timesec){
				stage = 2;
				int rating = (cursor.localPosition.x < 10 && cursor.localPosition.x > -10? 2 : (cursor.localPosition.x < 35 && cursor.localPosition.x > -35?1:0));
				atk.damage = comb.weapondamage() + rating;
				atk.pierce = comb.pierce() + rating*2;
				Destroy(this);
			}
		}
    }
}
