using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System;
using System.Linq;
using MailSender2.ViewModel;
using MailSender2.Classes;

namespace MailSender2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
            cbSmtpSelect.ItemsSource = VariablesSmtp.Smtpserv;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValue = "Value";
        }

        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnClock_Click(object sender, RoutedEventArgs e)
        {
            tbConrol.SelectedItem = tbPlanner;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            SchedulerClass sc = new SchedulerClass();
            TimeSpan tsSendTime = sc.GetSendTime(tbTimePicker.Text);
            if(tsSendTime==new TimeSpan())
            {
                MessageBox.Show("Некоректный формат даты");
                return;
            }
            DateTime dtSendTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            if(dtSendTime<DateTime.Now)
            {
                MessageBox.Show("Дата и время отправки не могут быть раньше, чем настоящее время!");
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(cbSenderSelect.Text,
                cbSenderSelect.SelectedValue.ToString(), BodyPost.Text, SubjectPost.Text, cbSmtpSelect.Text,
                int.Parse(((KeyValuePair<string, int>)cbSenderSelect.SelectedItem).Value.ToString()));
            var locator = (ViewModelLocator)FindResource("Locator");
            sc.SendEmails(emailSender, locator.Main.Emails);
        }

        private void BtnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            string strBody = BodyPost.Text;
            string strSubject = SubjectPost.Text;
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            string smtpServ = cbSmtpSelect.Text;
            int sPort = int.Parse(((KeyValuePair<string, int>)cbSmtpSelect.SelectedItem).Value.ToString());
            if(string.IsNullOrEmpty(strLogin))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            if(string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Укажите пароль отправителя");
                return;
            }
            if (string.IsNullOrEmpty(strBody))
            {
                MessageBox.Show("Письмо не заполнено");
                return;
            }
            Classes.EmailSendServiceClass emailSender = new Classes.EmailSendServiceClass(strLogin, strPassword, 
                strBody, strSubject, smtpServ, sPort);
            var locator = (ViewModelLocator)FindResource("Locator");
            emailSender.SendMails(locator.Main.Emails);
        }

        private void TabSwitcher_Back(object sender, RoutedEventArgs e)
        {
            if (tbConrol.SelectedIndex == 0) return;
            tbConrol.SelectedIndex--;
        }

        private void TabSwitcher_Forward(object sender, RoutedEventArgs e)
        {
            if (tbConrol.SelectedIndex == tbConrol.Items.Count - 1) return;
            tbConrol.SelectedIndex++;
        }
    }
}
