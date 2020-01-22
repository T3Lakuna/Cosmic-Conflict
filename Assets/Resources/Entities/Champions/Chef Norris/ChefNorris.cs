using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefNorris : Champion
{
    private bool toBuff;

    private new void Start()
    {
        this.toBuff = false;
        
        this.SetupChampion(60, 3, 0, 0, 750, 60, 3, .05, 400, 20, 2, .1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .7, .05, 35, 0,
            0, 0, 0, 0, 0, 0, 25, 0, 5, this.GetComponent<Animator>(), "Chef Norris",
            UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/Icon"),
            new Ability(0, this, "Good Eats", "Chef Norris gain speed for 4 seconds when Chef Norris casts an ability",
                0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/PassiveIcon"),
                () =>
                {
                    if (toBuff)
                    {
                        this.speed.PercentageBonusValue += .2;
                        this.StartCoroutine(removePassiveBuff());
                    }
                }),
            new Ability(10, this, "Spicy Chicken Teriyaki",
                "Chef Norris feeds Spicy Chicken Teriyaki to an ally. Ally gains fervor and speed.", 70,
                UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/PrimaryIcon"),
                () =>
                {
                    Collider targetCollider =
                        this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider;
                    if (!targetCollider)
                    {
                        this.resource += 70;
                        return;
                    }

                    Entity targetEntity = targetCollider.GetComponent<Entity>();
                    if (!targetEntity)
                    {
                        this.resource += 70;
                        return;
                    }

                    if (targetEntity.team != this.team)
                    {
                        this.resource += 70;
                        return;
                    }
                    if (Vector3.Distance(targetEntity.transform.position, this.transform.position) > 50)
                    {
                        this.resource += 70;
                        return;
                    }
                    targetEntity.fervor.PercentageBonusValue += .2;
                    targetEntity.speed.PercentageBonusValue += .1;
                    this.StartCoroutine(RemoveBuff("Spicy Chicken Teriyaki", targetEntity));
                    toBuff = true;
                }),
            new Ability(8, this, "Steak And Eggs",
                "Chef Norris feeds Steak and Eggs to an ally. Ally gains a shield and loses speed.", 60,
                UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/SecondaryIcon"),
                () =>
                {
                    Collider targetCollider =
                        this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider;
                    if (!targetCollider)
                    {
                        this.resource += 60;
                        return;
                    }

                    Entity targetEntity = targetCollider.GetComponent<Entity>();
                    if (!targetEntity)
                    {
                        this.resource += 60;
                        return;
                    }
                    if (targetEntity.team != this.team)
                    {
                        this.resource += 70;
                        return;
                    }
                    if (Vector3.Distance(targetEntity.transform.position, this.transform.position) > 50)
                    {
                        this.resource += 60;
                        return;
                    }

                    Ability.Heal(targetEntity, Ability.HealthType.Shield, 0, 0, .1);
                    targetEntity.speed.PercentageBonusValue -= .1;
                    this.StartCoroutine(RemoveBuff("Steak And Eggs", targetEntity));
                    toBuff = true;
                }),
            new Ability(12, this, "Double Decker Cheeseburger",
                "Chef Norris feeds a Double Decker Cheeseburger to an ally. Ally gains Adaptive and fervor.", 100,
                UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/TertiaryIcon"),
                () =>
                {
                    Collider targetCollider =
                        this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider;
                    if (!targetCollider)
                    {
                        this.resource += 100;
                        return;
                    }

                    Entity targetEntity = targetCollider.GetComponent<Entity>();
                    if (!targetEntity)
                    {
                        this.resource += 100;
                        return;
                    }
                    if (targetEntity.team != this.team)
                    {
                        this.resource += 70;
                        return;
                    }
                    if (Vector3.Distance(targetEntity.transform.position, this.transform.position) > 50)
                    {
                        this.resource += 100;
                        return;
                    }

                    if (targetEntity.damage.CurrentValue > targetEntity.magic.CurrentValue)
                    {
                        targetEntity.damage.PercentageBonusValue += .15;
                        this.StartCoroutine(RemoveBuff("DCCD", targetEntity));
                    }

                    if (targetEntity.magic.CurrentValue > targetEntity.damage.CurrentValue)
                    {
                        targetEntity.magic.PercentageBonusValue += .2;
                        this.StartCoroutine(RemoveBuff("DCCM", targetEntity));
                    }

                    targetEntity.fervor.PercentageBonusValue += .1;
                    toBuff = true;
                }),
            new Ability(100, this, "All You Can Eat Buffet",
                "Chef Norris ignites with passion and serves a full course meal to an Ally, empowering them with Adaptive, fervor, speed, and regeneration",
                120, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Chef Norris/UltimateIcon"),
                () =>
                {
                    Collider targetCollider =
                        this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider;
                    if (!targetCollider)
                    {
                        this.resource += 120;
                        return;
                    }

                    Entity targetEntity = targetCollider.GetComponent<Entity>();
                    if (!targetEntity)
                    {
                        this.resource += 120;
                        return;
                    }
                    if (targetEntity.team != this.team)
                    {
                        this.resource += 70;
                        return;
                    }
                    if (Vector3.Distance(targetEntity.transform.position, this.transform.position) > 50)
                    {
                        this.resource += 120;
                        return;
                    }

                    if (targetEntity.damage.CurrentValue > targetEntity.magic.CurrentValue)
                    {
                        targetEntity.damage.PercentageBonusValue += .25;
                        this.StartCoroutine(RemoveBuff("AYCABD", targetEntity));
                    }

                    if (targetEntity.magic.CurrentValue > targetEntity.damage.CurrentValue)
                    {
                        targetEntity.magic.CurrentValue += .3;
                        this.StartCoroutine(RemoveBuff("AYCABM", targetEntity));
                    }

                    targetEntity.fervor.PercentageBonusValue += .2;
                    targetEntity.speed.PercentageBonusValue += .2;
                    targetEntity.regeneration.PercentageBonusValue += 1;
                })
        );
    }

    private IEnumerator RemoveBuff(String decider, Entity target)
    {
        if (decider.Equals("Spicy Chicken Teriyaki"))
        {
            yield return new WaitForSeconds(6);
            target.fervor.PercentageBonusValue -= .2;
            target.speed.PercentageBonusValue -= .1;
        }

        if (decider.Equals("Steak And Eggs"))
        {
            yield return new WaitForSeconds(3);
            target.speed.PercentageBonusValue += .1;
        }

        if (decider.Equals("DCCD"))
        {
            yield return new WaitForSeconds(5);
            target.damage.PercentageBonusValue -= .15;
            target.fervor.PercentageBonusValue -= .1;
        }

        if (decider.Equals("DCCM"))
        {
            yield return new WaitForSeconds(5);
            target.magic.PercentageBonusValue -= .2;
            target.fervor.PercentageBonusValue -= .1;
        }

        if (decider.Equals("AYCABD"))
        {
            yield return new WaitForSeconds(10);
            target.damage.PercentageBonusValue -= .25;
            target.fervor.PercentageBonusValue -= .2;
            target.speed.PercentageBonusValue -= .2;
            target.regeneration.PercentageBonusValue -= 1;
        }

        if (decider.Equals("AYCABM"))
        {
            yield return new WaitForSeconds(10);
            target.magic.PercentageBonusValue -= .25;
            target.fervor.PercentageBonusValue -= .2;
            target.speed.PercentageBonusValue -= .2;
            target.regeneration.PercentageBonusValue -= 1;
        }
    }

    private IEnumerator removePassiveBuff()
    {
        toBuff = false;
        yield return new WaitForSeconds(4);
        this.speed.PercentageBonusValue -= .2;
    }
}