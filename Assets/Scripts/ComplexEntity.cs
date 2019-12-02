﻿using System;
using UnityEngine;

public abstract class ComplexEntity : Entity {
	[HideInInspector] public Ability passiveAbility;
	[HideInInspector] public Ability primaryAbility;
	[HideInInspector] public Ability secondaryAbility;
	[HideInInspector] public Ability tertiaryAbility;
	[HideInInspector] public Ability ultimateAbility;

	public void SetupComplexEntity(double vitalityBase, double vitalityScaling, double regenerationBase,
		double regenerationScaling, double energyBase, double energyScaling, double enduranceBase,
		double enduranceScaling, double armorBase, double armorScaling, double nullificationBase,
		double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling,
		double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase,
		double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling,
		double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, Team team,
		Ability passiveAbility, Ability primaryAbility, Ability secondaryAbility, Ability tertiaryAbility,
		Ability ultimateAbility) {
		this.SetupEntity(vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase,
			energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase,
			nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase,
			fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling,
			efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, team);
		this.passiveAbility = passiveAbility;
		this.primaryAbility = primaryAbility;
		this.secondaryAbility = secondaryAbility;
		this.tertiaryAbility = tertiaryAbility;
		this.ultimateAbility = ultimateAbility;
	}

	public void UpdateCooldowns() {
		// TODO
	}
}