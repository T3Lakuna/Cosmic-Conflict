using Photon.Pun;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviourPun, IPunObservable {
	public enum Lane { Top, Middle, Bottom }

	public static MatchManager Instance;
	[HideInInspector] public List<Champion> champions;
	[HideInInspector] public DateTime matchStartTime;
	public GameObject mapObject;
	public Collider floorCollider;
	public Collider structureBaseCollider;
	public Collider riverBedCollider;
	public Collider wallCollider;
	public TMP_Text kdaText;
	public TMP_Text blueTeamKillsText;
	public TMP_Text redTeamKillsText;
	public TMP_Text matchTimerText;
	public Image passiveIcon;
	public Image primaryIcon;
	public Image secondaryIcon;
	public Image tertiaryIcon;
	public Image ultimateIcon;
	public TMP_Text currencyText;
	public Image championIcon;
	public Image trinketIcon;
	public Image firstItemIcon;
	public Image secondItemIcon;
	public Image thirdItemIcon;
	public Image fourthItemIcon;
	public Image fifthItemIcon;
	public Image sixthItemIcon;
	public Sprite cooldownSprite;
	public Sprite missingSprite;
	public LayerMask mapLayerMask;
	public LayerMask entityLayerMask;
	public LayerMask abilityLayerMask;
	public LayerMask allGameLayerMasks;
	public Inhibitor blueTopInhibitor;
	public Inhibitor blueMiddleInhibitor;
	public Inhibitor blueBottomInhibitor;
	public Inhibitor redTopInhibitor;
	public Inhibitor redMiddleInhibitor;
	public Inhibitor redBottomInhibitor;
	public RuntimeAnimatorController idleAnimation;
	public RuntimeAnimatorController runAnimation;
	public Transform abilityHolder;
	public Material redMaterial;
	public Material blueMaterial;
	public Transform blueMinionHolder;
	public Transform redMinionHolder;
	public Transform blueChampionHolder;
	public Transform redChampionHolder;
	public GameObject minionPrefab;
	public GameObject[] blueTopPath;
	public GameObject[] blueMiddlePath;
	public GameObject[] blueBottomPath;
	public GameObject[] redTopPath;
	public GameObject[] redMiddlePath;
	public GameObject[] redBottomPath;
	private int minionWavesSpawned;

	private void Awake() {
		if (!MatchManager.Instance) { MatchManager.Instance = this; } else if (MatchManager.Instance != this) { UnityEngine.Object.Destroy(this); }
	}

	private void Start() {
		this.champions = new List<Champion>();
		this.matchStartTime = DateTime.Now;
		this.minionWavesSpawned = 0;

		if (PhotonNetwork.IsMasterClient) { this.StartCoroutine(this.SpawnMinionTick()); }
	}

	[PunRPC]
	public static void DestroyRpc(GameObject gameObject) { // For use in Tools only.
		UnityEngine.Object.Destroy(gameObject);
	}

	private System.Collections.IEnumerator SpawnMinionTick() {
		while (true) {
			yield return new WaitForSeconds(28);
			this.minionWavesSpawned++;
			int minionLevel = minionWavesSpawned / 3;

			// Super minions
			if (!this.blueTopInhibitor || !this.blueTopInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Red, this.redTopPath).LevelUp(minionLevel); }
			if (!this.blueMiddleInhibitor || !this.blueMiddleInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Red, this.redMiddlePath).LevelUp(minionLevel); }
			if (!this.blueBottomInhibitor || !this.blueBottomInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Red, this.redBottomPath).LevelUp(minionLevel); }
			if (!this.redTopInhibitor || !this.redTopInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Blue, this.blueTopPath).LevelUp(minionLevel); }
			if (!this.redMiddleInhibitor || !this.redMiddleInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Blue, this.blueMiddlePath).LevelUp(minionLevel); }
			if (!this.redBottomInhibitor || !this.redBottomInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Blue, this.blueBottomPath).LevelUp(minionLevel); }
			yield return new WaitForSeconds(0.25f);

			// Melee minions
			for (int i = 0; i < 3; i++) {
				MeleeMinion.CreateMeleeMinion(Entity.Team.Blue, this.blueTopPath).LevelUp(minionLevel);
				MeleeMinion.CreateMeleeMinion(Entity.Team.Blue, this.blueMiddlePath).LevelUp(minionLevel);
				MeleeMinion.CreateMeleeMinion(Entity.Team.Blue, this.blueBottomPath).LevelUp(minionLevel);
				MeleeMinion.CreateMeleeMinion(Entity.Team.Red, this.redTopPath).LevelUp(minionLevel);
				MeleeMinion.CreateMeleeMinion(Entity.Team.Red, this.redMiddlePath).LevelUp(minionLevel);
				MeleeMinion.CreateMeleeMinion(Entity.Team.Red, this.redBottomPath).LevelUp(minionLevel);
				yield return new WaitForSeconds(0.25f);
			}

			// Ranged minions
			for (int i = 0; i < 3; i++) {
				RangedMinion.CreateRangedMinion(Entity.Team.Blue, this.blueTopPath).LevelUp(minionLevel);
				RangedMinion.CreateRangedMinion(Entity.Team.Blue, this.blueMiddlePath).LevelUp(minionLevel);
				RangedMinion.CreateRangedMinion(Entity.Team.Blue, this.blueBottomPath).LevelUp(minionLevel);
				RangedMinion.CreateRangedMinion(Entity.Team.Red, this.redTopPath).LevelUp(minionLevel);
				RangedMinion.CreateRangedMinion(Entity.Team.Red, this.redMiddlePath).LevelUp(minionLevel);
				RangedMinion.CreateRangedMinion(Entity.Team.Red, this.redBottomPath).LevelUp(minionLevel);
				yield return new WaitForSeconds(0.25f);
			}

			// Cannon minions
			if (this.minionWavesSpawned % 3 == 0) {
				for (int i = 0; i < 3; i++) {
					CannonMinion.CreateCannonMinion(Entity.Team.Blue, this.blueTopPath).LevelUp(minionLevel);
					CannonMinion.CreateCannonMinion(Entity.Team.Blue, this.blueMiddlePath).LevelUp(minionLevel);
					CannonMinion.CreateCannonMinion(Entity.Team.Blue, this.blueBottomPath).LevelUp(minionLevel);
					CannonMinion.CreateCannonMinion(Entity.Team.Red, this.redTopPath).LevelUp(minionLevel);
					CannonMinion.CreateCannonMinion(Entity.Team.Red, this.redMiddlePath).LevelUp(minionLevel);
					CannonMinion.CreateCannonMinion(Entity.Team.Red, this.redBottomPath).LevelUp(minionLevel);
					yield return new WaitForSeconds(0.25f);
				}
			}
		}
	}

	private void Update() {
		if (!Player.localPlayer.champion || Player.localPlayer.champion.passiveAbility == null) { return; }

		this.championIcon.sprite = Player.localPlayer.champion.icon;

		this.kdaText.text = Player.localPlayer.champion.kills + " / " + Player.localPlayer.champion.deaths + " / " + Player.localPlayer.champion.deaths;

		int redTeamKills = 0;
		int blueTeamKills = 0;
		foreach (Champion champion in this.champions) {
			switch (champion.team) {
				case Entity.Team.Blue:
					blueTeamKills += champion.kills;
					break;
				case Entity.Team.Red:
					redTeamKills += champion.kills;
					break;
				case Entity.Team.Neutral:
					// LMAO somebody died to a creep.
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		this.blueTeamKillsText.text = "" + blueTeamKills;
		this.redTeamKillsText.text = "" + redTeamKills;

		TimeSpan matchTime = DateTime.Now - this.matchStartTime;
		this.matchTimerText.text = (int) matchTime.TotalMinutes + ":" + (int) (matchTime.TotalSeconds % 60);
		this.passiveIcon.sprite = Player.localPlayer.champion.passiveAbility.CurrentCooldown > 0 ? this.cooldownSprite : Player.localPlayer.champion.passiveAbility.Icon;
		this.primaryIcon.sprite = Player.localPlayer.champion.primaryAbility.CurrentCooldown > 0 ? this.cooldownSprite : Player.localPlayer.champion.primaryAbility.Icon;
		this.secondaryIcon.sprite = Player.localPlayer.champion.secondaryAbility.CurrentCooldown > 0 ? this.cooldownSprite : Player.localPlayer.champion.secondaryAbility.Icon;
		this.tertiaryIcon.sprite = Player.localPlayer.champion.tertiaryAbility.CurrentCooldown > 0 ? this.cooldownSprite : Player.localPlayer.champion.tertiaryAbility.Icon;
		this.ultimateIcon.sprite = Player.localPlayer.champion.ultimateAbility.CurrentCooldown > 0 ? this.cooldownSprite : Player.localPlayer.champion.ultimateAbility.Icon;
		this.currencyText.text = "" + Player.localPlayer.champion.currency;
		this.trinketIcon.sprite = Player.localPlayer.champion.trinket == null ? this.missingSprite : Player.localPlayer.champion.trinket.icon;

		Item[] items = Player.localPlayer.champion.items.ToArray();
		if (items.Length > 0) { this.firstItemIcon.sprite = items[0].icon; }

		if (items.Length > 1) { this.secondItemIcon.sprite = items[1].icon; }

		if (items.Length > 2) { this.thirdItemIcon.sprite = items[2].icon; }

		if (items.Length > 3) { this.fourthItemIcon.sprite = items[3].icon; }

		if (items.Length > 4) { this.fifthItemIcon.sprite = items[4].icon; }

		if (items.Length > 5) { this.sixthItemIcon.sprite = items[5].icon; }
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }
}