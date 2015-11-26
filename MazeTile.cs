using System;
using System.Linq;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace Litmus
{
	[Flags]
	public enum Movements2D
	{
		Forward = 1,
		Backward = 2,
		Left = 4, 
		Right = 8,
	}

	public enum Movements3D
	{
		UpForward = 16,
		DownForward = 32,
		UpBackward = 64,
		DownBackward = 128,
		UpLeft = 256,
		DownLeft = 512,
		UpRight = 1024,
		DownRight = 2048,
	}

	public class MazeTile : UIView
	{
		const float GUTTER = 0.25f;
		const float STROKE_WIDTH = 10f;
		readonly UIColor STROKE = UIColor.White;
		readonly UIColor FILL = UIColor.Red;

		public MazeTile (Movements2D movements)
		{
			this.Movements2D = movements;
		}

		Movements2D Movements2D {
			get;
			set;
		}

		public override void Draw (CoreGraphics.CGRect rect)
		{
			this.BackgroundColor = UIColor.FromRGBA (0, 0, 0, 0);
			switch (Movements2D) 
			{
			case Movements2D.Forward:
				{
					DrawForward ();
					break;
				}
			case Movements2D.Backward:
				{
					DrawBackward ();
					break;
				}
			case Movements2D.Right:
				{
					DrawRight ();
					break;
				}
			case Movements2D.Left:
				{
					DrawLeft ();
					break;
				}
			case Movements2D.Forward | Movements2D.Right:
				{
					DrawForwardRight ();
					break;
				}
			case Movements2D.Forward | Movements2D.Backward:
				{
					DrawForwardBackward ();
					break;
				}
			case Movements2D.Forward | Movements2D.Left:
				{
					DrawForwardLeft ();
					break;
				}
			case Movements2D.Backward | Movements2D.Right:
				{
					DrawBackwardRight ();
					break;
				}
			case Movements2D.Backward | Movements2D.Left:
				{
					DrawBackwardLeft ();
					break;
				}
			case Movements2D.Forward | Movements2D.Right | Movements2D.Left:
				{
					DrawForwardRightLeft ();
					break;
				}
			case Movements2D.Backward | Movements2D.Right | Movements2D.Left:
				{
					DrawBackwardRightLeft ();
					break;
				}
			case Movements2D.Right | Movements2D.Forward | Movements2D.Backward:
				{
					DrawRightForwardBackward ();
					break;
				}
			case Movements2D.Left | Movements2D.Forward | Movements2D.Backward:
				{
					DrawLeftForwardBackward ();
					break;
				}
			case Movements2D.Left | Movements2D.Right:
				{
					DrawLeftRight ();
					break;
				}
			case Movements2D.Forward | Movements2D.Backward | Movements2D.Left | Movements2D.Right:
				{
					DrawForwardRightBackwardLeft ();
					break;
				}
			}
		}

		private void DrawForward()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var y1 = 0;
				var y2 = this.Frame.Height / 2f;

				var points = new CGPoint[] {
					new CGPoint (x1, y1),
					new CGPoint (x1, y2),
					new CGPoint (x2, y2),
					new CGPoint (x2, y1)
				};

				body.AddLines (points);
				body.CloseSubpath ();

				outline.AddLines (points);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawBackward()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var y1 = this.Frame.Height;
				var y2 = this.Frame.Height / 2f;

				var points = new CGPoint[] {
					new CGPoint (x1, y1),
					new CGPoint (x1, y2),
					new CGPoint (x2, y2),
					new CGPoint (x2, y1)
				};

				body.AddLines (points);
				body.CloseSubpath ();

				outline.AddLines (points);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawRight()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x1 = this.Frame.Width;
				var x2 = this.Frame.Width / 2f;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;

				var points = new CGPoint[] {
					new CGPoint (x1, y1),
					new CGPoint (x2, y1),
					new CGPoint (x2, y2),
					new CGPoint (x1, y2),
				};

				body.AddLines (points);
				body.CloseSubpath ();

				outline.AddLines (points);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawLeft()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x1 = 0;
				var x2 = this.Frame.Width / 2f;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;

				var points = new CGPoint[] {
					new CGPoint (x1, y1),
					new CGPoint (x2, y1),
					new CGPoint (x2, y2),
					new CGPoint (x1, y2),
				};

				body.AddLines (points);
				body.CloseSubpath ();

				outline.AddLines (points);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawForwardRight()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x2, y0),
					new CGPoint (x2, y1),
					new CGPoint (x3, y1),
				};
				var blPoints = new CGPoint[] {
					new CGPoint (x3, y2),
					new CGPoint (x1, y2),
					new CGPoint (x1, y0),
				};

				body.AddLines (trPoints.Union(blPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (blPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawForwardBackward()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x2, y0),
					new CGPoint (x2, y3),
				};
				var blPoints = new CGPoint[] {
					new CGPoint (x1, y3),
					new CGPoint (x1, y0),
				};

				body.AddLines (trPoints.Union(blPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (blPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawLeftRight()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x0, y1),
					new CGPoint (x3, y1),
				};
				var blPoints = new CGPoint[] {
					new CGPoint (x3, y2),
					new CGPoint (x0, y2),
				};

				body.AddLines (trPoints.Union(blPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (blPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawForwardLeft()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x1, y0),
					new CGPoint (x1, y1),
					new CGPoint (x0, y1),
				};
				var blPoints = new CGPoint[] {
					new CGPoint (x0, y2),
					new CGPoint (x2, y2),
					new CGPoint (x2, y0),
				};

				body.AddLines (trPoints.Union(blPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (blPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawBackwardRight()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x3, y1),
					new CGPoint (x1, y1),
					new CGPoint (x1, y3),
				};
				var blPoints = new CGPoint[] {
					new CGPoint (x2, y3),
					new CGPoint (x2, y2),
					new CGPoint (x3, y2),
				};

				body.AddLines (trPoints.Union(blPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (blPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawBackwardLeft()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x0, y1),
					new CGPoint (x2, y1),
					new CGPoint (x2, y3),
				};
				var blPoints = new CGPoint[] {
					new CGPoint (x1, y3),
					new CGPoint (x1, y2),
					new CGPoint (x0, y2),
				};

				body.AddLines (trPoints.Union(blPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (blPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawForwardRightLeft()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x2, y0),
					new CGPoint (x2, y1),
					new CGPoint (x3, y1),
				};
				var bPoints = new CGPoint[] {
					new CGPoint (x3, y2),
					new CGPoint (x0, y2),
				};
				var tlPoints = new CGPoint[] {
					new CGPoint (x0, y1),
					new CGPoint (x1, y1),
					new CGPoint (x1, y0),
				};

				body.AddLines (trPoints.Union(bPoints).Union(tlPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (bPoints);
				outline.AddLines (tlPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawBackwardRightLeft()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x0, y1),
					new CGPoint (x3, y1),
				};
				var bPoints = new CGPoint[] {
					new CGPoint (x3, y2),
					new CGPoint (x2, y2),
					new CGPoint (x2, y3)
				};
				var tlPoints = new CGPoint[] {
					new CGPoint (x1, y3),
					new CGPoint (x1, y2),
					new CGPoint (x0, y2),
				};

				body.AddLines (trPoints.Union(bPoints).Union(tlPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (bPoints);
				outline.AddLines (tlPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawRightForwardBackward()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x2, y0),
					new CGPoint (x2, y1),
					new CGPoint (x3, y1)
				};
				var bPoints = new CGPoint[] {
					new CGPoint (x3, y2),
					new CGPoint (x2, y2),
					new CGPoint (x2, y3)
				};
				var tlPoints = new CGPoint[] {
					new CGPoint (x1, y3),
					new CGPoint (x1, y0),
				};

				body.AddLines (trPoints.Union(bPoints).Union(tlPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (bPoints);
				outline.AddLines (tlPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawLeftForwardBackward()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x1, y0),
					new CGPoint (x1, y1),
					new CGPoint (x0, y1)
				};
				var bPoints = new CGPoint[] {
					new CGPoint (x0, y2),
					new CGPoint (x1, y2),
					new CGPoint (x1, y3)
				};
				var tlPoints = new CGPoint[] {
					new CGPoint (x2, y3),
					new CGPoint (x2, y0),
				};

				body.AddLines (trPoints.Union(bPoints).Union(tlPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (bPoints);
				outline.AddLines (tlPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		private void DrawForwardRightBackwardLeft()
		{
			//get graphics context
			using (CGContext g = UIGraphics.GetCurrentContext ()) {

				//set up drawing attributes
				g.SetLineWidth (STROKE_WIDTH);
				//g.SetFillColor (0, 0, 178, 255);
				FILL.SetFill ();
				STROKE.SetStroke ();

				var outline = new CGPath();
				var body = new CGPath ();

				var x0 = 0;
				var x1 = this.Frame.Width * GUTTER;
				var x2 = this.Frame.Width - x1;
				var x3 = this.Frame.Width;
				var y0 = 0;
				var y1 = this.Frame.Height * GUTTER;
				var y2 = this.Frame.Height - y1;
				var y3 = this.Frame.Height;

				var trPoints = new CGPoint[] {
					new CGPoint (x2, y0),
					new CGPoint (x2, y1),
					new CGPoint (x3, y1)
				};
				var brPoints = new CGPoint[] {
					new CGPoint (x3, y2),
					new CGPoint (x2, y2),
					new CGPoint (x2, y3)
				};
				var blPoints = new CGPoint[] {
					new CGPoint (x1, y3),
					new CGPoint (x1, y2),
					new CGPoint (x0, y2)
				};
				var tlPoints = new CGPoint[] {
					new CGPoint (x0, y1),
					new CGPoint (x1, y1),
					new CGPoint (x1, y0)
				};

				body.AddLines (trPoints.Union(brPoints).Union(blPoints).Union(tlPoints).ToArray());
				body.CloseSubpath ();

				outline.AddLines (trPoints);
				outline.AddLines (brPoints);
				outline.AddLines (blPoints);
				outline.AddLines (tlPoints);

				g.AddPath(body);
				g.DrawPath(CGPathDrawingMode.Fill);

				g.AddPath(outline);
				g.DrawPath(CGPathDrawingMode.Stroke);
			}
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
		}


	}
}

