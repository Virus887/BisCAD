using BisCAD.ParametricModels;
using BisCAD.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BisCAD.UserControls
{
    public partial class PointUserControl : UserControl
    {
        public PointModel _pointModel { get; set; }
        public PointUserControl(PointModel model)
        {
            InitializeComponent();
            _pointModel = model;
            CreateBindings();
        }

        private void CreateBindings()
        {
            texboxName.DataBindings.Add("Text", _pointModel, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            //numericUpDowns
            numericPositionX.DataBindings.Add("Value", _pointModel, "PositionX", true, DataSourceUpdateMode.OnPropertyChanged);
            numericPositionY.DataBindings.Add("Value", _pointModel, "PositionY", true, DataSourceUpdateMode.OnPropertyChanged);
            numericPositionZ.DataBindings.Add("Value", _pointModel, "PositionZ", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void RefreshOnValueChanged(object sender, System.EventArgs e)
        {
            (sender as Control).Refresh();
            Global.mainForm.RecalculateCursorPosition();
            Global.mainForm.UpdateAllBezierCurves();
        }

    }
}
