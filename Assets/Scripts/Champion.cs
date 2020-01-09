using Photon.Pun;
using System;
using UnityEngine;

public abstract class Champion : ComplexEntity {
	[HideInInspector] public Player player;
	[HideInInspector] public int currency;
	[HideInInspector] public int uniqueUserId;
	[HideInInspector] public GameObject prefab;
	[HideInInspector] public Sprite icon;
	[HideInInspector] public DateTime lastTimeDamagedEnemyChampion;

	protected void SetupChampion(double damageBase, double damageScaling, double magicBase, double magicScaling, double vitalityBase, double vitalityScaling, double regenerationBase, double regenerationScaling, double energyBase, double energyScaling, double enduranceBase, double enduranceScaling, double armorBase, double armorScaling, double nullificationBase, double nullificationScaling, double forceBase, double forceScaling, double pierceBase, double pierceScaling, double vampBase, double vampScaling, double fervorBase, double fervorScaling, double speedBase, double speedScaling, double tenacityBase, double tenacityScaling, double critBase, double critScaling, double efficiencyBase, double efficiencyScaling, double rangeBase, double rangeScaling, double entityHeight, string name, Sprite icon, Ability passiveAbility, Ability primaryAbility, Ability secondaryAbility, Ability tertiaryAbility, Ability ultimateAbility) {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		this.player = MatchManager.Instance.localPlayer;
		this.SetupComplexEntity(damageBase, damageScaling, magicBase, magicScaling, vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase, energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, entityHeight, this.player.team, passiveAbility, primaryAbility, secondaryAbility, tertiaryAbility, ultimateAbility);
		this.currency = 0;
		this.name = name;
		this.icon = icon;
		this.player = MatchManager.Instance.localPlayer;
		this.uniqueUserId = this.player.uniqueUserId;
	}

	public void Start() {
		this.StartCoroutine(this.Tick());
	}

	public new void Update() {
		base.Update();

		if (!this.player) { return; }

		this.CheckPlayerActions();
		this.passiveAbility.Cast();
	}

	private System.Collections.IEnumerator Tick() {
		while (true) {
			this.currency += 3;
			yield return new WaitForSeconds(1);
		}
	}

	private void CheckPlayerActions() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		if (Input.GetKeyUp(KeyCode.Q) && this.primaryAbility.CurrentCooldown <= 0) { this.primaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.W) && this.secondaryAbility.CurrentCooldown <= 0) { this.secondaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.E) && this.tertiaryAbility.CurrentCooldown <= 0) { this.tertiaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.R) && this.ultimateAbility.CurrentCooldown <= 0) { this.ultimateAbility.Cast(); }

		if (Input.GetKeyUp(KeyCode.S)) { this.MovementCommand(this.transform.position); }

		if (Input.GetMouseButton(1)) {
			RaycastHit movementRaycast = this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask);
			RaycastHit basicAttackRaycast = this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask);
			if (basicAttackRaycast.collider) {
				Entity basicAttackEntity = basicAttackRaycast.collider.GetComponent<Entity>();
				if (basicAttackEntity) { this.BasicAttackCommand(basicAttackEntity); }
			} else if (movementRaycast.collider && movementRaycast.collider != MatchManager.Instance.wallCollider) { this.MovementCommand(movementRaycast.point); }
		}
	}
}
