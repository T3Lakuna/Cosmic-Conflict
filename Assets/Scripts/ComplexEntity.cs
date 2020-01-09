using UnityEngine;

public abstract class ComplexEntity : Entity {
	[HideInInspector] public Ability passiveAbility;
	[HideInInspector] public Ability primaryAbility;
	[HideInInspector] public Ability secondaryAbility;
	[HideInInspector] public Ability tertiaryAbility;
	[HideInInspector] public Ability ultimateAbility;

	public void SetupComplexEntity(double damageBase, double damageScaling, double magicBase, double magicScaling, double vitalityBase, double vitalityScaling, double regenerationBase, double regenerationScaling, double energyBase, double energyScaling, double enduranceBase, double enduranceScaling, double armorBase, double armorScaling, double nullificationBase, double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, double entityHeight, Team team, Ability passiveAbility, Ability primaryAbility, Ability secondaryAbility, Ability tertiaryAbility, Ability ultimateAbility) {
		this.SetupEntity(damageBase, damageScaling, magicBase, magicScaling, vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase, energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, entityHeight, team);
		this.passiveAbility = passiveAbility;
		this.primaryAbility = primaryAbility;
		this.secondaryAbility = secondaryAbility;
		this.tertiaryAbility = tertiaryAbility;
		this.ultimateAbility = ultimateAbility;
	}

	public new void Update() {
		base.Update();
		this.UpdateCooldowns();
	}

	private void UpdateCooldowns() {
		this.passiveAbility.UpdateCooldown();
		this.primaryAbility.UpdateCooldown();
		this.secondaryAbility.UpdateCooldown();
		this.tertiaryAbility.UpdateCooldown();
		this.ultimateAbility.UpdateCooldown();
	}
}