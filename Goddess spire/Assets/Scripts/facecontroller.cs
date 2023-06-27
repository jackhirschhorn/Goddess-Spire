using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facecontroller : MonoBehaviour
{
	public Transform nose;
	public Renderer lefteye;
	public Renderer righteye;
	public Renderer mouth;
	
	public bool centerlook;
	public Vector2 looktarg;
	public Texture2D[] topeyebrows;
	public int topeyebrow;
	public Texture2D[] bottomeyebrows;
	public int bottomeyebrow;
	public Texture2D[] externalclips;
	public int externalclip;
	public Texture2D[] internalclips;
	public int internalclip;
	public Texture2D[] eyebacks;
	public int eyeback;
	public Texture2D[] eyebackdetails;
	public int eyebackdetail;
	public Texture2D[] irises;
	public int iris;
	public Texture2D[] irisdetails;
	public int irisdetail;
	public Texture2D[] pupils;
	public int pupil;
	public Texture2D[] blinkeyelashes;
	public int blinkeyelash;
	public Texture2D[] highlights;
	public int highlight;
	public Color eyebrowcolor;
	public Color iriscolor;
	public Color scalaracolor;
	public float eyeraise;
	public float lowereyeraise;
	public float uppereyeraise;
	public float blinkthreshold;
	
	public Texture2D[] lips;
	public int lip;
	public Texture2D[] teeths;
	public int teeth;
	public Texture2D[] tongues;
	public int tongue;
	public Texture2D[] mouthclips;
	public int mouthclip;
	public Color lipcolor;
	public Color toothcolor;
	public Color tonguecolor;
	public Color backcolor;
	
	
	
    // Start is called before the first frame update
    void Start()
    {
		lefteye.material.SetFloat("_fliplook", 1);
		lefteye.material.SetFloat("_fliphighlights", 1);
		
		lefteye.material.SetTexture("_top_eyebrow", topeyebrows[topeyebrow]);
		lefteye.material.SetTexture("_bottom_eyebrow", bottomeyebrows[bottomeyebrow]);
		lefteye.material.SetTexture("_external_clip", externalclips[externalclip]);
		lefteye.material.SetTexture("_internal_clip", internalclips[internalclip]);
		lefteye.material.SetTexture("_eye_back", eyebacks[eyeback]);
		lefteye.material.SetTexture("_eyeback_detail", eyebackdetails[eyebackdetail]);
		lefteye.material.SetTexture("_iris", irises[iris]);
		lefteye.material.SetTexture("_iris_detail", irisdetails[irisdetail]);
		lefteye.material.SetTexture("_pupil", pupils[pupil]);
		lefteye.material.SetColor("_eyebrow_color", eyebrowcolor);
		lefteye.material.SetColor("_iriscolor", iriscolor);
		lefteye.material.SetColor("_scalara_color", scalaracolor);
		lefteye.material.SetFloat("_eye_raise", eyeraise);
		lefteye.material.SetFloat("_lower_eye_raise", lowereyeraise);
		lefteye.material.SetFloat("_upper_eye_raise", uppereyeraise);
		lefteye.material.SetFloat("_blink_threshold", blinkthreshold);
		
		righteye.material.SetTexture("_top_eyebrow", topeyebrows[topeyebrow]);
		righteye.material.SetTexture("_bottom_eyebrow", bottomeyebrows[bottomeyebrow]);
		righteye.material.SetTexture("_external_clip", externalclips[externalclip]);
		righteye.material.SetTexture("_internal_clip", internalclips[internalclip]);
		righteye.material.SetTexture("_eye_back", eyebacks[eyeback]);
		righteye.material.SetTexture("_eyeback_detail", eyebackdetails[eyebackdetail]);
		righteye.material.SetTexture("_iris", irises[iris]);
		righteye.material.SetTexture("_iris_detail", irisdetails[irisdetail]);
		righteye.material.SetTexture("_pupil", pupils[pupil]);
		righteye.material.SetColor("_eyebrow_color", eyebrowcolor);
		righteye.material.SetColor("_iriscolor", iriscolor);
		righteye.material.SetColor("_scalara_color", scalaracolor);
		righteye.material.SetFloat("_eye_raise", eyeraise);
		righteye.material.SetFloat("_lower_eye_raise", lowereyeraise);
		righteye.material.SetFloat("_upper_eye_raise", uppereyeraise);
		righteye.material.SetFloat("_blink_threshold", blinkthreshold);
		
		mouth.material.SetTexture("_lips", lips[lip]);
		mouth.material.SetTexture("_teeth", teeths[teeth]);
		mouth.material.SetTexture("_tongue", tongues[tongue]);
		mouth.material.SetTexture("_mouth_clipping", mouthclips[mouthclip]);
		mouth.material.SetColor("_lip_color",lipcolor);
		mouth.material.SetColor("_tooth_color",toothcolor);
		mouth.material.SetColor("_tongue_color",tonguecolor);
		mouth.material.SetColor("_back_color",backcolor);
		
		
    }

    // Update is called once per frame
    void Update()
    {
		lefteye.material.SetFloat("_fliplook", (centerlook?0:1));
        righteye.material.SetVector("_looktarg", looktarg);
		lefteye.material.SetVector("_looktarg", looktarg);
		
		nose.LookAt(Camera.main.transform);
    }
}
