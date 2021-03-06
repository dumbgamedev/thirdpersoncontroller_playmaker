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
	[Tooltip("Check to see if the character current has a specific item.")]

	public class  checkHasItem : FsmStateAction
	{
		public enum ItemType { Primary, Secondary, DualWield }

		[RequiredField]
		[CheckForComponent(typeof(Inventory))]
		[Tooltip("Select the current character setup with the third person controller.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The type of item to determine the amount")]
		[ObjectType(typeof(ItemType))]
		public FsmEnum itemType;

		[RequiredField]
		[Tooltip("The item to check if the character has.")]
		[ObjectType(typeof(Item))]
		public FsmObject itemToCheck;

		[Tooltip("Event to send if the agent has the item.")]
		public FsmEvent trueEvent;

		[Tooltip("Event to send if the agent does not have the item.")]
		public FsmEvent falseEvent;

		[Tooltip("Store if the agent has the item.")]
		[UIHint(UIHint.Variable)]
		public FsmBool store;

		[Tooltip("Check this box to preform this action every frame.")]
		public FsmBool everyFrame;

		private Item currentItem;
		Inventory invScript;

		public override void Reset()
		{

	//		itemType = ItemType.Primary;
			itemToCheck = null;
			store = false;
			trueEvent = null;
			falseEvent = null;
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

			currentItem = itemToCheck.Value as Item;
			var hasItem = invScript.GetCurrentItem(type).Equals(currentItem);
			store.Value = hasItem;

			Fsm.Event(hasItem ? trueEvent : falseEvent);


		}

	}
}