using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lrbez : MonoBehaviour
{
	public LineRenderer lr;
	public BezierCurve bc;
	public int resolution = 10;
	
	public bool hastarg = false;
	public Transform targ;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lr.positionCount = resolution;
		lr.SetPositions(bc.GetSegments(resolution));
		if(hastarg)targ.position = bc.EndPosition;
    }
}
