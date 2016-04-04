using UnityEngine;
using System.Collections;

public class SpellBase : MonoBehaviour {


	protected bool IsReleased = false;

	public float maxChargeTimeInSeconds = 3;
	public float Speed = 5;

	protected void Start () {


	}
		
	// Update is called once per frame
	public void Update () {


	}
		
	public void Release(Vector3 dir){
		IsReleased = true;

	}
		
}

