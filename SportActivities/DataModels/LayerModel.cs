using SharpMap.Layers;
using System.Drawing;

namespace SportActivities.DataModels
{
    public class LayerModel
    {
        public VectorLayer vectorLayer { get; set; }
        public LabelLayer labelLayer { get; set; }
        public LayerRecord layerRecord { get; set; }
        public Color geometryColor { get; set; }

        public LayerModel(VectorLayer vectorLayer, LabelLayer labelLayer, LayerRecord layerRecord)
        {
            this.vectorLayer = vectorLayer;
            this.labelLayer = labelLayer;
            this.layerRecord = layerRecord;
        }
    }
}
