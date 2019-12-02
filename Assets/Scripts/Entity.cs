using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using static Ability;

public abstract class Entity : MonoBehaviourPun, IPunObservable {
	public enum Team {
		Red,
		Blue,
		Neutral
	}

	private const int ExperiencePerLevelPerLevel = 50;

	[HideInInspector] public List<Ability> recentAbilitiesTaken;
	[HideInInspector] public List<StatusEffectType> currentStatusEffects;
	[HideInInspector] public Team team;

	[HideInInspector] public double health; // Current health
	[HideInInspector] public double shield; // Current shield
	[HideInInspector] public double physicalShield; // Current physical damage-only shield
	[HideInInspector] public double magicalShield; // Current magical damage-only shield
	[HideInInspector] public double resource; // Current resource
	[HideInInspector] public double experience; // Current progress towards next level
	[HideInInspector] public int level; // Current level

	[HideInInspector] public Stat vitality;
	[HideInInspector] public Stat regeneration;
	[HideInInspector] public Stat energy;
	[HideInInspector] public Stat endurance;
	[HideInInspector] public Stat armor;
	[HideInInspector] public Stat nullification;
	[HideInInspector] public Stat force;
	[HideInInspector] public Stat pierce;
	[HideInInspector] public Stat vamp;
	[HideInInspector] public Stat fervor;
	[HideInInspector] public Stat speed;
	[HideInInspector] public Stat tenacity;
	[HideInInspector] public Stat crit;
	[HideInInspector] public Stat efficiency;
	[HideInInspector] public Stat range;

	protected void SetupEntity(double vitalityBase, double vitalityScaling, double regenerationBase,
		double regenerationScaling, double energyBase, double energyScaling, double enduranceBase,
		double enduranceScaling, double armorBase, double armorScaling, double nullificationBase,
		double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling,
		double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase,
		double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling,
		double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, Team team) {
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

		this.health = this.vitality.currentValue;
		this.shield = 0;
		this.physicalShield = 0;
		this.magicalShield = 0;
		this.resource = this.energy.currentValue;
		this.level = 0;
		this.experience = 0;
		this.team = team;
		this.recentAbilitiesTaken = new List<Ability>();

		this.LevelUp();
	}

	public void ApplyAbility(Ability ability) {
		ability.Action();
		this.recentAbilitiesTaken.Add(ability);
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
		if (this.health < this.vitality.currentValue) {
			this.health += this.regeneration.currentValue;
		}

		if (this.health > this.vitality.currentValue) {
			this.health = this.vitality.currentValue;
		}

		if (this.resource < this.energy.currentValue) {
			this.resource += this.endurance.currentValue;
		}

		if (this.resource > this.energy.currentValue) {
			this.resource = this.energy.currentValue;
		}

		if (this.experience > this.level * ExperiencePerLevelPerLevel) {
			this.experience -= this.level * ExperiencePerLevelPerLevel;
			this.LevelUp();
		}
	}

	private void LevelUp() {
		level++;
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