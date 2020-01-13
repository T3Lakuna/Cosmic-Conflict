using UnityEngine;

public class RangedMinion : Minion {
	public static RangedMinion CreateRangedMinion(Team team, GameObject[] path) {
		RangedMinion rangedMinion = Tools.Instantiate("Entities/Simple/Minion/Model", Tools.PositionOnMapAt(path[0].transform.position)).AddComponent<RangedMinion>();
		rangedMinion.SetupMinion(30, 2, 450, 8, 25, team, path, "Ranged Minion");
		return rangedMinion;
	}
}
