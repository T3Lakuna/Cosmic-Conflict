using Photon.Pun;
using System;
using UnityEngine;

public class Tower : Structure {
	public Team inspectorTeam;
	public Tower protectorTower;
	public Inhibitor protectorInhibitorTop;
	public Inhibitor protectorInhibitorMiddle;
	public Inhibitor protectorInhibitorBottom;

	private void Start() { this.SetupStructure(300, 0, 0, 0, 10000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, 15, this.inspectorTeam); }

	private new void Update() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		base.Update();
		if (this.protectorTower && this.protectorTower.isActiveAndEnabled || this.protectorInhibitorTop && this.protectorInhibitorTop.isActiveAndEnabled && this.protectorInhibitorMiddle && this.protectorInhibitorMiddle.isActiveAndEnabled && this.protectorInhibitorBottom && this.protectorInhibitorBottom.isActiveAndEnabled) {
			this.health = this.vitality.CurrentValue;
			this.gameObject.SetActive(true);
		}

		if (!this.basicAttackTarget) { this.BasicAttackCommand(this.ClosestEntityInRange(true, false, false, false, false, true, this.range.CurrentValue)); } // Non-champion, non-structure enemies in range.

		if (!this.basicAttackTarget) { this.BasicAttackCommand(this.ClosestEntityInRange(true, false, false, true, false, true, this.range.CurrentValue)); } // Non-structure enemies in range.

		Entity championPriorityTarget = this.ClosestEntityInRange(true, false, false, true, false, false, this.range.CurrentValue);
		if (championPriorityTarget && DateTime.Now - championPriorityTarget.GetComponent<Champion>().lastTimeDamagedEnemyChampion < TimeSpan.FromSeconds(Time.deltaTime)) { this.BasicAttackCommand(championPriorityTarget); } // Target champions which attack allies.
	}
}