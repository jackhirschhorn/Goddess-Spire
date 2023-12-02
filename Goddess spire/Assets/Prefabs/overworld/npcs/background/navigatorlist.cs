using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigatorlist : MonoBehaviour
{
	
    public List<navnode> connections = new List<navnode>();
	public List<float> connectionswait = new List<float>();
	
	public Transform target;
	public float timer;
	public Rigidbody cc;
	public float rotspeed = 1f;
	public bool is_sprinting = false;
	
	//replace this if laggy
	
	public virtual IEnumerator Start(){ //pathfinding?
		while(true){
			if(target == null){//find target
				target = connections[0].transform;
			}
			//Vector2 rotass = target.position-transform.right;
			//transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0), 90f/Quaternion.Angle(transform.localRotation, Quaternion.Euler(0,(Mathf.Atan2(-rotass[1], rotass[0]) * Mathf.Rad2Deg),0))*rotspeed *(is_sprinting?2.5f:1)*Time.deltaTime);
			//cc.velocity += ((new Vector3(rotass.x,0,rotass.y)*(is_sprinting?2.9f:1.9f)*50f*Time.fixedDeltaTime));
			//cc.velocity = new Vector3(Mathf.Clamp(cc.velocity.x,rotass.x*(is_sprinting?2.9f:1.9f)*5f,rotass.x*(is_sprinting?2.9f:1.9f)*5f),cc.velocity.y,Mathf.Clamp(cc.velocity.z,rotass.y*(is_sprinting?2.9f:1.9f)*5f,rotass.y*(is_sprinting?2.9f:1.9f)*5f));
			transform.LookAt(target);
			cc.velocity = transform.forward*3;
			yield return new WaitForEndOfFrame();
		}
	}
	
}
