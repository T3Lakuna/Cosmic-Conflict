using UnityEngine;

public class CannonMinion : Minion {
	public static CannonMinion CreateCannonMinion(Team team, GameObject[] path) {
		CannonMinion cannonMinion = Tools.Instantiate("Entities/Simple/Minion/Model", Tools.PositionOnMapAt(path[0].transform.position)).AddComponent<CannonMinion>();
		cannonMinion.SetupMinion(60, 4, 1800, 30, 25, team, path, "Cannon Minion");
		return cannonMinion;
	}
}
