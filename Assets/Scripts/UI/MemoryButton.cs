﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MemoryButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
	public Collectible collectible;
	public MemoryMenu menu;

	private Image image;
	private Animator animator;
	private Button button;

	private void Awake()
	{
		image = GetComponent<Image>();
		animator = GetComponent<Animator>();
		button = GetComponent<Button>();
	}

	public void View()
	{
		collectible.ShowMemory();
	}

	public void Refresh()
	{
		if (collectible.isCollected)
		{
			Debug.Log("Collected " + gameObject.name);
			gameObject.SetActive(true);
			button.interactable = true;
			animator.speed = 0;
			animator.Play(collectible.animationName);
		}
		else
		{
			Debug.Log("uncollected" + gameObject.name);
			button.interactable = false;
			gameObject.SetActive(false);
			//animator.Play("CollectibleEmpty");
		}
	}

	private void Update()
	{
		//Debug.Log("Speed" + animator.speed);
	}

	public void OnSelect(BaseEventData eventData)
	{
		animator.speed = 1;
		Debug.Log("selected" + gameObject.name);
		animator.Play(collectible.animationName, -1, 0);
	}

	public void OnDeselect(BaseEventData eventData)
	{
		animator.speed = 0;
		Debug.Log("deselected" + gameObject.name);
		animator.Play(collectible.animationName, -1, 0);
	}
}
