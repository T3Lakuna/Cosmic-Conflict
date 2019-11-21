using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using static Ability;

public abstract class Entity : MonoBehaviourPun, IPunObservable {
	[HideInInspector] public List<Ability> recentAbilitiesTaken; // TODO
	[HideInInspector] public Team team;

	[HideInInspector] public double health; // Current health
	[HideInInspector] public double mana; // Current mana
	[HideInInspector] public double experience; // Current progress towards next level
	[HideInInspector] public int level; // Current level

	[HideInInspector] public double currentHealth; // Current maximum health
	[HideInInspector] public double currentRegeneration; // Current health regeneration
	[HideInInspector] public double currentMana; // Current maximum mana
	[HideInInspector] public double currentEndurance; // Current mana regeneration
	[HideInInspector] public double currentArmor; // Current physical resistance
	[HideInInspector] public double currentNullification; // Current magic resistance
	[HideInInspector] public double currentForce; // Current physical penetration
	[HideInInspector] public double currentPierce; // Current magic penetration
	[HideInInspector] public double currentVamp; // Current life steal
	[HideInInspector] public double currentFervor; // Current attack speed
	[HideInInspector] public double currentSpeed; // Current movement speed
	[HideInInspector] public double currentTenacity; // Current disable resistance
	[HideInInspector] public double currentCrit; // Current critical strike chance
	[HideInInspector] public double currentEfficiency; // Current cooldown reduction
	[HideInInspector] public double currentRange; // Current attack range

	[HideInInspector] public double healthBase; // Base maximum health
	[HideInInspector] public double healthScaling; // Maximum health per level
	[HideInInspector] public double healthBonus; // Bonus maximum health (flat) from effects
	[HideInInspector] public double healthPercentageBonus; // Bonus maximum health (percentage) from effects

	[HideInInspector] public double regenerationBase; // Base health regeneration
	[HideInInspector] public double regenerationScaling; // Health regeneration per level
	[HideInInspector] public double regenerationBonus; // Health regeneration (flat) from effects
	[HideInInspector] public double regenerationPercentageBonus; // Health regeneration (percentage) from effects

	[HideInInspector] public double manaBase; // Base maximum mana
	[HideInInspector] public double manaScaling; // Maximum mana per level
	[HideInInspector] public double manaBonus; // Bonus maximum mana (flat) from effects
	[HideInInspector] public double manaPercentageBonus; // Bonus maximum mana (percentage) from effects

	[HideInInspector] public double enduranceBase; // Base mana regeneration
	[HideInInspector] public double enduranceScaling; // Mana regeneration per level
	[HideInInspector] public double enduranceBonus; // Mana regeneration (flat) from effects
	[HideInInspector] public double endurancePercentageBonus; // Mana regeneration (percentage) from effects

	[HideInInspector] public double armorBase; // Base physical resistance
	[HideInInspector] public double armorScaling; // Physical resistance per level
	[HideInInspector] public double armorBonus; // Physical resistance (flat) from effects
	[HideInInspector] public double armorPercentageBonus; // Physical resistance (percentage) from effects

	[HideInInspector] public double nullificationBase; // Base magic resistance
	[HideInInspector] public double nullificationScaling; // Magic resistance per level
	[HideInInspector] public double nullificationBonus; // Magic resistance (flat) from effects
	[HideInInspector] public double nullificationPercentageBonus; // Magic resistance (percentage) from effects

	[HideInInspector] public double forceBase; // Base physical penetration
	[HideInInspector] public double forceScaling; // Physical penetration per level
	[HideInInspector] public double forceBonus; // Physical penetration (flat) from effects
	[HideInInspector] public double forcePercentageBonus; // Physical penetration (percentage) from effects

	[HideInInspector] public double pierceBase; // Base magic penetration
	[HideInInspector] public double pierceScaling; // Magic penetration per level
	[HideInInspector] public double pierceBonus; // Magic penetration (flat) from effects
	[HideInInspector] public double piercePercentageBonus; // Magic penetration (percentage) from effects

	[HideInInspector] public double vampBase; // Base life steal
	[HideInInspector] public double vampScaling; // Life steal per level
	[HideInInspector] public double vampBonus; // Life steal (flat) from effects
	[HideInInspector] public double vampPercentageBonus; // Life steal (percentage) from effects

