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

	protected void SetupChampion(float damageBase, float damageScaling, float magicBase, float magicScaling, float vitalityBase, float vitalityScaling, float regenerationBase, float regenerationScaling, float energyBase, float energyScaling, float enduranceBase, float enduranceScaling, float armorBase, float armorScaling, float nullificationBase, float nullificationScaling, float forceBase, float forceScaling, float pierceBase, float pierceScaling, float vampBase, float vampScaling, float fervorBase, float fervorScaling, float speedBase, float speedScaling, float tenacityBase, float tenacityScaling, float critBase, float critScaling, float efficiencyBase, float efficiencyScaling, float rangeBase, float rangeScaling, float entityHeight, Animator entityAnimator, string name, Sprite icon, Ability passiveAbility, Ability primaryAbility, Ability secondaryAbility, Ability tertiaryAbility, Ability ultimateAbility) {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		this.player = Player.localPlayer;
		this.SetupComplexEntity(damageBase, damageScaling, magicBase, magicScaling, vitalityBase, vitalityScaling, regenerationBase, regenerationScaling, energyBase, energyScaling, enduranceBase, enduranceScaling, armorBase, armorScaling, nullificationBase, nullificationScaling, forceBase, forceScaling, pierceBase, pierceScaling, vampBase, vampScaling, fervorBase, fervorScaling, speedBase, speedScaling, tenacityBase, tenacityScaling, critBase, critScaling, efficiencyBase, efficiencyScaling, rangeBase, rangeScaling, entityHeight, entityAnimator, this.player.team, true, passiveAbility, primaryAbility, secondaryAbility, tertiaryAbility, ultimateAbility);
		this.currency = 0;
		this.name = name;
		this.icon = icon;
		this.player = Player.localPlayer;
		this.uniqueUserId = this.player.uniqueUserId;

		MatchManager.Instance.champions.Add(this);
	}

	public void Start() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }
		this.StartCoroutine(this.Tick());
	}

	public new void Update() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		if (!this.player) { this.player = Player.localPlayer; }
		if (!this.player.champion) { this.player.champion = this; }

		base.Update();

		this.CheckPlayerActions();
		this.passiveAbility.Cast();
	}

	private System.Collections.IEnumerator Tick() {
		while (true) {
			this.currency += 1;
			yield return new WaitForSeconds(1);
		}
	}

	private void CheckPlayerActions() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		if (Input.GetKeyUp(KeyCode.Q) && this.primaryAbility.CurrentCooldown <= 0) { this.primaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.W) && this.secondaryAbility.CurrentCooldown <= 0) { this.secondaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.E) && this.tertiaryAbility.CurrentCooldown <= 0) { this.tertiaryAbility.Cast(); } else if (Input.GetKeyUp(KeyCode.R) && this.ultimateAbility.CurrentCooldown <= 0) { this.ultimateAbility.Cast(); }

		if (Input.GetKeyUp(KeyCode.S)) { this.MovementCommand(this.transform.position); }

		if (Input.GetMouseButton(1)) {
			Entity basicAttackEntity;
			RaycastHit movementRaycast = this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask);
			if (!Input.GetKey(KeyCode.LeftShift)) { // Direct target.
				RaycastHit basicAttackRaycast = this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask);
				if (basicAttackRaycast.collider) { basicAttackEntity = basicAttackRaycast.collider.GetComponent<Entity>(); } else { basicAttackEntity = null; }
			} else { basicAttackEntity = this.player.FuzzyMouseTargetEntity(true, false, false, true, true, true); }
			if (basicAttackEntity) { this.BasicAttackCommand(basicAttackEntity); } else if (movementRaycast.collider && movementRaycast.collider != MatchManager.Instance.wallCollider) { this.MovementCommand(movementRaycast.point); }
		}
	}
}