using UnityEngine;
using System.Collections.Generic;
using Opsive.ThirdPersonController.Abilities;


public class ExtendedJump : Jump
{
	public float Force { get { return m_Force; } set { m_Force = value; } }
	public float DoubleJumpForce { get { return m_DoubleJumpForce; } set { m_DoubleJumpForce = value; } }
		
}
