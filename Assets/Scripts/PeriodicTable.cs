using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PeriodicTable : MonoBehaviour
{
    public PeriodicElement childObj;
    public List<PeriodicElement> periodicElements;
    public List<float> elementsMass;

    private TextAsset namesFileAsset;
    private TextAsset symbolsFileAsset;
    private TextAsset massesFileAsset;

    private string[] namesArray;
    private string[] symbolsArray;
    private string[] massesArray;
    
    public PeriodicElement PeriodicElement
    {
        get { return childObj; }
        private set { childObj = value; }
    }
    public List<string> PeriodicElementName { get; private set; }
    public List<string> PeriodicElementSymbol { get; private set; }

    private void Awake()
    {
        periodicElements = new List<PeriodicElement>();
        elementsMass = new List<float>();
      
        PeriodicElementName = new List<string>();
        PeriodicElementSymbol = new List<string>();
    }
    private void Start()
    {
        ReadStoreData();
        InstantiateElements();
        SetChildsParameters();
        TranslateElements();
    }
    private void ReadStoreData()
    {
        string fileContent;
        
        namesFileAsset = Resources.Load<TextAsset>("PeriodicNames");
        fileContent = namesFileAsset.text;
        namesArray = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        symbolsFileAsset = Resources.Load<TextAsset>("PeriodicSymbols");
        fileContent = symbolsFileAsset.text;
        symbolsArray = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        massesFileAsset = Resources.Load<TextAsset>("PeriodicMass");
        fileContent = massesFileAsset.text;
        massesArray = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        for (int i = 0; i < namesArray.Length; i++)
        {
            PeriodicElementName.Add(namesArray[i]);

            PeriodicElementSymbol.Add(symbolsArray[i]);

            elementsMass.Add(float.Parse(massesArray[i], CultureInfo.InvariantCulture.NumberFormat));
        }
    }
    private void InstantiateElements()
    {
        for (int i = 0; i < elementsMass.Count; i++)
        {
             PeriodicElement = Instantiate(childObj, transform).GetComponent<PeriodicElement>();
        }
    }
    private void SetChildsParameters()
    {
        for (int child = 0; child < transform.childCount; child++)
        {
            int elNumber = child + 1;

            PeriodicElement element = transform.GetChild(child).GetComponent<PeriodicElement>();

            element.ElementMass = elementsMass[child];
            element.ElementNumber = elNumber;
            element.name = PeriodicElementName[child];

            transform.GetChild(child).GetComponentInChildren<TextMesh>().text = elNumber.ToString() + "\n" + PeriodicElementSymbol[child];

            periodicElements.Add(element);
        }
    }
    private void TranslateElements()
    {
        float fixedX = -10.2f;
        float fixedY = 4.8f;

        int currentChild = 0;
        float yOffset = -1.20f;
        float xOffset = 1.20f;
        float newX, newY;
        bool passed = false;
        
        for(int yPos = 0; yPos < 9; yPos++)
        {
            newY = yPos * yOffset;
            for(int xPos = 0; xPos < 18; xPos++)
            {
                if (yPos == 0 && xPos == 1)
                {
                    newX = 17 * xOffset;
                    xPos = 17;
                    transform.GetChild(currentChild).localPosition += new Vector3(newX + fixedX, newY + fixedY, 0.0f);
                    if (++currentChild == transform.childCount) return;
                    continue;
                }
                else if ((yPos == 1 || yPos == 2) && xPos == 2)
                {
                    newX = 12 * xOffset;
                    xPos = 12;
                    transform.GetChild(currentChild).localPosition += new Vector3(newX + fixedX, newY + fixedY, 0.0f);
                    if (++currentChild == transform.childCount) return;
                    continue;
                } 
                else if (yPos == 5 && xPos >= 3 && xPos <= 16 && !passed)
                {
                    newX = xPos * xOffset;
                    newY = 7 * yOffset;
                    transform.GetChild(currentChild).localPosition += new Vector3(newX + fixedX, newY + fixedY, 0.0f);
                    if (++currentChild == transform.childCount) return;
                    if(xPos == 16)
                    {
                        newY = yPos * yOffset;
                        xPos = 2;
                        passed = true;
                    }
                    continue;
                }
                else if ((yPos == 6 && xPos >= 3 && xPos <= 16 && passed))
                {
                    newX = xPos * xOffset;
                    newY = 8 * yOffset;
                    transform.GetChild(currentChild).localPosition += new Vector3(newX + fixedX, newY + fixedY, 0.0f);
                    if (++currentChild == transform.childCount) return;
                    if (xPos == 16)
                    {
                        newY = yPos * yOffset;
                        xPos = 2;
                        passed = false;
                    }
                    continue;
                }
                newX = xPos * xOffset;
                transform.GetChild(currentChild).localPosition += new Vector3(newX + fixedX, newY + fixedY, 0.0f);
                if (++currentChild == transform.childCount) return;
            }
        }
    }   
}