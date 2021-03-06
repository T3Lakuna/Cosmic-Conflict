If your champion isn't working in the game, or if you need help finding something, make sure to refer back to this document.

Issues in the inspector:
- If your character or ability object is falling into the floor, make sure that the GameObject layer is set to "Entity" or "Ability," respectively.
- If your character or ability object is having issues climbing inclines, walking into things, or shaking, make sure that the Rigidbody component has all six boxes marked under "Constraints," and that it is set to no gravity, not kinematic.
- If your character or ability object isn't being displayed or isn't interacting with anything, ensure that the GameObject has some sort of collider attached. Box colliders are easiest. Ensure that the hitbox is similar to the shape of the champion.
- If your character object isn't doing anything, make sure that you attached the champion's script to the GameObject.
- If your ability object is interacting physically with things when it shouldn't be, ensure that its collider is set to be a trigger.
- If your character is not behaving in multiplayer, make sure that the GameObject has a Photon View component that syncs the transform and character script. Also make sure that all ability models have a Photon View component.

Issues in scripts:
- If your champion isn't doing tick actions (i.e. gaining gold), ensure that you called "base.Start()" at the beginning of your champion's Start() method.
- If your champion isn't doing updates (i.e. regenerating), ensure that you called "base.Update()" at the beginning of your champion's Update() method IF IT EXISTS.

Remember that most things you need for writing abilities easily can be found as static methods in the Ability class.
When writing a collision action for an AbilityObject, the collidedEntity variable can be used to interact with the collided entity. The AbilityObject should be created on a line prior to the definition of the collision action so that this variable can be accessed.
The only thing most champions will need is a call to "this.SetupChampion()" in their Start() method.
