
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
using System.Windows.Media.Imaging;
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
            ImageDisplay.image = Image;
            ImageDisplay.imageKorrekt = ImageKorekt;
            ImageDisplay.imageFalsch = ImageFalsch;
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
                if(!Boolean.GetCorrectButton(button))
                {
                    button.Content = "Not me!";
                }
            }
            
            
        }



        private void OnGaze(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (button.GetHasGaze())
                {
                    //setBorders
                    button.BorderBrush = Brushes.Blue;
                    Color.SetCustomStroke(button, Brushes.Blue);
                    
                    //startFadeAnimation
                    ButtonAnimation.startAnimation(button);
                }
                else
                {
                    //setBorders
                    button.BorderBrush = Brushes.Black;
                    Color.SetCustomStroke(button, Brushes.Black);

                    //stopFadeAnimation
                    ButtonAnimation.stopAnimation();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Click");
        }

    }

    class ImageDisplay
    {
        public static Image image;
        public static Image imageKorrekt;
        public static Image imageFalsch;
        public static void setCorrectImage()
        {
            image.Source = imageKorrekt.Source;
        }
        internal static void setWrongImage()
        {
            image.Source = imageFalsch.Source;
        }
    }

    class ButtonAnimation
    {
        public static float AnimationSekunden = 0.7f;
        public static int Frames = 24;

        private static Button button;

        public static void startAnimation(Button button)
        {
            if(ButtonAnimation.button != null)
            {
                stopAnimation();
            }

            button.Background = Brushes.Transparent;
            Color.SetCustomBackground(button, Brushes.Transparent);
            ButtonAnimation.button = button;
        }

        public static void stopAnimation()
        {
            if (ButtonAnimation.button != null)
            {
                ButtonAnimation.button.Background = Brushes.Transparent;
                Color.SetCustomBackground(ButtonAnimation.button, Brushes.Transparent);
                ButtonAnimation.button = null;
            }
        }

        public static void animationStep(object sender, EventArgs e)
        {
            if (button != null)
            {
                //SolidColorBrush currentColor = Color.GetCustomBackground(button);
                SolidColorBrush currentColor = (SolidColorBrush)button.Background;
                System.Windows.Media.Color newColor = currentColor.Color;

                if (newColor.A >= 250)
                {
                    if (Boolean.GetCorrectButton(button))
                    {
                        newColor.R = 0;
                        newColor.G = 255;
                        newColor.B = 0;
                        newColor.A = 255;
                        button.BorderBrush = Brushes.Green;
                        Color.SetCustomStroke(button, Brushes.Green);
                        ImageDisplay.setCorrectImage();
                    } else
                    {
                        newColor.R = 255;
                        newColor.G = 0;
                        newColor.B = 0;
                        newColor.A = 255;
                        button.BorderBrush = Brushes.Red;
                        Color.SetCustomStroke(button, Brushes.Red);
                        ImageDisplay.setWrongImage();
                    }
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
                button.Background = new SolidColorBrush(newColor);
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

    public class Boolean : DependencyObject
    {
        private static readonly DependencyProperty CorrectButtonProperty =
            DependencyProperty.RegisterAttached("CorrectButton", typeof(bool), typeof(Boolean),
                new PropertyMetadata(null));

        public static void SetCorrectButton(DependencyObject obj, bool correct)
        {
            obj.SetValue(CorrectButtonProperty, correct);
        }

        public static bool GetCorrectButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(CorrectButtonProperty);
        }
    }
}
