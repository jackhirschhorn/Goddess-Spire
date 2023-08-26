using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public BezierCurve bz = new BezierCurve();
	public bool move = false;
	public int atk = 0;
	public int atktype = 0;
	public int pierce = 0;
	public float timer = 0;
	public float speed = 1;
	RaycastHit hit;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(move){
			timer += Time.deltaTime*speed;
			transform.position = bz.GetSegment(timer);
			transform.GetChild(0).LookAt(bz.GetSegment(Mathf.Clamp01(timer+0.01f)));
			if(timer >= 1){
				timer = 0;
				move = false;
				Destroy(gameObject);
			}
			if(Physics.Raycast(transform.position, bz.GetSegment(Mathf.Clamp01(timer+0.05f)), out hit, Vector3.Distance(transform.position,bz.GetSegment(Mathf.Clamp01(timer+0.05f))))){
				if(hit.transform.parent.GetComponent<Combatant>()){
					hit.transform.parent.GetComponent<Combatant>().take_damage(atk,pierce);
					Destroy(gameObject);
				}
			}
		}
    }
	
}
