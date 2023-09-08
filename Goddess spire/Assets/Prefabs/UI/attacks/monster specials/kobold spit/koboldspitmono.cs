using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koboldspitmono : MonoBehaviour
{
    public Animator anim;
	public int stage = 0;
	public Combatant comb;
	
	Transform clone;
	Transform clone2;
	Transform head;
	Vector3 front;
	Vector3 end;
	void Start(){
		clone = Instantiate(BattleMaster.pl[4]);
		clone.parent = BattleMaster.BM.combatmenu.parent;
		clone.position = new Vector3(0,0,0);
		clone2 = Instantiate(BattleMaster.pl[18]);
		head = anim.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1);
		clone2.parent = head;
		clone2.localPosition = new Vector3(0.9f,2.4f,-0.5f);
		clone2.localScale = new Vector3(2,2,2);
		clone2.rotation = Quaternion.Euler(0,0,0);
		BezierCurve bz = new BezierCurve();
		front = BattleMaster.unitlist(!anim.transform.parent.parent.GetComponent<Combatant>().isPC,0).transform.position +new Vector3((anim.transform.parent.parent.GetComponent<Combatant>().isPC?-1:1),0,0);
		end = BattleMaster.unitlist(!anim.transform.parent.parent.GetComponent<Combatant>().isPC,4).transform.position +new Vector3((anim.transform.parent.parent.GetComponent<Combatant>().isPC?1:-1),0,0); // FIX THIS
		bz.Points = new Vector3[]{clone2.position, Vector3.Lerp(head.position,Vector3.Lerp(front,end,0.5f),0.5f)+ new Vector3(-1,5,0), Vector3.Lerp(head.position,Vector3.Lerp(front,end,0.5f),0.5f)+ new Vector3(1,5,0),Vector3.Lerp(front,end,0.5f)};
		clone.GetComponent<lrbez>().bc.Points = bz.Points;
	}
	bool flip = false;
	float slider = 0.5f;
	float powar = 0f;
    // Update is called once per frame
    void Update()
    {
        if(stage == 0){
			
		} else if (stage == -1){
			anim.SetInteger("stage",-1);
			anim.SetInteger("atkanim",0);
			Destroy(clone.gameObject);
			Destroy(clone2.gameObject);
			Destroy(this);
		} else if (stage == 1){
			slider = Mathf.Clamp01(slider + (Time.deltaTime*(flip?-1:1)));
			if(slider == 1 || slider == 0)flip = !flip;
			clone.GetComponent<lrbez>().bc.Points = new Vector3[]{clone2.position, Vector3.Lerp(head.position,Vector3.Lerp(front,end,slider),0.5f)+ new Vector3(-1,5,0), Vector3.Lerp(head.position,Vector3.Lerp(front,end,slider),0.5f)+ new Vector3(1,5,0),Vector3.Lerp(front,end,slider)};
			if(Input.GetKey(KeyCode.Space)){
				stage = 2;
			}
		} else if (stage == 2){
			slider = Mathf.Clamp01(slider + (Time.deltaTime*(flip?-1:1)));
			if(slider == 1 || slider == 0)flip = !flip;
			clone.GetComponent<lrbez>().bc.Points = new Vector3[]{clone2.position, Vector3.Lerp(head.position,Vector3.Lerp(front,end,slider),0.5f)+ new Vector3(-1,5,0), Vector3.Lerp(head.position,Vector3.Lerp(front,end,slider),0.5f)+ new Vector3(1,5,0),Vector3.Lerp(front,end,slider)};
			powar += Time.deltaTime;
			if(powar >= 1.75f)stage = 3;
			if(Input.GetKeyUp(KeyCode.Space))stage = 4;
			clone2.localScale = new Vector3(2+(powar*2),2+(powar*2),2+(powar*2));
		} else if (stage == 3){
			//self damage
			Destroy(clone.gameObject);
			comb.take_damage(comb.magicdamage(0)+(int)powar,0,3+0);
			anim.SetInteger("stage",0);	
			anim.SetInteger("atkanim",0);
			BattleMaster.attackcallback(0);	
			clone = Instantiate(BattleMaster.pl[19]);//spawn an explosion
			clone.position = clone2.position;
			clone.localScale = new Vector3(4,4,4);
			Destroy(clone2.gameObject);
			Destroy(this);
			//play explosion
		} else if (stage == 4){
			//spit
			clone2.parent = BattleMaster.BM.transform;
			clone2.GetComponent<projectile>().atk = comb.magicdamage(0)+(int)powar; // make variable based on phenotype
			clone2.GetComponent<projectile>().atktype = 3+0;// make variable based on phenotype
			clone2.GetComponent<projectile>().pierce = 0; //???
			clone2.GetComponent<projectile>().AoEdist = powar*2;
			clone2.GetComponent<projectile>().bz.Points = clone.GetComponent<lrbez>().bc.Points;
			clone2.GetComponent<projectile>().move = true;
			clone2.localScale = new Vector3(2+(powar*2),2+(powar*2),2+(powar*2));
			Destroy(clone.gameObject);
			anim.SetInteger("stage",2);	
			anim.SetInteger("atkanim",0);		
			stage = 5;
		} else if (stage == 5){
			if(!clone2){				
				BattleMaster.attackcallback(0);	
				Destroy(this);
			}
		}
    }
}