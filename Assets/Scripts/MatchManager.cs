using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {
	public enum Lane { Top, Middle, Bottom }

	public static MatchManager Instance;
	[HideInInspector] public List<Champion> champions;
	[HideInInspector] public DateTime matchStartTime;
	public Player localPlayer;
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

		this.StartCoroutine(this.SpawnMinionTick());
	}

	private System.Collections.IEnumerator SpawnMinionTick() {
		while (true) {
			yield return new WaitForSeconds(28);
			this.minionWavesSpawned++;
			int minionLevel = minionWavesSpawned / 3;

			// Super minions
			if (!this.blueTopInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Red, this.redTopPath).LevelUp(minionLevel); }
			if (!this.blueMiddleInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Red, this.redMiddlePath).LevelUp(minionLevel); }
			if (!this.blueBottomInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Red, this.redBottomPath).LevelUp(minionLevel); }
			if (!this.redTopInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Blue, this.blueTopPath).LevelUp(minionLevel); }
			if (!this.redMiddleInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Blue, this.blueMiddlePath).LevelUp(minionLevel); }
			if (!this.redBottomInhibitor.isActiveAndEnabled) { SuperMinion.CreateSuperMinion(Entity.Team.Blue, this.blueBottomPath).LevelUp(minionLevel); }
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
		if (!this.localPlayer.champion) { return; }

		this.championIcon.sprite = this.localPlayer.champion.icon;

		this.kdaText.text = this.localPlayer.champion.kills + " / " + this.localPlayer.champion.deaths + " / " + this.localPlayer.champion.deaths;

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
		this.passiveIcon.sprite = this.localPlayer.champion.passiveAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.passiveAbility.Icon;
		this.primaryIcon.sprite = this.localPlayer.champion.primaryAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.primaryAbility.Icon;
		this.secondaryIcon.sprite = this.localPlayer.champion.secondaryAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.secondaryAbility.Icon;
		this.tertiaryIcon.sprite = this.localPlayer.champion.tertiaryAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.tertiaryAbility.Icon;
		this.ultimateIcon.sprite = this.localPlayer.champion.ultimateAbility.CurrentCooldown > 0 ? this.cooldownSprite : this.localPlayer.champion.ultimateAbility.Icon;
		this.currencyText.text = "" + this.localPlayer.champion.currency;
		this.trinketIcon.sprite = this.localPlayer.champion.trinket == null ? this.missingSprite : this.localPlayer.champion.trinket.icon;

		Item[] items = this.localPlayer.champion.items.ToArray();
		if (items.Length > 0) { this.firstItemIcon.sprite = items[0].icon; }

		if (items.Length > 1) { this.secondItemIcon.sprite = items[1].icon; }

		if (items.Length > 2) { this.thirdItemIcon.sprite = items[2].icon; }

		if (items.Length > 3) { this.fourthItemIcon.sprite = items[3].icon; }

		if (items.Length > 4) { this.fifthItemIcon.sprite = items[4].icon; }

		if (items.Length > 5) { this.sixthItemIcon.sprite = items[5].icon; }
	}
}