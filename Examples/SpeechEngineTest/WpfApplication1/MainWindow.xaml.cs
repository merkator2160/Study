using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//using Microsoft.Speech.Synthesis;
using System.Speech.Synthesis;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpeechSynthesizer _synthesizer;
        private ReadOnlyCollection<InstalledVoice> _voices; //array of installed voices


        public MainWindow()
        {
            InitializeComponent();

            _synthesizer = new SpeechSynthesizer { Volume = 100, Rate = 0 };

            _voices = _synthesizer.GetInstalledVoices();
            foreach (var x in _voices)
                SelectVoiceComboBox.Items.Add(x.VoiceInfo.Name);
            SelectVoiceComboBox.SelectedIndex = 0;
        }


        private void readBtn_Click(object sender, RoutedEventArgs e)
        {
            _synthesizer.SelectVoice(SelectVoiceComboBox.SelectedItem.ToString());
            _synthesizer.SpeakAsync(textBox1.Text);
        }
    }
}
