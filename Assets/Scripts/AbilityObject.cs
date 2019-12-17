using System;
using UnityEngine;

public class AbilityObject : MonoBehaviour {
	public Action collisionAction;
	public Action updateAction;
	public bool destroyOnHit;
	public Vector3 target;
	public double movementSpeed;
	public double maximumDistance;
	public Vector3 originalPosition;
	public Entity source;
	public Entity collidedEntity;
	public double lifespan;
	private DateTime timeCreated;

	private void Start() {
		this.originalPosition = this.transform.position;
		this.timeCreated = DateTime.Now;
	}

	private void Update() {
		if (DateTime.Now - timeCreated > TimeSpan.FromSeconds(lifespan)) { Tools.Destroy(this.gameObject); }
		if (Vector3.Distance(this.originalPosition, this.transform.position) > this.maximumDistance) { Tools.Destroy(this.gameObject); }
		if (this.transform.position == this.target) { Tools.Destroy(this.gameObject); }

		this.transform.position = Vector3.MoveTowards(this.transform.position, this.target, (float) this.movementSpeed * Time.deltaTime);
		this.updateAction();
	}

	private void OnTriggerEnter(Collider collider) {
		Entity collisionEntity = collider.gameObject.GetComponent<Entity>();
		if (!collisionEntity) { return; }
		if (collisionEntity.team == this.source.team) { return; }

		this.collidedEntity = collisionEntity;
		this.collisionAction();

		if (this.destroyOnHit) { Tools.Destroy(this.gameObject); }
	}
}