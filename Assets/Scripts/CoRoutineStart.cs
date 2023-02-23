using UnityEngine;
using UnityEngine.UI;

public class CoRoutineStart
{
    public ARTapToPlaceObject spawnedObjects;
    private Atom currentAtom;

    public void ChangeCoRoutine()
    {
        spawnedObjects.GetComponentInChildren<Atom>();
        currentAtom = spawnedObjects.spawnedAtom;

        if (currentAtom.isActiveAndEnabled)
        {
            if (Atom.modelButton)
            {
                SwitchToPlanetModel();
            }
            else
            {
                SwtichToQuantumModel();
            }
        }
    }
    private void SwitchToPlanetModel()
    {
        foreach (Electron e in currentAtom._electrons)
        {
            e.trail.enabled = true;
            e.StopTeleport();
        }

        Atom.planetModel = true;
        Atom.modelButton = false;
        GameObject.Find("Model").GetComponentInChildren<Text>().text = "Quantum";
    }
    private void SwtichToQuantumModel()
    {
        if (Atom.isRunning)
        {
            foreach (Electron e in currentAtom._electrons)
            {
                e.trail.enabled = false;
                e.trail.Clear();

                e.BeginTeleport();
            }
        }

        Atom.planetModel = false;
        Atom.modelButton = true;
        GameObject.Find("Model").GetComponentInChildren<Text>().text = "Planet";
    }
}