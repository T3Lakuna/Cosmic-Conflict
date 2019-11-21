using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;
	public List<Player> players;

	private void Awake() {
		if (GameManager.Instance == null) {
			GameManager.Instance = this;
			Object.DontDestroyOnLoad(this);
		} else if (GameManager.Instance != this) {
			Object.Destroy(this);
		}
	}
}
