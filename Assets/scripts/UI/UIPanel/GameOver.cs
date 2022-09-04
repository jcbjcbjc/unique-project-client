using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Assets.scripts.UI;
using Assets.scripts.Message;

public class GameOver : BaseUIForm
{
	
	
	public Button ReturnRoom;
	public Button QuitButton;
	
    private void Awake()
    {
		
    }

    void Start()
	{
		
	}

	public void OnClickReturn()
	{
	}
	public void OnClickQuit() {
		
	}
	

	public override void Close()
	{
		MessageCenter.RemoveMsgListener(this);
		CloseUIForm();
	}
}
