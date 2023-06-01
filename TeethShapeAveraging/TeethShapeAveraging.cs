using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;

namespace TeethShapeAveraging
{
	public enum ToothPos { Upper, Lower };
	public partial class TeethShapeAveraging : Form
	{
		List<Shape_Data> m1_data = new List<Shape_Data>();
		List<Shape_Data> M3_data = new List<Shape_Data>();

		Sample currentSample = new Sample();

		int currentLoadedImage_m1 = 0;
		int currentLoadedImage_M3 = 0;

		float margin = 0f;
		public TeethShapeAveraging()
		{
			InitializeComponent();
			
			DropBox.DragDrop += new DragEventHandler(DropBox_DragAndDrop);
			DropBox.DragEnter += new DragEventHandler(DropBox_DragEnter);
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
			string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			for (int i = 0; i < paths.Length; i++)
			{
				DropBox.Items.Add(paths[i]);
			}
		}
		private void Next_Picture_m1_Button_Click(object sender, EventArgs e)
		{
			if (m1_data.Count > 0)
			{
				currentLoadedImage_m1++;
				if (currentLoadedImage_m1 >= m1_data.Count)
					currentLoadedImage_m1 = 0;
				m1_Display.Image = m1_data[currentLoadedImage_m1].composedImage;
			}
		}
		private void Previous_Picture_m1_Button_Click(object sender, EventArgs e)
		{
			if (m1_data.Count > 0)
			{
				currentLoadedImage_m1--;
				if (currentLoadedImage_m1 < 0)
					currentLoadedImage_m1 = m1_data.Count - 1;
				m1_Display.Image = m1_data[currentLoadedImage_m1].composedImage;
			}
		}
		private void Next_Picture_M3_Button_Click(object sender, EventArgs e)
		{
			if (M3_data.Count > 0)
			{
				currentLoadedImage_M3++;
				if (currentLoadedImage_M3 >= M3_data.Count)
					currentLoadedImage_M3 = 0;
				M3_Display.Image = M3_data[currentLoadedImage_M3].composedImage;
			}
		}
		private void Previous_Picture_M3_Button_Click(object sender, EventArgs e)
		{
			if (M3_data.Count > 0)
			{
				currentLoadedImage_M3--;
				if (currentLoadedImage_M3 < 0)
					currentLoadedImage_M3 = M3_data.Count - 1;
				m1_Display.Image = M3_data[currentLoadedImage_M3].composedImage;
			}
		}
		private void Process_Images_Button_Click(object sender, EventArgs e)
		{
			if (DropBox.Items.Count > 0)
			{
				Process_Images_Button.Enabled = false;
				MarginBar.Enabled = false;

				progress_Bar.Maximum = DropBox.Items.Count + 1;
				backgroundWorker.RunWorkerAsync();
			}
		}
		void Processing()
		{
			backgroundWorker.ReportProgress(0);

			m1_data = new List<Shape_Data>();
			M3_data = new List<Shape_Data>();
			for (int i = 0; i < DropBox.Items.Count; i++)
			{
				string itemPath = DropBox.Items[i].ToString();
				string name = itemPath.Substring(itemPath.LastIndexOf('\\')+1);
				Image image = Image.FromFile(itemPath);
				if (itemPath.Contains("_L"))
					image.RotateFlip(RotateFlipType.RotateNoneFlipX);
				if (itemPath.Contains("_m1"))
				{
					Shape_Data tooth = new Shape_Data();
					tooth.baseImage = image;
					tooth.baseImageName = name;
					tooth.sampleName = Sample_Name_Box.Text;
					m1_data.Add(tooth);
				}
				else if (itemPath.Contains("_M3"))
				{
					Shape_Data tooth = new Shape_Data();
					tooth.baseImage = image;
					tooth.baseImageName = name;
					tooth.sampleName = Sample_Name_Box.Text;
					M3_data.Add(tooth);
				}
			} // sorting and flipping all imaages to right

			for (int k = 0; k < m1_data.Count; k++)
			{
				Bitmap bitmap = new Bitmap(m1_data[k].baseImage);
				bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY); // inverse y axis for processing (0;0 is left bottom with y - is up)
				float[,] values = new float[bitmap.Width, bitmap.Height];
				for (int i = 0; i < values.GetLength(0); i++)
				{
					for (int j = 0; j < values.GetLength(1); j++)
					{
						Color color = bitmap.GetPixel(i, j);
						values[i, j] = 1 - color.GetBrightness();
					}
				}
				float[,] values_extended = new float[values.GetLength(0) * 3 / 2, values.GetLength(1) * 3 / 2]; // needed to prevent transformations to go ouside of image
				for (int i = 0; i < values_extended.GetLength(0); i++)
				{
					for (int j = 0; j < values_extended.GetLength(1); j++)
					{
						values_extended[i, j] = 0;
					}
				} // fill values_extended with 0
				for (int i = values.GetLength(0) / 4, x = 0; i < values.GetLength(0) * 5 / 4; i++)
				{
					for (int j = values.GetLength(1) / 4, y = 0; j < values.GetLength(0) * 5 / 4; j++)
					{
						values_extended[i, j] = values[x, y];
						y++;
					}
					x++;
				} // add values in the center
				m1_data[k].rawValues = values_extended;
				m1_data[k].outlinePoints = GetOutline(values_extended, ToothPos.Lower);
				m1_data[k].POI = GetPointsOfInterest(m1_data[k]);
			}
			for (int k = 0; k < M3_data.Count; k++)
			{
				Bitmap bitmap = new Bitmap(M3_data[k].baseImage);
				bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY); // inverse y axis for processing (0;0 is left bottom with y - is up)
				float[,] values = new float[bitmap.Width, bitmap.Height];
				for (int i = 0; i < values.GetLength(0); i++)
				{
					for (int j = 0; j < values.GetLength(1); j++)
					{
						Color color = bitmap.GetPixel(i, j);
						values[i, j] = 1 - color.GetBrightness();
					}
				}
				float[,] values_extended = new float[values.GetLength(0) * 3 / 2, values.GetLength(1) * 3 / 2]; // needed to prevent transformations to go ouside of image
				for (int i = 0; i < values_extended.GetLength(0); i++)
				{
					for (int j = 0; j < values_extended.GetLength(1); j++)
					{
						values_extended[i, j] = 0;
					}
				} // fill values_extended with 0
				for (int i = values.GetLength(0) / 4, x = 0; i < values.GetLength(0) * 5 / 4; i++)
				{
					for (int j = values.GetLength(1) / 4, y = 0; j < values.GetLength(0) * 5 / 4; j++)
					{
						values_extended[i, j] = values[x, y];
						y++;
					}
					x++;
				} // add values in the center
				M3_data[k].rawValues = values_extended;
				M3_data[k].outlinePoints = GetOutline(values_extended, ToothPos.Upper);
				M3_data[k].POI = GetPointsOfInterest(M3_data[k]);
			}