	[HideInInspector] public double fervorBase; // Base attack speed
	[HideInInspector] public double fervorScaling; // Attack speed per level
	[HideInInspector] public double fervorBonus; // Attack speed (flat) from effects
	[HideInInspector] public double fervorPercentageBonus; // Attack speed (percentage) from effects

	[HideInInspector] public double speedBase; // Base movement speed
	[HideInInspector] public double speedScaling; // Movement speed per level
	[HideInInspector] public double speedBonus; // Movement speed (flat) from effects
	[HideInInspector] public double speedPercentageBonus; // Movement speed (percentage) from effects

	[HideInInspector] public double tenacityBase; // Base disable resistance
	[HideInInspector] public double tenacityScaling; // Disable resistance per level
	[HideInInspector] public double tenacityBonus; // Disable resistance (flat) from effects
	[HideInInspector] public double tenacityPercentageBonus; // Disable resistance (percentage) from effects

	[HideInInspector] public double critBase; // Base critical strike chance
	[HideInInspector] public double critScaling; // Critical strike chance per level
	[HideInInspector] public double critBonus; // Critical strike chance (flat) from effects
	[HideInInspector] public double critPercentageBonus; // Critical strike chance (percentage) from effects

	[HideInInspector] public double efficiencyBase; // Base cooldown reduction
	[HideInInspector] public double efficiencyScaling; // Cooldown reduction per level
	[HideInInspector] public double efficiencyBonus; // Cooldown reduction (flat) from effects
	[HideInInspector] public double efficiencyPercentageBonus; // Cooldown reduction (percentage) from effects

	[HideInInspector] public double rangeBase; // Base attack range
	[HideInInspector] public double rangeScaling; // Attack range per level
	[HideInInspector] public double rangeBonus; // Attack range (flat) from effects
	[HideInInspector] public double rangePercentageBonus; // Attack range (percentage) from effects

	public void SetupEntity(double healthBase, double healthScaling, double regenerationBase,
		double regenerationScaling,
		double manaBase, double manaScaling, double enduranceBase, double enduranceScaling, double armorBase,
		double armorScaling, double nullificationBase, double nullificationScaling, double forceBase,
		double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling,
		double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase,
		double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling,
		double rangeBase, double rangeScaling, Team team) {
		this.healthBase = healthBase;
		this.healthScaling = healthScaling;
		healthBonus = 0;
		healthPercentageBonus = 0;

		this.regenerationBase = regenerationBase;
		this.regenerationScaling = regenerationScaling;
		regenerationBonus = 0;
		regenerationPercentageBonus = 0;

		this.manaBase = manaBase;
		this.manaScaling = manaScaling;
		manaBonus = 0;
		manaPercentageBonus = 0;

		this.enduranceBase = enduranceBase;
		this.enduranceScaling = enduranceScaling;
		enduranceBonus = 0;
		endurancePercentageBonus = 0;

		this.armorBase = armorBase;
		this.armorScaling = armorScaling;
		armorBonus = 0;
		armorPercentageBonus = 0;

		this.nullificationBase = nullificationBase;
		this.nullificationScaling = nullificationScaling;
		nullificationBonus = 0;
		nullificationPercentageBonus = 0;

		this.forceBase = forceBase;
		this.forceScaling = forceScaling;
		forceBonus = 0;
		forcePercentageBonus = 0;

		this.pierceBase = pierceBase;
		this.pierceScaling = pierceScaling;
		pierceBonus = 0;
		piercePercentageBonus = 0;

		this.vampBase = vampBase;
		this.vampScaling = vampScaling;
		vampBonus = 0;
		vampPercentageBonus = 0;

		this.fervorBase = fervorBase;
		this.fervorScaling = fervorScaling;
		fervorBonus = 0;
		fervorPercentageBonus = 0;

		this.speedBase = speedBase;
		this.speedScaling = speedScaling;
		speedBonus = 0;
		speedPercentageBonus = 0;

		this.tenacityBase = tenacityBase;
		this.tenacityScaling = tenacityScaling;
		tenacityBonus = 0;
		tenacityPercentageBonus = 0;

		this.critBase = critBase;
		this.critScaling = critScaling;
		critBonus = 0;
		critPercentageBonus = 0;

		this.efficiencyBase = efficiencyBase;
		this.efficiencyScaling = efficiencyScaling;
		efficiencyBonus = 0;
		efficiencyPercentageBonus = 0;

		this.rangeBase = rangeBase;
		this.rangeScaling = rangeScaling;
		rangeBonus = 0;
		rangePercentageBonus = 0;

		UpdateStats();

		health = currentHealth;
		mana = currentMana;
		level = 0;
		experience = 0;
		this.team = team;
		recentAbilitiesTaken = new List<Ability>();

		LevelUp();
	}

