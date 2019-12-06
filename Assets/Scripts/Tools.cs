using System;
using System.Collections;
using UnityEngine;

public static class Tools {
	public static IEnumerator DoAfterTime(double delay, Action action) {
		yield return new WaitForSeconds((float) delay);
		action();
	}

	public static Vector2 MapPositionToWorldPosition(Vector3 worldPosition) {
		Vector3 mapScale = MatchManager.Instance.mapObject.transform.localScale;
		double mapActualX = mapScale.x * MatchManager.MapBaseX;
		double mapActualY = mapScale.y = MatchManager.MapBaseY;
		double mapActualZ = mapScale.z * MatchManager.MapBaseZ;

		return new Vector2(); // TODO
	}
}