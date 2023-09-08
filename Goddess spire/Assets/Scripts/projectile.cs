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
	public bool AoE = false;
	public float AoEdist = 0;
	public int explosionfx = -1;
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
				if(AoE){
					foreach(Combatant c in BattleMaster.combatants){
						if(Vector3.Distance(c.transform.position,transform.position) < AoEdist)c.take_damage(atk,pierce,atktype);
					}
					if(explosionfx != -1){
						Transform clone = Instantiate(BattleMaster.pl[explosionfx]);//spawn an explosion
						clone.position = transform.position;
						clone.localScale = transform.localScale;
					}
				}
				timer = 0;
				move = false;
				BattleMaster.killsound();
				Destroy(gameObject);
			} else if(Physics.Raycast(transform.position, bz.GetSegment(Mathf.Clamp01(timer+0.05f)), out hit, Vector3.Distance(transform.position,bz.GetSegment(Mathf.Clamp01(timer+0.05f))))){ 
				if(hit.transform.parent.GetComponent<Combatant>()){
					if(AoE){
						foreach(Combatant c in BattleMaster.combatants){
							if(Vector3.Distance(c.transform.position,transform.position) < AoEdist && c != hit.transform.parent.GetComponent<Combatant>())c.take_damage(atk,pierce,atktype);
							
						}
						if(explosionfx != -1){
							Transform clone = Instantiate(BattleMaster.pl[explosionfx]);//spawn an explosion
							clone.position = transform.position;
							clone.localScale = transform.localScale;
						}
					}
					hit.transform.parent.GetComponent<Combatant>().take_damage(atk,pierce,atktype);
					BattleMaster.killsound();
					Destroy(gameObject);
				}
			}
		}
    }
	
}
