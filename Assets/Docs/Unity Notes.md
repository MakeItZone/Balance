# Unity Notes

## Movement that Doesn't Go Through Walls

*TL;DR - don't use `transform.Translate` or even `rigidbody.MovePosition()`! Use `rigidbody.AddForce()` only!*

Super simple, but will be buggy: `transfrom.translate`. Very easy to push through walls, fall through floors. Used in a lot of samples, as it's really obvious what's going on. Also all movement directions are given in the game objects *local space*, so you can avoid discussing world vs local coordinates.

Slightly less simple, slightly less buggy: `rigidbody.Moveposition()`. Can tweak settings highlighted below to make it so that you can "push and peak" through walls, but you will be pushed back after a frame or two and not pass through the wall. Does require understanding world vs local coordinates.

Vocabulary: 
* static collider : just has a `collider` component. Will interact with rigidbodies; won't be moved by the physics sim.
* dynamic collider : has a `rigidbody` component. Will interact with rigidbodies and colliders. Will be affected by physics sim *unless* `isKinematic` is set (meaning movement is fully under script control.)

See [this page](https://docs.unity3d.com/Manual/CollidersOverview.html) for details and a handy summary table.

### Common Settings/Tweaks

* on player characters `rigidbody`: 
    * set `Collision Detection` to continuous. Slight performance penalty, but checks the physics more often, less chance to pop through objects
    * set `interpolation` to `interpolate`, especially if you're using `rigidbody.AddForces`. You'll want to put the `forces` into `fixedUpdate`, which get's called at a different rate to `update`. If this is not set, you'll probably find your player reacting jerkily to inputs. 
* `edit menu -> project settings`: 
  * `Physics -> Default Max Depenetration Velocity` - increase (not needed if using rb forces)
  * `Time -> Fixed Timestep` - fixed frame rate for the physics sim. Defaults to 50fps. More than 75fps (0.01333) tends to make thing twitchy and bad. (Not needed if using rb forces)
  * `Input Manager`, or custom input settings if using the new input manager - may want to tweak the `acceleration`, `snap to` etc for how digital inputs (wasd) are translated to analog axis. OR don't use the axis and just check for keys being pressed.
  * explore the other physics and input settings to tweak how things perform

### References
* [really good intro to using RB physics to not go through walls](https://answers.unity.com/questions/1788697/how-to-fix-my-player-from-phasing-through-walls.html) - note, mentions `rb.MovePosition` but in my limited experience, that didn't work reliably
* [more detailed example using rb to move](https://answers.unity.com/questions/1743970/make-player-not-go-through-walls.html) - note, it's set up to move character aligned to world coordinates. Doesn't work with mouse look type setups. For that you just need to add something like `transform.TransformDirection(moveInput) * moveSpeed` before using the vector with the RB forces (which are in world space)
* [explaining the different rb force modes](https://answers.unity.com/questions/789917/difference-and-uses-of-rigidbody-force-modes.html)
* [stopping a rigid body quickly - set the velocity and rotational velocity](https://answers.unity.com/questions/662811/rigidbody-how-to-stop-it-quickly.html)
* [a thread discussing ways to try and stop going through walls](https://forum.unity.com/threads/what-are-the-necessary-settings-to-prevent-objects-passing-through-each-other-at-high-speeds.384519/)