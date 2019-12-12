using System;
using System.Collections;
using UnityEngine;

public static class Tools {
	public static IEnumerator DoAfterTime(double delay, Action action) {
		yield return new WaitForSeconds((float) delay);
		action();
	}

	public static GameObject Instantiate(string prefabResourcesPath, Vector3 position) { return Photon.Pun.PhotonNetwork.InRoom ? Photon.Pun.PhotonNetwork.Instantiate(prefabResourcesPath, position, Quaternion.identity) : UnityEngine.Object.Instantiate(Resources.Load<GameObject>(prefabResourcesPath), position, Quaternion.identity); }

	public static void Destroy(GameObject gameObject) {
		if (Photon.Pun.PhotonNetwork.InRoom) { Photon.Pun.PhotonNetwork.Destroy(gameObject); } else { UnityEngine.Object.Destroy(gameObject); }
	}
}