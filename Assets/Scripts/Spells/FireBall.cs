using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent (typeof (ParticleSystem))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (SphereCollider))]
public class FireBall : SpellBase {


	public  GameObject fireBallPrefab;

	private ParticleSystem particles;
	private float maxParticleStartSpeed;
	private float maxParticleStartSize;

	private Rigidbody rigidBody;

	private SphereCollider collider;
	private float maxColliderRadius;

	private float colliderGrowRate;
	private float particleSpeedGrowRate;
	private float particleSizeGrowRate;

	public Vector3 Noise = Vector3.zero;
	public float Damping = 0.3f;
	Quaternion direction;

	 void Awake () {

		particles = gameObject.GetComponent<ParticleSystem>();
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		collider = gameObject.GetComponent<SphereCollider> ();

		maxParticleStartSpeed = particles.startSpeed;
		maxParticleStartSize = particles.startSize;

		particles.startSpeed = 0.0f;
		particles.startSize = 0.0f;

		collider.radius = 0.0f;

		colliderGrowRate = maxColliderRadius / (maxChargeTimeInSeconds);
		particleSpeedGrowRate = maxParticleStartSpeed / (maxChargeTimeInSeconds);
		particleSizeGrowRate = maxParticleStartSize / (maxChargeTimeInSeconds);

		direction = Quaternion.LookRotation (this.transform.forward * 1000);
		this.transform.Rotate (new Vector3 (Random.Range (-Noise.x, Noise.x), Random.Range (-Noise.y, Noise.y), Random.Range (-Noise.z, Noise.z)));

	}
	// delta time in seconds.
	public void Grow(float deltaTime){

		//If components are found
		if (particles && rigidBody && collider) {

			//grow size of the particles
			if (particles.startSize < maxParticleStartSize)
				particles.startSize += particleSizeGrowRate * deltaTime;

			//grow speed of the particles
			if (particles.startSpeed < maxParticleStartSpeed)
				particles.startSpeed += particleSpeedGrowRate * deltaTime;

			//grow radius of collider
			if (collider.radius <= maxColliderRadius)
				collider.radius += colliderGrowRate * deltaTime;
				
		}
			
	}
		
	public void Release(Vector3 dir){

		base.Release (dir);
		gameObject.transform.forward = dir;
		//rigidBody.velocity = dir * Speed;
	}

	virtual protected void Update () {


	}

	void LateUpdate ()
	{	
		if (IsReleased) {
			//this.transform.rotation = Quaternion.Lerp (this.transform.rotation, direction, Damping);
			this.transform.position += this.transform.forward * Speed * Time.deltaTime;
		}
	}
		

	virtual protected void OnCollisionEnter(Collision other)
	{

	
	}
}