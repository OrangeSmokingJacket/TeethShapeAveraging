using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace TeethShapeAveraging
{
	public partial class ShapeComparison : Form
	{
		TeethShapeAveraging mainWindow;
		public ShapeComparison(TeethShapeAveraging mainWindow)
		{
			this.mainWindow = mainWindow;
			InitializeComponent();
		}
		private void DropBox_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
			else
				e.Effect = DragDropEffects.None;
		}
		private void DropBox_DragAndDrop(object sender, DragEventArgs e)
		{
			First.Items.Clear();
			string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			if (paths.Length > 0)
			{
				if (paths[0].Contains(".tsa"))
					First.Items.Add(paths[0]);
			}
		}
		private void Second_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
			else
				e.Effect = DragDropEffects.None;
		}
		private void Second_DragAndDrop(object sender, DragEventArgs e)
		{
			Second.Items.Clear();
			string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			if (paths.Length > 0)
			{
				if (paths[0].Contains(".tsa"))
					Second.Items.Add(paths[0]);
			}
		}
		private void ProcessDataSwitch_Click(object sender, EventArgs e)
		{
			Hide();
			mainWindow.ShowDialog();
		}
		private void Clear_First_Button_Click(object sender, EventArgs e)
		{
			First.Items.Clear();
		}
		private void Clear_Second_Button_Click(object sender, EventArgs e)
		{
			Second.Items.Clear();
		}
		private void Start_Comparing_Click(object sender, EventArgs e)
		{
			if (First.Items.Count > 0 && Second.Items.Count > 0)
			{
				Start_Comparing.Enabled = false;

				Shape_Data first_m1 = new Shape_Data();
				Shape_Data first_M3 = new Shape_Data();
				Shape_Data second_m1 = new Shape_Data();
				Shape_Data second_M3 = new Shape_Data();

				string path = First.Items[0].ToString();
				if (File.Exists(path))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					FileStream stream = new FileStream(path, FileMode.Open);

					Sample_ForSaving sample = formatter.Deserialize(stream) as Sample_ForSaving;

					first_m1 = new Shape_Data(sample.m1);
					first_M3 = new Shape_Data(sample.M3);
					stream.Close();
				}
				path = Second.Items[0].ToString();
				if (File.Exists(path))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					FileStream stream = new FileStream(path, FileMode.Open);

					Sample_ForSaving sample = formatter.Deserialize(stream) as Sample_ForSaving;

					second_m1 = new Shape_Data(sample.m1);
					second_M3 = new Shape_Data(sample.M3);
					stream.Close();
				}

				m1_First_Display.Image = first_m1.composedImage;
				m1_First_Display.Update();
				M3_First_Display.Image = first_M3.composedImage;
				M3_First_Display.Update();
				m1_Second_Display.Image = second_m1.composedImage;
				m1_Second_Display.Update();
				M3_Second_Display.Image = second_M3.composedImage;
				M3_Second_Display.Update();

				float match_m1 = Comparasion(first_m1, second_m1);
				float match_M3 = Comparasion(first_M3, second_M3);

				Match_m1.Text = match_m1.ToString();
				Match_M3.Text = match_M3.ToString();

				Start_Comparing.Enabled = true;
			}
		}
		public static float Comparasion(Shape_Data a, Shape_Data b)
		{
			return (HalfCompare(a, b) + HalfCompare(b, a)) / 2f;
		} // get average, because there could be some variants in transformation due to pixalated space
		static float HalfCompare(Shape_Data a, Shape_Data b)
		{
			//transforming b to maximize matching to a
			Vector2 middlePoint = new Vector2(b.processedValues.GetLength(0) / 2f, b.processedValues.GetLength(1) / 2f);

			float[,] transformationMatrix = TeethShapeAveraging.GenerateTransformationMatrix(a.POI, b.POI, middlePoint);
			float[,] transformedValues_b = new float[b.processedValues.GetLength(0), b.processedValues.GetLength(1)];
			for(int i = 0; i < b.processedValues.GetLength(0); i++)
			{
				for (int j = 0; j < b.processedValues.GetLength(0); j++)
				{
					transformedValues_b[i, j] = 0f;
				}
			}
			transformedValues_b = TeethShapeAveraging.AddValues(transformedValues_b, b.processedValues, middlePoint, transformationMatrix);

			// actuall comparasion

			float matchRate = 0f;
			int pixelsCounted = 0;
			for (int i = 0; i < b.processedValues.GetLength(0); i++)
			{
				for (int j = 0; j < b.processedValues.GetLength(0); j++)
				{
					if (a.processedValues[i, j] != 0 && transformedValues_b[i, j] != 0)
					{
						matchRate += 1f - Math.Abs(a.processedValues[i, j] - transformedValues_b[i, j]);
						pixelsCounted++;
					}
				}
			}

			matchRate /= pixelsCounted; // normalizing to [0,1]

			return matchRate;
		}
	}
}
