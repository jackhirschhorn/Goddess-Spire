using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class overworldmanager : MonoBehaviour
{
	public static overworldmanager OM;
    
	public InputActionMap IA;
	public PlayerInput PI;
	public playercontroller pc;
	
	
	void Awake(){
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
	
	public static void resetplants(){
		OM.BroadcastMessage("plantresetpower");
		OM.BroadcastMessage("plantupdatelinks");
		OM.BroadcastMessage("plantupdatepower");
	}
}
