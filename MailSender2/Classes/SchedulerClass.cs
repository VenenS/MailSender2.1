using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MailSender2.Classes
{
    public class SchedulerClass
    {
        DispatcherTimer timer = new DispatcherTimer(); // таймер
        EmailSendServiceClass emailSender; // экземпляр класса, отвечающего за отправку писем
        DateTime dtSend; // дата и время отправки
        ObservableCollection<Email> emails;

        /// <summary>
        /// Метод, в который превращаем строку из текстбокса tbTimePicker в TimeSpan
        /// </summary>
        /// <param name="strSendTime"></param>
        /// <returns></returns>
        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try
            {
                tsSendTime = TimeSpan.Parse(strSendTime);
            }
            catch { }
            return tsSendTime;
        }

        /// <summary>
        /// Непосредственно отправка писем
        /// </summary>
        /// <param name="dtSend"></param>
        /// <param name="emailSender"></param>
        /// <param name="emails"></param>
        public void SendEmails(EmailSendServiceClass emailSender,
            ObservableCollection<Email> emails)
        {
            this.emailSender = emailSender; // Экземпляр класса, отвечающего за отправку писем
            this.emails = emails;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (dicDates.Count == 0)
            {
                timer.Stop();
                MessageBox.Show("Письма отправлены");
            }
            else if (dicDates.Keys.First<DateTime>().ToShortTimeString() == DateTime.Now.ToShortTimeString())
            {
                emailSender.strBody = dicDates[dicDates.Keys.First<DateTime>()];
                emailSender.strSubject = $"Рассылка от {dicDates.Keys.First<DateTime>().ToShortTimeString()}";
                emailSender.SendMails(emails);
                dicDates.Remove(dicDates.Keys.First<DateTime>());
            }
        }

        Dictionary<DateTime, string> dicDates = new Dictionary<DateTime, string>();
        public Dictionary<DateTime, string> DatesEmailTexts
        {
            get { return dicDates; }
            set
            {
                dicDates = value;
                dicDates = dicDates.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }
    }
}
