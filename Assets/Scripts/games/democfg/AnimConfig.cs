/*
 *  Leandro M. da Costa - 03/2017
 *  
 *  Slots Unity Projec 
 *  
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimConfig
{

	public RuntimeAnimatorController anim_0 = null;
	public RuntimeAnimatorController anim_1 = null;
	public RuntimeAnimatorController anim_2 = null;
	public RuntimeAnimatorController anim_3 = null;
	public RuntimeAnimatorController anim_4 = null;
	public RuntimeAnimatorController anim_5 = null;
	public RuntimeAnimatorController anim_6 = null;
	public RuntimeAnimatorController anim_7 = null;
	public RuntimeAnimatorController anim_8 = null;
	public RuntimeAnimatorController anim_9 = null;

	public AnimConfig ()
	{
		anim_0 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");
		anim_1 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");
		anim_2 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");
		anim_3 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");
		anim_4 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");
		anim_5 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");
		anim_6 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");
		anim_7 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");
		anim_8 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");
		anim_9 = Resources.Load<RuntimeAnimatorController>("Animation/IconCtrl");

	}

}


