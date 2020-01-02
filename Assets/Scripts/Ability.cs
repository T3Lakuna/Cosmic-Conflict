using System;
using System.Collections;
using UnityEngine;

public class Ability {
	public enum DamageType { Magical, Physical, True }

	public enum HealthType { Health, Shield, PhysicalShield, MagicalShield }

	public enum StatusEffectType { Stun, Slow, Root, Fear, Charm }

	public double CurrentCooldown;
	public readonly double BaseCooldown;
	public readonly Entity Source;
	public Entity Target;
	public string Name;
	public string Description;
	public readonly Action Action;
	public DateTime CastTime;
	public Sprite Icon;
	public double cost;

	public Ability(double maximumCooldown, Entity source, Entity target, string name, string description, double cost, Sprite icon, Action action) {
		this.BaseCooldown = maximumCooldown;
		this.CurrentCooldown = 0;
		this.Source = source;
		this.Name = name;
		this.Description = description;
		this.Action = action;
		this.Target = target;
		this.cost = cost;
		this.Icon = icon;
		this.CastTime = DateTime.Now;
	}

	public void UpdateCooldown() {
		double actualCooldown = this.BaseCooldown * (1 - this.Source.efficiency.CurrentValue / 100);
		if (this.CurrentCooldown > actualCooldown) { this.CurrentCooldown = actualCooldown; }

		this.CurrentCooldown = Math.Max(this.CurrentCooldown - Time.deltaTime, 0);
	}

	public void Cast() {
		if (this.CurrentCooldown > 0) {
			return;
		}

		if (this.Source.resource >= this.cost) {
			this.Source.resource -= this.cost;
		} else {
			return;
		}

		this.CurrentCooldown = this.BaseCooldown;
		this.Action();
	}

	public static AbilityObject CreateAbilityObject(string prefabResourcesPath, bool destroyOnHit, Entity source, Vector3 initialPosition, Vector3 target, double movementSpeed, double range, double lifespan, Action collisionAction, Action updateAction) {
		GameObject abilityObject = Tools.Instantiate(prefabResourcesPath, initialPosition);
		AbilityObject ability = abilityObject.AddComponent<AbilityObject>();
		ability.collisionAction = collisionAction;
		ability.target = target;
		ability.movementSpeed = movementSpeed;
		ability.updateAction = updateAction;
		ability.destroyOnHit = destroyOnHit;
		ability.maximumDistance = range;
		ability.source = source;
		ability.lifespan = lifespan;

		return ability;
	}

	public static void DoInArea(Vector3 center, double radius, Ability ability) {
		foreach (Collider target in Physics.OverlapSphere(center, (float) radius)) {
			Entity entity = target.GetComponent<Entity>();
			if (!entity) { continue; }

			ability.Target = entity;
			ability.Cast();
		}
	}

	public static void DealDamage(Entity target, DamageType type, double flatAmount, double percentageAmount) {
		string debugString = "DealDamage (target: " + target.name + ", type: " + type.ToString() + ", flat: " + flatAmount + ", percent: " + percentageAmount + "):\n";
		double effectiveHealth;
		HealthType[] damageOrder;
		switch (type) {
			case DamageType.Magical:
				effectiveHealth = target.health + target.health * (target.nullification.CurrentValue / 100.0);
				damageOrder = new[] { HealthType.MagicalShield, HealthType.Shield, HealthType.Health };
				break;
			case DamageType.Physical:
				damageOrder = new[] { HealthType.PhysicalShield, HealthType.Shield, HealthType.Health };
				effectiveHealth = target.health + target.health * (target.armor.CurrentValue / 100.0);
				break;
			case DamageType.True:
				damageOrder = new[] { HealthType.Shield, HealthType.Health };
				effectiveHealth = target.health;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}
		debugString += "Target effective health: " + effectiveHealth + ".\n";
		debugString += "Damage order: " + damageOrder + ".\n";

		double damageTaken = (effectiveHealth * (percentageAmount / 100)) + flatAmount;
		debugString += "Damage taken: " + damageTaken + ".\n";
		double effectiveDamageTaken = damageTaken * (target.vitality.CurrentValue / effectiveHealth);
		debugString += "Effective damage taken: " + effectiveDamageTaken + ".\n";
		double remainingDamage = effectiveDamageTaken;
		debugString += "---\n";

		foreach (HealthType healthType in damageOrder) {
			double originalValue;
			switch (healthType) {
				case HealthType.Health:
					debugString += "To health: " + remainingDamage + " damage on " + target.health + " hit points (";
					originalValue = target.health;
					target.health -= Math.Min(remainingDamage, target.health);
					remainingDamage -= originalValue - target.health;
					debugString += target.health + " health remaining, " + remainingDamage + " still to be dealt).\n";
					break;
				case HealthType.Shield:
					debugString += "To shield: " + remainingDamage + " damage on " + target.shield + " hit points (";
					originalValue = target.shield;
					target.shield -= Math.Min(remainingDamage, target.shield);
					remainingDamage -= originalValue - target.shield;
					debugString += target.shield + " shield remaining, " + remainingDamage + " still to be dealt).\n";
					break;
				case HealthType.PhysicalShield:
					debugString += "To physical shield: " + remainingDamage + " damage on " + target.physicalShield + " hit points (";
					originalValue = target.physicalShield;
					target.physicalShield -= Math.Min(remainingDamage, target.physicalShield);
					remainingDamage -= originalValue - target.physicalShield;
					debugString += target.physicalShield + " physical shield remaining, " + remainingDamage + " still to be dealt).\n";
					break;
				case HealthType.MagicalShield:
					debugString += "To magical shield: " + remainingDamage + " damage on " + target.magicalShield + " hit points (";
					originalValue = target.magicalShield;
					target.magicalShield -= Math.Min(remainingDamage, target.magicalShield);
					remainingDamage -= originalValue - target.magicalShield;
					debugString += target.magicalShield + " magical shield remaining, " + remainingDamage + " still to be dealt).\n";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		Debug.Log(debugString); // TODO: Remove debugString.
	}

	public static void Heal(Entity target, HealthType type, double duration, double flatAmount, double percentageAmount) {
		double finalAmount = flatAmount + (target.health + flatAmount) * percentageAmount;

		switch (type) {
			case HealthType.Health:
				target.health += finalAmount;
				break;
			case HealthType.MagicalShield:
				target.magicalShield += finalAmount;
				break;
			case HealthType.PhysicalShield:
				target.physicalShield += finalAmount;
				break;
			case HealthType.Shield:
				target.shield += finalAmount;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}

		if (duration > 0) {
			target.StartCoroutine(Ability.RemoveHeal(target, type, duration, finalAmount));
		}
	}

	private static IEnumerator RemoveHeal(Entity target, HealthType type, double delay, double amount) {
		yield return new WaitForSeconds((float) delay);
		switch (type) {
			case HealthType.Health:
				target.health -= amount;
				break;
			case HealthType.MagicalShield:
				target.magicalShield -= amount;
				break;
			case HealthType.PhysicalShield:
				target.physicalShield -= amount;
				break;
			case HealthType.Shield:
				target.shield -= amount;
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}
	}

	public static void ApplyStatusEffect(Entity target, StatusEffectType type, double duration, double value) {
		target.currentStatusEffects.Add(type);
		target.StartCoroutine(Ability.RemoveStatusEffect(target, type, duration));
	}

	private static IEnumerator RemoveStatusEffect(Entity target, StatusEffectType type, double delay) {
		yield return new WaitForSeconds((float) delay);
		target.currentStatusEffects.Remove(type);
	}
}