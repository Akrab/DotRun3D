using System;
using System.Collections.Generic;
using UnityEngine;

namespace DonRun3D.World.Column
{
    [Serializable]
    public class Columns
    {
        public List<ColumnSettingData> data = new List<ColumnSettingData>();

    }
    [Serializable]
    public class ColumnSettingData
    {
        public string name;
        public string id;
        public GameObject view;

        public ColumnSettingData()
        {
            var guid = Guid.NewGuid().ToString();
            guid = guid.Split("-")[0];
            id = guid;
        }
    }
}
