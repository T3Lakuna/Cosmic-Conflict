using System;
using UnityEngine;

public class Minion : Entity {
	private GameObject[] path;
	private int pathIndex;

	public void SetupMinion(double damageBase, double damageScaling, double vitalityBase, double vitalityScaling, double rangeBase, Team team, GameObject[] path, string name) {
		this.SetupEntity(damageBase, damageScaling, 0, 0, vitalityBase, vitalityScaling, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 25, 1, 0, 0, 0, 0, 0, 0, rangeBase, 0, 0, this.GetComponent<Animator>(), team);

		this.path = path;
		this.pathIndex = 0;

		switch (team) {
			case Team.Red:
				this.transform.Find("Dummy").GetComponent<Renderer>().material = MatchManager.Instance.redMaterial;
				this.transform.parent = MatchManager.Instance.redMinionHolder;
				break;
			case Team.Blue:
				this.transform.Find("Dummy").GetComponent<Renderer>().material = MatchManager.Instance.blueMaterial;
				this.transform.parent = MatchManager.Instance.blueMinionHolder;
				break;
			case Team.Neutral:
				break;
			default:
				break;
		}
		this.name = name;
	}

	private new void Update() {
		base.Update();

		if (!this.basicAttackTarget) { this.BasicAttackCommand(this.ClosestEntityInRange(true, false, false, false, true, true, this.range.CurrentValue)); } // Non-champion enemies in range.

		if (!this.basicAttackTarget) { this.BasicAttackCommand(this.ClosestEntityInRange(true, false, false, true, true, true, this.range.CurrentValue)); } // Enemies in range.

		Entity championPriorityTarget = this.ClosestEntityInRange(true, false, false, true, false, false, this.range.CurrentValue);
		if (championPriorityTarget && DateTime.Now - championPriorityTarget.GetComponent<Champion>().lastTimeDamagedEnemyChampion < TimeSpan.FromSeconds(Time.deltaTime)) { this.BasicAttackCommand(championPriorityTarget); } // Target champions which attack allies.

		if (this.transform.position == this.path[pathIndex].transform.position) {
			this.pathIndex++;
			if (this.pathIndex < path.Length) { this.MovementCommand(path[pathIndex].transform.position); }
		}

		if (this.basicAttackTarget) { this.movementTarget = this.transform.position; }

		if (this.movementTarget == this.transform.position && !this.basicAttackTarget) { this.MovementCommand(Tools.PositionOnMapAt(path[pathIndex].transform.position)); }
	}
}
