using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static Entity;

public static class Tools {
	// TODO: When rewriting, make generalized methods for things in an area, and another generalized method which uses that one to get the nearest of a thing in an area. These can replace things like Player.RaycastOnLayer, Player.FuzzyMouseTargetEntity, giving experience/gold on death, area of effect abilities, et cetera.
	// TODO: When rewriting, make a "targeting" class that can be passed into lots of things (such as the methods mentioned above) so that multiple methods don't have to have the same "can target champions" or "can target allies" (et cetera) parameters.
	// TODO: When rewriting, make a generalized method to go with the targeting class which returns whether or not a passed in entity should be affected using the specifications, so that it doesn't need to be rewritten at the top of every method which uses the targeting class.
	// TODO: Folders can be reorganized to have multiple Resources folders in subdirectories, to reduce extraneous code and possibly drastically reduce lag.

	// TODO: When rewriting, replace Tools.Instantiate and Tools.Destroy with specific calls throughout the code using PhotonNetwork.offlineMode for singleplayer.
	public static GameObject Instantiate(string prefabResourcesPath, Vector3 position) { return Photon.Pun.PhotonNetwork.InRoom ? Photon.Pun.PhotonNetwork.Instantiate(prefabResourcesPath, position, Quaternion.identity) : UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<GameObject>(prefabResourcesPath), position, Quaternion.identity); }

	public static void Destroy(GameObject gameObject) {
		if (Photon.Pun.PhotonNetwork.InRoom) {
			if (gameObject.GetPhotonView().InstantiationId > 0) { Photon.Pun.PhotonNetwork.Destroy(gameObject); } else { MatchManager.Instance.photonView.RPC("DestroyRpc", RpcTarget.All, gameObject); }
		} else { UnityEngine.Object.Destroy(gameObject); }
	}

	public static Vector3 PositionOnMapAt(Vector3 position) { return new Vector3(position.x, (float) Tools.HeightOfMapAt(position.x, position.z), position.z); }

	public static float HeightOfMapAt(float x, float z) {
		Ray heightRay = new Ray(new Vector3((float) x, 100, (float) z), new Vector3(0, -90, 0));
		Physics.Raycast(heightRay, out RaycastHit hit, float.MaxValue, MatchManager.Instance.mapLayerMask);
		if (hit.collider != MatchManager.Instance.floorCollider && hit.collider != MatchManager.Instance.riverBedCollider && hit.collider != MatchManager.Instance.structureBaseCollider) { return -1; }

		return hit.point.y;
	}

	public static float Random() {
		RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
		byte[] bytes = new byte[8];
		provider.GetBytes(bytes);
		provider.Dispose();
		// ReSharper disable once PossibleLossOfFraction
		return BitConverter.ToUInt64(bytes, 0) / (1 << 11) / (float) (1UL << 53);
	}

	public static Value GetFromDictionary<Key, Value>(Key key, Dictionary<Key, Value> dictionary) {
		dictionary.TryGetValue(key, out Value result);
		return result;
	}

	public static Vector3 StartingPositionForTeam(Team team) {
		switch (team) {
			case Team.Red:
				return new Vector3(300, 10, 300);
			case Team.Blue:
				return new Vector3(-300, 10, -300);
			case Team.Neutral:
				return new Vector3(0, 10, 0);
			default:
				return new Vector3(0, 10, 0);
		}
	}
}