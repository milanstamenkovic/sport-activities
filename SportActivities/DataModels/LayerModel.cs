using SharpMap.Layers; 

namespace SportActivities.DataModels
{
    public class LayerModel
    {
        public VectorLayer vectorLayer { get; set; }
        public LabelLayer labelLayer { get; set; }

        public LayerModel(VectorLayer vectorLayer, LabelLayer labelLayer)
        {
            this.vectorLayer = vectorLayer;
            this.labelLayer = labelLayer;
        }
    }
}
