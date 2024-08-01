using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardButton : _ObjectInteractable
{
    [SerializeField] private BoardButtonType buttonType;
    [SerializeField] private InteractableBoard board;
    public bool _enabled = false;

    private void Start()
    {
        board = GetComponentInParent<InteractableBoard>();
    }

    public override void OnLookAt() { }

    public override void OnLookAway() { }

    public override void OnInteraction()
    {
        if (!_enabled) return;

        if (buttonType == BoardButtonType.startButton)
            board.StartGame();
        else if (buttonType == BoardButtonType.DifficultyButton)
            board.ChangeDifficulty();
    }
}

public enum BoardButtonType
{
    startButton, DifficultyButton
}
