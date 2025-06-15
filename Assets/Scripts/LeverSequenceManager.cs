using Cainos.PixelArtPlatformer_Dungeon;
using System.Collections.Generic;
using UnityEngine;

public class LeverSequenceManager : MonoBehaviour
{
    [Header("Правильная последовательность (по ID)")]
    public List<int> correctSequence;

    [Header("Рычаги")]
    public List<Switch> levers; 

    [Header("Дверь")]
    public Door targetDoor;

    private List<int> currentInput = new List<int>();
    private List<Switch> pressedLevers = new List<Switch>();

    public void RegisterLeverPress(int leverId, Switch lever)
    {
        currentInput.Add(leverId);
        pressedLevers.Add(lever);

        if (currentInput.Count < correctSequence.Count)
            return;

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (currentInput[i] != correctSequence[i])
            {
                ResetAll();
                return;
            }
        }

        if (targetDoor != null)
            targetDoor.IsOpened = true;
    }

    private void ResetAll()
    {
        currentInput.Clear();

        foreach (var lever in pressedLevers)
        {
            if (lever != null)
                lever.IsOn = false;
        }

        pressedLevers.Clear();
    }
}
