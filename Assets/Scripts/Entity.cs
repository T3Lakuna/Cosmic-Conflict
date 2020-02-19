using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using static Ability;
using static StatusEffect;

public abstract class Entity : MonoBehaviourPun, IPunObservable {
	public enum Team { Red, Blue, Neutral }

	protected const int ExperiencePerLevelPerLevel = 50;

	[HideInInspector] public List<StatusEffectType> currentStatusEffects;
	[HideInInspector] public List<Item> items;
	[HideInInspector] public Item trinket;
	[HideInInspector] public Team team;
	[HideInInspector] public Vector3 movementTarget;
	[HideInInspector] public Entity basicAttackTarget;
	[HideInInspector] public float basicAttackCooldown;
	[HideInInspector] public Ability basicAttackAbility;
	[HideInInspector] public List<System.Action<Entity>> basicAttackActions;
	[HideInInspector] public List<System.Action<Entity>> bufferedBasicAttackActions;
	private EntityDisplay _display;

	[HideInInspector] public int kills;
	[HideInInspector] public int deaths;
	[HideInInspector] public int assists;

	[HideInInspector] public float health; // Current health
	[HideInInspector] public float shield; // Current shield
	[HideInInspector] public float physicalShield; // Current physical damage-only shield
	[HideInInspector] public float magicalShield; // Current magical damage-only shield
	[HideInInspector] public float resource; // Current resource
	[HideInInspector] public float experience; // Current progress towards next level
	[HideInInspector] public int level; // Current level

	// TODO: When rewriting, combine these into a dictionary or stat-holder class of some sort.
	[HideInInspector] public Stat damage; // Physical damage
	[HideInInspector] public Stat magic; // Magical damage
	[HideInInspector] public Stat vitality; // Maximum health
	[HideInInspector] public Stat regeneration; // Health regeneration
	[HideInInspector] public Stat energy; // Maximum resource
	[HideInInspector] public Stat endurance; // Resource regeneration
	[HideInInspector] public Stat armor; // Physical damage resistance
	[HideInInspector] public Stat nullification; // Magical damage resistance.
	[HideInInspector] public Stat force; // Physical damage resistance penetration
	[HideInInspector] public Stat pierce; // Magical damage resistance penetration
	[HideInInspector] public Stat vamp; // Life steal
	[HideInInspector] public Stat fervor; // Attack speed
	[HideInInspector] public Stat speed; // Movement speed
	[HideInInspector] public Stat tenacity; // Status effect duration reduction
	[HideInInspector] public Stat crit; // Critical attack chance
	[HideInInspector] public Stat efficiency; // Ability cooldown reduction
	[HideInInspector] public Stat range; // Attack range

	// TODO: When rewriting, add class to hold physical components (renderer, animator, bounds, etc.)
	[HideInInspector] public float entityHeight;
	[HideInInspector] public Animator entityAnimator;

	private Vector3 respawnPoint;
	private bool respawnable;

