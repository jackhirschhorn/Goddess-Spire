using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class spriteswapmaster : MonoBehaviour
{
	public spriteswapper[] spriteswaps = new spriteswapper[0];
	public int[] indexlist = new int[0];
	public int character = 0;
	public Animator anim;
	
    // Start is called before the first frame update
    void Start()
    {
        swap(character);
    }
	
	public void swap(int i){
		character = i;
		for(int i2 = 0; i2 < spriteswaps.Length; i2++){
			spriteswaps[i2].indexoffset = indexlist[i2]*character;
		}
		anim.SetInteger("char",character);
	}

}
