using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class spriteswapmaster : MonoBehaviour
{
	public spriteswapper[] spriteswaps = new spriteswapper[0];
	public int[] indexlist = new int[0];
	public int character = 0;
	public spriteswapper[] spriteswaps2 = new spriteswapper[0];
	public int[] indexlist2 = new int[0];
	public int clas = 0;
	
	public Animator anim;
	
    // Start is called before the first frame update
    void Start()
    {
        swap();
		swapclas();
    }
	
	public void swap(int i){
		character = i;
		for(int i2 = 0; i2 < spriteswaps.Length; i2++){
			spriteswaps[i2].indexoffset = indexlist[i2]*character;
		}
		anim.SetInteger("char",character);
	}
	
	public void swap(){
		for(int i2 = 0; i2 < spriteswaps.Length; i2++){
			spriteswaps[i2].indexoffset = indexlist[i2]*character;
		}
		anim.SetInteger("char",character);
	}
	
	public void swapclas(int i){
		clas = i;
		for(int i2 = 0; i2 < spriteswaps2.Length; i2++){
			spriteswaps2[i2].indexoffset = indexlist2[i2]*(clas+1);
		}
	}
	
	public void swapclas(){
		for(int i2 = 0; i2 < spriteswaps2.Length; i2++){
			spriteswaps2[i2].indexoffset = indexlist2[i2]*(clas+1);
		}
	}

}
