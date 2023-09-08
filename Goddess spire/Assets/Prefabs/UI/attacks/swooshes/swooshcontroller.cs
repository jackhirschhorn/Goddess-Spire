using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class swooshcontroller : MonoBehaviour
{
    public ParticleSystem part;
	public int dam;
	public int damtype;
	private ParticleSystem.ShapeModule shap;
	private ParticleSystem.EmissionModule emis;
	private ParticleSystem.MainModule sett;
	public Color ten;
	public Color five;
	public Color one;
	public TextMeshProUGUI ind;
	public bool nodam = false;
	
	
	public void dothething(){
		shap = part.shape;
		emis = part.emission;
		sett = part.main;
		ind.text = dam+"";
		if(nodam)ind.text = "NO DAMAGE";
		transform.GetComponent<Animator>().SetInteger("anim",damtype);
		StartCoroutine("dothething1");
	}
	
	public IEnumerator dothething1(){
		while(dam > 0){
			if(damtype != 2 && damtype != 6)part.transform.localPosition = new Vector3((damtype == 0?-1:(damtype >= 3?Random.Range(-10,10)*0.1f:0)),0,(damtype >= 9?(Random.Range(-5,5)*0.1f)+1.9f:(Random.Range(-10,10)*0.1f)+0.5f));
			if(dam >= 10){
				sett.startColor = new ParticleSystem.MinMaxGradient(ten);
				sett.startSize = 2*(damtype == 2 || damtype == 6?0.25f:1);
				dam -= 10;
			} else if (dam >= 5){
				sett.startColor = new ParticleSystem.MinMaxGradient(five);
				sett.startSize = 1.5f*(damtype == 2 || damtype == 6?0.25f:1);				
				dam -= 5;
			} else {
				sett.startColor = new ParticleSystem.MinMaxGradient(one);
				sett.startSize = 1*(damtype == 2 || damtype == 6?0.25f:1);					
				dam -= 1;
			}	
			part.Emit(1);
			/*			
			ParticleSystem.Burst burst = emis.GetBurst(0);
			burst.count = (int)(timer*12);
			emis.SetBurst(0,burst);
			shap.arc = (int)(timer*12)*30;
			*/
			yield return new WaitForSeconds(0.05f);
		}
		Destroy(this.gameObject,1.1f);
	}
}
