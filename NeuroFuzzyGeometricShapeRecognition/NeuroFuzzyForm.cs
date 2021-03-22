using System;
using System.Windows.Forms;
using NeuroFuzzyBusinessLogic;

namespace NeuroFuzzyGeometricShapeRecognition
{
    public partial class NeuroFuzzyView : Form
    {
        public NeuroFuzzyView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: DELETE THIS
            MessageBox.Show(NeuroFuzzyPredictor.Predict(new float[] { 1f, 2f }));
        }
    }
}
