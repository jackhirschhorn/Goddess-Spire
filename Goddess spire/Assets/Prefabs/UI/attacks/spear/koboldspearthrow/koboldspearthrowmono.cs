using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koboldspearthrowmono : animredirect
{
    public Animator anim;
	public int stage = 0;
	public Combatant comb;
	// Start is called before the first frame update
	
	public Transform hand;
	
	Transform clone = null;
    void Start()
    {
		hand = anim.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(3);
		bz = new BezierCurve();
		bz.Points = new Vector3[]{transform.position + new Vector3(0,3,0), transform.position + new Vector3((anim.transform.parent.parent.GetComponent<Combatant>().isPC?2:-2),3,0), transform.position + new Vector3((anim.transform.parent.parent.GetComponent<Combatant>().isPC?3f:-3f),2,0), transform.position + new Vector3((anim.transform.parent.parent.GetComponent<Combatant>().isPC?3:-3),0,0)};
		clone = Instantiate(BattleMaster.pl[4]);
		clone.parent = BattleMaster.BM.combatmenu.parent;
		clone.position = new Vector3(0,0,0);
		clone.GetComponent<lrbez>().bc.Points = new Vector3[]{hand.position,Vector3.Lerp(hand.position,bz.GetSegment(0.5f),0.33f)+ new Vector3(0,0.5f,0),Vector3.Lerp(hand.position,bz.GetSegment(0.5f),0.66f)+ new Vector3(0,0.5f,0), bz.GetSegment(0.5f)};
		//clone.GetComponent<spearattackindicator>().comb = anim.transform.parent.parent.GetComponent<Combatant>();
		target = clone.GetComponent<lrbez>().bc.Points[3];
	}
	
	public Vector3 target;
	public BezierCurve bz;
	public float slider = 0.5f;
	public float power = 1f;
	public Vector3 trg;
	public float timer = 3.1f;
    // Update is called once per frame
    void Update()
    {
		if(stage == 0){		
			clone.GetComponent<lrbez>().bc.Points = new Vector3[]{hand.position,Vector3.Lerp(hand.position,bz.GetSegment(slider),0.33f)+ new Vector3(0,0.5f,0),Vector3.Lerp(hand.position,bz.GetSegment(slider),0.66f)+ new Vector3(0,0.5f,0), bz.GetSegment(slider)};	
		} else if(stage == -1){
			anim.SetInteger("stage",-1);
			anim.SetInteger("atkanim",0);
			Destroy(clone.gameObject);
			Destroy(this);
		} else if(stage == 1){
			slider = Mathf.Clamp01(slider - (Time.deltaTime/2f));
			if(Input.GetKey(KeyCode.A)){
				slider = Mathf.Clamp01(slider - Time.deltaTime);
			} else if (Input.GetKey(KeyCode.D)){
				slider = Mathf.Clamp01(slider + Time.deltaTime);				
			} else if (Input.GetKey(KeyCode.W)){
				trg = bz.GetSegment(slider);
				stage = 2;
			}
			timer -= Time.deltaTime;
			if(timer <= 0){
				trg = bz.GetSegment(slider);
				stage = 2;
				timer = 3.1f;
			}
			clone.GetComponent<lrbez>().bc.Points = new Vector3[]{hand.position,Vector3.Lerp(hand.position,bz.GetSegment(slider),0.33f)+ new Vector3(0,0.5f,0),Vector3.Lerp(hand.position,bz.GetSegment(slider),0.66f)+ new Vector3(0,0.5f,0), bz.GetSegment(slider)};
		} else if (stage == 2){
			power = Mathf.Clamp(power + Time.deltaTime,1,2.5f);
			clone.GetComponent<lrbez>().bc.Points = new Vector3[]{hand.position,Vector3.Lerp(hand.position,Vector3.LerpUnclamped(transform.position,trg,power),0.33f)+ new Vector3(0,0.5f,0),Vector3.Lerp(hand.position,Vector3.LerpUnclamped(transform.position,trg,power),0.66f)+ new Vector3(0,0.5f,0), Vector3.LerpUnclamped(transform.position,trg,power)};
			if(Input.GetKeyUp(KeyCode.W)){
				stage = 3;
				BattleMaster.killsound();
				BattleMaster.makesoundtokill(4);
			}
			timer -= Time.deltaTime;
			if(timer <= 0){
				stage = 3;
				BattleMaster.killsound();
				BattleMaster.makesoundtokill(4);
			}
		} else if (stage == 3){
			if(clone)Destroy(clone.gameObject);	
			anim.SetInteger("stage",2);
		} else if (stage == 4){
			if(!clone3){
				BattleMaster.attackcallback(0);
				anim.SetInteger("stage",3);
				anim.SetInteger("atkanim",0);
				Destroy(this);
			}
		}
    }
	public bool thrown = false;
	public Transform clone3;
	public override void directed(float f){
		if(!thrown){
			clone3 = Instantiate(BattleMaster.pl[5]);
			clone3.position = hand.GetChild(0).position;
			clone3.GetComponent<projectile>().atk = comb.weapondamage();
			clone3.GetComponent<projectile>().pierce = comb.pierce();
			clone3.GetComponent<projectile>().atktype = 0;
			Transform clone2 = Instantiate(hand.GetChild(0));
			clone3.GetChild(0).rotation = hand.GetChild(0).rotation;
			clone2.parent = clone3.GetChild(0);
			clone2.position = hand.GetChild(0).position;
			clone2.rotation = hand.GetChild(0).rotation;
			clone2.localScale = hand.GetChild(0).lossyScale;
			Vector3 startpoint = clone2.position;
			Vector3 endpoint = transform.position + new Vector3(Mathf.Abs(transform.position.x-Vector3.LerpUnclamped(transform.position,trg,power).x)*(anim.transform.parent.parent.GetComponent<Combatant>().isPC?4:-4),-1,0);
			clone3.GetComponent<projectile>().bz.Points = new Vector3[]{startpoint,Vector3.LerpUnclamped(transform.position,trg,power),Vector3.LerpUnclamped(transform.position,trg,power) + new Vector3(Mathf.Abs(transform.position.x-Vector3.LerpUnclamped(transform.position,trg,power).x)*(anim.transform.parent.parent.GetComponent<Combatant>().isPC?2:-2),0,0),endpoint};
			clone3.GetComponent<projectile>().speed = 0.85f*((2.5f-power)/4f+1);
			clone3.GetComponent<projectile>().move = true;
			thrown = true;
			stage = 4;
		}
	}
}
