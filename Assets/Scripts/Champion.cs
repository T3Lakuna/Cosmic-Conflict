using Photon.Pun;
using UnityEngine;

public abstract class Champion : ComplexEntity {
	public Player player;
	public int currency;

	public void SetupChampion(double vitalityBase, double vitalityScaling, double regenerationBase, double regenerationScaling, double energyBase, double energyScaling, double enduranceBase, double enduranceScaling, double armorBase, double armorScaling, double nullificationBase, double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, Team team, Ability passiveAbility, Ability primaryAbility, Ability secondaryAbility, Ability tertiaryAbility, Ability ultimateAbility) {
		this.SetupComplexEntity(vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase, energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, team, passiveAbility, primaryAbility, secondaryAbility, tertiaryAbility, ultimateAbility);
		this.currency = 0;
		this.player = MatchManager.Instance.localPlayer;
	}

	private void Start() { this.StartCoroutine(this.Tick()); }

	private void Update() {
		this.CheckPlayerActions();
		this.UpdateCooldowns();
	}

	private System.Collections.IEnumerator Tick() {
		while (true) {
			this.UpdateStats();
			this.RegenerateResources();
			this.passiveAbility.Cast();
			this.currency += 3;
			yield return new WaitForSeconds(1);
		}
	}

	private void CheckPlayerActions() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		if (Input.GetKeyUp(KeyCode.Q) && this.primaryAbility.CurrentCooldown <= 0) { this.primaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.W) && this.secondaryAbility.CurrentCooldown <= 0) { this.secondaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.E) && this.tertiaryAbility.CurrentCooldown <= 0) { this.tertiaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.R) && this.ultimateAbility.CurrentCooldown <= 0) { this.ultimateAbility.Cast(); }

		if (!Physics.Raycast(this.player.playerCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit mousePosition)) { return; }

		if (mousePosition.collider != MatchManager.Instance.floorCollider && mousePosition.collider != MatchManager.Instance.structureBaseCollider && mousePosition.collider != MatchManager.Instance.riverBedCollider) { return; }
	}
}