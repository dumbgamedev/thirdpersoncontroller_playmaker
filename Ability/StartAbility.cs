using UnityEngine;
using Opsive.ThirdPersonController;
using Opsive.ThirdPersonController.Abilities;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

namespace Opsive.ThirdPersonController.ThirdParty.PlayMaker
{
	[Tooltip("Tries to start the ability.")]
	[ActionCategory("Third Person Controller")]
	[HelpUrl("https://www.opsive.com/assets/ThirdPersonController/documentation.php")]
	public class StartAbility : FsmStateAction
	{
		[Tooltip("A reference to the agent. If null it will be retrieved from the current GameObject.")]
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgentBridge))]    
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The name of the ability to start.")]
		[Title("Ability Name")]
		[RequiredField]
		public FsmString abilityType;
		
		[Tooltip("If multiple abilities types are found, the priority index can be used to specify which ability should be started")]
		public FsmInt priorityIndex = -1;
		
		public FsmBool everyFrame;
		
		private GameObject prevGO;
		private NavMeshAgentBridge controller;
		private ControllerHandler controllerHandler;
		private string abilityString;
		private Ability ability;
		
		public override void Reset()
		{
			gameObject= null;
			abilityType = string.Empty;
			priorityIndex = -1;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			
			if (!everyFrame.Value)
			{
				doAbility();
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				doAbility();
			}
		}
		
		
		void doAbility()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			if (go != prevGO) {
				controller = go.GetComponentInParent<NavMeshAgentBridge>();
				controllerHandler = go.GetComponentInParent<ControllerHandler>();
				abilityString = "Opsive.ThirdPersonController.Abilities."+abilityType.Value;
				var abilities = controller.GetComponents(System.Type.GetType(abilityString));
				if (abilities.Length > 1) {
					if (priorityIndex.Value != -1) {
						for (int i = 0; i < abilities.Length; ++i) {
							var localAbility = abilities[i] as Ability;
							if (localAbility.Index == priorityIndex.Value) {
								ability = localAbility;
								break;
							}
						}
					} else {
						ability = abilities[0] as Ability;
					}
				} else if (abilities.Length == 1) {
					ability = abilities[0] as Ability;
				}
				prevGO = go;
			}
			
			if (ability == null) {
				return;
			}
			
			controllerHandler.TryStartAbility(ability);

		}
		
	}
}