using BisCAD.ParametricModels;
using BisCAD.ParametricModels.Curves;
using BisCAD.UserControls;
using BisCAD.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace BisCAD
{
    public partial class MainForm : Form
    {
        private Scene _scene;
        public MainForm()
        {
            InitializeComponent();
            InitializeAllComponents();
        }

        private void InitializeAllComponents()
        {
            Global.Timer.Start();
            Global.mainForm = this;
            _scene = new Scene(glControl, CheckboxShowPlatform);
            listBoxElements.DataSource = _scene.ParametricModels;

            //TODO: Refactor!
            // For right click
            //assign a contextmenustrip
            listboxContextMenu = new ContextMenuStrip();
            listBoxElements.ContextMenuStrip = listboxContextMenu;
        }

        private void GLControl_Load(object sender, EventArgs e) => _scene.Load();

        private void GLControl_Dispose(object sender, EventArgs e) => _scene.Dispose();

        private void GLControl_Resize(object sender, EventArgs e) => _scene.Resize();

        private void GLControl_Paint(object sender, PaintEventArgs e) => _scene.Render();



        // --------------- SELECTED CHANGED -------
        private void listBoxElements_SelectedIndexChanged(object sender, EventArgs e)
        {           
            if (turnOffSelectedIndexChanged) return;

            RecalculateCursorPosition();
            MarkSelectedItems();

            // Show right side
            UserControl uc = null;
            if (listBoxElements.SelectedIndices.Count == 0 || listBoxElements.SelectedIndices.Count > 1)
            {
                uc = new ElementGroupUserControl(_scene.ParametricModels.Where(x=>x.IsSelected == true).ToList(), _scene.Cursor);
            }

            if (listBoxElements.SelectedIndices.Count == 1)
            {
                var model = GetSelectedObjectsList().First();
                
                if (model is TorusParametricModel) 
                {
                    uc = new TorusUserControl(model as TorusParametricModel);
                }
                else if (model is PointModel)
                {
                    uc = new PointUserControl(model as PointModel);
                }
                else if (model is MultiBezierCurveModel)
                {
                    uc = new MultiBezierCurveControl(model as MultiBezierCurveModel, _scene.Cursor as CursorModel);
                }
                else if (model is MultiBezierCurveC2Model)
                {
                    uc = new MultiBezierCurveC2Control(model as MultiBezierCurveC2Model, _scene.Cursor as CursorModel);
                }
            }
            AddUserControl(uc);

        }

        private void MarkSelectedItems()
        {
            _scene.ParametricModels.ForEach(x => x.IsSelected = false);
            var objects = GetSelectedObjectsList();
            foreach (var obj in objects)
                obj.IsSelected = true;
        }


        private bool turnOffSelectedIndexChanged = false;

        public void RefreshElementsListBox()
        {
            turnOffSelectedIndexChanged = true;

            List<int> indices = new List<int>();

            foreach (var obj in listBoxElements.SelectedIndices)
                indices.Add((int)obj);

            listBoxElements.DataSource = null;
            listBoxElements.DataSource = _scene.ParametricModels;
            listBoxElements.DisplayMember = "DisplayName";

            foreach (var idx in indices)
                listBoxElements.SetSelected(idx, true);

            turnOffSelectedIndexChanged = false;
            listBoxElements.Refresh();
        }


        public List<IParametricModel> GetSelectedObjectsList()
        {
            if (listBoxElements.SelectedIndices.Count == 0) return new List<IParametricModel>();

            List<int> selectedIndices = new List<int>();
            foreach (var obj in listBoxElements.SelectedIndices)
                selectedIndices.Add((int)obj);

            List<IParametricModel> models = new List<IParametricModel>();
            foreach (int i in selectedIndices)
            {
                models.Add(_scene.ParametricModels[i]);
            }
            return models;
        }

        public void RecalculateCursorPosition()
        {
            var selectedObjects = GetSelectedObjectsList();
            if (selectedObjects.Count == 0) return;

            Vector3 position = Vector3.Zero;

            //if (selectedObjects.Count > 1)
            //{
            //    position += new Vector3(_scene.Cursor.PositionX, _scene.Cursor.PositionY, _scene.Cursor.PositionZ);
            //}

            foreach (var item in selectedObjects)
            {
                position += new Vector3(item.PositionX, item.PositionY, item.PositionZ);
            }
            if (selectedObjects.Count > 1) position /= selectedObjects.Count;

            _scene.Cursor.PositionX = position.X;
            _scene.Cursor.PositionY = position.Y;
            _scene.Cursor.PositionZ = position.Z;
        }
        


        private void buttonDeleteSelectedItems_Click(object sender, EventArgs e)
        {
            var selectedObjects = GetSelectedObjectsList();
            if (selectedObjects.Count == 0) return;
            selectedObjects.ForEach(x => 
            {
                _scene.ParametricModels.Remove(x);
                _scene.ParametricModels.ForEach(model =>
               {
                   if (model is MultiBezierCurveModel)
                   {
                       (model as MultiBezierCurveModel).BernsteinPoints.Remove(x);
                       (model as MultiBezierCurveModel).Update();
                   }
                   if (model is MultiBezierCurveC2Model)
                   {
                       (model as MultiBezierCurveC2Model).DeBoorePoints.Remove(x);
                       (model as MultiBezierCurveC2Model).Update();
                   }
               });
            });
            listBoxElements.DataSource = null;
            listBoxElements.DataSource = _scene.ParametricModels;
            listBoxElements.DisplayMember = "DisplayName";

            listBoxElements.Refresh();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementGroupUserControl uc = new ElementGroupUserControl(_scene.ParametricModels.Where(x => x.IsSelected == true).ToList(), _scene.Cursor);
            AddUserControl(uc);
        }

        public void UpdateAllBezierCurves()
        {
            _scene.ParametricModels.ForEach(x => { if (x is MultiBezierCurveModel || x is MultiBezierCurveC2Model) x.Update();});
        }
    }
}
