using System;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WpfApplication6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Label[] LabelMap;
        SpeechSynthesizer FortuneTeller;
        DispatcherTimer AnimationEngine;
        int CharacterToBeHighlighted;
        int NumberOfIterations;

        public MainWindow()
        {
            InitializeComponent();

            LabelMap = new Label[] { F, L, A, M, E, S };

            FortuneTeller = new SpeechSynthesizer();
            FortuneTeller.SelectVoiceByHints(VoiceGender.Female);
            FortuneTeller.Rate = -2;

            AnimationEngine = new DispatcherTimer();
            AnimationEngine.Interval = TimeSpan.FromMilliseconds(500);
            AnimationEngine.Tick += AnimationEngine_Tick;

            IconImage.Source = new BitmapImage(new Uri(Environment.CurrentDirectory.ToString() + @"\Resources\polynesian_mascot_flame1.png"));
        }

        void AnimationEngine_Tick(object sender, EventArgs e)
        {
            //for (int i = 0; i < 6; i++)
            //{
            //    LabelMap[i].Opacity = 1;
            //}
            if (CharacterToBeHighlighted - 1 >= 0)
            {
                LabelMap[(CharacterToBeHighlighted - 1) % 6].Opacity = 1;
            }
            else
            {
                LabelMap[5].Opacity = 1;
            }
            LabelMap[CharacterToBeHighlighted % 6].Opacity = 0.5;
            if (CharacterToBeHighlighted < NumberOfIterations)
            {
                CharacterToBeHighlighted++;
            }
            else
            {
                AnimationEngine.Stop();
                NameDisplayer.Text = HisName.Text.ToUpper() + "  and  " + HerName.Text.ToUpper();
                switch (CharacterToBeHighlighted % 6)
                {
                    case 0:
                        Result.Text = "Meant to be FRIENDS";
                        break;
                    case 1:
                        Result.Text = "Meant to be LOVERS";
                        break;
                    case 2:
                        Result.Text = "Meant to be AFFECTIONATE PEOPLE";
                        break;
                    case 3:
                        Result.Text = "Meant to be MARRIED";
                        break;
                    case 4:
                        Result.Text = "Meant to be ENEMIES";
                        break;
                    case 5:
                        Result.Text = "Meant to be SIBLINGS";
                        break;
                }
                FortuneTeller.SpeakAsync(HisName.Text.ToLower() + " and " + HerName.Text.ToLower() + " are " + Result.Text.ToLower());

                HisName.Text = "His Name";
                HerName.Text = "Her Name";

                if(HisName.IsFocused == true)
                {
                    HisName.SelectAll();
                }
                else if(HerName.IsFocused == true)
                {
                    HerName.SelectAll();
                }
            }
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            /*FortuneTeller.SpeakAsyncCancelAll();

            if (HisName.Text.Length > 0 && HerName.Text.Length > 0)
            {
                int ConsolidatedLength = 0;
                string BoyName = HisName.Text.ToUpper();
                string GirlName = HerName.Text.ToUpper();
                bool[] FlamesMap = new bool[6] { true, true, true, true, true, true };

                bool[] CumulativeCharacterMap = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
                

                for (int i = 0; i < 6; i++)
                {
                    LabelMap[i].Opacity = 1;
                }

                for (int i = 0; i < BoyName.Length; i++)
                {
                    if (BoyName[i] <= 'Z' && BoyName[i] >= 'A')
                    {
                        CumulativeCharacterMap[BoyName[i] - 'A'] = true;
                    }
                }

                for (int i = 0; i < GirlName.Length; i++)
                {
                    if (GirlName[i] <= 'Z' && GirlName[i] >= 'A')
                    {
                        CumulativeCharacterMap[GirlName[i] - 'A'] = true;
                    }
                }

                for (int i = 0; i < 26; i++)
                {
                    if (CumulativeCharacterMap[i] == true)
                    {
                        ConsolidatedLength++;
                    }
                }

                int CurrentIteration = 0;
                int CurrentPosition = 0;

                while (true)
                {
                    int NumberOfCasesLeft = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        if (FlamesMap[i] == true)
                        {
                            NumberOfCasesLeft++;
                        }
                    }

                    if (NumberOfCasesLeft == 1)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (FlamesMap[i] == true)
                            {
                                NumberOfIterations = 18 + i;
                                CharacterToBeHighlighted = 0;
                                AnimationEngine.Start();
                                FortuneTeller.SpeakAsync("Calculating your probable relationship status");
                                break;
                            }
                        }
                        break;
                    }

                    if (FlamesMap[CurrentIteration % 6] == true)
                    {
                        CurrentPosition++;
                    }

                    if (CurrentPosition == ConsolidatedLength)
                    {
                        FlamesMap[CurrentIteration % 6] = false;
                        CurrentPosition = 0;
                    }
                    CurrentIteration++;
                }
            }
            else
            {
                FortuneTeller.SpeakAsync("Sorry! But we require your name and your partner's name to provide you with a result");
            }*/

            FortuneTeller.SpeakAsyncCancelAll();

            if(HisName.Text == HerName.Text)
            {
                FortuneTeller.SpeakAsync("Sorry! But you are supposed to give different names!");
                MessageBox.Show("Sorry! But you are supposed to give different names!", "Error!");
                return;
            }

            if (HisName.Text.Length > 0 && HerName.Text.Length > 0 && HisName.Text != "His Name" && HerName.Text != "Her Name")
            {
                int ConsolidatedLength = 0;
                string BoyName = HisName.Text.ToUpper();
                string GirlName = HerName.Text.ToUpper();
                //bool[] FlamesMap = new bool[6] { true, true, true, true, true, true };

                int[] GirlCharacterMap = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                int[] BoyCharacterMap = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                for (int i = 0; i < 6; i++)
                {
                    LabelMap[i].Opacity = 1;
                }

                for (int i = 0; i < BoyName.Length; i++)
                {
                    if (BoyName[i] <= 'Z' && BoyName[i] >= 'A')
                    {
                        BoyCharacterMap[BoyName[i] - 'A']++;
                    }
                }

                for (int i = 0; i < GirlName.Length; i++)
                {
                    if (GirlName[i] <= 'Z' && GirlName[i] >= 'A')
                    {
                        GirlCharacterMap[GirlName[i] - 'A']++;
                    }
                }

                for (int i = 0; i < 26; i++)
                {
                    if (BoyCharacterMap[i] > GirlCharacterMap[i])
                    {
                        ConsolidatedLength += BoyCharacterMap[i] - GirlCharacterMap[i];
                    }
                    else
                    {
                        ConsolidatedLength += GirlCharacterMap[i] - BoyCharacterMap[i];
                    }
                }

                int deletePosition = 0;
                ConsolidatedLength--;
                List<int> FLAMES = new List<int>();
                FLAMES.Add(0);
                FLAMES.Add(1);
                FLAMES.Add(2);
                FLAMES.Add(3);
                FLAMES.Add(4);
                FLAMES.Add(5);
                for(int i=6; i>1; i--)
                {
                    FLAMES.RemoveAt((deletePosition = (ConsolidatedLength + deletePosition) % i));
                }
                NumberOfIterations = 18 + FLAMES[0];
                CharacterToBeHighlighted = 0;
                AnimationEngine.Start();
                FortuneTeller.SpeakAsync("Calculating your probable relationship status");

                /*int CurrentIteration = 0;
                int CurrentPosition = 0;

                while (true)
                {
                    int NumberOfCasesLeft = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        if (FlamesMap[i] == true)
                        {
                            NumberOfCasesLeft++;
                        }
                    }

                    if (NumberOfCasesLeft == 1)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (FlamesMap[i] == true)
                            {
                                NumberOfIterations = 18 + i;
                                CharacterToBeHighlighted = 0;
                                AnimationEngine.Start();
                                FortuneTeller.SpeakAsync("Calculating your probable relationship status");
                                break;
                            }
                        }
                        break;
                    }

                    if (FlamesMap[CurrentIteration % 6] == true)
                    {
                        CurrentPosition++;
                    }

                    if (CurrentPosition == ConsolidatedLength)
                    {
                        FlamesMap[CurrentIteration % 6] = false;
                        CurrentPosition = 0;
                    }
                    CurrentIteration++;
                }*/
            }
            else
            {
                FortuneTeller.SpeakAsync("Sorry! But we require your name and your partner's name to provide you with a result");
                MessageBox.Show("Sorry! But we require your name and your partner's name to provide you with a result", "Error!");
            }
        }

        private void HisName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (HisName.Text == "His Name")
            {
                HisName.Clear();
            }
        }

        private void HerName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (HerName.Text == "Her Name")
            {
                HerName.Clear();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.DragMove();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void HisName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(HerName.Text == "Her Name" || HerName.Text == "")
                {
                    HerName.Focus();
                }
                else
                {
                    GoButton_Click(sender, e);
                }
            }
        }

        private void HerName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(HisName.Text == "His Name" || HisName.Text == "")
                {
                    HisName.Focus();
                }
                else
                {
                    GoButton_Click(sender, e);
                }
            }
        }
    }
}