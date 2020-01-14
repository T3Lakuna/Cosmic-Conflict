using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alliskator : Champion
{
    // Start is called before the first frame update
  private new void Start()
    {/*
        base.Start();
        this.SetupChampion(89,3.5,0,0,1100,105,6.5,.75,655,22,6,.5,0,0,0,0,0,0,0,0,0,0,.8,.04,30,0,0,0,0,0,0,0,10,0,6,this.GetComponent<Animator>(),"Alliskator",UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Alliskator/Icon"),
        new Ability(10,this,"IDK slash and mash","after enough time Alliskator attacks dealing double damage and gaining move speed",0,UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Alliskator/PassiveIcon",() => {

          }),
        new Ability(8,this,"Heavy Strike", "Alliskator attack with massive force dealing more damage",0,UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Alliskator/PrimaryIcon",() => {
          Entity targetEntity = this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider.GetComponent<Entity>();
  			  AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/TertiaryModel",
  				false, true, false, true, this, this.transform.position, targetEntity.transform.position, targetEntity, 10000, 10, 100);
  			  abilityObject.collisionAction = () => {
  				  Ability.DealDamage(false, this, targetEntity, Ability.DamageType.Physical, 175, 0);
  			  };
          }),
        new Ability(),
        new Ability(),
        new Ability();*/
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
    }
}
