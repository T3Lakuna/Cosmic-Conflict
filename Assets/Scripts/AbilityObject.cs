using System;
using UnityEngine;

public class AbilityObject : MonoBehaviour {
	public Ability sourceAbility;
	public Action collisionAction;
	public Action updateAction;
	public bool destroyOnHit;
	public Vector3 target;
	public int movementSpeed;

	private void Update() {
		this.transform.position = Vector3.MoveTowards(this.transform.position, this.target, this.movementSpeed * Time.deltaTime);
		this.updateAction();
	}

	private void OnCollisionEnter(Collision collision) {
		Entity collisionEntity = collision.gameObject.GetComponent<Entity>();
		if (!collisionEntity) { return; }

		this.collisionAction();

		if (this.destroyOnHit) { Tools.Destroy(this.gameObject); }
	}
}