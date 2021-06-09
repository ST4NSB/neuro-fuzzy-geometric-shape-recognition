using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataLayer.Enums;
using DataLayer.Models;
using NeuroFuzzyBusinessLogic;

namespace NeuroFuzzyGeometricShapeRecognition
{
    public partial class NeuroFuzzyView : Form
    {

        private const int SAMPLES = 16; // 16 OR 32
        public System.Drawing.Point current = new System.Drawing.Point();
        public System.Drawing.Point old = new System.Drawing.Point();
        public Pen pen = new Pen(Color.Black, 1);
        Bitmap bitmap;
        Graphics graphicsToSave;
        List<Shape> TrainingShapes = new List<Shape>();
        List<Shape> TestingShapes = new List<Shape>();

        Shape currentShape;

        public NeuroFuzzyView()
        {
            InitializeComponent();
            comboBoxShape.Items.Add(GeometricalShapeType.Circle);
            comboBoxShape.Items.Add(GeometricalShapeType.Square);
            comboBoxShape.Items.Add(GeometricalShapeType.Triangle);

            #region Initializare componente de desenare si imaginea bitmap din panel

            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);

            bitmap = new Bitmap(panelPaint.Width, panelPaint.Height);
            graphicsToSave = Graphics.FromImage(bitmap);
            panelPaint.BackgroundImage = bitmap;
            panelPaint.BackgroundImageLayout = ImageLayout.None;

            #endregion
        }



        #region Controloalele de desenare

        private void panelPaint_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                current = e.Location;
                graphicsToSave.DrawLine(pen, old, current);

