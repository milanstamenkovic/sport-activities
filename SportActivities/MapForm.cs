using Npgsql;
using SharpMap.Layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportActivities
{
    public partial class MapForm : Form
    {
        private ConnectionStringSettingsCollection connectionStrings;

        public MapForm()
        {
            InitializeComponent();

            this.connectionStrings = ConfigurationManager.ConnectionStrings;

            List<LayerRecord> list = GetAllLayers();
            VectorLayer layerRoads = new VectorLayer("Roads");
            //layerRoads.DataSource = new SharpMap.Providers.
            mapBox.Map.BackgroundLayer.Add(CreateBackgroundLayer());
            mapBox.Refresh();
        }

        public ILayer CreateBackgroundLayer()
        {
            return new TileLayer(new BruTile.Web.OsmTileSource(), "OSM");
        }

        public List<LayerRecord> GetAllLayers()
        {
            List<LayerRecord> layers;
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStrings["PostgreSQL"].ConnectionString))
            {
                conn.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("select * from geometry_columns where f_table_schema = 'public';", conn))
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    layers = new List<LayerRecord>();
                    while (reader.Read())
                    {
                        layers.Add(new LayerRecord(reader[1].ToString(), reader[2].ToString(),
                            reader[3].ToString(), (int)reader[4], (int)reader[5], reader[6].ToString()));
                    }
                }
            }

            return layers;
        }
    }
}
