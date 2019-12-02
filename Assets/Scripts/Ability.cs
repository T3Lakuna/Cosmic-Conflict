using System;

public class Ability {
	public enum DamageType {
		Magical, Physical, True
	}

	public enum HealthType {
		Health, Shield, PhysicalShield, MagicalShield
	}

	public enum StatusEffectType {
		Stun, Slow
	}

	public Entity source;
	public String name;
	public String description;
	public Action action;
	public DateTime castTime;

	public Ability(Entity source, String name, String description, Action action) { // Non-targeted ability
		this.source = source;
		this.name = name;
		this.description = description;
		this.action = action;
		this.castTime = DateTime.Now;
	}

	public Ability(Entity source, String name, String description, double duration, Action<Entity> action, Entity target) : this(source, name, description, () => action(target)) { } // Targeted ability

	public static void DealDamage(Entity target, DamageType type, double flatAmount, double percentageAmount) {
		double effectiveHealth;
		switch (type) {
			case DamageType.Magical:
				effectiveHealth = target.health + target.health * (target.nullification.currentValue / 100.0);
				break;
			case DamageType.Physical:
				effectiveHealth = target.health + target.health * (target.armor.currentValue / 100.0);
				break;
			case DamageType.True:
				effectiveHealth = target.health;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		double damageTaken = (effectiveHealth * percentageAmount) + flatAmount;
		double effectiveDamageTaken = damageTaken * (target.vitality.currentValue / effectiveHealth);
		target.health -= effectiveDamageTaken;
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
		}

		if (duration > 0) {
			Tools.DoAfterTime(duration, () => {
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
				}
			});
		}
	}

	public static void ApplyStatusEffect(Entity target, StatusEffectType type, double duration, double value) {
		target.currentStatusEffects.Add(type);
		Tools.DoAfterTime(duration, () => { target.currentStatusEffects.Remove(type); });
	}
}
