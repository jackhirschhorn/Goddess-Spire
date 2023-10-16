using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class overworldmanager : MonoBehaviour
{
	public static overworldmanager OM;
    
	public InputActionAsset IA;
	public PlayerInput PI;
	
	void awake(){
		OM = this;
	}
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
