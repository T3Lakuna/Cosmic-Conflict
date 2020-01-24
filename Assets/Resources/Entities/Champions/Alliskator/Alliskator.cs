using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class Alliskator : Champion
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        this.SetupChampion(89, 3.5, 0, 0, 1100, 105, 6.5, .75, 655, 22, 6, .5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .8, .04,
            30, 0, 0, 0, 0, 0, 0, 0, 10, 0, 7, this.GetComponent<Animator>(), "Alliskator",
            UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Alliskator/Icon"),
            new Ability(10, this, "IDK slash and mash",
                "after enough time Alliskator attacks dealing double damage and gaining move speed", 0, null,
                () => { }),
            new Ability(8, this, "Heavy Strike", "Alliskator attack with massive force dealing more damage", 45,
                UnityEngine.Resources.Load<UnityEngine.Sprite>(null),
                () =>
                {
                    this.basicAttackActions.Add((hi) =>
                    {
                        Ability.DealDamage(true, this, hi, Ability.DamageType.Physical, this.damage.CurrentValue + 100, 50);
                        this.basicAttackActions.Clear();

                    });
                  
                }),
            new Ability(10, this, "Smack Smack Attack",
                "Alliskator whales on an enemy stunning them after the second attack", 40, null, () =>
                {
                    this.basicAttackActions.Add((poop) =>
                    {
                        Ability.DealDamage(true,this,poop, Ability.DamageType.Physical, this.damage.CurrentValue + 50, 0);
                        this.basicAttackActions.Add((fart) =>
                        {
                            if (basicAttackTarget == fart )
                            { 
                                Ability.DealDamage(true,this,poop, Ability.DamageType.Physical, this.damage.CurrentValue + 50, 0);
                                basicAttackTarget.currentStatusEffects.Add(new StatusEffect(StatusEffect.StatusEffectType.immobilize,1,1,false,false,basicAttackTarget));
                            }
                        });
                    });
                }),
            new Ability(12, this, "Eye on the Prey",
                "Alliskater looks with such intet he slows an enemy and causing his next auto attack agaist that target to stun them",
                55, null,
                () =>
                {
                    Collider targetCollider =
                        this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider;
                    if (!targetCollider)
                    {
                        this.resource += 45;
                        this.primaryAbility.CurrentCooldown -= 8;
                        return;
                    } // Return mana cost when not casting! (No target.)

                    Entity targetEntity = targetCollider.GetComponent<Entity>();
                    if (!targetEntity)
                    {
                        this.resource += 45;
                        this.primaryAbility.CurrentCooldown -= 8;
                        return;
                    } // Return mana cost when not casting! (No target entity.)

                    if (targetEntity.team == this.team)
                    {
                        this.resource += 45;
                        this.primaryAbility.CurrentCooldown -= 8;
                        return;
                    } // Return mana cost when not casting! (Targeted ally.)

                    if (Vector3.Distance(targetEntity.transform.position, this.transform.position) > 20)
                    {
                        this.resource += 50;
                        this.primaryAbility.CurrentCooldown -= 8;
                        return;
                    } // Return mana cost when not casting! (Out of range.)

                    targetEntity.speed.PercentageBonusValue = -.25;
                    this.StartCoroutine(this.RemoveSlow(targetEntity));
                    if (basicAttackTarget == targetEntity)
                    {
                        this.basicAttackActions.Add((stunn) =>
                        {
                            Ability.DealDamage(true,this,stunn,Ability.DamageType.Physical, this.damage.CurrentValue / 3 + 50,0);
                            targetEntity.currentStatusEffects.Add(new StatusEffect(StatusEffect.StatusEffectType.immobilize,1,1,false,false,targetEntity));
                            this.basicAttackActions.Clear();
                        });
                    }
                    else
                    {
                        this.basicAttackActions.Add((nostunn) =>
                        {
                            Ability.DealDamage(true, this, nostunn, Ability.DamageType.Physical, 50, 0);
                            this.basicAttackActions.Clear();
                        });
                    }
               


                }),
            new Ability(100, this, "BIG BOYS", "OH LAWD HE COMING", 100,
                UnityEngine.Resources.Load<UnityEngine.Sprite>(null), () =>
                {
                    this.entityHeight += 3;
                    this.speed.BonusValue += 20;
                    this.vitality.BonusValue += this.vitality.CurrentValue/2 + 300;
                    Ability.Heal(this,Ability.HealthType.Health,1,this.vitality.CurrentValue/2 + 300,0);
                    this.StartCoroutine(this.removeBigBoi());
                })
        );

    }

    private IEnumerator removeBigBoi()
    {
        yield return new WaitForSeconds(8);
        this.entityHeight -= 3;
        this.speed.BonusValue -= 20;
        this.vitality.BonusValue -= this.vitality.CurrentValue/2 + 300;

        
    }

    private IEnumerator RemoveSlow(Entity targetEntity)
    {
        yield return new WaitForSeconds(1);
        targetEntity.speed.PercentageBonusValue = .25;
    }

    


}




