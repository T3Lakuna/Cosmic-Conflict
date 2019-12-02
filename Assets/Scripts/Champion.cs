using Photon.Pun;
using UnityEngine;

public abstract class Champion : ComplexEntity {
	public Player player;
	public int currency;

	public void SetupChampion(double vitalityBase, double vitalityScaling, double regenerationBase, double regenerationScaling, double energyBase, double energyScaling, double enduranceBase, double enduranceScaling, double armorBase, double armorScaling, double nullificationBase, double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, Team team, Ability passiveAbility, Ability primaryAbility, Ability secondaryAbility, Ability tertiaryAbility, Ability ultimateAbility) {
		this.SetupComplexEntity(vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase, energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, team, passiveAbility, primaryAbility, secondaryAbility, tertiaryAbility, ultimateAbility);
		this.currency = 0;
	}

	private void Update() {
		this.UpdateStats();
		this.RegenerateResources();
		this.UpdateCooldowns();
		this.CheckPlayerActions();
		this.passiveAbility.action();
	}

	public void CheckPlayerActions() {
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