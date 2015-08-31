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
using System.ServiceModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace QQApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IChatCallBack
    {
        string username = string.Empty;
        QQProxy proxy;
        string[] friendList;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var m = e.OriginalSource as MenuItem;
            switch (m.Header.ToString())
            {
                case "Login":
                    Login();
                    break;
                case "Exit":
                    Leave();
                    break;
            }
        }

        private void Login()
        {
            try
            {
                Window1 LoginWin = new Window1();
                LoginWin.ShowDialog();
                username = LoginWin.GetName();

                InstanceContext ins = new InstanceContext(this);
                proxy = new QQProxy(ins);

                IAsyncResult iar = proxy.BeginJoin(username, new AsyncCallback(OnEndJoin), null);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void OnEndJoin(IAsyncResult iar)
        {
            try
            {
                friendList = proxy.EndJoin(iar);
                FriendListBind();

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void Leave()
        {
            try
            {
                proxy.Leave();
                //proxy.Close();
                //proxy = null;

                FriendListListView.Items.Clear();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void FriendListBind()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate()
            {
                for (int i = 0; i < friendList.Count(); i++)
                {
                    FriendListListView.Items.Add(friendList[i]);
                }
            }, null);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Leave();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        #region implement IChatCallBack interface
        public void UserEnter(string name)
        {
            ShowTextBox.AppendText("User " + name + " --------login---------" + DateTime.Now.ToString() + Environment.NewLine);
            FriendListListView.Items.Add(name);
        }

        public void UserLeave(string name)
        {
            ShowTextBox.AppendText("User " + name + " --------exit---------" + DateTime.Now.ToString() + Environment.NewLine);
            FriendListListView.Items.Remove(name);
        }

        public void Receive(string name, string message)
        {
            ShowTextBox.AppendText(DateTime.Now.ToString() + "    " + name + " SAY: " + Environment.NewLine);
            ShowTextBox.AppendText(message + Environment.NewLine);
        }

        public void ReceiveWhisper(string name, string message)
        {
            ShowTextBox.AppendText(DateTime.Now.ToString() + Environment.NewLine + "    " + name + " Whisper to you: " + Environment.NewLine);
            ShowTextBox.AppendText(message + Environment.NewLine);
        }
        #endregion

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InputTextBox.Text.Trim() == string.Empty)
                    return;

                proxy.Say(InputTextBox.Text.Trim());
                InputTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (InputTextBox.Text.Trim() == string.Empty)
                        return;

                    proxy.Say(InputTextBox.Text.Trim());
                    InputTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void WhisperButton_Click(object sender, RoutedEventArgs e)
        {
            string name = (string)FriendListListView.SelectedValue;
            try
            {
                if (InputTextBox.Text.Trim() == string.Empty)
                    return;

                proxy.Whisper(name, InputTextBox.Text.Trim());
                InputTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
