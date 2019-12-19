using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Ability {
	public enum DamageType { Magical, Physical, True }

	public enum HealthType { Health, Shield, PhysicalShield, MagicalShield }

	public enum StatusEffectType { Stun, Slow, Root, Fear, Charm }

	public double CurrentCooldown;
	public readonly double BaseCooldown;
	public int Level;
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
		this.Level = 0;
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
		double actualCooldown = this.BaseCooldown * (1 - this.Source.efficiency.CurrentValue);
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

		double damageTaken = (effectiveHealth * percentageAmount) + flatAmount;
		double effectiveDamageTaken = damageTaken * (target.vitality.CurrentValue / effectiveHealth);
		double remainingDamage = effectiveDamageTaken;

		foreach (HealthType healthType in damageOrder) {
			double originalValue;
			switch (healthType) {
				case HealthType.Health:
					originalValue = target.health;
					target.health -= Math.Min(remainingDamage, target.health);
					remainingDamage -= originalValue - target.health;
					break;
				case HealthType.Shield:
					originalValue = target.shield;
					target.shield -= Math.Min(remainingDamage, target.shield);
					remainingDamage -= originalValue - target.shield;
					break;
				case HealthType.PhysicalShield:
					originalValue = target.physicalShield;
					target.physicalShield -= Math.Min(remainingDamage, target.physicalShield);
					remainingDamage -= originalValue - target.physicalShield;
					break;
				case HealthType.MagicalShield:
					originalValue = target.magicalShield;
					target.magicalShield -= Math.Min(remainingDamage, target.magicalShield);
					remainingDamage -= originalValue - target.magicalShield;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
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
			target.StartCoroutine(Tools.DoAfterTime(duration, () => {
				switch (type) {
					case HealthType.Health:
						target.health -= finalAmount;
						break;
					case HealthType.MagicalShield:
						target.magicalShield -= finalAmount;
						break;
					case HealthType.PhysicalShield:
						target.physicalShield -= finalAmount;
						break;
					case HealthType.Shield:
						target.shield -= finalAmount;
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(type), type, null);
				}
			}));
		}
	}

	public static void ApplyStatusEffect(Entity target, StatusEffectType type, double duration, double value) {
		target.currentStatusEffects.Add(type);
		target.StartCoroutine(Tools.DoAfterTime(duration, () => { target.currentStatusEffects.Remove(type); }));
	}
}