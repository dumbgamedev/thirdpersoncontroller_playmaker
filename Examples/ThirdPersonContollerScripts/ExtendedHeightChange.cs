using UnityEngine;
using System.Collections.Generic;
using Opsive.ThirdPersonController.Abilities;


public class ExtendedHeightChange : HeightChange
{
	
	public string StatePrefix { get { return m_StatePrefix; } set { m_StatePrefix = value; } }
	public string StartState { get { return m_StartState; } set { m_StartState = value; } }
	public string IdleState { get { return m_IdleState; } set { m_IdleState = value; } }
	public string MovementState { get { return m_MovementState; } set { m_MovementState = value; } }
	public string StopState { get { return m_StopState; } set { m_StopState = value; } }
		
}
