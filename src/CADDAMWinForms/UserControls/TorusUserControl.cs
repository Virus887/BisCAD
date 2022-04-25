using BisCAD.ParametricModels;
using BisCAD.Utils;
using System.Windows.Forms;

namespace BisCAD.UserControls
{
    public partial class TorusUserControl : UserControl
    {
        public TorusParametricModel _torusParametricModel { get; set; } 
        public TorusUserControl(TorusParametricModel model)
        {
            InitializeComponent();
            _torusParametricModel = model;
            CreateBindings();
        }


        private void CreateBindings()
        {
            textboxName.DataBindings.Add("Text", _torusParametricModel, "Name", true, DataSourceUpdateMode.OnPropertyChanged);

            //Sliders
            sliderR1.DataBindings.Add("Value", _torusParametricModel, "R1", true, DataSourceUpdateMode.OnPropertyChanged);
            sliderR2.DataBindings.Add("Value", _torusParametricModel, "R2", true, DataSourceUpdateMode.OnPropertyChanged);
            sliderR1Partitions.DataBindings.Add("Value", _torusParametricModel, "Partitions1", true, DataSourceUpdateMode.OnPropertyChanged);
            sliderR2Partitions.DataBindings.Add("Value", _torusParametricModel, "Partitions2", true, DataSourceUpdateMode.OnPropertyChanged);
            labelR1.DataBindings.Add("Text", _torusParametricModel, "R1", true, DataSourceUpdateMode.OnPropertyChanged, "", "R1: #,0.00");
            labelR2.DataBindings.Add("Text", _torusParametricModel, "R2", true, DataSourceUpdateMode.OnPropertyChanged, "", "R2: #,0.00");
            labelR1Partitions.DataBindings.Add("Text", _torusParametricModel, "Partitions1", true, DataSourceUpdateMode.OnPropertyChanged, "", "R1 partitions: #,0");
            labelR2Partitions.DataBindings.Add("Text", _torusParametricModel, "Partitions2", true, DataSourceUpdateMode.OnPropertyChanged, "", "R2 partitions: #,0");
            
            //numericUpDowns
            numericPositionX.DataBindings.Add("Value", _torusParametricModel, "PositionX", true, DataSourceUpdateMode.OnPropertyChanged);
            numericPositionY.DataBindings.Add("Value", _torusParametricModel, "PositionY", true, DataSourceUpdateMode.OnPropertyChanged);
            numericPositionZ.DataBindings.Add("Value", _torusParametricModel, "PositionZ", true, DataSourceUpdateMode.OnPropertyChanged);
            numericScaleX.DataBindings.Add("Value", _torusParametricModel, "ScaleX", true, DataSourceUpdateMode.OnPropertyChanged);
            numericScaleY.DataBindings.Add("Value", _torusParametricModel, "ScaleY", true, DataSourceUpdateMode.OnPropertyChanged);
            numericScaleZ.DataBindings.Add("Value", _torusParametricModel, "ScaleZ", true, DataSourceUpdateMode.OnPropertyChanged);
            numericRotationX.DataBindings.Add("Value", _torusParametricModel, "RotationX", true, DataSourceUpdateMode.OnPropertyChanged);
            numericRotationY.DataBindings.Add("Value", _torusParametricModel, "RotationY", true, DataSourceUpdateMode.OnPropertyChanged);
            numericRotationZ.DataBindings.Add("Value", _torusParametricModel, "RotationZ", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void RefreshOnValueChanged(object sender, System.EventArgs e)
        {
            (sender as Control).Refresh();
            Global.mainForm.RecalculateCursorPosition();
        }
    }
}
