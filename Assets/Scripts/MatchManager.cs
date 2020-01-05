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

	private void Awake() {
		if (!MatchManager.Instance) { MatchManager.Instance = this; } else if (MatchManager.Instance != this) { UnityEngine.Object.Destroy(this); }
	}

	private void Start() {
		this.champions = new List<Champion>();
		this.matchStartTime = DateTime.Now;
	}

	private void Update() {
		if (!this.localPlayer.champion) { return; }

		this.championIcon.sprite = this.localPlayer.champion.icon;

		this.kdaText.text = this.localPlayer.champion.kills + " / " + this.localPlayer.champion.deaths + " / " + this.localPlayer.champion.deaths;

		int redTeamKills = 0;
		int blueTeamKills = 0;
		foreach (Champion champion in this.champions) {
			if (champion.team == Entity.Team.Blue) {
				blueTeamKills += champion.kills;
			} else if (champion.team == Entity.Team.Red) {
				redTeamKills += champion.kills;
			}
		}
		this.blueTeamKillsText.text = "" + blueTeamKills;
		this.redTeamKillsText.text = "" + redTeamKills;

		TimeSpan matchTime = DateTime.Now - this.matchStartTime;
		this.matchTimerText.text = (int) matchTime.TotalMinutes + ":" + (int) (matchTime.TotalSeconds % 60);

		if (this.localPlayer.champion.passiveAbility.CurrentCooldown > 0) {
			this.passiveIcon.sprite = this.cooldownSprite;
		} else {
			this.passiveIcon.sprite = this.localPlayer.champion.passiveAbility.Icon;
		}

		if (this.localPlayer.champion.primaryAbility.CurrentCooldown > 0) {
			this.primaryIcon.sprite = this.cooldownSprite;
		} else {
			this.primaryIcon.sprite = this.localPlayer.champion.primaryAbility.Icon;
		}

		if (this.localPlayer.champion.secondaryAbility.CurrentCooldown > 0) {
			this.secondaryIcon.sprite = this.cooldownSprite;
		} else {
			this.secondaryIcon.sprite = this.localPlayer.champion.secondaryAbility.Icon;
		}

		if (this.localPlayer.champion.tertiaryAbility.CurrentCooldown > 0) {
			this.tertiaryIcon.sprite = this.cooldownSprite;
		} else {
			this.tertiaryIcon.sprite = this.localPlayer.champion.tertiaryAbility.Icon;
		}

		if (this.localPlayer.champion.ultimateAbility.CurrentCooldown > 0) {
			this.ultimateIcon.sprite = this.cooldownSprite;
		} else {
			this.ultimateIcon.sprite = this.localPlayer.champion.ultimateAbility.Icon;
		}

		this.currencyText.text = "" + this.localPlayer.champion.currency;

		if (this.localPlayer.champion.trinket == null) {
			this.trinketIcon.sprite = this.missingSprite;
		} else {
			this.trinketIcon.sprite = this.localPlayer.champion.trinket.icon;
		}

		Item[] items = this.localPlayer.champion.items.ToArray();
		if (items.Length > 0) {
			this.firstItemIcon.sprite = items[0].icon;
		}
		if (items.Length > 1) {
			this.secondItemIcon.sprite = items[1].icon;
		}
		if (items.Length > 2) {
			this.thirdItemIcon.sprite = items[2].icon;
		}
		if (items.Length > 3) {
			this.fourthItemIcon.sprite = items[3].icon;
		}
		if (items.Length > 4) {
			this.fifthItemIcon.sprite = items[4].icon;
		}
		if (items.Length > 5) {
			this.sixthItemIcon.sprite = items[5].icon;
		}
	}
}