using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koboldspitmono : MonoBehaviour
{
    public Animator anim;
	public int stage = 0;
	public Combatant comb;
	public List<Color> cols = new List<Color>();
	
	Transform clone;
	Transform clone2;
	Transform head;
	Vector3 front;
	Vector3 end;
	public bool first = true;
	public float timer = 0;
	
	public Vector3 AItarg;
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
		var main = clone2.GetComponent<ParticleSystem>().main;
		main.startColor = new ParticleSystem.MinMaxGradient(cols[comb.phenotype*2], cols[comb.phenotype*2+1]);
		main = clone2.GetChild(0).GetComponent<ParticleSystem>().main;
		main.startColor = new ParticleSystem.MinMaxGradient(cols[comb.phenotype*2], cols[comb.phenotype*2+1]);
		BezierCurve bz = new BezierCurve();
		front = BattleMaster.unitlist(!comb.isPC,0).transform.position +new Vector3((comb.isPC?-1:1),0,0);
		end = BattleMaster.unitlist(!comb.isPC,BattleMaster.targettotal(!comb.isPC)-1).transform.position +new Vector3((comb.isPC?1:-1),0,0); 
		bz.Points = new Vector3[]{clone2.position, Vector3.Lerp(head.position,Vector3.Lerp(front,end,0.5f),0.5f)+ new Vector3(-1,5,0), Vector3.Lerp(head.position,Vector3.Lerp(front,end,0.5f),0.5f)+ new Vector3(1,5,0),Vector3.Lerp(front,end,0.5f)};
		clone.GetComponent<lrbez>().bc.Points = bz.Points;
		BattleMaster.makesound(5);
		if(!comb.isPC){
			List<Combatant> temp = BattleMaster.gettargs(!comb.isPC);
			int rando = Random.Range(0,temp.Count*2-1);
			if(rando%2 == 0){
				AItarg = temp[(int)rando/2].transform.position;
			} else {
				AItarg = Vector3.Lerp(temp[(int)(rando-1)/2].transform.position,temp[(int)(rando+1)/2].transform.position,0.5f);
			}
			
		}
	}
	bool flip = false;
	float slider = 0.5f;
	float powar = 0f;
    // Update is called once per frame
    void Update()
    {
		if(comb.isPC){
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
				if(Input.GetKeyDown(KeyCode.E) && !first){
					stage = 2;
					BattleMaster.makesoundtokill(8);
				}
				first = false;
			} else if (stage == 2){
				slider = Mathf.Clamp01(slider + (Time.deltaTime*(flip?-1:1)));
				if(slider == 1 || slider == 0)flip = !flip;
				clone.GetComponent<lrbez>().bc.Points = new Vector3[]{clone2.position, Vector3.Lerp(head.position,Vector3.Lerp(front,end,slider),0.5f)+ new Vector3(-1,5,0), Vector3.Lerp(head.position,Vector3.Lerp(front,end,slider),0.5f)+ new Vector3(1,5,0),Vector3.Lerp(front,end,slider)};
				powar += Time.deltaTime;
				if(powar >= 1.75f)stage = 3;
				if(Input.GetKeyUp(KeyCode.E))stage = 4;
				clone2.localScale = new Vector3(2+(powar*2),2+(powar*2),2+(powar*2));
			} else if (stage == 3){
				//self damage
				BattleMaster.killsound();
				Destroy(clone.gameObject);
				anim.SetInteger("stage",-1);	
				anim.SetInteger("atkanim",0);				
				comb.take_damage(comb.magicdamage(0)+(int)powar,0,3+comb.phenotype);
				BattleMaster.attackcallback(0);	
				clone = Instantiate(BattleMaster.pl[19]);//spawn an explosion
				var main = clone.GetComponent<ParticleSystem>().main;
				main.startColor = new ParticleSystem.MinMaxGradient(cols[comb.phenotype*2], cols[comb.phenotype*2+1]);
				clone.position = clone2.position;
				clone.localScale = new Vector3(4,4,4);
				Destroy(clone2.gameObject);
				Destroy(this);
				//play explosion
			} else if (stage == 4){
				//spit
				BattleMaster.killsound();
				BattleMaster.makesound(6);			
				BattleMaster.makesoundtokill(7);
				var main = BattleMaster.pl[19].GetComponent<ParticleSystem>().main;
				main.startColor = new ParticleSystem.MinMaxGradient(cols[comb.phenotype*2], cols[comb.phenotype*2+1]);
				clone2.parent = BattleMaster.BM.transform;
				clone2.GetComponent<projectile>().atk = comb.magicdamage(comb.phenotype)+(int)powar; // make variable based on phenotype
				clone2.GetComponent<projectile>().atktype = 3+comb.phenotype;// make variable based on phenotype
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
		} else {
			if(stage == 1){
				BattleMaster.makesoundtokill(8);
				clone.GetComponent<lrbez>().bc.Points = new Vector3[]{clone2.position, Vector3.Lerp(head.position,AItarg,0.5f)+ new Vector3(2,8,0), Vector3.Lerp(head.position,AItarg,0.5f)+ new Vector3(-2,8,0),AItarg};
				stage = 2;
				timer = 0;
			} else if (stage == 2){
				timer += Time.deltaTime;
				clone2.localScale = new Vector3(2+(timer*2),2+(timer*2),2+(timer*2));
				if(timer >= 1.5f){
					stage = 3;
				}
			} else if (stage == 3){
				//spit
				BattleMaster.killsound();
				BattleMaster.makesound(6);			
				BattleMaster.makesoundtokill(7);
				var main = BattleMaster.pl[19].GetComponent<ParticleSystem>().main;
				main.startColor = new ParticleSystem.MinMaxGradient(cols[comb.phenotype*2], cols[comb.phenotype*2+1]);
				clone2.parent = BattleMaster.BM.transform;
				clone2.GetComponent<projectile>().atk = comb.magicdamage(comb.phenotype)+2; // make variable based on phenotype
				clone2.GetComponent<projectile>().atktype = 3+comb.phenotype;// make variable based on phenotype
				clone2.GetComponent<projectile>().pierce = 0; //???
				clone2.GetComponent<projectile>().AoEdist = 3;
				clone2.GetComponent<projectile>().bz.Points = clone.GetComponent<lrbez>().bc.Points;
				clone2.GetComponent<projectile>().move = true;
				clone2.localScale = new Vector3(2+(timer*2),2+(timer*2),2+(timer*2));
				Destroy(clone.gameObject);
				anim.SetInteger("stage",2);	
				anim.SetInteger("atkanim",0);		
				stage = 4;
			} else if (stage == 4){
				if(!clone2){				
					BattleMaster.attackcallback(0);	
					Destroy(this);
				}
			}
		}
    }
}
