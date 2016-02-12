﻿using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using BrutileArcGIS.Lib;
using BrutileArcGIS.forms;
using BrutileArcGIS.lib;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace BrutileArcGIS.commands
{
    [ProgId("AboutBruTileCommand")]
    public sealed class AboutBruTileCommand : BaseCommand
    {
        private IApplication _application;

        public AboutBruTileCommand()
        {
            m_category = "BruTile";
            m_caption = "&About ArcBruTile...";
            m_message = "About BruTile...";
            m_toolTip = m_caption;
            m_name = "AboutBruTileCommand";
        }

        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            _application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                m_enabled = true;
            else
                m_enabled = false;
        }

        public override void OnClick()
        {
            var url = "http://online3.map.bdimg.com/tile/?qt=tile&styles=sl&x={x}&y={y}&z={z}";
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






            //var bruTileAboutBox = new BruTileAboutBox();
            //bruTileAboutBox.ShowDialog(new ArcMapWindow(_application));
        }
    }
}


