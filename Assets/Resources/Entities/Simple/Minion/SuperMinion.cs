using UnityEngine;

public class SuperMinion : Minion {
	public static SuperMinion CreateSuperMinion(Team team, GameObject[] path) {
		SuperMinion superMinion = Tools.Instantiate("Entities/Simple/Minion/Model", Tools.PositionOnMapAt(path[0].transform.position)).AddComponent<SuperMinion>();
		superMinion.SetupMinion(360, 1, 3600, 15, 5, team, path, "Super Minion");
		return superMinion;
	}
}