			for (int i = 0; i < m1_data.Count; i++)
			{
				backgroundWorker.ReportProgress(i + 1);
				float[,] processed = new float[m1_data[i].rawValues.GetLength(0), m1_data[i].rawValues.GetLength(1)];
				for (int x = 0; x < processed.GetLength(0); x++)
				{
					for (int y = 0; y < processed.GetLength(1); y++)
					{
						processed[x, y] = m1_data[i].rawValues[x, y];
					}
				}
				Vector2 middlePoint = new Vector2(m1_data[i].rawValues.GetLength(0) / 2f, m1_data[i].rawValues.GetLength(1) / 2f);
				for (int j = 0; j < m1_data.Count; j++)
				{
					if (i != j)
					{
						float[,] addition = new float[m1_data[j].rawValues.GetLength(0), m1_data[j].rawValues.GetLength(1)];
						for (int x = 0; x < addition.GetLength(0); x++)
						{
							for (int y = 0; y < addition.GetLength(1); y++)
							{
								addition[x, y] = m1_data[j].rawValues[x, y];
							}
						}
						processed = AddValues(processed, addition, middlePoint, GenerateTransformationMatrix(m1_data[i].POI, m1_data[j].POI, middlePoint));
					}
				}
				for (int x = 0; x < processed.GetLength(0); x++)
				{
					for (int y = 0; y < processed.GetLength(1); y++)
					{
						processed[x, y] /= m1_data.Count;
						if (processed[x, y] < margin)
							processed[x, y] = 0f;
						if (processed[x, y] > 1f)
							processed[x, y] = 1f;
					}
				} // to average values
				m1_data[i].processedValues = processed;
				m1_data[i].composedImage = ImageFromValues(processed);
				m1_data[i].margin = margin;
			} // m1 processing
			for (int i = 0; i < M3_data.Count; i++)
			{
				backgroundWorker.ReportProgress(i + m1_data.Count);
				float[,] processed = new float[M3_data[i].rawValues.GetLength(0), M3_data[i].rawValues.GetLength(1)];
				for (int x = 0; x < processed.GetLength(0); x++)
				{
					for (int y = 0; y < processed.GetLength(1); y++)
					{
						processed[x, y] = M3_data[i].rawValues[x, y];
					}
				}
				Vector2 middlePoint = new Vector2(M3_data[i].rawValues.GetLength(0) / 2f, M3_data[i].rawValues.GetLength(1) / 2f);
				for (int j = 0; j < M3_data.Count; j++)
				{
					if (i != j)
					{
						float[,] addition = new float[M3_data[j].rawValues.GetLength(0), M3_data[j].rawValues.GetLength(1)];
						for (int x = 0; x < addition.GetLength(0); x++)
						{
							for (int y = 0; y < addition.GetLength(1); y++)
							{
								addition[x, y] = M3_data[j].rawValues[x, y];
							}
						}
						processed = AddValues(processed, addition, middlePoint, GenerateTransformationMatrix(M3_data[i].POI, M3_data[j].POI, middlePoint));
					}
				}
				for (int x = 0; x < processed.GetLength(0); x++)
				{
					for (int y = 0; y < processed.GetLength(1); y++)
					{
						processed[x, y] /= M3_data.Count;
						if (processed[x, y] < margin)
							processed[x, y] = 0f;
						if (processed[x, y] > 1f)
							processed[x, y] = 1f;
					}
				} // to average values
				M3_data[i].processedValues = processed;
				M3_data[i].composedImage = ImageFromValues(processed);
				M3_data[i].margin = margin;
			} // M3 processing

			backgroundWorker.ReportProgress(m1_data.Count + M3_data.Count + 1);
			
			currentSample.m1 = FindBestAverage(m1_data);
			currentSample.M3 = FindBestAverage(M3_data);

