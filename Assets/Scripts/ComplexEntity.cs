using Photon.Pun;
using System;
using UnityEngine;

public abstract class ComplexEntity : Entity {
	[HideInInspector] public bool blueTeam;

	[HideInInspector] public double passiveCooldown; // Seconds before next passive use
	[HideInInspector] public double primaryCooldown; // Seconds before next primary ability use
	[HideInInspector] public double secondaryCooldown; // Seconds before next secondary ability use
	[HideInInspector] public double tertiaryCooldown; // Seconds before next tertiary ability use
	[HideInInspector] public double ultimateCooldown; // Seconds before next ultimate ability use

	[HideInInspector] public double currentPassiveCooldown; // Maximum seconds before next passive use
	[HideInInspector] public double currentPrimaryCooldown; // Maximum seconds before next primary ability use
	[HideInInspector] public double currentSecondaryCooldown; // Maximum seconds before next secondary ability use
	[HideInInspector] public double currentTertiaryCooldown; // Maximum seconds before next tertiary ability use
	[HideInInspector] public double currentUltimateCooldown; // Maximum seconds before next ultimate ability use

	[HideInInspector] public double basePassiveCooldown; // Base seconds before next passive use
	[HideInInspector] public double basePrimaryCooldown; // Base seconds before next primary ability use
	[HideInInspector] public double baseSecondaryCooldown; // Base seconds before next secondary ability use
	[HideInInspector] public double baseTertiaryCooldown; // Base seconds before next tertiary ability use
	[HideInInspector] public double baseUltimateCooldown; // Base seconds before next ultimate ability use

	// TODO: Get ability level points on level-up
	[HideInInspector] public int primaryLevel; // Level of primary ability
	[HideInInspector] public int secondaryLevel; // Level of secondary ability
	[HideInInspector] public int tertiaryLevel; // Level of tertiary ability
	[HideInInspector] public int ultimateLevel; // Level of ultimate ability

	[HideInInspector] public Ability passiveAbility;
	[HideInInspector] public Ability primaryAbility;
	[HideInInspector] public Ability secondaryAbility;
	[HideInInspector] public Ability tertiaryAbility;
	[HideInInspector] public Ability ultimateAbility;

	public void SetupComplexEntity(double healthBase, double healthScaling, double regenerationBase,
		double regenerationScaling, double manaBase, double manaScaling, double enduranceBase, double enduranceScaling,
		double armorBase, double armorScaling, double nullificationBase, double nullificationScaling, double forceBase,
		double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling,
		double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase,
		double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling,
		double rangeBase, double rangeScaling, Team team, Ability passiveAbility, Ability primaryAbility,
		Ability secondaryAbility, Ability tertiaryAbility, Ability ultimateAbility) {
		this.SetupEntity(healthBase, healthScaling, regenerationBase, regenerationScaling, manaBase, manaScaling,
			enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling,
			forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling,
			speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase,
			efficiencyScaling, rangeBase, rangeScaling, team);
		this.passiveAbility = passiveAbility;
		this.primaryAbility = primaryAbility;
		this.secondaryAbility = secondaryAbility;
		this.tertiaryAbility = tertiaryAbility;
		this.ultimateAbility = ultimateAbility;

		this.primaryLevel = 0;
		this.secondaryLevel = 0;
		this.tertiaryLevel = 0;
		this.ultimateLevel = 0;
	}

	public void ComplexEntityUpdate() {
		this.EntityUpdate();
		this.UpdateCooldowns();
		this.CheckPlayerActions();
		this.passiveAbility.action();
	}

	private void UpdateCooldowns() {
		this.currentPassiveCooldown = this.basePassiveCooldown - (this.basePassiveCooldown * this.currentEfficiency);
		this.currentPrimaryCooldown = this.basePrimaryCooldown - (this.basePrimaryCooldown * this.currentEfficiency);
		this.currentSecondaryCooldown =
			this.baseSecondaryCooldown - (this.baseSecondaryCooldown * this.currentEfficiency);
		this.currentTertiaryCooldown = this.baseTertiaryCooldown - (this.baseTertiaryCooldown * this.currentEfficiency);
		this.currentUltimateCooldown = this.baseUltimateCooldown - (this.baseUltimateCooldown * this.currentEfficiency);

		if (this.passiveCooldown > this.currentPassiveCooldown) {
			this.passiveCooldown = this.currentPassiveCooldown;
		}

		if (this.primaryCooldown > this.currentPrimaryCooldown) {
			this.primaryCooldown = this.currentPrimaryCooldown;
		}

		if (this.secondaryCooldown > this.currentSecondaryCooldown) {
			this.secondaryCooldown = this.currentSecondaryCooldown;
		}

		if (this.tertiaryCooldown > this.currentTertiaryCooldown) {
			this.tertiaryCooldown = this.currentTertiaryCooldown;
		}

		if (this.ultimateCooldown > this.currentUltimateCooldown) {
			this.ultimateCooldown = this.currentUltimateCooldown;
		}

		this.passiveCooldown = Math.Max(this.passiveCooldown -= Time.deltaTime, 0);
		this.primaryCooldown = Math.Max(this.primaryCooldown -= Time.deltaTime, 0);
		this.secondaryCooldown = Math.Max(this.secondaryCooldown -= Time.deltaTime, 0);
		this.tertiaryCooldown = Math.Max(this.tertiaryCooldown -= Time.deltaTime, 0);
		this.ultimateCooldown = Math.Max(this.ultimateCooldown -= Time.deltaTime, 0);
	}

	private void CheckPlayerActions() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) {
			return;
		}

		if (Input.GetKeyUp(KeyCode.Q) && this.primaryCooldown <= 0) {
			this.primaryCooldown = this.currentPrimaryCooldown;
			this.primaryAbility.action();
		}
		else if (Input.GetKeyUp(KeyCode.W) && this.secondaryCooldown <= 0) {
			this.secondaryCooldown = this.currentSecondaryCooldown;
			this.secondaryAbility.action();
		}
		else if (Input.GetKeyUp(KeyCode.E) && this.tertiaryCooldown <= 0) {
			this.tertiaryCooldown = this.currentTertiaryCooldown;
			this.tertiaryAbility.action();
		}
		else if (Input.GetKeyUp(KeyCode.R) && this.ultimateCooldown <= 0) {
			this.ultimateCooldown = this.currentUltimateCooldown;
			this.ultimateAbility.action();
		}
	}
}