using System;

public class Ability {
	public String name;
	public String description;
	public Action action;
	public DateTime castTime;

	public enum DamageType {
		Magical, Physical
	}

	public enum HealthType {
		Health, Shield, PhysicalShield, MagicalShield
	}

	public enum CrowdControlType {
		Stun, Slow
	}

	public Ability(String name, String description, Action action) { // Non-targeted ability
		this.name = name;
		this.description = description;
		this.action = action;
		this.castTime = DateTime.Now;
	}

	public Ability(String name, String description, Action<Entity> action, Entity target) { // Targeted ability
		this.name = name;
		this.description = description;
		this.action = () => action(target);
		this.castTime = DateTime.Now;
	}
}
