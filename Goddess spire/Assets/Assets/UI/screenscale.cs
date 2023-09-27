using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenscale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		RectTransform myRect = transform as RectTransform;
        myRect.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
	
	void LateUpdate(){
		transform.localScale = new Vector3(1f/transform.parent.localScale.x,1f/transform.parent.localScale.y,1);
	}

}
