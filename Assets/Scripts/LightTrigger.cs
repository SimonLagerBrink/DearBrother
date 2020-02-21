﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightTrigger : MonoBehaviour
{
	public List<Light2D> lightsToTurnOff = new List<Light2D>();
	public List<Light2D> lightsToTurnOn = new List<Light2D>();
	public List<GameObject> objectsToToggleLights = new List<GameObject>();
    void Update()
    {
		if (ShouldTriggerLights())
		{
			for (int i = 0; i < lightsToTurnOff.Count; i++)
			{
				lightsToTurnOff[i].enabled = false;
			}
			for (int i = 0; i < lightsToTurnOn.Count; i++)
			{
				lightsToTurnOn[i].enabled = true;
			}
		}
    }
	private bool ShouldTriggerLights()
	{
		for (int i = 0; i < objectsToToggleLights.Count; i++)
		{
			if (objectsToToggleLights[i] == true)
			{
				return false;
			}
		}
		return true;
	}
}
