using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

	// Steam controller stuff
	private SteamVR_TrackedObject trackedObj;
	private GameObject collidingObject;
	private GameObject objectInHand;


	public GameObject explosionPrefab;

	private GameObject explosion;
	//private float GRENADE_FORCE = 3f;
	private static float GRENADE_RADIUS = 2f;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	private void SetCollidingObject(Collider col)
	{
		// If  already holding object, or target object has no Rigidbody, do not grab
		if ((collidingObject) || !col.GetComponent<Rigidbody>())
		{
			return;
		}
		// Otherwise assign object as a potential grab target
		collidingObject = col.gameObject;
	}

	// Update is called once per frame
	void Update() {

		if (Controller.GetHairTriggerDown())
		{
			if (collidingObject)
			{
				GrabObject();
			}
		}

		if (Controller.GetHairTriggerUp())
		{
			if (objectInHand)
			{
				ReleaseObject();
			}
		}
	}

	// Sets an object for collision if triggers enter each other
	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}

	// Sets an object for collision if triggers overlap for a while
	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	// Removes other object for collision if they don't overlap anymore
	public void OnTriggerExit(Collider other)
	{
		if (!collidingObject)
		{
			return;
		}

		collidingObject = null;
	}

	private void GrabObject()
	{
		objectInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}

	// Add joint which handles movement and breaking strength
	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	// Releases object in hand and starts explosion countdown
	private void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

			Rigidbody objectBody = objectInHand.GetComponent<Rigidbody>();
			objectBody.velocity = Controller.velocity;
			objectBody.angularVelocity = Controller.angularVelocity;

			if (objectInHand.CompareTag("Grenade")) ExplodeStart(objectInHand);
		}
		objectInHand = null;

	}

	// Starts the Co-routine for an explosion - needed in order to set a delay
	private void ExplodeStart(GameObject destroyObject)
	{
		StartCoroutine(Explosion(destroyObject));
	}

	private IEnumerator Explosion(GameObject destroyObject)
	{
		yield return new WaitForSecondsRealtime(2.0f);

		if (destroyObject != null)
		{
			destroyObject.GetComponent<GrenadeScript>().SetTimer();

			CreateExplosionParticles(destroyObject);
			DestroyExplosionObjects(destroyObject);
			DestroyHeldArmedGrenade();

		}
	}

	// Creates the particle system for the grenade explosion
	private void CreateExplosionParticles(GameObject explodingObject)
	{
		Rigidbody objectBody = explodingObject.GetComponent<Rigidbody>();

		explosion = Instantiate(explosionPrefab);
		ParticleSystem explosionSystem = explosion.GetComponent<ParticleSystem>();
		explosionSystem.Play();
		explosionSystem.transform.position = objectBody.position;
	}

	// Destroy the explosion, targets in explosion radius, and the exploding object
	private void DestroyExplosionObjects(GameObject explodingObject)
	{
		Rigidbody objectBody = explodingObject.GetComponent<Rigidbody>();

		Destroy(explosion, 1f);
		DestroyTargets(objectBody.position, GRENADE_RADIUS);
		Destroy(explodingObject);
	}

	// If an armed grenade explodes in your hand, detach it from hand
	private void DestroyHeldArmedGrenade()
	{
		if (objectInHand != null && objectInHand.GetComponent<GrenadeScript>().IsTimerSet() && GetComponent<FixedJoint>())
		{
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());
			objectInHand = null;
		}
	}

	// Destroys target colliders within a radius of a centre position
	private void DestroyTargets(Vector3 centre, float radius)
	{
		Collider[] hitColliders = Physics.OverlapSphere(centre, radius);
		foreach (Collider hit in hitColliders)
		{
			if (hit.CompareTag("Target"))
			{
				GameObject target = hit.GetComponent<Collider>().gameObject;
				Destroy(target);
			}
		}
	}

	/*
	private void AddGrenadeForce(GameObject explodingObject)
	{
		Rigidbody objectBody = explodingObject.GetComponent<Rigidbody>();
		objectBody.AddExplosionForce(GRENADE_FORCE, objectBody.position, GRENADE_RADIUS, 3.0F);
	}
	*/
}
