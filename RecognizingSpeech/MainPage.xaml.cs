using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RecognizingSpeech
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SpeechRecognizer _recognizer = null;
        public MainPage()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(
            NavigationEventArgs e)
        {
            _recognizer = new SpeechRecognizer();
            List<string> phrases = new List<string>()
                { "red", "yellow", "white", "blue", "green" };
            SpeechRecognitionListConstraint listConstraint =
                new SpeechRecognitionListConstraint(phrases);
            _recognizer.Constraints.Add(listConstraint);
            await _recognizer.CompileConstraintsAsync();
        }

        private Color GetColor(string color)
        {
            switch (color)
            {
                case "red": return Colors.Red;
                case "yellow": return Colors.Yellow;
                case "white": return Colors.White;
                case "blue": return Colors.Blue;
                case "green": return Colors.Green;
                default: return Colors.Black;
            }
        }
        private async void GrdMain_Tapped(object sender,
            TappedRoutedEventArgs e)
        {
            SpeechRecognitionResult result =
                await _recognizer.RecognizeWithUIAsync();
            if (result.Status ==
                SpeechRecognitionResultStatus.Success)
            {
                GrdMain.Background =
                    new SolidColorBrush(GetColor(result.Text));
            }
        }
    }
}
