using UnityEngine;
using static MatchManager;

public class Inhibitor : Structure {
	public Team inspectorTeam;
	public Tower protector;

	private void Start() { this.SetupStructure(0, 0, 0, 0, 10000, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, this.inspectorTeam); }

	private new void Update() {
		base.Update();
		if (this.protector.isActiveAndEnabled) {
			this.health = this.vitality.CurrentValue;
			this.gameObject.SetActive(true);
		}
	}
}
