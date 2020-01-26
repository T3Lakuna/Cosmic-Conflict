using Photon.Pun;

public class Inhibitor : Structure {
	public Team inspectorTeam;
	public Tower protector;

	private void Start() { this.SetupStructure(0, 0, 0, 0, 10000, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, this.inspectorTeam); }

	private new void Update() {
		if (PhotonNetwork.InRoom && !this.photonView.IsMine) { return; }

		base.Update();
		if (this.protector && this.protector.isActiveAndEnabled) {
			this.health = this.vitality.CurrentValue;
			this.gameObject.SetActive(true);
		}
	}
}
