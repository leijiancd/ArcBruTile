﻿using System.Runtime.InteropServices;
using BrutileArcGIS.lib;
using BrutileArcGIS.Lib;
using BrutileArcGIS.Properties;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;

namespace BrutileArcGIS.commands
{
    [ProgId("AddBaiduLayerCommand")]
    public class AddBaiduLayerCommand : BaseCommand
    {

        private IApplication _application;

        public AddBaiduLayerCommand()
        {
            m_category = "BruTile";
            m_caption = "&Add Baidu map";
            m_message = "AddBaidu map";
            m_toolTip = m_caption;
            m_name = "AddBaiduLayerCommand";
            m_bitmap = Resources.download;
        }

        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            _application = hook as IApplication;

            if (hook is IMxApplication)
                m_enabled = true;
            else
                m_enabled = false;
        }

        public override void OnClick()
        {
            var url = "http://online{s}.map.bdimg.com/tile/?qt=tile&styles=pl&x={x}&y={y}&z={z}";
            var baiduconfig = new BaiduConfig("Baidu", url);

            var layerType = EnumBruTileLayer.InvertedTMS;
            var mxdoc = (IMxDocument)_application.Document;
            var map = mxdoc.FocusMap;

            var brutileLayer = new BruTileLayer(_application, baiduconfig, layerType)
            {
                Name = "Baidu",
                Visible = true
            };
            var env = new EnvelopeClass();
            env.XMin = 7728334;
            env.YMin = 1755189;
            env.XMax = 16173851;
            env.YMax = 7411992;
            brutileLayer.Extent = env;

            ((IMapLayers)map).InsertLayer(brutileLayer, true, 0);

        }
    }
}
