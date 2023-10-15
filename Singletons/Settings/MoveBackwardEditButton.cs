using Godot;
using System;

public partial class MoveBackwardEditButton : Button
{
	SettingsManager SM;
	string State = "Idle";

	public override void _Ready()
	{
		SM = GetNode<SettingsManager>("/root/SettingsManager");
		SM.MoveBackwardChanged += OnMoveBackwardChanged;
	}

    public override void _Pressed()
    {
        if(State == "Idle" && !(SM.StateMachine.currentState is SettingsState_KeybindEdit))
		{
			State = "Edit";
			SelfModulate = new Color(1, 0, 0, 1);
			SM.StateMachine.ChangeState(new SettingsState_KeybindEdit(SM));
		}
    }

	public void OnMoveBackwardChanged(string value)
	{
		State = "Idle";
		Text = value;
		SelfModulate = new Color(1, 1, 1, 1);
	}

	public override void _Input(InputEvent @event)
	{
		if(State == "Edit")
		{	
			if(@event is InputEventKey key)
			{
				SM.SetMoveBackward(key.Keycode);
			}
		}
		
	}
}
