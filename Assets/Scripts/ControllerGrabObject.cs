using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject grenadeTemplate;

	private GameObject explosion;

	private SteamVR_TrackedObject trackedObj;
	private GameObject collidingObject;
	private GameObject objectInHand;

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
		//If  already holding object, or target object has no Rigidbody, do not grab
		if (collidingObject || !col.GetComponent<Rigidbody>())
		{
			return;
		}
		//Otherwise assign object as a potential grab target
		collidingObject = col.gameObject;
	}

	// Update is called once per frame
	void Update() {
		// 1
		if (Controller.GetHairTriggerDown())
		{
			if (collidingObject)
			{
				GrabObject();
			}
		}

		// 2
		if (Controller.GetHairTriggerUp())
		{
			if (objectInHand)
			{
				ReleaseObject();
			}
		}
	}

	//Sets an object for collision if triggers enter each other
	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}

	//Sets an object for collision if triggers overlap for a while
	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	//Removes other object for collision if they don't overlap anymore
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

	//Add joint which handles movement and breaking strength
	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	private void ReleaseObject()
	{
		// 1
		if (GetComponent<FixedJoint>())
		{
			// 2
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());
			// 3
			Rigidbody objectBody = objectInHand.GetComponent<Rigidbody>();
			objectBody.velocity = Controller.velocity;
			objectBody.angularVelocity = Controller.angularVelocity;

			ExplodeStart(objectInHand);

		}
		// 4
		objectInHand = null;

	}

	private void ExplodeStart(GameObject destroyObject)
	{
		StartCoroutine(Explosion(destroyObject));
	}

	private IEnumerator Explosion(GameObject destroyObject)
	{
		//TODO - set ignore collision between controller and grenade once thrown to prevent regrabbing of thrown grenade
		//Physics.IgnoreCollision(
		yield return new WaitForSecondsRealtime(2.0f);
		if (destroyObject != null)
		{
			Rigidbody objectBody = destroyObject.GetComponent<Rigidbody>();
			explosion = Instantiate(explosionPrefab);
			ParticleSystem explosionSystem = explosion.GetComponent<ParticleSystem>();
			explosionSystem.Play();

			explosionSystem.transform.position = objectBody.position;

			Destroy(explosion, 1f);
			DestroyTargets(objectBody.position, 5f);
			Destroy(destroyObject);
			CreateNewGrenade();

			//In case grenade gets regrabbed during explosion timer, disconnect everything
			if (GetComponent<FixedJoint>())
			{
				GetComponent<FixedJoint>().connectedBody = null;
				Destroy(GetComponent<FixedJoint>());
				objectInHand = null;
			}
		}

	}

	private void CreateNewGrenade()
	{
		grenadeTemplate.GetComponent<InstantiateGrenade>().CreateGrenade();
	}

	private void DestroyTargets(Vector3 centre, float radius)
	{
		Collider[] hitColliders = Physics.OverlapSphere(centre, radius);
		int i = 0;
		while (i < hitColliders.Length)
		{
			if (hitColliders[i].CompareTag("Target"))
			{
				Destroy(hitColliders[i].GetComponent<Collider>().gameObject);
			}
			i++;
		}
	}

}