	protected void SetupEntity(float damageBase, float damageScaling, float magicBase, float magicScaling, float vitalityBase, float vitalityScaling, float regenerationBase, float regenerationScaling, float energyBase, float energyScaling, float enduranceBase, float enduranceScaling, float armorBase, float armorScaling, float nullificationBase, float nullificationScaling, float forceBase, float forceScaling, float pierceBase, float pierceScaling, float vampBase, float vampScaling, float fervorBase, float fervorScaling, float speedBase, float speedScaling, float tenacityBase, float tenacityScaling, float critBase, float critScaling, float efficiencyBase, float efficiencyScaling, float rangeBase, float rangeScaling, float entityHeight, Animator entityAnimator, Team team, bool respawnable) {
		this.damage = new Stat(damageBase, damageScaling, Stat.StatId.Damage);
		this.magic = new Stat(magicBase, magicScaling, Stat.StatId.Magic);
		this.vitality = new Stat(vitalityBase, vitalityScaling, Stat.StatId.Vitality);
		this.regeneration = new Stat(regenerationBase, regenerationScaling, Stat.StatId.Regeneration);
		this.energy = new Stat(energyBase, energyScaling, Stat.StatId.Energy);
		this.endurance = new Stat(enduranceBase, enduranceScaling, Stat.StatId.Endurance);
		this.armor = new Stat(armorBase, armorScaling, Stat.StatId.Armor);
		this.nullification = new Stat(nullificationBase, nullificationScaling, Stat.StatId.Nullification);
		this.force = new Stat(forceBase, forceScaling, Stat.StatId.Force);
		this.pierce = new Stat(pierceBase, pierceScaling, Stat.StatId.Pierce);
		this.vamp = new Stat(vampBase, vampScaling, Stat.StatId.Vamp);
		this.fervor = new Stat(fervorBase, fervorScaling, Stat.StatId.Fervor);
		this.speed = new Stat(speedBase, speedScaling, Stat.StatId.Speed);
		this.tenacity = new Stat(tenacityBase, tenacityScaling, Stat.StatId.Tenacity);
		this.crit = new Stat(critBase, critScaling, Stat.StatId.Crit);
		this.efficiency = new Stat(efficiencyBase, efficiencyScaling, Stat.StatId.Efficiency);
		this.range = new Stat(rangeBase, rangeScaling, Stat.StatId.Range);

		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		this.entityHeight = entityHeight;
		this.entityAnimator = entityAnimator;

		this.kills = 0;
		this.deaths = 0;
		this.assists = 0;

		this.respawnable = respawnable;
		this.respawnPoint = this.transform.position;

		this.UpdateStats();

		this.health = this.vitality.CurrentValue;
		this.shield = 0;
		this.physicalShield = 0;
		this.magicalShield = 0;
		this.resource = this.energy.CurrentValue;
		this.level = 0;
		this.experience = 0;
		this.team = team;
		this.currentStatusEffects = new List<StatusEffectType>();
		this.items = new List<Item>();

		this.movementTarget = Tools.PositionOnMapAt(this.transform.position);
		this.basicAttackTarget = null;
		this.basicAttackCooldown = 0;
		this.basicAttackAbility = null;
		this.basicAttackActions = new List<Action<Entity>>();
		this.bufferedBasicAttackActions = new List<Action<Entity>>();

		this.LevelUp();
	}

	protected void MovementCommand(Vector3 targetPosition) {
		this.basicAttackTarget = null;
		this.movementTarget = Tools.PositionOnMapAt(targetPosition);
		this.transform.LookAt(this.movementTarget);
		this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
		if (this.entityAnimator) { this.entityAnimator.runtimeAnimatorController = MatchManager.Instance.runAnimation; }
	}

	protected Entity ClosestEntityInRange(bool includeEnemies, bool includeAllies, bool includeSelf, bool includeChampions, bool includeStructures, bool includeOtherEntities, float maximumRange) {
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, (float) maximumRange, MatchManager.Instance.entityLayerMask);
		Entity closestEntity = null;
		foreach (Collider hitCollider in hitColliders) {
			if (Vector3.Distance(hitCollider.transform.position, this.transform.position) > maximumRange) { continue; } // Necessary for some reason. Redundancy is always good, I guess...

			Entity collidedEntity = hitCollider.GetComponent<Entity>();
			if (!includeSelf && collidedEntity == this) { continue; }

			if (!includeEnemies && collidedEntity.team != this.team || !includeAllies && collidedEntity.team == this.team) { continue; }

			Champion collidedChampion = collidedEntity.GetComponent<Champion>();
			Structure collidedStructure = collidedEntity.GetComponent<Structure>();
			if (!includeChampions && collidedChampion || !includeStructures && collidedStructure || !includeOtherEntities && !collidedStructure && !collidedChampion) { continue; }

			if (closestEntity == null) { closestEntity = collidedEntity; }

			if (Vector3.Distance(this.transform.position, closestEntity.transform.position) < Vector3.Distance(this.transform.position, collidedEntity.transform.position)) { continue; }

			closestEntity = collidedEntity;
		}

