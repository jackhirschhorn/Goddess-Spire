using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class spriteswapper : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[0];
	public Image img;
	public int spritenum = 0;
	public int indexoffset = 0;

    // Update is called once per frame
    void Update()
    {
        img.sprite = sprites[spritenum+indexoffset];
    }
}
