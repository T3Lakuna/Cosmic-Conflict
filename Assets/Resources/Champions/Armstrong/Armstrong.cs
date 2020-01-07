using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armstrong : Champion
{
    // Start is called before the first frame update
    void Start()
    {
      base.Start();

      this.SetupChampion(62, 3.8, 0, 0, 539, 70, 4, 0.6, 300, 25, 7.4, 0.5, 26, 2.5, 31, 1, 0, 0, 0, 0, 0, 0, 0.7, 0, 10, .1, 0, 0, 0, 0, 0, 0, 30, 0, "Captain Armstrong", Resources.Load<Sprite>("Champions/Armstrong/Temp Icon"),
        new Ability(0, this, "TestPassive", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Champions/Armstrong/Passive"), () => {
          // Used with primary to easily test leveling up.


          }
        }),
        new Ability(0, this, "TestPrimary", "Test.", 0, UnityEngine.Resources.Load<Sprite>("Champions/Armstrong/Q"), () => {
          // Can be used with passive to test leveling up, or to test items.

          //AOE AA
        }),
        new Ability(3, this, "TestSecondary", "Test.", 100, UnityEngine.Resources.Load<Sprite>("Champions/Armstrong/W"), () => {
          // Example skillshot ability.

          AbilityObject abilityObject = Ability.CreateAbilityObject("Champions/TestChamp/SecondaryModel", true, false, false, this, this.transform.position, Tools.PositionOnMapAt(this.player.RaycastOnLayer(MatchManager.Instance.mapLayerMask).point, this.entityRenderer.bounds.size.y), null, 30, 30, 3);
          abilityObject.collisionAction = () => { Ability.DealDamage(abilityObject.collidedEntity, Ability.DamageType.True, this.damage.CurrentValue * 0.5, 0); };
        }),
        new Ability(0, this, "TestTertiary", "Test.", 50, UnityEngine.Resources.Load<Sprite>("Champions/Armstrong/E"), () => {
          // Example two-part ability (adding shield, then removing it later).

          this.magicalShield += this.magic.CurrentValue * 10;
          this.physicalShield += this.damage.CurrentValue * 10;
          this.shield += this.magic.CurrentValue * 5 + this.damage.CurrentValue * 5;
          this.StartCoroutine(this.TertiaryRemoveShield());
          this.SetupComplexEntity.critBase = 15;
        }),
        new Ability(0, this, "TestUltimate", "Test.", 50, UnityEngine.Resources.Load<Sprite>("Champions/Armstrong/R"), () => {
          // Example point-click area-of-effect ability.

          Collider targetCollider = this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider;
          if (!targetCollider) { this.resource += 50; return; } // Return mana cost when not casting! (No target.)
          Entity targetEntity = targetCollider.GetComponent<Entity>();
          if (!targetEntity) { this.resource += 50; return; } // Return mana cost when not casting! (No target entity.)
          if (targetEntity.team == this.team) { this.resource += 50; return; } // Return mana cost when not casting! (Targeted ally.)
          if (Vector3.Distance(targetEntity.transform.position, this.transform.position) > 10) { this.resource += 50; return; } // Return mana cost when not casting! (Out of range.)
          AbilityObject abilityObject = Ability.CreateAbilityObject("Champions/TestChamp/TertiaryModel", true, false, true, this, this.transform.position, targetEntity.transform.position, targetEntity, 10, 10, 10);
          Ability aoeAbility = new Ability(0, this, "TestUltimate AoE", "Test.", 0, null, () => { });
          aoeAbility.Action = () => { Ability.DealDamage(aoeAbility.target, Ability.DamageType.Magical, 0, 15); };
          abilityObject.collisionAction = () => { Ability.DoInArea(abilityObject.collidedEntity.transform.position, 10, false, aoeAbility); };
        })
      );
    }

    // Update is called once per frame
    void Update()
    {

    }
}