	public void TakeDamage(Entity damageSource, DamageType type, int flatDamage, double percentageDamage) {
		double effectiveHealth = health;
		switch (type) {
			case DamageType.Magical:
				effectiveHealth = health + health * (currentNullification / 100.0);
				break;
			case DamageType.Physical:
				effectiveHealth = health + health * (currentArmor / 100.0);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		double damageTaken = (effectiveHealth * percentageDamage) + flatDamage;
		double effectiveDamageTaken = damageTaken * (currentHealth / effectiveHealth);
		health -= effectiveDamageTaken;
	}

	public void Heal(Entity healSource, HealthType type, int flatHealing, double percentageHealing) {
		// TODO
	}

	public void ApplyCrowdControl(Entity crowdControlSource, CrowdControlType type, int duration) {
		// TODO
	}

	public void EntityUpdate() {
		UpdateStats();
		RegenerateResources();
	}

	private void UpdateStats() {
		currentHealth = healthBase + healthBonus + (healthBase + healthBonus) * healthPercentageBonus;
		currentRegeneration = regenerationBase + regenerationBonus +
		                      (regenerationBase + regenerationBonus) * regenerationPercentageBonus;
		currentMana = manaBase + manaBonus + (manaBase + manaBonus) * manaPercentageBonus;
		currentEndurance = enduranceBase + enduranceBonus + (enduranceBase + enduranceBonus) * endurancePercentageBonus;
		currentArmor = armorBase + armorBonus + (armorBase + armorBonus) * armorPercentageBonus;
		currentNullification = nullificationBase + nullificationBonus +
		                       (nullificationBase + nullificationBonus) * nullificationPercentageBonus;
		currentForce = forceBase + forceBonus + (forceBase + forceBonus) * forcePercentageBonus;
		currentPierce = pierceBase + pierceBonus + (pierceBase + pierceBonus) * piercePercentageBonus;
		currentVamp = vampBase + vampBonus + (vampBase + vampBonus) * vampPercentageBonus;
		currentFervor = fervorBase + fervorBonus + (fervorBase + fervorBonus) * fervorPercentageBonus;
		currentSpeed = speedBase + speedBonus + (speedBase + speedBonus) * speedPercentageBonus;
		currentTenacity = tenacityBase + tenacityBonus + (tenacityBase + tenacityBonus) * tenacityPercentageBonus;
		currentCrit = critBase + critBonus + (critBase + critBonus) * critPercentageBonus;
		currentEfficiency = efficiencyBase + efficiencyBonus +
		                    (efficiencyBase + efficiencyBonus) * efficiencyPercentageBonus;
		currentRange = rangeBase + rangeBonus + (rangeBase + rangeBonus) * rangePercentageBonus;
	}

	private void RegenerateResources() {
		if (health < currentHealth) {
			health += currentRegeneration;
		}

		if (health > currentHealth) {
			health = currentHealth;
		}

		if (mana < currentMana) {
			mana += currentEndurance;
		}

		if (mana > currentMana) {
			mana = currentMana;
		}
	}

	private void LevelUp() {
		level++;
		healthBase += healthScaling;
		regenerationBase += regenerationScaling;
		manaBase += manaScaling;
		enduranceBase += enduranceScaling;
		armorBase += armorScaling;
		nullificationBase += nullificationScaling;
		forceBase += forceScaling;
		pierceBase += pierceScaling;
		vampBase += vampScaling;
		fervorBase += fervorScaling;
		speedBase += speedScaling;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		// TODO
	}
}