using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Thread thread;
        Thread[] athread;

        private void Syn_Click(object sender, RoutedEventArgs e)
        {
            if (athread != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (athread[i].ThreadState == ThreadState.Running || athread[i].ThreadState == ThreadState.WaitSleepJoin)
                    {
                        athread[i].Abort();
                    }
                }
            }
            thread = new Thread(Start);
            thread.Start();
        }

        private void Start()
        {
            NullAll();
            WakeUp();
            Drink();
            Eat();
            GoOut();
        }

        private void NullAll()
        {
            Dispatcher.Invoke(() => WakeUpBar.Value = 0);
            Dispatcher.Invoke(() => DrinkBar.Value = 0);
            Dispatcher.Invoke(() => EatBar.Value = 0);
            Dispatcher.Invoke(() => GoOutBar.Value = 0);
        }

        private void WakeUp()
        {
            while (Dispatcher.Invoke(() => WakeUpBar.Value < 100))
            {
                Dispatcher.Invoke(() => WakeUpBar.Value += 5);
                Thread.Sleep(200);
            }
        }

        private void Drink()
        {
            while (Dispatcher.Invoke(() => DrinkBar.Value < 100))
            {
                Dispatcher.Invoke(() => DrinkBar.Value += 5);
                Thread.Sleep(50);
            }
        }

        private void Eat()
        {
            while (Dispatcher.Invoke(() => EatBar.Value < 100))
            {
                Dispatcher.Invoke(() => EatBar.Value += 5);
                Thread.Sleep(80);
            }
        }

        private void GoOut()
        {
            while (Dispatcher.Invoke(() => GoOutBar.Value < 100))
            {
                Dispatcher.Invoke(() => GoOutBar.Value += 5);
                Thread.Sleep(100);
            }
        }

        private void Asyn_Click(object sender, RoutedEventArgs e)
        {
            if(thread != null)
            {
                if(thread.ThreadState == ThreadState.Running || thread.ThreadState == ThreadState.WaitSleepJoin)
                {
                    thread.Abort();
                }
            }
            NullAll();
            athread = new Thread[4];
            athread[0] = new Thread(WakeUp);
            athread[1] = new Thread(Drink);
            athread[2] = new Thread(Eat);
            athread[3] = new Thread(GoOut);
            for (int i = 0; i < 4; i++) 
            {
                athread[i].Start();
            }
        }
    }
}
