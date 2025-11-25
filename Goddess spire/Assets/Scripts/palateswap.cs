using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//[ExecuteInEditMode]
public class palateswap : MonoBehaviour
{
	public bool swap = false;
	public bool sprites = false;
	public List<Texture2D> tex = new List<Texture2D>();
	public List<RenderTexture> rtex = new List<RenderTexture>();
	public List<Texture2D> rtexf = new List<Texture2D>();
	public Material swapmat;
	public List<Renderer> rends = new List<Renderer>();
	public bool combatant = true;
	private Combatant comb;
	private combatantdata combdata;

	
	void Start(){
		swap = true;
		if(combatant && !sprites){
			for(int i = 0; i < tex.Count; i++){
				rtexf.Add(new Texture2D(256,256,TextureFormat.ARGB32,false));
			}
		}
	}
    // Update is called once per frame
    void Update()
    {
		if(swap && !sprites){
			if(combatant){
				comb = transform.parent.parent.GetComponent<Combatant>();
				swapmat.SetColor("_COLOR_RED", comb.red);
				swapmat.SetColor("_COLOR_BLUE", comb.blue);
				swapmat.SetColor("_COLOR_GREEN", comb.green);
			}
			for(int i = 0; i < tex.Count; i++){
				Graphics.Blit(tex[i],rtex[i],swapmat,0); 
				if(combatant)Graphics.CopyTexture(rtex[i],rtexf[i]);
			}
			swap = false;
			if(combatant){
				for(int i = 0; i < rends.Count; i++){
					rends[i].material.SetTexture("_BaseMap", rtexf[i]);
				}
			}
		}
		if(sprites && swap){
			for(int i = 0; i < tex.Count; i++){
				if(combatant){
					comb = transform.parent.parent.parent.parent.GetComponent<Combatant>();
					Color[] pixels = tex[i].GetPixels();
					for(int i2 = 0; i2 < pixels.Length; i2++){
						if(pixels[i2] == Color.red){
							pixels[i2] = comb.red;
						}
						if(pixels[i2] == Color.blue){
							pixels[i2] = comb.blue;
						}
						if(pixels[i2] == Color.green){
							pixels[i2] = comb.green;
						}
					}
					rtexf[i].SetPixels(pixels);
					rtexf[i].Apply();
				} else {
					combdata = transform.parent.parent.GetComponent<combatantdataholder>().cd[overworldmanager.OM.pc.partyleader];
					Color[] pixels = tex[i].GetPixels();
					for(int i2 = 0; i2 < pixels.Length; i2++){
						if(pixels[i2] == Color.red){
							pixels[i2] = combdata.red;
						}
						if(pixels[i2] == Color.blue){
							pixels[i2] = combdata.blue;
						}
						if(pixels[i2] == Color.green){
							pixels[i2] = combdata.green;
						}
					}
					rtexf[i].SetPixels(pixels);
					rtexf[i].Apply();
				}
				
			}
			swap = false;
		}
        
    }
	
}
