using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {
	public const int MapBaseX = 256;
	public const int MapBaseY = 16;
	public const int MapBaseZ = 256;

	public static MatchManager Instance;
	private List<Champion> _champions;
	public Player localPlayer;
	public GameObject mapObject;

	private void Awake() {
		if (!MatchManager.Instance) { MatchManager.Instance = this; } else if (MatchManager.Instance != this) { Object.Destroy(this); }
	}

	private void Start() { this._champions = new List<Champion>(); }
}