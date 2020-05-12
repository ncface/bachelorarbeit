
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Tobii.Interaction;
using Tobii.Interaction.Wpf;

namespace WpfApp2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            //Button5.AddHasGazeChangedHandler(OnGaze);
            addGazeHandlerToButtons();

            ButtonAnimation.initAnimation();
        }
        private void addGazeHandlerToButtons()
        {
            List <Button> buttons= new List<Button>();
            buttons.Add(Button1);
            buttons.Add(Button2);
            buttons.Add(Button3);
            buttons.Add(Button4);
            buttons.Add(Button5);
            buttons.Add(Button6);
            buttons.Add(Button7);
            buttons.Add(Button8);
            buttons.Add(Button9);
            buttons.Add(Button10);
            buttons.Add(Button11);
            buttons.Add(Button12);
            buttons.Add(Button13);
            buttons.Add(Button14);
            buttons.Add(Button15);
            buttons.Add(Button16);
            buttons.Add(Button17);
            buttons.Add(Button18);
            buttons.Add(Button19);
            buttons.Add(Button20);
            buttons.Add(Button21);
            buttons.Add(Button22);
            buttons.Add(Button23);
            buttons.Add(Button24);
            buttons.Add(Button25);
            buttons.Add(Button26);
            buttons.Add(Button27);
            buttons.Add(Button28);
            buttons.Add(Button29);
            buttons.Add(Button30);
            buttons.Add(Button31);
            buttons.Add(Button32);
            buttons.Add(Button33);
            buttons.Add(Button34);
            buttons.Add(Button35);
            buttons.Add(Button36);
            foreach (Button button in buttons)
            {
                button.AddHasGazeChangedHandler(OnGaze);
            }
            
            
        }



        private void OnGaze(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (button.GetHasGaze())
                {
                    button.BorderBrush = Brushes.Red;
                    object test = button.Template.FindName("Ellipse", button);

                    Color.SetCustomStroke(button, Brushes.Red);
                    ButtonAnimation.startAnimation(button);
                }
                else
                {
                    Color.SetCustomStroke(button, Brushes.Black);
                    button.BorderBrush = Brushes.Black;
                    ButtonAnimation.stopAnimation();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Click");
        }

    }

    class ButtonAnimation
    {
        public static int AnimationSekunden = 2;
        public static int Frames = 24;

        private static Button button;

        public static void startAnimation(Button button)
        {
            if(ButtonAnimation.button != null)
            {
                stopAnimation();
            }
            Color.SetCustomBackground(button, Brushes.Transparent);
            ButtonAnimation.button = button;
            
        }

        public static void stopAnimation()
        {
            if (ButtonAnimation.button != null)
            {
                Color.SetCustomBackground(ButtonAnimation.button, Brushes.Transparent);
                ButtonAnimation.button = null;


            }
        }

        public static void animationStep(object sender, EventArgs e)
        {
            if (button != null)
            {
                SolidColorBrush currentColor = Color.GetCustomBackground(button);
                System.Windows.Media.Color newColor = currentColor.Color;

                if (newColor.A >= 250)
                {
                    newColor.R = 0;
                    newColor.G = 255;
                    newColor.B = 0;
                    newColor.A = 255;
                    button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                else
                {
                    newColor.R = 0;
                    newColor.G = 0;
                    newColor.B = 255;
                    newColor.A += (byte)(255 / (AnimationSekunden * Frames));
                }
                Color.SetCustomBackground(button, new SolidColorBrush(newColor));
            }
        }

        internal static void initAnimation()
        {
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(animationStep);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000/Frames);
            dispatcherTimer.Start();
        }
    }

    public class Color : DependencyObject
    {
        private static readonly DependencyProperty CustomStrokeProperty =
            DependencyProperty.RegisterAttached("CustomStroke", typeof(SolidColorBrush), typeof(Color),
                new PropertyMetadata(null));

        public static void SetCustomStroke(DependencyObject obj, SolidColorBrush color)
        {
            obj.SetValue(CustomStrokeProperty, color);
        }

        public static SolidColorBrush GetCustomStroke(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(CustomStrokeProperty);
        }

        private static readonly DependencyProperty CustomBackgroundProperty =
            DependencyProperty.RegisterAttached("CustomBackground", typeof(SolidColorBrush), typeof(Color),
                new PropertyMetadata(null));

        public static void SetCustomBackground(DependencyObject obj, SolidColorBrush color)
        {
            obj.SetValue(CustomBackgroundProperty, color);
        }

        public static SolidColorBrush GetCustomBackground(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(CustomBackgroundProperty);
        }
    }
}
