using System;
using System.Collections;
using UnityEngine;

public class Ability {
	// TODO: When rewriting, move StatusEffect stuff into Ability for consistency.

	public enum DamageType { Magical, Physical, True }

	public enum HealthType { Health, Shield, PhysicalShield, MagicalShield }

	public float CurrentCooldown;
	public readonly float BaseCooldown;
	public readonly Entity Source;
	public string Name;
	public string Description;
	public Action Action;
	public DateTime CastTime;
	public Sprite Icon;
	public float cost;
	public Entity target; // Used for area-of-effect abilities.

	public Ability(float maximumCooldown, Entity source, string name, string description, float cost, Sprite icon, Action action) {
		this.BaseCooldown = maximumCooldown;
		this.CurrentCooldown = 0;
		this.Source = source;
		this.Name = name;
		this.Description = description;
		this.Action = action;
		this.cost = cost;
		this.Icon = icon;
		this.CastTime = DateTime.Now;
	}

	public void UpdateCooldown() {
		float actualCooldown = this.BaseCooldown * (1 - this.Source.efficiency.CurrentValue / 100);
		if (this.CurrentCooldown > actualCooldown) { this.CurrentCooldown = actualCooldown; }

		this.CurrentCooldown = Math.Max(this.CurrentCooldown - Time.deltaTime, 0);
	}

	public void Cast() {
		if (this.Source.currentStatusEffects.Contains(StatusEffect.StatusEffectType.silence)) { return; }

		if (this.CurrentCooldown > 0) { return; }

		if (this.Source.resource >= this.cost) { this.Source.resource -= this.cost; } else { return; }

		this.CurrentCooldown = this.BaseCooldown;
		this.Action();
	}

	public static AbilityObject CreateAbilityObject(string prefabResourcesPath, bool destroyAtMaxRange, bool destroyOnHit, bool canHitAllies, bool canOnlyHitTarget, Entity source, Vector3 initialPosition, Vector3 target, Entity targetEntity, float movementSpeed, float range, float lifespan) {
		GameObject abilityObject = Tools.Instantiate(prefabResourcesPath, initialPosition);
		AbilityObject ability = abilityObject.AddComponent<AbilityObject>();
		ability.target = target;
		ability.movementSpeed = movementSpeed;
		ability.destroyAtMaxRange = destroyAtMaxRange;
		ability.destroyOnHit = destroyOnHit;
		ability.maximumDistance = range;
		ability.source = source;
		ability.lifespan = lifespan;
		ability.canHitAllies = canHitAllies;
		ability.canOnlyHitTarget = canOnlyHitTarget;
		ability.targetEntity = targetEntity;

		return ability;
	}

	// TODO: When rewriting, make this not disgusting.
	public static void DoInArea(Vector3 center, float radius, bool canHitAllies, Ability ability) {
		foreach (Collider target in Physics.OverlapSphere(center, (float) radius, MatchManager.Instance.entityLayerMask)) {
			Entity entity = target.GetComponent<Entity>();
			if (!entity) { continue; }

			if (!canHitAllies && entity.team == ability.Source.team) { continue; }

			ability.target = entity;
			ability.Cast();
		}
	}

