// (c) Eric Vander Wal, 2017 All rights reserved.
// Custom Action by DumbGameDev
// www.dumbgamedev.com

using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace Opsive.ThirdPersonController.ThirdParty.PlayMaker
{
	[ActionCategory("Third Person Controller Inventory")]
	[Tooltip("Get an item gameobject by ID.")]

	public class  getGameobjectItemByID : FsmStateAction
	{
		
		[RequiredField]
		[CheckForComponent(typeof(Inventory))]
		[Tooltip("Select the current character setup with the third person controller.")]
		public FsmOwnerDefault gameObject;
		
		public FsmInt itemID;

		[Tooltip("Check this box to preform this action every frame.")]
		public FsmBool everyFrame;
		
		public FsmGameObject itemGameobject;

		Inventory invScript;

		public override void Reset()
		{

			itemGameobject = null;
			itemID = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			invScript = go.GetComponent<Inventory>();

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
			
			itemGameobject.Value = invScript.SharedMethod_GameObjectWithItemID(itemID.Value);
		}

	}
}