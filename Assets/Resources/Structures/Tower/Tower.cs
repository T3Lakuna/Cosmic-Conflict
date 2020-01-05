public class Tower : Structure {
	public Team inspectorTeam;
	public Tower protectorTower;
	public Inhibitor protectorInhibitorTop;
	public Inhibitor protectorInhibitorMiddle;
	public Inhibitor protectorInhibitorBottom;

	private void Start() { this.SetupStructure(100, 0, 0, 0, 5000, 0, 0, 0, 0, 0, 0, 0, 100, 0, 100, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, this.inspectorTeam); }

	private new void Update() {
		base.Update();
		if (this.protectorTower && this.protectorTower.isActiveAndEnabled || this.protectorInhibitorTop && this.protectorInhibitorTop.isActiveAndEnabled && this.protectorInhibitorMiddle && this.protectorInhibitorMiddle.isActiveAndEnabled && this.protectorInhibitorBottom && this.protectorInhibitorBottom.isActiveAndEnabled) {
			this.health = this.vitality.CurrentValue;
			this.gameObject.SetActive(true);
		}
		if (!this.basicAttackTarget) { this.BasicAttackCommand(this.ClosestEntityInRange(true, false, false, false, false, true, this.range.CurrentValue)); } // Non-champion, non-structure enemies in range.
		if (!this.basicAttackTarget) { this.BasicAttackCommand(this.ClosestEntityInRange(true, false, false, true, false, true, this.range.CurrentValue)); } // Non-structure enemies in range.
																																							 // TODO: Target enemy champion if they attack an ally champion and the current target isn't a champion.
	}
}
