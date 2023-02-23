using UnityEngine;
using UnityEngine.UI;

public class ButtonPause
{
    public ARTapToPlaceObject spawnedObjects;
    private Atom currentAtom;

    public void PauseElectrons()
    {
        spawnedObjects.GetComponentInChildren<Atom>();
        currentAtom = spawnedObjects.spawnedAtom;

        if (currentAtom.isActiveAndEnabled)
        {
            if (Atom.isRunning)
            {
                if (!Atom.planetModel)
                {
                    foreach (Electron e in currentAtom._electrons)
                    {
                        e.StopTeleport();
                    }
                }
                else
                {
                    foreach (Electron e in currentAtom._electrons)
                    {
                        e.trail.enabled = false;
                        e.trail.Clear();
                    }
                }

                Atom.isRunning = false;
                GameObject.Find("Pause").GetComponentInChildren<Text>().text = ">";
            }
            else
            {
                if (!Atom.planetModel)
                {
                    foreach (Electron e in currentAtom._electrons)
                    {
                        if (e.orbit.enabled)
                        {
                            e.orbit.enabled = false;
                        }
                        e.BeginTeleport();
                    }
                }
                else
                {
                    foreach (Electron e in currentAtom._electrons)
                    {
                        if (e.orbit.enabled)
                        {
                            e.orbit.enabled = false;
                        }
                        e.trail.enabled = true;
                    }
                }

                Atom.isRunning = true;
                GameObject.Find("Pause").GetComponentInChildren<Text>().text = "||";
            }
        }
    }
}