		return closestEntity;
	}

	private void Die() {
		// Give rewards for killing.
		int goldReward = 10;
		int experienceReward = 10;
		if (this.GetComponent<Champion>() || this.GetComponent<Structure>()) {
			goldReward = 100;
			experienceReward = 50;
		}
		Ability aoeAbility = new Ability(0, this, "Bounty", "Recieve your rewards for killing a foe.", 0, null, () => { });
		aoeAbility.Action = () => {
			Champion targetChampion = aoeAbility.target.GetComponent<Champion>();
			if (targetChampion) {
				targetChampion.currency += goldReward;
				targetChampion.experience += experienceReward;
			}
		};
		Ability.DoInArea(this.transform.position, 70, false, aoeAbility);

		// Respawn or don't.
		if (this.respawnable) { this.StartCoroutine(this.Respawn()); } else { Destroy(this.gameObject); }
	}

	private IEnumerator Respawn() {
		float respawnTimer = 5 + (float) ((DateTime.Now - MatchManager.Instance.matchStartTime).TotalSeconds / 30);
		DateTime deathTime = DateTime.Now;
		while ((DateTime.Now - deathTime).TotalSeconds < respawnTimer) {
			this.health = 1;
			this.resource = 0;
			this.transform.position = this.respawnPoint;
			this.movementTarget = this.transform.position;
			yield return null;
		}
		this.health = this.vitality.CurrentValue;
		this.resource = this.energy.CurrentValue;
	}

	protected void BasicAttackCommand(Entity target) {
		if (!target) { return; }

		this.basicAttackTarget = target;
		float oldBasicAttackCooldown;
		if (this.basicAttackAbility != null) { oldBasicAttackCooldown = this.basicAttackAbility.CurrentCooldown; } else { oldBasicAttackCooldown = 0; }

		if (this.range.CurrentValue > 5) { // Ranged
			this.basicAttackAbility = new Ability(1 / this.fervor.CurrentValue, this, "Basic Attack", "A basic attack.", 0, null,
				() => {
					AbilityObject abilityObject = Ability.CreateAbilityObject("Prefabs/BasicAttack", false, true, false, true, this, this.transform.position, this.basicAttackTarget.transform.position, this.basicAttackTarget, 50, this.range.CurrentValue, 10);
					abilityObject.collisionAction = () => {
						Ability.DealDamage(true, this, abilityObject.collidedEntity, DamageType.Physical, this.damage.CurrentValue, 0);
						if (Tools.Random() < this.crit.CurrentValue) { Ability.DealDamage(true, this, abilityObject.collidedEntity, DamageType.Physical, this.damage.CurrentValue, 0); } // Critical hit.
						foreach (System.Action<Entity> action in this.basicAttackActions) { action.Invoke(abilityObject.collidedEntity); }
						this.basicAttackActions = this.bufferedBasicAttackActions;
						this.bufferedBasicAttackActions.Clear();
					};
					abilityObject.updateAction = () => { abilityObject.movementSpeed += abilityObject.movementSpeed * Time.deltaTime / 2; }; // Accelerate to guarantee connection with target.
				}) { CurrentCooldown = oldBasicAttackCooldown };
		} else {
			this.basicAttackAbility = new Ability(1 / this.fervor.CurrentValue, this, "Basic Attack", "A basic attack.", 0, null,
			() => {
				Ability.DealDamage(true, this, this.basicAttackTarget, DamageType.Physical, this.damage.CurrentValue, 0);
				if (Tools.Random() < this.crit.CurrentValue) { Ability.DealDamage(true, this, this.basicAttackTarget, DamageType.Physical, this.damage.CurrentValue, 0); } // Critical hit.
				foreach (System.Action<Entity> action in this.basicAttackActions) { action.Invoke(this.basicAttackTarget); }
				this.basicAttackActions.Clear();
			}) { CurrentCooldown = oldBasicAttackCooldown };
		} // Melee
	}

	public void Update() {
		this.UpdateDisplay();

		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		if (this.health <= 0) { this.Die(); } // Death due to low health.

		this.BasicAttackUpdate();
		this.MovementUpdate();
		this.UpdateStats();
		this.RegenerateResources();
	}

	private void MovementUpdate() {
		if (this.currentStatusEffects.Contains(StatusEffectType.immobilize)) { return; }
		if (this.movementTarget == null) { this.movementTarget = this.transform.position; }
		Vector3 step = Tools.PositionOnMapAt(Vector3.MoveTowards(this.transform.position, movementTarget, (float) this.speed.CurrentValue * Time.deltaTime));
		if (Tools.HeightOfMapAt(step.x, step.z) == -1) { this.movementTarget = this.transform.position; } else { this.gameObject.transform.position = step; }

		if (this.entityAnimator && this.transform.position == this.movementTarget) { this.entityAnimator.runtimeAnimatorController = MatchManager.Instance.idleAnimation; }
	}

	private void BasicAttackUpdate() {
		this.basicAttackAbility?.UpdateCooldown();

		if (!this.basicAttackTarget) { return; }

		if (this.basicAttackTarget.team == this.team || !this.basicAttackTarget.isActiveAndEnabled) {
			this.basicAttackTarget = null;
			return;
		}

		if (Vector3.Distance(this.transform.position, this.basicAttackTarget.transform.position) > Math.Min(this.range.CurrentValue + 10, 30)) { // Out of vision range.
			this.basicAttackTarget = null;
			return;
		} else if (Vector3.Distance(this.transform.position, this.basicAttackTarget.transform.position) > this.range.CurrentValue) {
			this.movementTarget = this.basicAttackTarget.transform.position; // TODO: Make sure these changes work.
			return;
		}

		this.basicAttackAbility?.Cast();
	}

	private void UpdateDisplay() {
		if (!this._display) { this._display = EntityDisplay.CreateEntityDisplay(this); }

		this._display.healthText.text = "" + (int) this.health;
		this._display.physicalShieldText.text = "" + (int) this.physicalShield;
		this._display.magicalShieldText.text = "" + (int) this.magicalShield;
		this._display.shieldText.text = "" + (int) this.shield;
		this._display.resourceText.text = "" + (int) this.resource;
	}

	private void UpdateStats() {
		this.damage.Update();
		this.magic.Update();
		this.vitality.Update();
		this.regeneration.Update();
		this.energy.Update();
		this.endurance.Update();
		this.armor.Update();
		this.nullification.Update();
		this.force.Update();
		this.pierce.Update();
		this.vamp.Update();
		this.fervor.Update();
		this.speed.Update();
		this.tenacity.Update();
		this.crit.Update();
		this.efficiency.Update();
		this.range.Update();
	}

	private void RegenerateResources() {
		if (this.health < this.vitality.CurrentValue) { this.health += this.regeneration.CurrentValue * Time.deltaTime; }

		if (this.health > this.vitality.CurrentValue) { this.health = this.vitality.CurrentValue; }

		if (this.resource < this.energy.CurrentValue) { this.resource += this.endurance.CurrentValue * Time.deltaTime; }

		if (this.resource > this.energy.CurrentValue) { this.resource = this.energy.CurrentValue; }

		if (this.experience >= this.level * Entity.ExperiencePerLevelPerLevel) {
			this.experience -= this.level * Entity.ExperiencePerLevelPerLevel;
			this.LevelUp();
		}
	}

	public void LevelUp(int times) {
		for (int i = 0; i < times; i++) { this.LevelUp(); }
	}

	private void LevelUp() {
		this.level++;
		if (this.level == 1) { return; } // Start with base stats.

		this.damage.LevelUp();
		this.magic.LevelUp();
		this.vitality.LevelUp();
		this.regeneration.LevelUp();
		this.energy.LevelUp();
		this.endurance.LevelUp();
		this.armor.LevelUp();
		this.nullification.LevelUp();
		this.force.LevelUp();
		this.pierce.LevelUp();
		this.vamp.LevelUp();
		this.fervor.LevelUp();
		this.speed.LevelUp();
		this.tenacity.LevelUp();
		this.crit.LevelUp();
		this.efficiency.LevelUp();
		this.range.LevelUp();
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		stream.Serialize(ref this.health);
		stream.Serialize(ref this.shield);
		stream.Serialize(ref this.physicalShield);
		stream.Serialize(ref this.magicalShield);
		stream.Serialize(ref this.resource);
		stream.Serialize(ref this.damage.CurrentValue);
		stream.Serialize(ref this.magic.CurrentValue);
		stream.Serialize(ref this.vitality.CurrentValue);
		stream.Serialize(ref this.regeneration.CurrentValue);
		stream.Serialize(ref this.energy.CurrentValue);
		stream.Serialize(ref this.endurance.CurrentValue);
		stream.Serialize(ref this.armor.CurrentValue);
		stream.Serialize(ref this.nullification.CurrentValue);
		stream.Serialize(ref this.force.CurrentValue);
		stream.Serialize(ref this.pierce.CurrentValue);
		stream.Serialize(ref this.vamp.CurrentValue);
		stream.Serialize(ref this.fervor.CurrentValue);
		stream.Serialize(ref this.speed.CurrentValue);
		stream.Serialize(ref this.tenacity.CurrentValue);
		stream.Serialize(ref this.crit.CurrentValue);
		stream.Serialize(ref this.efficiency.CurrentValue);
		stream.Serialize(ref this.range.CurrentValue);
	}
}