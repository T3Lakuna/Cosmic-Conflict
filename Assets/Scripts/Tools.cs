using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public static class Tools {
	public static IEnumerator DoAfterTime(double delay, Action action) {
		yield return new WaitForSeconds((float) delay);
		action();
	}

	public static GameObject Instantiate(string prefabResourcesPath, Vector3 position) { return Photon.Pun.PhotonNetwork.InRoom ? Photon.Pun.PhotonNetwork.Instantiate(prefabResourcesPath, position, Quaternion.identity) : UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<GameObject>(prefabResourcesPath), position, Quaternion.identity); }

	public static void Destroy(GameObject gameObject) { if (Photon.Pun.PhotonNetwork.InRoom) { Photon.Pun.PhotonNetwork.Destroy(gameObject); } else { UnityEngine.Object.Destroy(gameObject); } }

	public static Vector3 PositionOnMapAt(Vector3 position, double objectHeightForOffset) { return new Vector3(position.x, (float) Tools.HeightOfMapAt(position.x, position.z) + (float) objectHeightForOffset / 2 + 0.0f, position.z); }

	public static double HeightOfMapAt(double x, double z) {
		Ray heightRay = new Ray(new Vector3((float) x, 100, (float) z), new Vector3(0, -90, 0));
		Physics.Raycast(heightRay, out RaycastHit hit);
		if (hit.collider != MatchManager.Instance.floorCollider && hit.collider != MatchManager.Instance.riverBedCollider && hit.collider != MatchManager.Instance.structureBaseCollider) { return -1; }

		return hit.point.y;
	}

	public static double Random() {
		RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
		byte[] bytes = new byte[8];
		provider.GetBytes(bytes);
		provider.Dispose();
		// ReSharper disable once PossibleLossOfFraction
		return BitConverter.ToUInt64(bytes, 0) / (1 << 11) / (double) (1UL << 53);
	}
}