using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AeroViewer
{
    public partial class Loader : UserControl
    {
        public Loader()
        {
            InitializeComponent();

            ellipse1.BeginAnimation(OpacityProperty,
                Resources["ellipse1Animation"] as DoubleAnimationUsingKeyFrames);
            ellipse2.BeginAnimation(OpacityProperty,
                Resources["ellipse2Animation"] as DoubleAnimationUsingKeyFrames);
            ellipse3.BeginAnimation(OpacityProperty,
                Resources["ellipse3Animation"] as DoubleAnimationUsingKeyFrames);
            ellipse4.BeginAnimation(OpacityProperty,
                Resources["ellipse4Animation"] as DoubleAnimationUsingKeyFrames);
            ellipse5.BeginAnimation(OpacityProperty,
                Resources["ellipse5Animation"] as DoubleAnimationUsingKeyFrames);
            ellipse6.BeginAnimation(OpacityProperty,
                Resources["ellipse6Animation"] as DoubleAnimationUsingKeyFrames);
            ellipse7.BeginAnimation(OpacityProperty,
                Resources["ellipse7Animation"] as DoubleAnimationUsingKeyFrames);
            ellipse8.BeginAnimation(OpacityProperty,
                Resources["ellipse8Animation"] as DoubleAnimationUsingKeyFrames);
        }
    }
}
