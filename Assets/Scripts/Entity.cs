using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using static Ability;

public abstract class Entity : MonoBehaviourPun, IPunObservable {
	public enum Team { Red, Blue, Neutral }

	private const int ExperiencePerLevelPerLevel = 50;

	[HideInInspector] public List<StatusEffectType> currentStatusEffects;
	[HideInInspector] public List<Item> items;
	[HideInInspector] public Team team;
	[HideInInspector] public Vector3 movementTarget;

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

	protected void SetupEntity(double damageBase, double damageScaling, double magicBase, double magicScaling, double vitalityBase, double vitalityScaling, double regenerationBase, double regenerationScaling, double energyBase, double energyScaling, double enduranceBase, double enduranceScaling, double armorBase, double armorScaling, double nullificationBase, double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, Team team) {
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
		this.currentStatusEffects = new List<StatusEffectType>();
		this.items = new List<Item>();

		this.LevelUp();
	}

	public void Die(Ability cause) {
		Debug.Log(this.name + " died."); // TODO
	}

	public void MovementCommand(Vector3 targetPosition) {
		this.MovementCommand(targetPosition.x, targetPosition.y);
	}

	public void MovementCommand(Vector2 targetPosition) {
		this.MovementCommand(targetPosition.x, targetPosition.y);
	}

	public void MovementCommand(double x, double z) {
		this.movementTarget = new Vector3((float) x, (float) Tools.HeightOfMapAt(x, z), (float) z);
	}

	public void MovementUpdate() {
		Vector3 step = Vector3.MoveTowards(this.transform.position, movementTarget, (float) this.speed.CurrentValue * Time.deltaTime);
		this.gameObject.transform.position = new Vector3(step.x, (float) Tools.HeightOfMapAt(step.x, step.z), step.z);
	}

	public void BasicAttack(Entity target) {
		if (Vector3.Distance(this.transform.position, target.transform.position) > this.range.CurrentValue) { return; }
		AbilityObject ability = new GameObject().AddComponent<AbilityObject>();
		ability = Ability.CreateAbilityObject("Prefabs/BasicAttack", true, target.transform.position, target.transform.position, 1, () => { Ability.DealDamage(target, Ability.DamageType.Physical, this.damage.CurrentValue, 0); }, () => { ability.target = target.transform.position; });
	}

	protected void UpdateStats() {
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

	protected void RegenerateResources() {
		if (this.health < this.vitality.CurrentValue) { this.health += this.regeneration.CurrentValue; }

		if (this.health > this.vitality.CurrentValue) { this.health = this.vitality.CurrentValue; }

		if (this.resource < this.energy.CurrentValue) { this.resource += this.endurance.CurrentValue; }

		if (this.resource > this.energy.CurrentValue) { this.resource = this.energy.CurrentValue; }

		if (this.experience > this.level * Entity.ExperiencePerLevelPerLevel) {
			this.experience -= this.level * Entity.ExperiencePerLevelPerLevel;
			this.LevelUp();
		}
	}

	private void LevelUp() {
		this.level++;
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