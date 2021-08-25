using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MovieApp.ViewModels;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Threading.Tasks;

namespace MovieApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailsView : ContentPage
    {
        MovieDetailViewModel vm;
        public MovieDetailsView()
        {
            InitializeComponent();
            vm = new MovieDetailViewModel();
        }




        //Drawing the Gradiant Shading 
        #region Method for Drawing the Shading of the button Add to fav
        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            using (SKPaint paint = new SKPaint())
            {
                //defining the color for the shadow
                SKColor shadowColor = Color.FromHex("#1EBBB7").ToSKColor();

                paint.IsDither = true;
                paint.IsAntialias = true;
                paint.Color = shadowColor;

                // create filter for drop shadow
                paint.ImageFilter = SKImageFilter.CreateDropShadow(
                    dx: 0, dy: 0,
                    sigmaX: 40, sigmaY: 40,
                    color: shadowColor,
                    shadowMode: SKDropShadowImageFilterShadowMode.DrawShadowOnly);

                //define where to draw the object 
                var margin = info.Width / 10;
                var shadowBounds = new SKRect(margin, -40, info.Width - margin, 10);
                canvas.DrawRoundRect(shadowBounds, 10, 10, paint);
            }

        }
    }
    #endregion

}