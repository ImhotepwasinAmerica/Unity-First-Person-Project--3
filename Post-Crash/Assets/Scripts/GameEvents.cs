﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public event Action DeleteAllTheThings, SaveAllTheThings;
    public event Action<int> DoorwayOpen, DoorwayClose;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteAllItems()
    {
        if (DeleteAllTheThings != null)
        {
            DeleteAllTheThings();
        }
    }

    public void SaveAllItems()
    {
        if (SaveAllTheThings != null)
        {
            SaveAllTheThings();
        }
    }

    public void OpenTheDoor(int num)
    {
        if(DoorwayOpen != null)
        {
            DoorwayOpen(num);
        }
    }

    public void CloseTheDoor(int num)
    {
        if (DoorwayClose != null)
        {
            DoorwayClose(num);
        }
    }
}
