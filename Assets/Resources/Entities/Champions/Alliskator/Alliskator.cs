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
            30, 0, 0, 0, 0, 0, 0, 0, 10, 0, 6, this.GetComponent<Animator>(), "Alliskator",
            UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Alliskator/Icon"),
            new Ability(10, this, "IDK slash and mash",
                "after enough time Alliskator attacks dealing double damage and gaining move speed", 0, null,
                () => { }),
            new Ability(8, this, "Heavy Strike", "Alliskator attack with massive force dealing more damage", 45,
                UnityEngine.Resources.Load<UnityEngine.Sprite>(null),
                () =>
                {
                    this.basicAttackActions.Add((Slam) =>
                    {
                        Ability.DealDamage(true, this, basicAttackTarget.basicAttackTarget,
                            Ability.DamageType.Physical, 100, 0);
                    });
                }),
            new Ability(10, this, "Smack Smack Attack",
                "Alliskator whales on an enemy stunning them after the second attack", 40, null, () => { }),
            new Ability(12, this, "Eye on the Prey",
                "Alliskater looks with such intet he slows an enemy and causing his next auto attack agaist that target to stun them",
                55, null,
                () => { }),
            new Ability(100, this, "BIG BOYS", "OH LAWD HE COMING", 100,
                UnityEngine.Resources.Load<UnityEngine.Sprite>(null), () =>
                {
                    this.speed.BonusValue += 30;
                    this.vitality.BonusValue += 300;
                    this.StartCoroutine(this.removeBigBoi());
                })
        );

    }

    private IEnumerator removeBigBoi()
    {
        yield return new WaitForSeconds(8);
        this.speed.BonusValue -= 30;
        this.vitality.BonusValue -= 300;

        ;
    }


}




// Update is called once per frame
