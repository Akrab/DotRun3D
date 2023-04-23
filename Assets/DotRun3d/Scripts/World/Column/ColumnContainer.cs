﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leopotam.EcsLite;
using UnityEngine;

namespace DonRun3D.World.Column
{

    public class ColumnContainer
    {
        public string id;
        public bool isPool = false;
        public EcsPackedEntity entity;
        public int line;
        public ColorMaterial colorMaterial;
        public ColumnView view;

    }
    
    public interface IColumnLine : ILineContainer
    {
        public List<ColumnContainer> data { get; }
    }

    public class LineContainer : IColumnLine
    {        
        private List<ColumnContainer> _data;
        public int line { get; set; }

        public EcsPackedEntity entity { get; set; }
        public List<ColumnContainer> data => _data;

        public LineContainer(int count)
        {
            _data = new List<ColumnContainer>(count);
        }
    }

    public interface ILineContainer
    {
        int line { get; set; }
        EcsPackedEntity entity { get; set; }
        
    }

    public interface IPosition
    {
        public Vector3 position { get; set; }
        
    }

    public interface IColorBorder
    {
        
    }
    
    public class ColorMaterial
    {
        public Material material;
        public ColorType colorType;
    }
}
