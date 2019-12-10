using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {
	public static MatchManager Instance;
	[HideInInspector] public List<Champion> champions;
	public Player localPlayer;
	public GameObject mapObject;
	public Collider floorCollider;
	public Collider structureBaseCollider;
	public Collider riverBedCollider;
	public Collider wallCollider;

	private void Awake() {
		if (!MatchManager.Instance) { MatchManager.Instance = this; } else if (MatchManager.Instance != this) { Object.Destroy(this); }
	}

	private void Start() { this.champions = new List<Champion>(); }
}