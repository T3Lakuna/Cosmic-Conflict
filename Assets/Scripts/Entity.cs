using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Ability;

public class Entity : MonoBehaviourPun, IPunObservable {
	[HideInInspector] public List<Ability> recentAbilitiesTaken; // TODO

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

	public void TakeDamage(Entity damageSource, DamageType type, int flatDamage, double percentageDamage) {
		double effectiveHealth = this.health;
		switch (type) {
			case DamageType.Magical:
				effectiveHealth = this.health + this.health * (this.currentNullification / 100.0);
				break;
			case DamageType.Physical:
				effectiveHealth = this.health + this.health * (this.currentArmor / 100.0);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		double damageTaken = (effectiveHealth * percentageDamage) + flatDamage;
		double effectiveDamageTaken = damageTaken * (this.currentHealth / effectiveHealth);
		this.health -= effectiveDamageTaken;
	}

	public void Heal(Entity healSource, HealthType type, int flatHealing, double percentageHealing) {
		// TODO
	}

	public void ApplyCrowdControl(Entity crowdControlSource, CrowdControlType type, int duration) {
		// TODO
	}

	private void Update() {
		this.UpdateStats();
		this.RegenerateResources();
	}

	private void UpdateStats() {
		this.currentHealth = (this.healthBase + this.healthBonus) * this.healthPercentageBonus;
		this.currentRegeneration = (this.regenerationBase + this.regenerationBonus) * this.regenerationPercentageBonus;
		this.currentMana = (this.manaBase + this.manaBonus) * this.manaPercentageBonus;
		this.currentEndurance = (this.enduranceBase + this.enduranceBonus) * this.endurancePercentageBonus;
		this.currentArmor = (this.armorBase + this.armorBonus) * this.armorPercentageBonus;
		this.currentNullification = (this.nullificationBase + this.nullificationBonus) * this.nullificationPercentageBonus;
		this.currentForce = ((forceBase + this.forceBonus) * this.forcePercentageBonus);
		this.currentPierce = (this.pierceBase + this.pierceBonus) * this.piercePercentageBonus;
		this.currentVamp = (this.vampBase + this.vampBonus) * this.vampPercentageBonus;
		this.currentFervor = (this.fervorBase + this.fervorBonus) * this.fervorPercentageBonus;
		this.currentSpeed = (this.speedBase + this.speedBonus) * this.speedPercentageBonus;
		this.currentTenacity = (this.tenacityBase + this.tenacityBonus) * this.tenacityPercentageBonus;
		this.currentCrit = (this.critBase + this.critBonus) * this.critPercentageBonus;
		this.currentEfficiency = (this.efficiencyBase + this.efficiencyBonus) * this.efficiencyPercentageBonus;
		this.currentRange = (this.rangeBase + this.rangeBonus) * this.rangePercentageBonus;
	}

	private void RegenerateResources() {
		if (this.health < this.currentHealth) {
			this.health += this.currentRegeneration;
		}

		if (this.health > this.currentHealth) {
			this.health = this.currentHealth;
		}

		if (this.mana < this.currentMana) {
			this.mana += this.currentEndurance;
		}

		if (this.mana > this.currentMana) {
			this.mana = this.currentMana;
		}
	}

	private void LevelUp() {
		this.level++;
		this.healthBase += this.healthScaling;
		this.regenerationBase += this.regenerationScaling;
		this.manaBase += this.manaScaling;
		this.enduranceBase += this.enduranceScaling;
		this.armorBase += this.armorScaling;
		this.nullificationBase += this.nullificationScaling;
		this.forceBase += this.forceScaling;
		this.pierceBase += this.pierceScaling;
		this.vampBase += this.vampScaling;
		this.fervorBase += this.fervorScaling;
		this.speedBase += this.speedScaling;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		// TODO
	}
}