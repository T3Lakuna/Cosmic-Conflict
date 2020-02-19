using UnityEngine;

public class RangedMinion : Minion {
	public static Minion CreateRangedMinion(Team team, GameObject[] path) {
		Minion rangedMinion = Tools.Instantiate("Entities/Simple/Minion/Model", Tools.PositionOnMapAt(path[0].transform.position)).GetComponent<Minion>();
		rangedMinion.SetupMinion(20, 2, 200, 5, 25, team, path, "Ranged Minion");
		return rangedMinion;
	}
}
