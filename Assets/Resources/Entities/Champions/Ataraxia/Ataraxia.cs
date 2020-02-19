
﻿/*using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Ataraxia : Champion
{
    private int itemCount;
    void Start()
    {
        foreach (Champion champion in MatchManager.Instance.champions)
        {
            //TODO
        }
        this.SetupChampion(80,4,0,0,775,75,4,.05,450,25,2,.15,0,0,0,0,0,0,0,0,0,0,.8,.05,40,0,0,0,0,0,0,0,30,0,6,this.GetComponent<Animator>(),"Ataraxia",
            UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Ataraxia/Icon"),
            new Ability(0,this,"A King's Natural Rights","Ataraxia gains bonus gold and gains stat bonuses at certain amounts of completed items.",
                0,UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Ataraxia/PassiveIcon"),
                () =>
                {
                    foreach (var item in this.items)
                    {
                        if (item.completed)
                        {
                            itemCount++;
                        }
                    }
                }),
            new Ability(4,this,"King's Strike",
                "Ataraxia fires weapons from his vault in a straight line, dealing damage to the first enemy hit. If King's Shot hits an enemy, it will reduce it's cooldown.",
                35,UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Ataraxia/PrimaryIcon"),
                () =>
                {
                    AbilityObject abilityObject = Ability.CreateAbilityObject(
                        "Entities/Champions/TestChamp/SecondaryModel", true, true, false, false, this,
                        this.transform.position,
                        Tools.PositionOnMapAt(this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).point),
                        null, 180, 95, 10);
                    abilityObject.collisionAction = () =>
                    {
                        Ability.DealDamage(false, this, abilityObject.collidedEntity, Ability.DamageType.Physical,
                            80 + (this.damage.CurrentValue * 1.2), 0);
                        this.primaryAbility.CurrentCooldown -= this.primaryAbility.BaseCooldown * .4;
                    };
                    
                }),
            new Ability(11,this,"King's Subjugation","Ataraxia fires a chain from his vault in a straight line, dealing damage and"), 


        );
    }

}
*/