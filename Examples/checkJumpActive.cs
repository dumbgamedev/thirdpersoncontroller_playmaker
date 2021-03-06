// (c) Eric Vander Wal, 2017 All rights reserved.
// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using Opsive.ThirdPersonController;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Third Person Controller Action")]
	[Tooltip("Check if Height Change is Active.")]

	public class  checkJumpActive : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgentBridge))]
		[Tooltip("NavMeshAgentBridge from Thirdperson Controller is required for this action.")]
		public FsmOwnerDefault gameObject;

		public FsmBool heightChangeActive;

		[Tooltip("Check this box to preform this action every frame.")]
		public FsmBool everyFrame;

		ExtendedHeightChange pmScript;

		public override void Reset()
		{

			gameObject = null;
			heightChangeActive = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			pmScript = go.GetComponent<ExtendedHeightChange>();

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

			heightChangeActive.Value = pmScript.IsActive;
		}

	}
}