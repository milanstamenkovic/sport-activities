﻿using GeoAPI.CoordinateSystems.Transformations;
using Npgsql;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using SharpMap.Layers;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;

namespace SportActivities
{
    public class DataManagement
    {
        public CoordinateTransformationFactory ctFact { get; set; }
        public ICoordinateTransformation transfCoord { get; set; }
        public ICoordinateTransformation reverseTransfCoord { get; set; }
        public  ConnectionStringSettingsCollection connectionStrings { get; set; }
        public string connectionParams;

        public DataManagement()
        {
            ctFact = new CoordinateTransformationFactory();
            transfCoord = ctFact.CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, ProjectedCoordinateSystem.WebMercator);
            reverseTransfCoord = ctFact.CreateFromCoordinateSystems(ProjectedCoordinateSystem.WebMercator, GeographicCoordinateSystem.WGS84);

            connectionStrings = ConfigurationManager.ConnectionStrings;
            connectionParams = connectionStrings["PostgreSQL"].ConnectionString;
        }

        public LabelLayer createLabelLayer(VectorLayer vectorLayer, string attributeName)
        {
            LabelLayer labelLayer = new LabelLayer(vectorLayer.LayerName);
            labelLayer.DataSource = vectorLayer.DataSource;
            labelLayer.LabelColumn = attributeName;
            labelLayer.Style.CollisionDetection = true;
            labelLayer.Style.CollisionBuffer = new SizeF(20, 20);
            labelLayer.MultipartGeometryBehaviour = LabelLayer.MultipartGeometryBehaviourEnum.Largest;
            labelLayer.Style.Font = new Font(FontFamily.GenericSansSerif, 10);
            labelLayer.CoordinateTransformation = transfCoord;
            labelLayer.ReverseCoordinateTransformation = reverseTransfCoord;

            return labelLayer;
        }

        public List<string> getAllLayerlAttributes(string layer)
        {
            List<string> attributes;

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionParams))
            {
                conn.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("select column_name from information_schema.columns where table_name='" + layer + "';", conn))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    attributes = new List<string>();
                    while (reader.Read())
                    {
                        attributes.Add(reader[0].ToString());
                    }
                }
            }

            return attributes;
        }
    }
}
