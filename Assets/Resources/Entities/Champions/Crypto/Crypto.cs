<<<<<<< HEAD

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crypto : Champion
{
    // Start is called before the first frame update
    private new void Start()
    {

      this.SetupChampion(70, 2.5, 0, 0, 850, 100, 6, .5, 455, 18, 6, .5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .7, .04, 40, 0,
        0, 0, 0, 0, 0, 0, 10, 0, 4, this.GetComponent<Animator>(), "Crypto",
        UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Icon"),
        new Ability(10, this, "Vulnerability Breach", "Crypto finds the weakness in his enemies when their at their weakest allowing his next attack on the enemy to do bonus damage based missing health", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Passive"),
          () =>
          {

          }),
        new Ability(8, this, "Fire Wall Breaker", "Crypto loads his blade with a virse that will slow the enemy hit and reduce their resistances",45, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Primary"), () =>
          {
            Entity targetEntity = this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider.GetComponent<Entity>();
            AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/TertiaryModel",
              false, true, false, true, this, this.transform.position, targetEntity.transform.position, targetEntity, 10000, 10, 100);
            abilityObject.collisionAction = () =>
            {
              Ability.DealDamage(false,this, targetEntity, Ability.DamageType.Magical, 100.0, 0.0);
              Ability.ApplyStatusEffect(targetEntity,Ability.ApplyStatusEffect());

            };


          }),
        new Ability(20, this, "EMP", "Crypto lobes an EMP exploding on contact dealing damage and slowing and silenceing everyone in the area", 80, UnityEngine.Resources.Load<UnityEngine.Sprite>("ok look im tired and i messed up my leg so ill do the art later"),
          () =>
          {
            
          }),
        new Ability(18, this, "Holo-Trans-Locator","Crypto creates a hologram while going invisable Crypto can recast the ability to swap places with the clone",90, UnityEngine.Resources.Load<UnityEngine.Sprite>("Same thing i said in his w"),
          () =>
          {

          }),
        new Ability(120, this, "Black Out",
          "Crypto unleashes a burst of energy all around him dealing damaging and silenceing everyone inside the area( Also so are we doing the tower disable thingy because i think its a cool consept like we can nerf the damage and silence the tower thing just seems cool thank you for coming to my ted talk)", 100, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/dang"),
          () =>
          {

          })
      );
    }
=======
﻿using UnityEngine;

public class Crypto : Champion {
	// Start is called before the first frame update
	private new void Start() {

		this.SetupChampion(70, 2.5, 0, 0, 850, 100, 6, .5, 455, 18, 6, .5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, .7, .04, 40, 0,
		  0, 0, 0, 0, 0, 0, 10, 0, 4, this.GetComponent<Animator>(), "Crypto",
		  UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Icon"),
		  new Ability(10, this, "Vulnerability Breach", "Crypto finds the weakness in his enemies when their at their weakest allowing his next attack on the enemy to do bonus damage based missing health", 0, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Passive"),
			() => {

			}),
		  new Ability(8, this, "Fire Wall Breaker", "Crypto loads his blade with a virse that will slow the enemy hit and reduce their resistances", 45, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/Primary"), () => {
			  Entity targetEntity = this.player.RaycastOnLayer(MatchManager.Instance.entityLayerMask).collider.GetComponent<Entity>();
			  AbilityObject abilityObject = Ability.CreateAbilityObject("Entities/Champions/TestChamp/TertiaryModel",
				false, true, false, true, this, this.transform.position, targetEntity.transform.position, targetEntity, 10000, 10, 100);
			  abilityObject.collisionAction = () => {
				  Ability.DealDamage(false, this, targetEntity, Ability.DamageType.Magical, 100, 0);
			  };


		  }),
		  new Ability(20, this, "EMP", "Crypto lobes an EMP exploding on contact dealing damage and slowing and silenceing everyone in the area", 80, UnityEngine.Resources.Load<UnityEngine.Sprite>("ok look im tired and i messed up my leg so ill do the art later"),
			() => {

			}),
		  new Ability(18, this, "Holo-Trans-Locator", "Crypto creates a hologram while going invisable Crypto can recast the ability to swap places with the clone", 90, UnityEngine.Resources.Load<UnityEngine.Sprite>("Same thing i said in his w"),
			() => {

			}),
		  new Ability(120, this, "Black Out",
			"Crypto unleashes a burst of energy all around him dealing damaging and silenceing everyone inside the area( Also so are we doing the tower disable thingy because i think its a cool consept like we can nerf the damage and silence the tower thing just seems cool thank you for coming to my ted talk)", 100, UnityEngine.Resources.Load<UnityEngine.Sprite>("Entities/Champions/Crypto/dang"),
			() => {

			})
		);
	}
>>>>>>> cb30b21f38a7f24830dd802121dc6e443c82a334
}

