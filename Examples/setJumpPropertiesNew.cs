// (c) Eric Vander Wal, 2017 All rights reserved.
// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using Opsive.ThirdPersonController;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Third Person Controller Action")]
	[Tooltip("Set Jump properties using the extended jump script.")]

	public class  setJumpPropertiesNew : FsmStateAction
	{

		// This is your gameobject with the third person controller on it
		[RequiredField]
		[Tooltip("Thirdperson Controller is required for this action.")]
		public FsmOwnerDefault gameObject;

		// Playmaker Variable
		public FsmFloat jumpForce;
		public FsmFloat doubleJumpForce;

		// Playmaker Bool to check if this ability is active
		public FsmBool jumpActive;

		// Playmaker bool to toggle everyframe off or on
		[Tooltip("Check this box to preform this action every frame.")]
		public FsmBool everyFrame;

		// Name the script that we want to access
		ExtendedJump jumpScript;

		// Reset values
		// Bools should be false and Floats, Ints, or Gameobjects should be null.
		public override void Reset()
		{

			gameObject = null;
			doubleJumpForce = null;
			jumpForce = null;
			jumpActive = false;
			everyFrame = false;
		}


		// This fires when the script first starts
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			jumpScript = go.GetComponent<ExtendedJump>();

			if (!everyFrame.Value)
			{
				DoScript();
				Finish();
			}

		}


		// Do this every frame.
		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				DoScript();
			}
		}


		// Do this when this script is running correctly
		void DoScript()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			// Put your method or variables here.

			jumpScript.Force = jumpForce.Value;
			jumpScript.DoubleJumpForce = doubleJumpForce.Value;
			
			// Checks if jump is active. Can remove.
			jumpActive.Value = jumpScript.IsActive;


		}

	}
}