                old = current;
                panelPaint.Invalidate();
            }
        }
        private void panelPaint_MouseDown(object sender, MouseEventArgs e)
        {
            if (comboBoxSession.SelectedItem == null || comboBoxShape.SelectedItem == null)
            {
                MessageBox.Show("Please select the type of session and the type of shape");
                return;
            }
            old = e.Location;
        }



        #endregion

        #region Private Methods

        private void ResetDrawingArea()
        {
            bitmap = new Bitmap(panelPaint.Width, panelPaint.Height);
            graphicsToSave = Graphics.FromImage(bitmap);
            panelPaint.BackgroundImage = bitmap;
            panelPaint.BackgroundImageLayout = ImageLayout.None;
        }
        private void ShowCenterOfShape(int sizePoint)
        {
            if (comboBoxSession.SelectedItem != null && comboBoxShape.SelectedItem != null)
            {
                currentShape = new Shape((GeometricalShapeType)comboBoxShape.SelectedItem);

                currentShape.TakePointsFromShape(bitmap);
                currentShape.CalculateGravityCenter();


                panelPaint.Invalidate();

            }
            else
            {
                MessageBox.Show("Select the type of session and type of shape");
                return;
            }

            graphicsToSave.FillRectangle((Brush)Brushes.Blue, currentShape.GravityCenter.X, currentShape.GravityCenter.Y, sizePoint, sizePoint);
        }

        private void ShowSegments()
        {
            foreach (System.Drawing.Point point in currentShape.ListOfHolePoints)
            {
                graphicsToSave.DrawLine(pen, currentShape.GravityCenter, point);
                currentShape.ListOfSegments.Add(GetPoints(currentShape.GravityCenter, point));
                panelPaint.Invalidate();
            }
        }

        private Segment GetPoints(System.Drawing.Point p1, System.Drawing.Point p2)
        {
            Segment segmentPoints = new Segment();

            // no slope (vertical line)
            if (p1.X == p2.X)
            {
                for (int y = p1.Y; y <= p2.Y; y++)
                {
                    System.Drawing.Point p = new System.Drawing.Point(p1.X, y);
                    segmentPoints.ListOfPoints.Add(p);
                }
            }
            else
            {
                // swap p1 and p2 if p2.X < p1.X
                if (p2.X < p1.X)
                {
                    System.Drawing.Point temp = p1;
                    p1 = p2;
                    p2 = temp;
                }

                int deltaX = p2.X - p1.X;
                int deltaY = p2.Y - p1.Y;
                double error = -1.0f;
                int deltaErr = Math.Abs(deltaY / deltaX);

                int y = p1.Y;
                for (int x = p1.X; x <= p2.X; x++)
                {
                    System.Drawing.Point p = new System.Drawing.Point(x, y);
                    segmentPoints.ListOfPoints.Add(p);

                    error += deltaErr;

                    while (error >= 0.0f)
                    {

                        y++;
                        segmentPoints.ListOfPoints.Add(new System.Drawing.Point(x, y));
                        error -= 1.0f;
                    }
                }

                //if (segmentPoints.ListOfPoints.Last() != p2)
                //{
                //    int index = segmentPoints.ListOfPoints.IndexOf(p2);
                //    segmentPoints.ListOfPoints.RemoveRange(index + 1, segmentPoints.ListOfPoints.Count - index - 1);
                //}
                segmentPoints.ListOfPoints.RemoveAt(segmentPoints.ListOfPoints.Count - 1);
            }

            return segmentPoints;
        }

        private void ShpwPreprocessedShape()
        {
            ResetDrawingArea();
            //Delete redundant points
            //Bitmap bitmapToReturn = new Bitmap(300, 300);
            List<System.Drawing.Point> newListPoints = currentShape.ListOfHolePoints;
            foreach (Segment segment in currentShape.ListOfSegments)
            {

                int ct = 0;
                System.Drawing.Point lastPoint = new System.Drawing.Point();
                foreach (System.Drawing.Point point in currentShape.ListOfHolePoints)
                {

                    if (segment.ListOfPoints.Contains(point))
                    {
                        ct++;

                        if (ct >= 2)
                        {
                            newListPoints.Remove(lastPoint);
                            graphicsToSave.FillRectangle((Brush)Brushes.Red, lastPoint.X, lastPoint.Y, 3, 3);
                            panelPaint.Invalidate();
                            break;
                        }
                        lastPoint = point;
                    }
                }
            }

            currentShape.ListOfHolePoints = newListPoints;

            // Generate the image

            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 300; j++)
                {
                    System.Drawing.Point point = new System.Drawing.Point(i,j);
                    if (newListPoints.Contains(point))
                    {
                        graphicsToSave.FillRectangle((Brush)Brushes.Black, i, j, 1, 1);
                    }
                    else
                    {
                        graphicsToSave.FillRectangle((Brush)Brushes.White, i, j, 1, 1);
                    }
                }
            }


            //return bitmapToReturn;
        }
        #endregion

        #region Events
        private void btnShowCenterPoint_Click(object sender, EventArgs e)
        {
            ShowCenterOfShape(3);
        }

        private void btnResetImage_Click(object sender, EventArgs e)
        {
            ResetDrawingArea();
        }

        private void btnShowSegments_Click(object sender, EventArgs e)
        {
            ShowSegments();      
        }

        private void btnPreprocessedShape_Click(object sender, EventArgs e)
        {
            ShpwPreprocessedShape();
        }

        private void btnInterestPoints_Click(object sender, EventArgs e)
        {
            int step = currentShape.ListOfHolePoints.Count / SAMPLES;

            for (int i = 0; i < currentShape.ListOfHolePoints.Count - 15; i += step)
            {
                
                System.Drawing.Point currentPoint = currentShape.ListOfHolePoints.ElementAt(i);

                currentShape.ListOfInterestPoints.Add(currentPoint);

                graphicsToSave.FillRectangle((Brush)Brushes.Blue, currentPoint.X, currentPoint.Y, 3, 3);
                panelPaint.Invalidate();
            }


           // ---------------------------TBD + corrected----------------------------
            //double targetAngle = System.Math.Round(((double)(2 * 180) / SAMPLES), 2);
            //currentShape.ListOfInterestPoints.Add(currentShape.ListOfHolePoints.ElementAt(0));
            ////System.Drawing.Point currentPoint = currentShape.ListOfHolePoints.ElementAt(0);
            //for (int i = 0; i < currentShape.ListOfHolePoints.Count; i++)
            //{
            //    for (int j = i; j < currentShape.ListOfHolePoints.Count; j++)
            //    {
            //        System.Drawing.Point secondPoint = currentShape.ListOfHolePoints.ElementAt(j);
            //        double angle = System.Math.Round(GetAngleBetweenPoints(currentPoint, secondPoint), 2);
            //        if (angle == targetAngle)
            //        {
            //            graphicsToSave.FillRectangle((Brush)Brushes.Blue, secondPoint.X, secondPoint.Y, 3, 3);
            //            panelPaint.Invalidate();
            //            currentShape.ListOfInterestPoints.Add(secondPoint);
            //            currentPoint = secondPoint;
            //            break;
            //        }
            //    }
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: DELETE THIS
            MessageBox.Show("test");
        }

        private void btnSendShape_Click(object sender, EventArgs e)
        {

            if (comboBoxSession.SelectedItem != null && comboBoxShape.SelectedItem != null)
            {
                currentShape = new Shape((GeometricalShapeType)comboBoxShape.SelectedItem);

                //currentShape.TakePointsFromShape(bitmap);
                //currentShape.CalculateGravityCenter();

                panelPaint.Invalidate();
                ResetDrawingArea();
            }
            else
            {
                MessageBox.Show("Select the type of session and type of shape");
                return;
            }


            if (comboBoxSession.SelectedItem == "Testing")
            {
                TestingShapes.Add(currentShape);
            }
            else if (comboBoxSession.SelectedItem == "Training")
            {
                TrainingShapes.Add(currentShape);
            }
            else
            { return; }

            panelPreprocessed.BackgroundImage = currentShape.GenerateShapeWithSignificant();
        }

        #endregion

        #region Methods that will be corrected

        double GetAngleBetweenPoints(System.Drawing.Point first, System.Drawing.Point second)
        {

            // ------------------ TBD! ------------------------
            System.Drawing.Point center = currentShape.GravityCenter;

            //double numerator = first.Y * (center.X - second.X) + center.Y * (second.X - first.X) + second.Y * (first.X - center.X);
            //double denominator = (first.X - center.X) * (center.X - second.X) + (first.Y - center.Y) * (center.Y - second.Y);
            //double ratio = numerator / denominator;

            //double angleRad = Math.Atan(ratio);
            //double angleDeg = (angleRad * 180) / Math.PI;

            //if (angleDeg < 0)
            //{
            //    angleDeg = 180 + angleDeg;
            //}

            double angleDeg = Math.Atan2(second.Y - center.Y, second.X - center.X) -
               Math.Atan2(first.Y - center.Y, first.X - center.X);
            

            return angleDeg;
        }

        #endregion


    }

}
