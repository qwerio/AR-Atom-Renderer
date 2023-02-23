using System;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour
{
    public List<GameObject> _prefabs;
    public List<Electron> _electrons;
    public int _atomicNumber;
    public float _atomicMass;

    public static bool planetModel;
    public static bool isRunning;
    public static bool modelButton;

    private int _protonsCount;
    private int _neutronsCount;
    private int _electronCount;
    private List<(string, int)> electronsOnSublayers;

    void Start()
    {
        InitializeParameters(_atomicNumber, _atomicMass);
        SpawnElectrons();
        SpawnNucleus();
    }
    void Update()
    {
        if(planetModel)
        {
            foreach (Electron e in _electrons)
            {
                e.RotateElectron();
            }
        } 
    }
    private void InitializeParameters(int atomNumber, float atomMass)
    {
        planetModel = true;
        isRunning = true;
        modelButton = false;

        _electronCount = atomNumber;
        _protonsCount = atomNumber;
        _neutronsCount = (int)Math.Round(atomMass) - atomNumber;
        _electrons = new List<Electron>(atomNumber);

        electronsOnSublayers = new List<(string, int)>();
    }
    private void SpawnElectrons()
    {
        List<List<int>> elOnSublevel = ElectronsOnSublevels(_electronCount);

        float energyLevelDistance = 1.0f;

        for (int level = 0; level < elOnSublevel.Count; level++)
        {
            float subLevelDistance = 0.0f;

            for (int subLevel = 0; subLevel < elOnSublevel[level].Count; subLevel++)
            {
                int subLevelCounter = 1;
                if (elOnSublevel[level][subLevel] != 0)
                {
                    for (int subLevelCount = 0; subLevelCount < elOnSublevel[level][subLevel]; subLevelCount++)
                    {
                        Electron electron = Instantiate(_prefabs[0], transform).GetComponent<Electron>();
                        electron._energyLevel = energyLevelDistance;
                        electron._subLevel = subLevelDistance;
                        if (subLevelCounter > Math.Floor(elOnSublevel[level][subLevel] / 2.0f))
                            electron._positiveSpin = false;
                        _electrons.Add(electron);
                        subLevelCounter++;
                    }
                }
                subLevelDistance += 0.08f;
            }
            energyLevelDistance += 0.8f;
        }
    }
    private List<List<int>> ElectronsOnSublevels(int atomicNumber)
    {
        //Electron Distribution on Sublayers (subayer, Layer)
        int s1, s2, p2, s3, p3, s4, d3, p4, s5, d4, p5, s6, f4, d5, p6, s7, f5, d6, p7;

        //s1
        if (atomicNumber <= 2)
            s1 = atomicNumber;
        else
            s1 = 2;
        //s2
        if (atomicNumber >= 2 && atomicNumber <= 4)
            s2 = atomicNumber - 2;
        else if (atomicNumber < 2)
            s2 = 0;
        else
            s2 = 2;
        //p2
        if (atomicNumber >= 4 && atomicNumber <= 10)
            p2 = (atomicNumber - 4);
        else if (atomicNumber < 4)
            p2 = 0;
        else
            p2 = 6;
        //s3
        if (atomicNumber >= 10 && atomicNumber <= 12)
            s3 = atomicNumber - 10;
        else if (atomicNumber < 10)
            s3 = 0;
        else
            s3 = 2;
        //p3
        if (atomicNumber >= 12 && atomicNumber <= 18)
            p3 = atomicNumber - 12;
        else if (atomicNumber < 12)
            p3 = 0;
        else
            p3 = 6;
        //s4
        if (atomicNumber >= 18 && atomicNumber <= 20)
            s4 = atomicNumber - 18;
        else if (atomicNumber < 18)
            s4 = 0;
        else
            s4 = 2;
        //d3
        if (atomicNumber >= 20 && atomicNumber <= 30)
            d3 = atomicNumber - 20;
        else if (atomicNumber < 20)
            d3 = 0;
        else
            d3 = 10;
        //p4
        if (atomicNumber >= 30 && atomicNumber <= 36)
            p4 = atomicNumber - 30;
        else if (atomicNumber < 30)
            p4 = 0;
        else
            p4 = 6;
        //s5
        if (atomicNumber >= 36 && atomicNumber <= 38)
            s5 = atomicNumber - 36;
        else if (atomicNumber < 36)
            s5 = 0;
        else
            s5 = 2;
        //d4
        if (atomicNumber >= 38 && atomicNumber <= 48)
            d4 = atomicNumber - 38;
        else if (atomicNumber < 38)
            d4 = 0;
        else
            d4 = 10;
        //p5
        if (atomicNumber >= 48 && atomicNumber <= 54)
            p5 = atomicNumber - 48;
        else if (atomicNumber < 48)
            p5 = 0;
        else
            p5 = 6;
        //s6
        if (atomicNumber >= 54 && atomicNumber <= 56)
            s6 = atomicNumber - 54;
        else if (atomicNumber < 54)
            s6 = 0;
        else
            s6 = 2;
        //f4
        if (atomicNumber >= 56 && atomicNumber <= 70)
            f4 = atomicNumber - 56;
        else if (atomicNumber < 56)
            f4 = 0;
        else
            f4 = 14;
        //d5
        if (atomicNumber >= 70 && atomicNumber <= 80)
            d5 = atomicNumber - 70;
        else if (atomicNumber < 70)
            d5 = 0;
        else
            d5 = 10;
        //p6
        if (atomicNumber >= 80 && atomicNumber <= 86)
            p6 = atomicNumber - 80;
        else if (atomicNumber < 80)
            p6 = 0;
        else
            p6 = 6;
        //s7
        if (atomicNumber <= 88 && atomicNumber >= 86)
            s7 = atomicNumber - 86;
        else if (atomicNumber < 86)
            s7 = 0;
        else
            s7 = 2;
        //f5
        if (atomicNumber >= 88 && atomicNumber <= 102)
            f5 = atomicNumber - 88;
        else if (atomicNumber < 88)
            f5 = 0;
        else
            f5 = 14;
        //d6
        if (atomicNumber >= 102 && atomicNumber <= 112)
            d6 = atomicNumber - 102;
        else if (atomicNumber < 102)
            d6 = 0;
        else
            d6 = 10;
        //p7
        if (atomicNumber >= 112 && atomicNumber <= 118)
            p7 = atomicNumber - 112;
        else if (atomicNumber < 112)
            p7 = 0;
        else
            p7 = 6;

        //Create and Fill List of Tuples - name of the subplayer and count of the electrons
        electronsOnSublayers.Add((nameof(s1), s1));
        electronsOnSublayers.Add((nameof(s2), s2));
        electronsOnSublayers.Add((nameof(p2), p2));
        electronsOnSublayers.Add((nameof(s3), s3));
        electronsOnSublayers.Add((nameof(p3), p3));
        electronsOnSublayers.Add((nameof(s4), s4));
        electronsOnSublayers.Add((nameof(d3), d3));
        electronsOnSublayers.Add((nameof(p4), p4));
        electronsOnSublayers.Add((nameof(s5), s5));
        electronsOnSublayers.Add((nameof(d4), d4));
        electronsOnSublayers.Add((nameof(p5), p5));
        electronsOnSublayers.Add((nameof(s6), s6));
        electronsOnSublayers.Add((nameof(f4), f4));
        electronsOnSublayers.Add((nameof(d5), d5));
        electronsOnSublayers.Add((nameof(p6), p6));
        electronsOnSublayers.Add((nameof(s7), s7));
        electronsOnSublayers.Add((nameof(f5), f5));
        electronsOnSublayers.Add((nameof(d6), d6));
        electronsOnSublayers.Add((nameof(p7), p7));

        //Create 2D Array to store the number of electrons on each sublayer
        List<List<int>> electronsOnLayers = new List<List<int>>();
        int energyLevels = 7;

        for (int i = 0; i < energyLevels; i++)
        {
            electronsOnLayers.Add(new List<int> { 0, 0, 0, 0 });
        }

        //Fill the 2D Array with count of electrons on sublayer
        for (int i = 0; i < electronsOnSublayers.Count; i++)
        {
            string sublayerName = electronsOnSublayers[i].Item1;
            int nunmberOfElectrons = electronsOnSublayers[i].Item2;

            switch (sublayerName[0])
            {
                case 's':
                    electronsOnLayers[(int)sublayerName[1] - '0' - 1][0] = nunmberOfElectrons;
                    break;
                case 'p':
                    electronsOnLayers[(int)sublayerName[1] - '0' - 1][1] = nunmberOfElectrons;
                    break;
                case 'd':
                    electronsOnLayers[(int)sublayerName[1] - '0' - 1][2] = nunmberOfElectrons;
                    break;
                case 'f':
                    electronsOnLayers[(int)sublayerName[1] - '0' - 1][3] = nunmberOfElectrons;
                    break;
            }
        }
        return electronsOnLayers;
    }
    private void SpawnNucleus()
    {
        Nucleon nucleon = Instantiate(_prefabs[1], transform).GetComponent<Nucleon>();

        float sphereRadius = nucleon.r * 2.0f;
        float distanceBetweenPoints = 0.17f;
        int nucleonCount = _protonsCount + _neutronsCount;

        nucleonCount--;
        _protonsCount--;

        while (nucleonCount > 0)
        {
            List<Vector3> points = PointsOnSphere(sphereRadius, distanceBetweenPoints);
            while (points.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, points.Count);
                if (--nucleonCount < 0)
                    break;

                if (nucleonCount % 2 == 0 && _protonsCount > 0)
                {
                    nucleon = Instantiate(_prefabs[1], this.transform).GetComponent<Nucleon>();
                    nucleon.transform.localPosition = points[randomIndex];
                    _protonsCount--;
                }
                else
                {
                    nucleon = Instantiate(_prefabs[2], this.transform).GetComponent<Nucleon>();
                    nucleon.transform.localPosition = points[randomIndex];
                    _neutronsCount--;
                }
                points.RemoveAt(randomIndex);
            }
            sphereRadius += distanceBetweenPoints;
        }
    }
    private List<Vector3> PointsOnSphere(float sphereRadius, float distanceBetweenPoints)
    {
        List<Vector3> allPoints = new List<Vector3>();
        List<Vector3> pointsOnLatitude = PointsOnSemiCircle(sphereRadius, distanceBetweenPoints);
        foreach (Vector3 point in pointsOnLatitude)
        {
            List<Vector3> pointsOnLognitude = new List<Vector3>(PointsOnCircle(point, distanceBetweenPoints, sphereRadius));
            allPoints.AddRange(pointsOnLognitude);
        }
        return allPoints;
    }
    private List<Vector3> PointsOnCircle(Vector3 pointOnLatitude, float distanceBetween, float sphereRadius)
    {
        List<Vector3> points = new List<Vector3>();

        float theta = Vector3.Angle(transform.up, pointOnLatitude);
        theta *= Mathf.Deg2Rad;

        float circleRadius = (float)Math.Sin(theta) * sphereRadius;
        float phi = 2.0f * (float)Math.Asin(distanceBetween / (2 * circleRadius));
        int count = (int)(2.0 * Math.PI / phi);

        phi = 2.0f * (float)Math.PI / count;

        float cosTheta = (float)Math.Cos(theta);
        float angle;
        for (int i = 0; i < count; i++)
        {
            angle = i * phi;
            float x = circleRadius * (float)Math.Cos(angle);
            float y = cosTheta * sphereRadius;
            float z = circleRadius * (float)Math.Sin(angle);
            points.Add(new Vector3(x, y, z));
        }
        return points;
    }
    private List<Vector3> PointsOnSemiCircle(float radius, float distanceBetween)
    {
        List<Vector3> points = new List<Vector3>();
        float phi = 2.0f * (float)Math.Asin(distanceBetween / (2.0f * radius));
        int count = (int)(Math.PI * 2.0f / phi) / 2 + 1;

        float angle;

        for (int i = 0; i < count; i++)
        {
            angle = i * phi;
            float x = 0.0f;
            float y = radius * (float)Math.Cos(angle);
            float z = radius * (float)Math.Sin(angle);
            points.Add(new Vector3(x, y, z));
        }
        return points;
    }
}