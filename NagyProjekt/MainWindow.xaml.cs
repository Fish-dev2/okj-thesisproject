using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media;
using System.IO;

namespace NagyProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;
        MediaPlayer zeneplayer;
        MediaPlayer effektplayer;
        MainWindowHelper mwhelper;
        string pressedbutton;

        public MainWindow()
        {
            InitializeComponent();
            FoAblakElindit();

        }

        private void HatterCsereEsemeny(object sender, EventArgs e)
        {
            if (mwhelper.BGCounter > 0)
            {
                mwhelper.BGCounter--;
            }
            if (mwhelper.BGCounter == 0)
            {
                if (mwhelper.ElsoKep)
                {
                    if (Hatter2.Opacity != 0.6)
                    {
                        Hatter1.Opacity = Hatter1.Opacity - 0.1;
                        Hatter2.Opacity = Hatter2.Opacity + 0.1;
                    }
                    else
                    {
                        mwhelper.ElsoKep = false;
                        mwhelper.BGCounter = 50;
                    }
                }
                else
                {
                    if (Hatter1.Opacity != 0.6)
                    {
                        Hatter1.Opacity = Hatter1.Opacity + 0.1;
                        Hatter2.Opacity = Hatter2.Opacity - 0.1;
                    }
                    else
                    {
                        mwhelper.ElsoKep = true;
                        mwhelper.BGCounter = 50;
                    }
                }
            }
        }

        private void FoAblakElindit()
        {
            //mwhelper létrehozás, értékadások, elinditások
            mwhelper = new MainWindowHelper();
            pressedbutton = "";
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(60);
            timer.Tick += HatterCsereEsemeny;
            timer.Start();

            //hangfájlok betöltése
            zeneplayer = new MediaPlayer();
            zeneplayer.Open(new Uri("Eroforrasok/Hangok/title.wav", UriKind.Relative));
            zeneplayer.MediaEnded += async (sender, e) =>
            {
                zeneplayer.Position = TimeSpan.Zero;
            };
            zeneplayer.Play();

            effektplayer = new MediaPlayer();
            effektplayer.Open(new Uri("Eroforrasok/Hangok/shoot.mp3", UriKind.Relative));
            effektplayer.Stop();


            //////////////////////////////////////////
            //képváltogató beállit
            ResetButtons(null, null);
            Hatter1.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Eroforrasok/Texturak/teszt1.jpg")));
            Hatter2.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Eroforrasok/Texturak/teszt2.jpg")));
            Hatter1.Opacity = 0.6;
            Hatter2.Opacity = 0.0;

            mwhelper.ElsoKep = true;
            mwhelper.BGCounter = 50;

            //0.6 opacity a max a képnek


            //setup slider az FXhez és a Musichoz
            slider.ValueChanged += async (sender, e) =>
            {
                mwhelper.FXHangero = Convert.ToInt32(slider.Value);
                effektplayer.Volume = mwhelper.FXHangero / 10.0;
                effektplayer.Position = TimeSpan.Zero;
                effektplayer.Play();
            };
            text.DataContext = slider;
            FXSlider.MouseLeave += async (sender, e) =>
            {
                ketto.Opacity = 1;
                FXSlider.Opacity = 0;
                Panel.SetZIndex(FXSlider, -1);

            };
            ////////////////////////////
            slider2.ValueChanged += async (sender, e) =>
            {
                mwhelper.ZeneHangero = Convert.ToInt32(slider2.Value);
                zeneplayer.Volume = mwhelper.ZeneHangero / 10.0;
            };
            text2.DataContext = slider2;
            MusicSlider.MouseLeave += async (sender, e) =>
            {
                harom.Opacity = 1;
                MusicSlider.Opacity = 0;
                Panel.SetZIndex(MusicSlider, -1);

            };
            //ne lehessen tabolni rejtett dolgokra
            this.PreviewKeyDown += async (sender, e) =>
            {
                if (e.Key == Key.Tab || e.Key == Key.RightAlt || e.Key == Key.LeftAlt)
                {
                    e.Handled = true;
                }
            };

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            SelectButton(sender);

        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            if (pressedbutton == "" || (label.Tag != null && label.Tag.ToString() == "sec"))
            {
                UnSelectButton(sender);
            }
        }

        private void SelectButton(object sender)
        {
            Label label = (Label)sender;
            SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Black);
            solidColorBrush.Opacity = 0.05;
            label.Background = solidColorBrush;
            label.BorderBrush = new SolidColorBrush(Colors.DarkGreen);
            label.BorderThickness = new Thickness(2);
            label.Padding = new Thickness(0);
        }
        private void UnSelectButton(object sender)
        {
            Label label = (Label)sender;
            label.Background = new SolidColorBrush(Colors.Transparent);
            label.BorderBrush = new SolidColorBrush(Colors.Transparent);
            label.BorderThickness = new Thickness(0);
            label.Padding = new Thickness(2);
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;

            if (label.Tag == null || label.Tag.ToString() != "sec")
            {
                pressedbutton = label.Name;
                atlatszo1.Opacity = 0.4;
                atlatszo2.Opacity = 0.4;
                atlatszo3.Opacity = 0.4;
                Panel.SetZIndex(helper1, 2);
                Panel.SetZIndex(helper2, 2);
                newgamebutton.Opacity = 0.4;
                continuebutton.Opacity = 0.4;
                settingsbutton.Opacity = 0.4;
                aboutbutton.Opacity = 0.4;
                quitbutton.Opacity = 0.4;
                label.Opacity = 1;
                SelectButton(label);
                switch (label.Name)
                {
                    case "newgamebutton":
                        egy.Content = "Choose gamemode:";
                        ketto.Content = "Survival";

                        MakeLabelButton(ketto);
                        egy.Opacity = 1;
                        break;
                    case "continuebutton":
                        if (false)//ha van mentett állás, akkor lehet folytatni
                        {
                            egy.Content = "Are you sure?";
                            ketto.Content = "Yes";
                            harom.Content = "No";
                            MakeLabelButton(ketto);
                            MakeLabelButton(harom);
                            egy.Opacity = 1;
                        }
                        else
                        {
                            egy.Content = "You don't have saves.";
                            egy.Opacity = 1;
                        }

                        break;
                    case "settingsbutton":
                        ketto.Content = "Set Effects Volume";
                        harom.Content = "Set Music Volume";
                        negy.Content = "Check Highscores";
                        ot.Content = "DELETE ALL SAVES";

                        MakeLabelButton(ketto);
                        MakeLabelButton(harom);
                        MakeLabelButton(negy);
                        MakeLabelButton(ot);
                        break;
                    case "aboutbutton":
                        ketto.Content = "Made by: Bálint Füzi";
                        harom.Content = "In 2021";
                        negy.Content = "At BGSZC Szily";

                        ketto.HorizontalContentAlignment = HorizontalAlignment.Center;
                        harom.HorizontalContentAlignment = HorizontalAlignment.Center;
                        negy.HorizontalContentAlignment = HorizontalAlignment.Center;

                        ketto.Opacity = 1;
                        harom.Opacity = 1;
                        negy.Opacity = 1;
                        break;
                    case "quitbutton":
                        ketto.Content = "Are you sure?";
                        harom.Content = "Yes";
                        negy.Content = "No";

                        MakeLabelButton(negy);
                        MakeLabelButton(harom);
                        ketto.Opacity = 1;
                        break;
                }
            }
            else
            {
                switch (pressedbutton)
                {
                    case "newgamebutton":
                        Jatekkezelo jatekkezelo;
                        switch (label.Name)
                        {
                            case "ketto":
                                zeneplayer.Pause();
                                jatekkezelo = new Jatekkezelo("map.gamemap", new Jatekos(0, 0, 100, 100, 5, 5, 5, 5, "survivor.png"));
                                string kiir = "Esc: Quit to menu\nSpace: Build where your mouse is pointing\nW,A,S,D: Move\nQ,E: Change Weapons";
                                MessageBox.Show(kiir, "Movement:", MessageBoxButton.OK);
                                zeneplayer.Play();
                                //survival
                                break;
                            case "harom":
                                //practice
                                break;
                        }
                        break;
                    case "continuebutton":
                        switch (label.Name)
                        {
                            case "ketto":
                                //firstlvl
                                break;
                            case "harom":
                                //secondlvl
                                break;
                            case "negy":
                                //thirdlvl
                                break;
                            case "ot":
                                //fourthlvl
                                break;
                        }
                        break;
                    case "settingsbutton":
                        switch (label.Name)
                        {
                            case "ketto":
                                FXSlider.Opacity = 1;
                                ketto.Opacity = 0;
                                Panel.SetZIndex(FXSlider, 2);
                                //FX
                                break;
                            case "harom":
                                MusicSlider.Opacity = 1;
                                harom.Opacity = 0;
                                Panel.SetZIndex(MusicSlider, 2);
                                //music
                                break;
                            case "negy":
                                //hi-scores
                                break;
                            case "ot":
                                //DELETESAVE
                                break;
                        }
                        break;
                    case "quitbutton":
                        switch (label.Name)
                        {
                            case "harom":
                                this.Close();
                                //quit
                                break;
                            case "negy":
                                ResetButtons(null,null);
                                //noquit
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void MakeLabelButton(Label label)
        {
            label.MouseEnter += Button_MouseEnter;
            label.MouseLeave += Button_MouseLeave;
            label.MouseLeftButtonDown += Label_MouseLeftButtonDown;
            label.Opacity = 1;
        }

        private void ResetButtons(object sender, MouseButtonEventArgs e)
        {
            Panel.SetZIndex(helper1, -2);
            Panel.SetZIndex(helper2, -2);
            ResetLabel(egy);
            ResetLabel(ketto);
            ResetLabel(harom);
            ResetLabel(negy);
            ResetLabel(ot);
            atlatszo1.Opacity = 0;
            atlatszo2.Opacity = 0;
            atlatszo3.Opacity = 0;
            pressedbutton = "";
            newgamebutton.Opacity = 1;
            continuebutton.Opacity = 1;
            settingsbutton.Opacity = 1;
            aboutbutton.Opacity = 1;
            quitbutton.Opacity = 1;
            UnSelectButton(newgamebutton);
            UnSelectButton(continuebutton);
            UnSelectButton(settingsbutton);
            UnSelectButton(aboutbutton);
            UnSelectButton(quitbutton);
            FXSlider.Opacity = 0;
            Panel.SetZIndex(FXSlider, -1);
            MusicSlider.Opacity = 0;
            Panel.SetZIndex(MusicSlider, -1);
        }

        private void ResetLabel(Label label)
        {
            label.Content = "";
            label.Opacity = 0;
            label.MouseEnter -= Button_MouseEnter;
            label.MouseLeave -= Button_MouseLeave;
            label.MouseLeftButtonDown -= Label_MouseLeftButtonDown;
            label.HorizontalContentAlignment = HorizontalAlignment.Left;
        }

        private void FoablakBezar(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //mentse el a beállitásokat
            zeneplayer.Stop();

            StreamWriter sw = new StreamWriter("Eroforrasok/user.gamesettings");
            sw.WriteLine("///values for sfx and music");
            sw.WriteLine("sfx:" + mwhelper.FXHangero.ToString()) ;
            sw.WriteLine("music:" + mwhelper.ZeneHangero.ToString());
            sw.Flush();
            sw.Close();
        }
    }

    internal class MainWindowHelper : Bindable
    {
        private int backgroundcounter;

        public int BGCounter
        {
            get { return backgroundcounter; }
            set { backgroundcounter = value; }
        }
        private bool elsokep;

        public bool ElsoKep
        {
            get { return elsokep; }
            set { elsokep = value; }
            //sethez: Állitson be uj képet
        }

        private int fxHangero;

        public int FXHangero
        {
            get { return fxHangero; }
            set { fxHangero = value; }
        }

        private int zeneHangero;

        public int ZeneHangero
        {
            get { return zeneHangero; }
            set { zeneHangero = value; }
        }


        public MainWindowHelper()
        {

        }
    }
}
