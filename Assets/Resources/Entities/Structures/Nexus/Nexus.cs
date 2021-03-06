﻿using Photon.Pun;
using UnityEngine;

public class Nexus : Structure {
	public Team inspectorTeam;
	public Tower topProtector;
	public Tower bottomProtector;

	private void Start() { this.SetupStructure(0, 0, 0, 0, 10000, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, this.inspectorTeam); }

	private new void Update() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		base.Update();
		if (this.topProtector && this.topProtector.isActiveAndEnabled || this.bottomProtector && this.bottomProtector.isActiveAndEnabled) {
			this.health = this.vitality.CurrentValue;
			this.gameObject.SetActive(true);
		}
		// TODO: Make enemy team win if this is dead.
	}
}
