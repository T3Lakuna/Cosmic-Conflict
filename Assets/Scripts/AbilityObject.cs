﻿using System;
using UnityEngine;

public class AbilityObject : MonoBehaviour {
	public Action collisionAction;
	public Action updateAction;
	public Action timeoutAction;
	public bool destroyOnHit;
	public bool destroyAtMaxRange;
	public Vector3 target;
	public Entity targetEntity;
	public float movementSpeed;
	public float maximumDistance;
	public Vector3 originalPosition;
	public Entity source;
	public Entity collidedEntity;
	public float lifespan;
	private DateTime _timeCreated;
	public bool canHitAllies;
	public bool canOnlyHitTarget;

	private void Start() {
		this.originalPosition = this.transform.position;
		this._timeCreated = DateTime.Now;
		this.transform.parent = MatchManager.Instance.abilityHolder;
		this.name = this.source.name + "'s Ability";
	}

	private void Update() {
		if (DateTime.Now - this._timeCreated > TimeSpan.FromSeconds(this.lifespan)) {
			this.timeoutAction?.Invoke();
			Tools.Destroy(this.gameObject);
		}

		if (this.destroyAtMaxRange && Vector3.Distance(this.originalPosition, this.transform.position) > this.maximumDistance) { Tools.Destroy(this.gameObject); }

		if (this.destroyAtMaxRange && this.transform.position == this.target) { Tools.Destroy(this.gameObject); }

		this.transform.position = Vector3.MoveTowards(this.transform.position, this.target, (float) this.movementSpeed * Time.deltaTime);
		if (this.targetEntity) { this.target = this.targetEntity.transform.position; }

		this.updateAction?.Invoke();
	}

	private void OnTriggerEnter(Collider collider) {
		Entity collisionEntity = collider.gameObject.GetComponent<Entity>();
		if (!collisionEntity) { return; }

		if (!this.canHitAllies && collisionEntity.team == this.source.team) { return; }

		if (this.canOnlyHitTarget && collisionEntity != this.targetEntity) { return; }

		this.collidedEntity = collisionEntity;
		this.collisionAction?.Invoke();

		if (this.destroyOnHit) { Tools.Destroy(this.gameObject); }
	}
}