// (c) Eric Vander Wal, 2017 All rights reserved.
// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using Opsive.ThirdPersonController;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Third Person Controller Action")]
	[Tooltip("Set Height properties using the extended height script.")]

	public class  setHeightProperties : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgentBridge))]
		[Tooltip("NavMeshAgentBridge from Thirdperson Controller is required for this action.")]
		public FsmOwnerDefault gameObject;

		public FsmString statePrefix;
		public FsmString startState;
		public FsmString idleState;
		public FsmString movementState;
		public FsmString stopState;


		[Tooltip("Check this box to preform this action every frame.")]
		public FsmBool everyFrame;

		ExtendedHeightChange heightScript;

		public override void Reset()
		{

			gameObject = null;
			statePrefix = null;
			startState = null;
			idleState = null;
			movementState = null;
			stopState = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			heightScript = go.GetComponent<ExtendedHeightChange>();

			if (!everyFrame.Value)
			{
				DoScript();
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				DoScript();
			}
		}

		void DoScript()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			heightScript.MovementState = movementState.Value;
			heightScript.StatePrefix = statePrefix.Value;
			heightScript.IdleState = idleState.Value;
			heightScript.StartState = startState.Value;
			heightScript.StopState = stopState.Value;
		}

	}
}