	// TODO: When rewriting, combine stats somehow so that they can be searched with the stat id (to avoid something like this). Either a seperate "holder" class or just a Dictionary should work.
	public static IEnumerator StatChange(Entity target, float duration, Stat.StatId stat, float amount, float percentageAmount) {
		switch (stat) {
			case Stat.StatId.Damage:
				target.damage.BonusValue += amount;
				target.damage.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.damage.BonusValue -= amount;
					target.damage.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Magic:
				target.magic.BonusValue += amount;
				target.magic.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.magic.BonusValue -= amount;
					target.magic.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Vitality:
				target.vitality.BonusValue += amount;
				target.vitality.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.vitality.BonusValue -= amount;
					target.vitality.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Regeneration:
				target.regeneration.BonusValue += amount;
				target.regeneration.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.regeneration.BonusValue -= amount;
					target.regeneration.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Energy:
				target.energy.BonusValue += amount;
				target.energy.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.energy.BonusValue -= amount;
					target.energy.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Endurance:
				target.endurance.BonusValue += amount;
				target.endurance.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.endurance.BonusValue -= amount;
					target.endurance.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Armor:
				target.armor.BonusValue += amount;
				target.armor.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.armor.BonusValue -= amount;
					target.armor.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Nullification:
				target.nullification.BonusValue += amount;
				target.nullification.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.nullification.BonusValue -= amount;
					target.nullification.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Force:
				target.force.BonusValue += amount;
				target.force.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.force.BonusValue -= amount;
					target.force.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Pierce:
				target.pierce.BonusValue += amount;
				target.pierce.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.pierce.BonusValue -= amount;
					target.pierce.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Vamp:
				target.vamp.BonusValue += amount;
				target.vamp.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.vamp.BonusValue -= amount;
					target.vamp.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Fervor:
				target.fervor.BonusValue += amount;
				target.fervor.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.fervor.BonusValue -= amount;
					target.fervor.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Speed:
				target.speed.BonusValue += amount;
				target.speed.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.speed.BonusValue -= amount;
					target.speed.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Tenacity:
				target.tenacity.BonusValue += amount;
				target.tenacity.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.tenacity.BonusValue -= amount;
					target.tenacity.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Crit:
				target.crit.BonusValue += amount;
				target.crit.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.crit.BonusValue -= amount;
					target.crit.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Efficiency:
				target.efficiency.BonusValue += amount;
				target.efficiency.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.efficiency.BonusValue -= amount;
					target.efficiency.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			case Stat.StatId.Range:
				target.range.BonusValue += amount;
				target.range.PercentageBonusValue += percentageAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.range.BonusValue -= amount;
					target.range.PercentageBonusValue -= percentageAmount;
				} else {
					yield return null;
				}
				break;
			default:
				break;
		}
	}

	public static void DealDamage(bool canHitStructures, Entity source, Entity target, DamageType type, float flatAmount, float percentageAmount) {
		if (!target) { return; }
		if (!canHitStructures && target.GetComponent<Structure>()) { return; }

		float effectiveHealth;
		HealthType[] damageOrder;
		switch (type) {
			case DamageType.Magical:
				effectiveHealth = target.vitality.CurrentValue + target.vitality.CurrentValue * ((target.nullification.CurrentValue - source.pierce.CurrentValue) / 100.0f);
				damageOrder = new[] { HealthType.MagicalShield, HealthType.Shield, HealthType.Health };
				break;
			case DamageType.Physical:
				effectiveHealth = target.vitality.CurrentValue + target.vitality.CurrentValue * ((target.armor.CurrentValue - source.force.CurrentValue) / 100.0f);
				damageOrder = new[] { HealthType.PhysicalShield, HealthType.Shield, HealthType.Health };
				break;
			case DamageType.True:
				effectiveHealth = target.vitality.CurrentValue;
				damageOrder = new[] { HealthType.Shield, HealthType.Health };
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}

		float damageTaken = (effectiveHealth * (percentageAmount / 100)) + flatAmount;
		float effectiveDamageTaken = damageTaken * (target.vitality.CurrentValue / effectiveHealth);
		float remainingDamage = effectiveDamageTaken;

		Ability.Heal(source, HealthType.Health, 0, effectiveDamageTaken * source.vamp.CurrentValue, 0); // Vamp

		foreach (HealthType healthType in damageOrder) {
			float originalValue;
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

		if (!source) { return; } // In case source dies before attack collides.
		Champion sourceChampion = source.GetComponent<Champion>();
		if (sourceChampion && target.GetComponent<Champion>()) { sourceChampion.lastTimeDamagedEnemyChampion = DateTime.Now; }
	}

	public static IEnumerator Heal(Entity target, HealthType type, float duration, float flatAmount, float percentageAmount) {
		float finalAmount = flatAmount + target.health * percentageAmount;

		switch (type) {
			case HealthType.Health:
				target.health += finalAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.health -= finalAmount;
				} else {
					yield return null;
				}
				break;
			case HealthType.MagicalShield:
				target.magicalShield += finalAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.magicalShield -= finalAmount;
				} else {
					yield return null;
				}
				break;
			case HealthType.PhysicalShield:
				target.physicalShield += finalAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.physicalShield -= finalAmount;
				} else {
					yield return null;
				}
				break;
			case HealthType.Shield:
				target.shield += finalAmount;
				if (duration > 0) {
					yield return new WaitForSeconds(duration);
					target.shield -= finalAmount;
				} else {
					yield return null;
				}
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}
	}
}
