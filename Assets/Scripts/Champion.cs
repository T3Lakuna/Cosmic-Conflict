﻿using Photon.Pun;
using UnityEngine;

public abstract class Champion : ComplexEntity {
	[HideInInspector] public Player player;
	[HideInInspector] public int currency;
	[HideInInspector] public string championName;
	[HideInInspector] public int uniqueUserId;
	[HideInInspector] public GameObject prefab;

	protected void SetupChampion(double damageBase, double damageScaling, double magicBase, double magicScaling, double vitalityBase, double vitalityScaling, double regenerationBase, double regenerationScaling, double energyBase, double energyScaling, double enduranceBase, double enduranceScaling, double armorBase, double armorScaling, double nullificationBase, double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, string name, Ability passiveAbility, Ability primaryAbility, Ability secondaryAbility, Ability tertiaryAbility, Ability ultimateAbility) {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		this.player = MatchManager.Instance.localPlayer;
		this.SetupComplexEntity(damageBase, damageScaling, magicBase, magicScaling, vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase, energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, this.player.team, passiveAbility, primaryAbility, secondaryAbility, tertiaryAbility, ultimateAbility);
		this.currency = 0;
		this.championName = name;
		this.player = MatchManager.Instance.localPlayer;
		this.uniqueUserId = this.player.uniqueUserId;
	}

	private void Start() { this.StartCoroutine(this.Tick()); }

	private void Update() {
		if (!this.player) { return; }

		this.CheckPlayerActions();
		this.UpdateStats();
		this.UpdateCooldowns();
		this.MovementUpdate();
		this.passiveAbility.Cast();
	}

	private System.Collections.IEnumerator Tick() {
		while (true) {
			this.RegenerateResources();
			this.currency += 3;
			yield return new WaitForSeconds(1);
		}
	}

	private void CheckPlayerActions() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		if (Input.GetKeyUp(KeyCode.Q) && this.primaryAbility.CurrentCooldown <= 0) { this.primaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.W) && this.secondaryAbility.CurrentCooldown <= 0) { this.secondaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.E) && this.tertiaryAbility.CurrentCooldown <= 0) { this.tertiaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.R) && this.ultimateAbility.CurrentCooldown <= 0) { this.ultimateAbility.Cast(); }

		if (Input.GetKeyUp(KeyCode.S)) { this.MovementCommand(this.transform.position); }

		if (!Physics.Raycast(this.player.playerCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit mousePosition)) { return; }

		if (mousePosition.collider != MatchManager.Instance.floorCollider && mousePosition.collider != MatchManager.Instance.structureBaseCollider && mousePosition.collider != MatchManager.Instance.riverBedCollider) { return; }

		if (Input.GetMouseButton(1)) { this.MovementCommand(mousePosition.point); }
	}
}