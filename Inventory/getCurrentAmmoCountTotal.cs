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
	[Tooltip("Gets the current weapons total ammo count. Can be used for primary, secondary or dual.")]

	public class  getCurrentAmmoCountTotal : FsmStateAction
	{

		public enum ItemType { Primary, Secondary, DualWield }

		[RequiredField]
		[CheckForComponent(typeof(Inventory))]
		[Tooltip("Select the current character setup with the third person controller.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The type of item to determine if it has any ammo")]
		[ObjectType(typeof(ItemType))]
		public FsmEnum itemType;

		public FsmInt ammoCountLoaded;
		public FsmInt ammoCountUnloaded;
		

		[Tooltip("Check this box to preform this action every frame.")]
		public FsmBool everyFrame;

		private int _ammoCountLoaded;
		private int _ammoCountUnloaded;
		

		Inventory invScript;

		public override void Reset()
		{

			ammoCountLoaded = null;
			ammoCountUnloaded = null;
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

			System.Type type = null;
			switch ((ItemType)itemType.Value) {
			case ItemType.Primary:
				type = typeof(PrimaryItemType);
				break;
			case ItemType.Secondary:
				type = typeof(SecondaryItemType);
				break;
			case ItemType.DualWield:
				type = typeof(DualWieldItemType);
				break;
			}

			_ammoCountLoaded = invScript.GetCurrentItemCount(type, true); 
			ammoCountLoaded.Value = _ammoCountLoaded;
			_ammoCountUnloaded = invScript.GetCurrentItemCount(type, false);
			ammoCountUnloaded.Value = _ammoCountUnloaded;
		}

	}
}