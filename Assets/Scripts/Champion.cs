using Photon.Pun;
using System;
using UnityEngine;

public abstract class Champion : Entity {
	public Player player;
	public bool blueTeam;

	public double passiveCooldown; // Seconds before next passive use
	public double primaryCooldown; // Seconds before next primary ability use
	public double secondaryCooldown; // Seconds before next secondary ability use
	public double tertiaryCooldown; // Seconds before next tertiary ability use
	public double ultimateCooldown; // Seconds before next ultimate ability use

	public double currentPassiveCooldown; // Maximum seconds before next passive use
	public double currentPrimaryCooldown; // Maximum seconds before next primary ability use
	public double currentSecondaryCooldown; // Maximum seconds before next secondary ability use
	public double currentTertiaryCooldown; // Maximum seconds before next tertiary ability use
	public double currentUltimateCooldown; // Maximum seconds before next ultimate ability use

	public double basePassiveCooldown; // Base seconds before next passive use
	public double basePrimaryCooldown; // Base seconds before next primary ability use
	public double baseSecondaryCooldown; // Base seconds before next secondary ability use
	public double baseTertiaryCooldown; // Base seconds before next tertiary ability use
	public double baseUltimateCooldown; // Base seconds before next ultimate ability use

	// TODO: Get ability level points on level-up
	public int primaryLevel; // Level of primary ability
	public int secondaryLevel; // Level of secondary ability
	public int tertiaryLevel; // Level of tertiary ability
	public int ultimateLevel; // Level of ultimate ability

	public Ability passiveAbility;
	public Ability primaryAbility;
	public Ability secondaryAbility;
	public Ability tertiaryAbility;
	public Ability ultimateAbility;

	private void Update() {
		this.UpdateCooldowns();
		this.CheckPlayerActions();
		this.passiveAbility.action();
	}

	private void UpdateCooldowns() {
		this.currentPassiveCooldown = this.basePassiveCooldown - (this.basePassiveCooldown * this.currentEfficiency);
		this.currentPrimaryCooldown = this.basePrimaryCooldown - (this.basePrimaryCooldown * this.currentEfficiency);
		this.currentSecondaryCooldown = this.baseSecondaryCooldown - (this.baseSecondaryCooldown * this.currentEfficiency);
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
		} else if (Input.GetKeyUp(KeyCode.W) && this.secondaryCooldown <= 0) {
			this.secondaryCooldown = this.currentSecondaryCooldown;
			this.secondaryAbility.action();
		} else if (Input.GetKeyUp(KeyCode.E) && this.tertiaryCooldown <= 0) {
			this.tertiaryCooldown = this.currentTertiaryCooldown;
			this.tertiaryAbility.action();
		} else if (Input.GetKeyUp(KeyCode.R) && this.ultimateCooldown <= 0) {
			this.ultimateCooldown = this.currentUltimateCooldown;
			this.ultimateAbility.action();
		}
	}
}