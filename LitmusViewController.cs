using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Litmus
{
	public partial class LitmusViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public LitmusViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle
		UILabel	_begin;
		MazeTile _currentTile;
		float _fontHeight;
		string _fontName = "Heiti SC";
		int _fontSize = 64;
		UISwipeGestureRecognizer _swipeUp, _swipeDown, _swipeLeft, _swipeRight;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.BackgroundColor = UIColor.FromRGB (22, 22, 22);
			var text = new NSString ("Begin");
			var size = MeasureTextSize (text, this.View.Frame.Width, _fontSize, _fontName);
			_fontHeight = (float)size.Height;

			var frame = new Rectangle (new Point (0, (int)(this.View.Frame.Height / 2f - _fontHeight)), new Size ((int)this.View.Frame.Width, 96));
			// Perform any additional setup after loading the view, typically from a nib.
			_begin = new UILabel (frame) {
				Text = "Begin",
				TextColor = UIColor.White,
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.FromRGBA(0,0,0,0),
				Font = UIFont.FromName(_fontName, _fontSize),
				Alpha = 0,
				ShadowColor = UIColor.FromRGBA(255,255,255,125),
				ShadowOffset = Size.Empty
			};
			_currentTile = new MazeTile (Movements2D.Left | Movements2D.Backward | Movements2D.Forward | Movements2D.Right) {
				Frame = this.View.Frame,
				Alpha = 0
			};

			this.View.Add (_currentTile);
			this.View.Add (_begin);

			_swipeUp = new UISwipeGestureRecognizer (SwipedUp){ Direction = UISwipeGestureRecognizerDirection.Up };
			_swipeDown = new UISwipeGestureRecognizer (SwipedDown){ Direction = UISwipeGestureRecognizerDirection.Down };
			_swipeLeft = new UISwipeGestureRecognizer (SwipedLeft){ Direction = UISwipeGestureRecognizerDirection.Left };
			_swipeRight = new UISwipeGestureRecognizer (SwipedRight){ Direction = UISwipeGestureRecognizerDirection.Right };

			_currentTile.AddGestureRecognizer (_swipeUp);
			_currentTile.AddGestureRecognizer (_swipeDown);
			_currentTile.AddGestureRecognizer (_swipeLeft);
			_currentTile.AddGestureRecognizer (_swipeRight);
		}

		MazeTile _nextTile = null;
		RectangleF _currentFrame;
		private void SwipedUp()
		{
			var frameBelow = new RectangleF (new PointF (0f, (float)this.View.Frame.Height), (SizeF)this.View.Frame.Size);
			_currentFrame = new RectangleF (new PointF (0f, (float)-this.View.Frame.Height), (SizeF)this.View.Frame.Size);
			_nextTile = new MazeTile (Movements2D.Forward | Movements2D.Backward) {
				Frame = frameBelow,
				Alpha = 1.0f
			};
			_begin.Text = "Up";
			this.View.AddSubview (_nextTile);
		}

		private void SwipedDown()
		{
			var frameAbove = new RectangleF (new PointF (0f, (float)-this.View.Frame.Height), (SizeF)this.View.Frame.Size);
			_currentFrame = new RectangleF (new PointF (0f, (float)this.View.Frame.Height), (SizeF)this.View.Frame.Size);
			_nextTile = new MazeTile (Movements2D.Backward | Movements2D.Left) {
				Frame = frameAbove,
				Alpha = 1.0f
			};
			_begin.Text = "Down";
			this.View.AddSubview (_nextTile);
		}

		private void SwipedLeft()
		{
			var frameAbove = new RectangleF (new PointF ((float)this.View.Frame.Width, 0), (SizeF)this.View.Frame.Size);
			_currentFrame = new RectangleF (new PointF ((float)-this.View.Frame.Width, 0), (SizeF)this.View.Frame.Size);
			_nextTile = new MazeTile (Movements2D.Left | Movements2D.Forward | Movements2D.Backward) {
				Frame = frameAbove,
				Alpha = 1.0f
			};
			_begin.Text = "Left";
			this.View.AddSubview (_nextTile);
		}

		private void SwipedRight()
		{
			var frameAbove = new RectangleF (new PointF ((float)-this.View.Frame.Width, 0), (SizeF)this.View.Frame.Size);
			_currentFrame = new RectangleF (new PointF ((float)this.View.Frame.Width, 0), (SizeF)this.View.Frame.Size);
			_nextTile = new MazeTile (Movements2D.Right | Movements2D.Forward) {
				Frame = frameAbove,
				Alpha = 1.0f
			};
			_begin.Text = "Left";
			this.View.AddSubview (_nextTile);
		}

		private CGSize MeasureTextSize(string text, double width,
			double fontSize, string fontName = null)
		{
			var nsText = new NSString(text);
			var boundSize = new SizeF((float)width, float.MaxValue);
			var options = NSStringDrawingOptions.UsesFontLeading |
				NSStringDrawingOptions.UsesLineFragmentOrigin;

			if (fontName == null)
			{
				fontName = "HelveticaNeue";
			}

			var attributes = new UIStringAttributes {
				Font = UIFont.FromName(fontName, (float)fontSize)
			};

			return nsText.GetBoundingRect(boundSize, options, attributes, null).Size;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		public override bool PrefersStatusBarHidden ()
		{
			return true;
		}

		bool _hasRun = false;
		CGRect _lastFrame = CGRect.Empty;

		public override void ViewDidLayoutSubviews ()
		{
			if (_lastFrame == CGRect.Empty)
				_lastFrame = this.View.Frame;
			
			base.ViewDidLayoutSubviews ();

			var textTop = this.View.Frame.Height / 2f - _fontHeight / 2f;
			var textFrame = new Rectangle (new Point (0, (int)textTop), new Size ((int)this.View.Frame.Width, 96));

			if (_nextTile != null) {
				UIView.Animate (0.75,
					0,
					UIViewAnimationOptions.BeginFromCurrentState | UIViewAnimationOptions.CurveEaseInOut,
					() => {
						_nextTile.Frame = this.View.Frame;
						_currentTile.Frame = _currentFrame;
						//_currentTile.Center = new CGPoint( _currentFrame.Width /2f, -_currentFrame.Height/2f);
					},
					() => 
					{
						_currentTile = _nextTile;
						_currentTile.AddGestureRecognizer (_swipeUp);
						_currentTile.AddGestureRecognizer (_swipeDown);
						_currentTile.AddGestureRecognizer (_swipeLeft);
						_currentTile.AddGestureRecognizer (_swipeRight);
					});
			} else {
				
				UIView.Animate (2,
					1,
					UIViewAnimationOptions.BeginFromCurrentState | UIViewAnimationOptions.CurveEaseOut,
					() => {
						_begin.Alpha = 1.0f;
						_begin.Frame = textFrame;
						_currentTile.Alpha = 1.0f;
					},
					null);
				_hasRun = true;

			}

//			if (_hasRun && _lastFrame.Width == this.View.Frame.Width) {
//				_currentTile.Frame = this.View.Frame;
//				_begin.Frame = textFrame;
//			}
				
			_lastFrame = this.View.Frame;
		}
			

		#endregion
	}
}

