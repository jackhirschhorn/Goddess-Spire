using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class palateswap : MonoBehaviour
{
	public bool swap = false;
	public List<Texture2D> tex = new List<Texture2D>();
	public List<RenderTexture> rtex = new List<RenderTexture>();
	public Material swapmat;

	
	void Start(){
		swap = true;
	}
    // Update is called once per frame
    void Update()
    {
		if(swap){
			for(int i = 0; i < tex.Count; i++){
				Graphics.Blit(tex[i],rtex[i],swapmat,0); 
			}
			swap = false;
		}
        
    }
	
}
