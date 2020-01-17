using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using static Ability;

public abstract class Entity : MonoBehaviourPun, IPunObservable {
	public enum Team { Red, Blue, Neutral }

	protected const int ExperiencePerLevelPerLevel = 50;

	[HideInInspector] public List<StatusEffect> currentStatusEffects;
	[HideInInspector] public List<Item> items;
	[HideInInspector] public Item trinket;
	[HideInInspector] public Team team;
	[HideInInspector] public Vector3 movementTarget;
	[HideInInspector] public Entity basicAttackTarget;
	[HideInInspector] public double basicAttackCooldown;
	[HideInInspector] public Ability basicAttackAbility;
	[HideInInspector] public System.Action<Entity>[] basicAttackActions;
	private EntityDisplay _display;

	[HideInInspector] public int kills;
	[HideInInspector] public int deaths;
	[HideInInspector] public int assists;

	[HideInInspector] public double health; // Current health
	[HideInInspector] public double shield; // Current shield
	[HideInInspector] public double physicalShield; // Current physical damage-only shield
	[HideInInspector] public double magicalShield; // Current magical damage-only shield
	[HideInInspector] public double resource; // Current resource
	[HideInInspector] public double experience; // Current progress towards next level
	[HideInInspector] public int level; // Current level

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

	[HideInInspector] public double entityHeight;
	[HideInInspector] public Animator entityAnimator;

	protected void SetupEntity(double damageBase, double damageScaling, double magicBase, double magicScaling, double vitalityBase, double vitalityScaling, double regenerationBase, double regenerationScaling, double energyBase, double energyScaling, double enduranceBase, double enduranceScaling, double armorBase, double armorScaling, double nullificationBase, double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, double entityHeight, Animator entityAnimator, Team team) {
		this.entityHeight = entityHeight;
		this.entityAnimator = entityAnimator;

		this.kills = 0;
		this.deaths = 0;
		this.assists = 0;

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

		this.UpdateStats();

		this.health = this.vitality.CurrentValue;
		this.shield = 0;
		this.physicalShield = 0;
		this.magicalShield = 0;
		this.resource = this.energy.CurrentValue;
		this.level = 0;
		this.experience = 0;
		this.team = team;
		this.currentStatusEffects = new List<StatusEffect>();
		this.items = new List<Item>();

		this.movementTarget = Tools.PositionOnMapAt(this.transform.position);
		this.basicAttackTarget = null;
		this.basicAttackCooldown = 0;
		this.basicAttackAbility = null;

		this.LevelUp();
	}

	protected void MovementCommand(Vector3 targetPosition) {
		this.movementTarget = Tools.PositionOnMapAt(targetPosition);
		this.transform.LookAt(this.movementTarget);
		this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
		if (this.entityAnimator) { this.entityAnimator.runtimeAnimatorController = MatchManager.Instance.runAnimation; }
	}

	protected Entity ClosestEntityInRange(bool includeEnemies, bool includeAllies, bool includeSelf, bool includeChampions, bool includeStructures, bool includeOtherEntities, double maximumRange) {
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

	private void Die() { this.gameObject.SetActive(false); }

	protected void BasicAttackCommand(Entity target) {
		if (!target) { return; }

		this.basicAttackTarget = target;
		double oldBasicAttackCooldown;
		if (this.basicAttackAbility != null) { oldBasicAttackCooldown = this.basicAttackAbility.CurrentCooldown; } else { oldBasicAttackCooldown = 0; }

		if (this.range.CurrentValue > 5) { // Ranged
			this.basicAttackAbility = new Ability(1 / this.fervor.CurrentValue, this, "Basic Attack", "A basic attack.", 0, null, () => {
																																	  AbilityObject abilityObject = Ability.CreateAbilityObject("Prefabs/BasicAttack", false, true, false, true, this, this.transform.position, this.basicAttackTarget.transform.position, this.basicAttackTarget, 50, this.range.CurrentValue, 10);
																																	  abilityObject.collisionAction = () => {
																																										  Ability.DealDamage(true, this, abilityObject.collidedEntity, DamageType.Physical, this.damage.CurrentValue, 0);
																																										  foreach (System.Action<Entity> action in this.basicAttackActions) { action.Invoke(abilityObject.collidedEntity); }
																																									  };
																																	  abilityObject.updateAction = () => { abilityObject.movementSpeed += abilityObject.movementSpeed * Time.deltaTime / 2; }; // Accelerate to guarantee connection with target.
																																  }) {CurrentCooldown = oldBasicAttackCooldown};
		} else { this.basicAttackAbility = new Ability(1 / this.fervor.CurrentValue, this, "Basic Attack", "A basic attack.", 0, null, () => { Ability.DealDamage(true, this, this.basicAttackTarget, DamageType.Physical, this.damage.CurrentValue, 0); }) {CurrentCooldown = oldBasicAttackCooldown}; } // Melee
	}

	public void Update() {
		if (this.health <= 0) { this.Die(); } // Death due to low health.

		this.BasicAttackUpdate();
		this.MovementUpdate();
		this.UpdateStats();
		this.UpdateDisplay();
		this.RegenerateResources();
	}

	private void MovementUpdate() {
		Vector3 step = Tools.PositionOnMapAt(Vector3.MoveTowards(this.transform.position, movementTarget, (float) this.speed.CurrentValue * Time.deltaTime));
		if (Tools.HeightOfMapAt(step.x, step.z) == -1) { this.movementTarget = this.transform.position; } else { this.gameObject.transform.position = step; }

		if (this.entityAnimator && this.transform.position == this.movementTarget) { this.entityAnimator.runtimeAnimatorController = MatchManager.Instance.idleAnimation; }
	}

	private void BasicAttackUpdate() {
		this.basicAttackAbility?.UpdateCooldown();

		if (!this.basicAttackTarget) { return; }

		if (Vector3.Distance(this.transform.position, this.basicAttackTarget.transform.position) > this.range.CurrentValue || this.basicAttackTarget.team == this.team || !this.basicAttackTarget.isActiveAndEnabled) {
			this.basicAttackTarget = null;
			return;
		}

		this.basicAttackAbility?.Cast();
	}

	private void UpdateDisplay() {
		if (!this._display) { this._display = EntityDisplay.CreateEntityDisplay(this); }

		this._display.healthText.text = (int) this.health + " / " + (int) this.vitality.CurrentValue;
		this._display.physicalShieldText.text = "" + (int) this.physicalShield;
		this._display.magicalShieldText.text = "" + (int) this.magicalShield;
		this._display.shieldText.text = "" + (int) this.shield;
		this._display.resourceText.text = (int) this.resource + " / " + (int) this.energy.CurrentValue;
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
		// TODO
	}
}