			foreach(Shape_Data data in m1_data)
			{
				data.matchingBest = ShapeComparison.Comparasion(data, currentSample.m1);
			}
			foreach(Shape_Data data in M3_data)
			{
				data.matchingBest = ShapeComparison.Comparasion(data, currentSample.M3);
			}
		}
		List<Vector2> GetOutline(float[,] values, ToothPos toothPos)
		{
			Vector2 start = new Vector2();
			Vector2 end = new Vector2();

			if (toothPos == ToothPos.Lower)
			{
				for (int y = 0; y < values.GetLength(1) / 2; y++)
				{
					int mostLeft = -1;
					bool leftPartEnded = false;
					int mostRight = -1;
					for (int x = 0; x < values.GetLength(0); x++)
					{
						if (values[x, y] >= 0.5f)
						{
							if (mostLeft == -1)
								mostLeft = x;
							if (leftPartEnded)
								mostRight = x;
						}
						else
						{
							if (mostLeft != -1)
							{
								leftPartEnded = true;
							}
						}
					}
					if (mostLeft != -1 && mostRight != -1)
					{
						start = new Vector2(mostLeft, y);
						end = new Vector2(mostRight, y);
						break;
					}
				}
			} // search for start and end to trace outline
			else
			{
				for (int y = values.GetLength(1) - 1; y >= values.GetLength(1) / 2; y--)
				{
					int mostLeft = -1;
					bool leftPartEnded = false;
					int mostRight = -1;
					for (int x = 0; x < values.GetLength(0); x++)
					{
						if (values[x, y] >= 0.5f)
						{
							if (mostLeft == -1)
								mostLeft = x;
							if (leftPartEnded)
								mostRight = x;
						}
						else
						{
							if (mostLeft != -1)
							{
								leftPartEnded = true;
							}
						}
					}
					if (mostLeft != -1 && mostRight != -1)
					{
						start = new Vector2(mostRight, y);
						end = new Vector2(mostLeft, y);
						break;
					}
				}
			}

			List<Vector2> outline = new List<Vector2>();
			bool AtTheEdge(Point p)
			{
				Point left = new Point(p.X - 1, p.Y);
				Point up = new Point(p.X, p.Y + 1);
				Point right = new Point(p.X + 1, p.Y);
				Point down = new Point(p.X, p.Y - 1);
				return values[p.X, p.Y] >= 0.5f && (values[left.X, left.Y] < 0.5f || values[up.X, up.Y] < 0.5f || values[right.X, right.Y] < 0.5f || values[down.X, down.Y] < 0.5f);
			}

			outline.Add(start);
			Vector2 currentPoint = start;
			if (toothPos == ToothPos.Lower)
			{
				while (currentPoint != end)
				{
					Vector2 nextPoint = new Vector2(currentPoint.X - 1, currentPoint.Y); // LEFT
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X - 1, currentPoint.Y + 1); // LEFT_UP
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X, currentPoint.Y + 1); // UP
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X + 1, currentPoint.Y + 1); // UP_RIGHT
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X + 1, currentPoint.Y); // RIGHT
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X + 1, currentPoint.Y - 1); // RIGHT_DOWN
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X, currentPoint.Y - 1); // DOWN
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X - 1, currentPoint.Y - 1); // DOWN_LEFT
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					break;
				}
			}
			else
			{
				while (currentPoint != end)
				{
					Vector2 nextPoint = new Vector2(currentPoint.X + 1, currentPoint.Y); // RIGHT
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X + 1, currentPoint.Y - 1); // RIGHT_DOWN
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X, currentPoint.Y - 1); // DOWN
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X - 1, currentPoint.Y - 1); // DOWN_LEFT
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X - 1, currentPoint.Y); // LEFT
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X - 1, currentPoint.Y + 1); // LEFT_UP
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X, currentPoint.Y + 1); // UP
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					nextPoint = new Vector2(currentPoint.X + 1, currentPoint.Y + 1); // UP_RIGHT
					if (AtTheEdge(new Point((int)nextPoint.X, (int)nextPoint.Y)) && !outline.Contains(nextPoint))
					{
						currentPoint = nextPoint;
						outline.Add(currentPoint);
						continue;
					}
					break;
				}
			}

			// Get relevant part of outline

			float minDstSqr = float.MaxValue;
			int halfOutlineLength = outline.Count / 2;
			int[] narrowestParIndices = new int[2];
			for (int i = 0; i < outline.Count; i++)
			{
				for (int j = i + halfOutlineLength; j < outline.Count; j++) // since narrowest part at the start of loop we can exclude points to close to each other (here this dst is half)
				{
					float sqrDst = Vector2.DistanceSquared(outline[i], outline[j]);
					if (sqrDst < minDstSqr)
					{
						minDstSqr = sqrDst;
						narrowestParIndices[0] = i;
						narrowestParIndices[1] = j;
					}
				}
			}
			List<Vector2> properOutLine = new List<Vector2>();
			for(int i = narrowestParIndices[0]; i <= narrowestParIndices[1]; i++)
			{
				properOutLine.Add(outline[i]);
			}

			return properOutLine;
		} // tracing outline
		List<Vector2> GetPointsOfInterest(Shape_Data tooth) // For Lagurus lagurus only (m1 & M3)
		{
			List<Vector2> outlinePoints = tooth.outlinePoints;
			ToothPos toothPos = tooth.toothPos;
			List<Vector2> pointsOfInterest = new List<Vector2>();

			Vector2 ProjectPointOnLine(Vector2 line_A, Vector2 line_B, Vector2 point)
			{
				if (line_A.X == line_B.X)
				{
					return new Vector2(line_A.X, point.Y);
				}
				double m = (double)(line_B.Y - line_A.Y) / (line_B.X - line_A.X);
				double b = line_A.Y - (m * line_A.X);

				double x = (m * point.Y + point.X - m * b) / (m * m + 1);
				double y = (m * m * point.Y + m * point.X + b) / (m * m + 1);

				return new Vector2((int)x, (int)y);
			}

			pointsOfInterest.Add(outlinePoints[0]);
			pointsOfInterest.Add(outlinePoints[outlinePoints.Count-1]); // start and end of outline is narrowest part

			float minDstSqr = float.MaxValue;
			int halfOutlineLength = outlinePoints.Count / 2;
			Vector2[] narrowestPart = new Vector2[2];
			for (int i = 0; i < outlinePoints.Count; i++)
			{
				for (int j = i + halfOutlineLength; j < outlinePoints.Count; j++) // since narrowest part at the start of loop we can exclude points to close to each other (here this dst is half)
				{
					float sqrDst = Vector2.DistanceSquared(outlinePoints[i], outlinePoints[j]);
					if (sqrDst < minDstSqr)
					{
						minDstSqr = sqrDst;
						narrowestPart[0] = outlinePoints[i];
						narrowestPart[1] = outlinePoints[j];
					}
				}
			}
			pointsOfInterest.AddRange(narrowestPart);
			
			Vector2 middlePoint = new Vector2((narrowestPart[0].X + narrowestPart[1].X) / 2, (narrowestPart[0].Y + narrowestPart[1].Y) / 2);
			Vector2 furthestPoint = new Vector2();
			float maxDstSqr = 0;
			foreach (Vector2 p in outlinePoints)
			{
				float dstSqr = Vector2.DistanceSquared(p, middlePoint);
				if (dstSqr > maxDstSqr)
				{
					maxDstSqr = dstSqr;
					furthestPoint = p;
				}
			} // search for furthest point from narrowest part
			pointsOfInterest.Add(furthestPoint);

			Vector2 furthestLeft = new Vector2();
			Vector2 furthestRight = new Vector2();
			float leftMaxDstSqr = 0;
			float rightMaxDstSqr = 0;

			Vector2 leftProjected = new Vector2(); //needed for calculations later
			Vector2 rightProjected = new Vector2(); //needed for calculations later

			if (toothPos == ToothPos.Lower)
			{
				foreach (Vector2 p in outlinePoints)
				{
					if (p.Y < middlePoint.Y)
						continue;
					Vector2 projected = ProjectPointOnLine(middlePoint, furthestPoint, p);
					if (projected != Vector2.Zero)
					{
						float dstSqr = Vector2.DistanceSquared(projected, p);
						if (p.X >= projected.X)
						{
							if (dstSqr > rightMaxDstSqr)
							{
								rightMaxDstSqr = dstSqr;
								furthestRight = p;
								rightProjected = projected;
							}
						}
						else
						{
							if (dstSqr > leftMaxDstSqr)
							{
								leftMaxDstSqr = dstSqr;
								furthestLeft = p;
								leftProjected = projected;
							}
						}
					}
				} // search for furthest right and left points
			}
			else
			{
				foreach (Vector2 p in outlinePoints)
				{
					if (p.Y > middlePoint.Y)
						continue;
					Vector2 projected = ProjectPointOnLine(middlePoint, furthestPoint, p);
					if (projected != Vector2.Zero)
					{
						float dstSqr = Vector2.DistanceSquared(projected, p);
						if (p.X >= projected.X)
						{
							if (dstSqr > rightMaxDstSqr)
							{
								rightMaxDstSqr = dstSqr;
								furthestRight = p;
								rightProjected = projected;
							}
						}
						else
						{
							if (dstSqr > leftMaxDstSqr)
							{
								leftMaxDstSqr = dstSqr;
								furthestLeft = p;
								leftProjected = projected;
							}
						}
					}
				} // search for furthest right and left points
			}

			tooth.asymmetry = (rightProjected - leftProjected).Length() / (middlePoint - furthestPoint).Length(); // calculating assymetry (no use here, but needed for other purposes)
			tooth.ruggedness = outlinePoints.Count / (middlePoint - furthestPoint).Length(); // calculating ruggedness (no use here, but needed for other purposes)

			pointsOfInterest.Add(furthestLeft);
			pointsOfInterest.Add(furthestRight);

			Vector2 closestLeft = new Vector2();
			Vector2 closestRight = new Vector2();
			float leftMinDst = float.MaxValue;
			float rightMinDst = float.MaxValue;

			if (toothPos == ToothPos.Lower)
			{
				foreach (Vector2 p in outlinePoints)
				{
					if (p.X >= rightProjected.X && p.Y >= furthestRight.Y)
					{
						float sqrDst = Vector2.DistanceSquared(rightProjected, p);
						if (sqrDst < rightMinDst)
						{
							rightMinDst = sqrDst;
							closestRight = p;
						}
					}
					if (p.X <= leftProjected.X && p.Y >= furthestLeft.Y)
					{
						float sqrDst = Vector2.DistanceSquared(leftProjected, p);
						if (sqrDst < leftMinDst)
						{
							leftMinDst = sqrDst;
							closestLeft = p;
						}
					}
				} // search for closest right and left points
			}
			else
			{
				foreach (Vector2 p in outlinePoints)
				{
					if (p.X >= rightProjected.X && p.Y <= furthestRight.Y)
					{
						float sqrDst = Vector2.DistanceSquared(rightProjected, p);
						if (sqrDst < rightMinDst)
						{
							rightMinDst = sqrDst;
							closestRight = p;
						}
					}
					if (p.X <= leftProjected.X && p.Y <= furthestLeft.Y)
					{
						float sqrDst = Vector2.DistanceSquared(leftProjected, p);
						if (sqrDst < leftMinDst)
						{
							leftMinDst = sqrDst;
							closestLeft = p;
						}
					}
				} // search for closest right and left points
			}
			pointsOfInterest.Add(closestLeft);
			pointsOfInterest.Add(closestRight);

			return pointsOfInterest;
		}
		public static float[,] GenerateTransformationMatrix(List<Vector2> A, List<Vector2> B, Vector2 middlePoint) // B to match A
		{
			List<Vector2> converted_A = new List<Vector2>();
			List<Vector2> converted_B = new List<Vector2>();

			foreach(Vector2 p in A)
			{
				converted_A.Add(new Vector2(p.X - middlePoint.X, p.Y - middlePoint.Y));
			}
			foreach(Vector2 p in B)
			{
				converted_B.Add(new Vector2(p.X - middlePoint.X, p.Y - middlePoint.Y));
			} // conversion points into vectors from the middle to future manipulations

			float[,] transformationMatrix = new float[3,3];
			float[,] rotationMatrix = new float[3,3];
			float[,] scaleMatrix = new float[3,3];

			Vector2 posDifference = Vector2.Zero;
			for (int i = 0; i < converted_B.Count; i++)
			{
				posDifference += converted_A[i] - converted_B[i];
			}
			posDifference /= converted_B.Count;

			// translation

			transformationMatrix[0, 0] = 1f;
			transformationMatrix[0, 1] = 0f;
			transformationMatrix[0, 2] = 0f;
			transformationMatrix[1, 0] = 0f;
			transformationMatrix[1, 1] = 1f;
			transformationMatrix[1, 2] = 0f;
			transformationMatrix[2, 0] = posDifference.X;
			transformationMatrix[2, 1] = posDifference.Y;
			transformationMatrix[2, 2] = 1f;

			for (int i = 0; i < converted_B.Count; i++)
			{
				converted_B[i] += posDifference;
			}

			//rotation

			double averageAngle = 0f;
			for (int i = 0; i < converted_B.Count; i++)
			{
				float dot = converted_B[i].X * converted_A[i].X + converted_B[i].Y * converted_A[i].Y;
				float det = converted_B[i].X * converted_A[i].Y - converted_B[i].Y * converted_A[i].X;
				averageAngle += Math.Atan2(det, dot);
			}
			averageAngle /= converted_B.Count;

			rotationMatrix[0, 0] = (float)Math.Cos(averageAngle);
			rotationMatrix[0, 1] = (float)Math.Sin(averageAngle);
			rotationMatrix[0, 2] = 0f;
			rotationMatrix[1, 0] = -(float)Math.Sin(averageAngle);
			rotationMatrix[1, 1] = (float)Math.Cos(averageAngle);
			rotationMatrix[1, 2] = 0f;
			rotationMatrix[2, 0] = 0f;
			rotationMatrix[2, 1] = 0f;
			rotationMatrix[2, 2] = 1f;

			//scaling

			float averageScaling = 0f;
			for (int i = 0; i < converted_B.Count; i++)
			{
				averageScaling += converted_A[i].Length() / converted_B[i].Length();
			}
			averageScaling /= converted_B.Count;

			scaleMatrix[0, 0] = averageScaling;
			scaleMatrix[0, 1] = 0f;
			scaleMatrix[0, 2] = 0f;
			scaleMatrix[1, 0] = 0f;
			scaleMatrix[1, 1] = averageScaling;
			scaleMatrix[1, 2] = 0f;
			scaleMatrix[2, 0] = 0f;
			scaleMatrix[2, 1] = 0f;
			scaleMatrix[2, 2] = 1f;

			// adding matrices

			transformationMatrix = MatrixMultiplication(transformationMatrix, rotationMatrix);
			transformationMatrix = MatrixMultiplication(transformationMatrix, scaleMatrix);

			//inversion

			float determinant =
				transformationMatrix[0, 0] * (transformationMatrix[1, 1] * transformationMatrix[2, 2] - transformationMatrix[1, 2] * transformationMatrix[2, 1]) -
				transformationMatrix[1, 0] * (transformationMatrix[0, 1] * transformationMatrix[2, 2] - transformationMatrix[0, 2] * transformationMatrix[2, 1]) +
				transformationMatrix[2, 0] * (transformationMatrix[0, 1] * transformationMatrix[1, 2] - transformationMatrix[0, 2] * transformationMatrix[1, 1]);

			float[,] transformationMatrix_T = new float[3,3]; // transposed transformationMatrix
			for (int i = 0; i < 3; i ++)
			{
				for (int j = 0; j < 3; j++)
				{
					transformationMatrix_T[i, j] = transformationMatrix[j, i];
				}
			}

			float[,] inverseTransformationMatrix = new float[3, 3]; // Adjugate to transformationMatrix_T

			inverseTransformationMatrix[0, 0] = (transformationMatrix_T[1, 1] * transformationMatrix_T[2, 2] - transformationMatrix_T[1, 2] * transformationMatrix_T[2, 1]) / determinant;
			inverseTransformationMatrix[0, 1] = -(transformationMatrix_T[1, 0] * transformationMatrix_T[2, 2] - transformationMatrix_T[1, 2] * transformationMatrix_T[2, 0]) / determinant;
			inverseTransformationMatrix[0, 2] = (transformationMatrix_T[1, 0] * transformationMatrix_T[2, 1] - transformationMatrix_T[1, 1] * transformationMatrix_T[2, 0]) / determinant;
			inverseTransformationMatrix[1, 0] = -(transformationMatrix_T[0, 1] * transformationMatrix_T[2, 2] - transformationMatrix_T[0, 2] * transformationMatrix_T[2, 1]) / determinant;
			inverseTransformationMatrix[1, 1] = (transformationMatrix_T[0, 0] * transformationMatrix_T[2, 2] - transformationMatrix_T[0, 2] * transformationMatrix_T[2, 0]) / determinant;
			inverseTransformationMatrix[1, 2] = -(transformationMatrix_T[0, 0] * transformationMatrix_T[2, 1] - transformationMatrix_T[0, 1] * transformationMatrix_T[2, 0]) / determinant;
			inverseTransformationMatrix[2, 0] = (transformationMatrix_T[0, 1] * transformationMatrix_T[1, 2] - transformationMatrix_T[0, 2] * transformationMatrix_T[1, 1]) / determinant;
			inverseTransformationMatrix[2, 1] = -(transformationMatrix_T[0, 0] * transformationMatrix_T[1, 2] - transformationMatrix_T[0, 2] * transformationMatrix_T[1, 0]) / determinant;
			inverseTransformationMatrix[2, 2] = (transformationMatrix_T[0, 0] * transformationMatrix_T[1, 1] - transformationMatrix_T[0, 1] * transformationMatrix_T[1, 0]) / determinant;

			return inverseTransformationMatrix;
		}
		static float[,] MatrixMultiplication(float[,] matrix_1, float[,] matrix_2)
		{
			float[,] result = new float[matrix_2.GetLength(0), matrix_1.GetLength(1)];
			for (int i = 0; i < result.GetLength(0); i++)
			{
				for (int j = 0; j < result.GetLength(1); j++)
				{
					result[i, j] = 0f;
					for (int k = 0; k < matrix_1.GetLength(0); k++)
					{
						result[i, j] += matrix_1[k, j] * matrix_2[i, k];
					}
				}
			}
			return result;
		}
		public static float[,] AddValues(float[,] values_A, float[,] values_B, Vector2 middlePoint, float[,] transformationMatrix)
		{
			for (int i = 0; i < values_A.GetLength(0); i++)
			{
				for (int j = 0; j < values_A.GetLength(1); j++)
				{
					float[,] input = new float[1, 3];
					input[0, 0] = i - middlePoint.X;
					input[0, 1] = j - middlePoint.Y;
					input[0, 2] = 1;

					float[,] output = MatrixMultiplication(transformationMatrix, input);
					int x = (int)output[0, 0] + (int)middlePoint.X;
					int y = (int)output[0, 1] + (int)middlePoint.Y;
					if (x >= 0 && x < values_B.GetLength(0) && y >=0 && y < values_B.GetLength(1))
						values_A[i, j] += values_B[x, y];
				}
			}
			return values_A;
		}
		float GetAverageValue(float[,] values)
		{
			float value = 0;
			int pixelsAmount = 0;
			for (int i = 0; i < values.GetLength(0); i++)
			{
				for (int j = 0; j < values.GetLength(1); j++)
				{
					if (values[i,j] > 0)
					{
						value += values[i, j];
						pixelsAmount++;
					}
				}
			}
			return value / pixelsAmount;
		}
		Shape_Data FindBestAverage(List<Shape_Data> teeth)
		{
			int bestOneYet = -1;
			float bestAverage = 0f;
			for (int i = 0; i < teeth.Count; i++)
			{
				float currentAverage = GetAverageValue(teeth[i].processedValues);
				if (currentAverage > bestAverage)
				{
					bestAverage = currentAverage;
					bestOneYet = i;
				}
			}
			if (bestOneYet == -1)
				return null;
			return teeth[bestOneYet];
		}
		Image ImageFromValues(float[,] values)
		{
			Bitmap bitmap = new Bitmap(values.GetLength(0), values.GetLength(1));
			
			for (int i = 0; i < values.GetLength(0); i++)
			{
				for (int j = 0; j < values.GetLength(1); j++)
				{
					int value = (int)((1f - values[i, j]) * 255f);
					Color color = Color.FromArgb(value, value, value);
					bitmap.SetPixel(i, j, color);
				}
			}
			bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY); // inversing y-axis back
			return bitmap;
		}
		void Save()
		{
			Stream stream;
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				string folderName = folderBrowserDialog.SelectedPath + "\\";
				saveFileDialog.FileName = folderName + "Best_FullData.tsa";

				if ((stream = saveFileDialog.OpenFile()) != null)
				{
					BinaryFormatter formatter = new BinaryFormatter();

					Sample_ForSaving saveFile = new Sample_ForSaving();
					saveFile.sampleName = Sample_Name_Box.Text;
					saveFile.m1 = new Shape_Data_ForSaving(currentSample.m1);
					saveFile.M3 = new Shape_Data_ForSaving(currentSample.M3);

					formatter.Serialize(stream, saveFile);
					stream.Close();
				}

				if (m1_data.Count > 0)
				{
					DirectoryInfo m1_folder = Directory.CreateDirectory(folderName + @"AllComposedImages\m1\");
					for (int i = 0; i < m1_data.Count; i++)
					{
						m1_data[i].composedImage.Save(Path.Combine(m1_folder.FullName, "Based_on_" + m1_data[i].baseImageName + ".png"));
					}
				}
				if (M3_data.Count > 0)
				{
					DirectoryInfo M3_folder = Directory.CreateDirectory(folderName + @"AllComposedImages\M3\");
					for (int i = 0; i < M3_data.Count; i++)
					{
						M3_data[i].composedImage.Save(Path.Combine(M3_folder.FullName, "Based_on_" + M3_data[i].baseImageName + ".png"));
					}
				}

				if (currentSample.m1 != null)
					currentSample.m1.composedImage.Save(folderName + "Best_based_on_" + currentSample.m1.baseImageName + ".png");
				if (currentSample.M3 != null)
					currentSample.M3.composedImage.Save(folderName + "Best_based_on_" + currentSample.M3.baseImageName + ".png");

				DataGridView dataGrid_m1 = new DataGridView();
				DataGridView dataGrid_M3 = new DataGridView();

				DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[5];
				for(int i = 0; i < columns.Length; i++)
				{
					columns[i] = new DataGridViewTextBoxColumn();
				}

				dataGrid_m1.Columns.AddRange(columns);
				string[] raw = new string[] { "Name","Margin", "Asymmetry", "Ruggedness", "Similarity to Best" };
				dataGrid_m1.Rows.Add(raw);
				for(int i = 0; i < m1_data.Count; i++)
				{
					raw[0] = m1_data[i].baseImageName;
					raw[1] = m1_data[i].margin.ToString(CultureInfo.InvariantCulture);
					raw[2] = m1_data[i].asymmetry.ToString(CultureInfo.InvariantCulture);
					raw[3] = m1_data[i].ruggedness.ToString(CultureInfo.InvariantCulture);
					raw[4] = m1_data[i].matchingBest.ToString(CultureInfo.InvariantCulture);

					dataGrid_m1.Rows.Add(raw);
				}

				columns = new DataGridViewTextBoxColumn[5];
				for (int i = 0; i < columns.Length; i++)
				{
					columns[i] = new DataGridViewTextBoxColumn();
				}

				dataGrid_M3.Columns.AddRange(columns);
				raw = new string[] { "Name", "Margin", "Asymmetry", "Ruggedness", "Similarity to Best" };
				dataGrid_M3.Rows.Add(raw);
				for(int i = 0; i < M3_data.Count; i++)
				{
					raw[0] = M3_data[i].baseImageName;
					raw[1] = M3_data[i].margin.ToString(CultureInfo.InvariantCulture);
					raw[2] = M3_data[i].asymmetry.ToString(CultureInfo.InvariantCulture);
					raw[3] = M3_data[i].ruggedness.ToString(CultureInfo.InvariantCulture);
					raw[4] = M3_data[i].matchingBest.ToString(CultureInfo.InvariantCulture);

					dataGrid_M3.Rows.Add(raw);
				}


				// creating Excel Application  
				Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
				Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
				Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
				app.Visible = false;
				worksheet = workbook.Sheets["Лист1"]; // default first
				worksheet = workbook.ActiveSheet;
				worksheet.Name = "m1";

				bool retry;
				for (int i = 1; i < dataGrid_m1.Columns.Count + 1; i++)
				{
					worksheet.Cells[1, i] = dataGrid_m1.Columns[i - 1].HeaderText;
				}
				for (int i = 0; i < dataGrid_m1.Rows.Count - 1; i++)
				{
					for (int j = 0; j < dataGrid_m1.Columns.Count; j++)
					{
						retry = true;
						do
						{
							try
							{
								worksheet.Cells[i + 1, j + 1] = dataGrid_m1.Rows[i].Cells[j].Value.ToString(); // sometimes Excel fails here so we try again;
								retry = false;
							}
							catch (Exception exp)
							{
								System.Threading.Thread.Sleep(10);
							}
						} while (retry);
					}
				}

				retry = true;
				do
				{
					try
					{
						workbook.Sheets.Add(); // sometimes Excel fails at anything...
						retry = false;
					}
					catch (Exception exp)
					{
						System.Threading.Thread.Sleep(10);
					}
				} while (retry);

				worksheet = workbook.Sheets["Лист2"]; // default second
				worksheet = workbook.ActiveSheet;
				do
				{
					try
					{
						worksheet.Name = "M3"; // sometimes Excel fails at anything...
						retry = false;
					}
					catch (Exception exp)
					{
						System.Threading.Thread.Sleep(10);
					}
				} while (retry); // Excel is broken even here..................................

				for (int i = 1; i < dataGrid_M3.Columns.Count + 1; i++)
				{
					worksheet.Cells[1, i] = dataGrid_M3.Columns[i - 1].HeaderText;
				}
				for (int i = 0; i < dataGrid_M3.Rows.Count - 1; i++)
				{
					for (int j = 0; j < dataGrid_M3.Columns.Count; j++)
					{
						retry = true;
						do
						{
							try
							{
								worksheet.Cells[i + 1, j + 1] = dataGrid_M3.Rows[i].Cells[j].Value.ToString(); // sometimes Excel fails here so we try again;
								retry = false;
							}
							catch (Exception exp)
							{
								System.Threading.Thread.Sleep(10);
							}
						} while (retry);
					}
				}

				workbook.SaveAs(folderName + Sample_Name_Box.Text + "_DataTable.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				app.Quit();
			}
		}
		private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			Processing();
		}
		private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			progress_Bar.Value = e.ProgressPercentage;
			if (e.ProgressPercentage == 0)
				progress_Bar_Textbox.Text = "Importing images...";
			else
			{
				if (e.ProgressPercentage == (DropBox.Items.Count + 1))
					progress_Bar_Textbox.Text = "Finishing...";
				else
					progress_Bar_Textbox.Text = "Processing: " + e.ProgressPercentage + " out of " + DropBox.Items.Count;
			}
			progress_Bar_Textbox.Refresh();
			progress_Bar.Update();
		}
		private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			progress_Bar_Textbox.Text = "Done";
			progress_Bar_Textbox.Refresh();
			Process_Images_Button.Enabled = true;
			MarginBar.Enabled = true;
		}
		private void Clear_DropBox_Button_Click(object sender, EventArgs e)
		{
			DropBox.Items.Clear();
		}
		private void Save_Button_Click(object sender, EventArgs e)
		{
			Save_Button.Enabled = false;
			Save();
			Save_Button.Enabled = true;
		}
		private void CompareShapesSwitch_Click(object sender, EventArgs e)
		{
			ShapeComparison comparisonForm = new ShapeComparison(this);
			Hide();
			comparisonForm.ShowDialog();
		}
		private void MarginBar_Scroll(object sender, EventArgs e)
		{
			margin = MarginBar.Value / 100f;
			MarginDisplayText.Text = "Magrin: " + margin;
		}
	}
	public class Shape_Data
	{
		public string sampleName;
		public string baseImageName;
		public Image composedImage;
		public Image baseImage;
		public float[,] rawValues;
		public float[,] processedValues;
		public List<Vector2> outlinePoints;
		public List<Vector2> POI;
		public float asymmetry;
		public float ruggedness;
		public float matchingBest;
		public float margin;
		public ToothPos toothPos;

		public Shape_Data(){}
		public Shape_Data(Shape_Data_ForSaving savedData)
		{
			sampleName = savedData.sampleName;
			baseImageName = savedData.baseImageName;
			composedImage = savedData.composedImage;
			baseImage = savedData.baseImage;
			rawValues = savedData.rawValues;
			processedValues = savedData.processedValues;
			outlinePoints = new List<Vector2>();
			for (int i = 0; i < savedData.outlinePoints.Count; i++)
			{
				outlinePoints.Add(new Vector2(savedData.outlinePoints[i][0], savedData.outlinePoints[i][1]));
			}
			POI = new List<Vector2>();
			for (int i = 0; i < savedData.POI.Count; i++)
			{
				POI.Add(new Vector2(savedData.POI[i][0], savedData.POI[i][1]));
			}
			asymmetry = savedData.asymmetry;
			ruggedness = savedData.ruggedness;
			matchingBest = savedData.matchingBest;
			margin = savedData.margin;
			toothPos = savedData.toothPos_isLower ? ToothPos.Lower : ToothPos.Upper;
		}
	}
	public class Sample
	{
		public string sampleName;
		public Shape_Data m1;
		public Shape_Data M3;
	}
	[System.Serializable]
	public class Shape_Data_ForSaving
	{
		public string sampleName;
		public string baseImageName;
		public Image composedImage;
		public Image baseImage;
		public float[,] rawValues;
		public float[,] processedValues;
		public List<float[]> outlinePoints;
		public List<float[]> POI;
		public float asymmetry;
		public float ruggedness;
		public float matchingBest;
		public float margin;
		public bool toothPos_isLower;

		public Shape_Data_ForSaving(Shape_Data shape_Data)
		{
			sampleName = shape_Data.sampleName;
			baseImageName = shape_Data.baseImageName;
			composedImage = shape_Data.composedImage;
			baseImage = shape_Data.baseImage;
			rawValues = shape_Data.rawValues;
			processedValues = shape_Data.processedValues;
			outlinePoints = new List<float[]>();
			foreach (Vector2 v in shape_Data.outlinePoints)
			{
				float[] temp = new float[2];
				temp[0] = v.X;
				temp[1] = v.Y;
				outlinePoints.Add(temp);
			}
			POI = new List<float[]>();
			foreach (Vector2 v in shape_Data.POI)
			{
				float[] temp = new float[2];
				temp[0] = v.X;
				temp[1] = v.Y;
				POI.Add(temp);
			}
			asymmetry = shape_Data.asymmetry;
			ruggedness = shape_Data.ruggedness;
			matchingBest = shape_Data.matchingBest;
			margin = shape_Data.margin;
			toothPos_isLower = shape_Data.toothPos == ToothPos.Lower ? true : false;
		}
	}
	[System.Serializable]
	public class Sample_ForSaving
	{
		public string sampleName;
		public Shape_Data_ForSaving m1;
		public Shape_Data_ForSaving M3;
	}
}