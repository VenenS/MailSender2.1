﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodePasswordDLL;

namespace MailSender2.Classes
{
    public class VariablesClass
    {
        public static Dictionary<string, string> Senders
        {
            get { return dicSenders; }
        }
        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            {"79257443993@yandex.ru" , CodePassword.getPassword ( "{3t1l2m6" ) },
            {"sok74@yandex.ru" , CodePassword.getPassword ( "{3t1l2m6" ) },
        };
    }
    public static class VariablesSmtp
    {
        public static Dictionary<string, int> Smtpserv
        {
            get { return dicServers; }
        }
        private static Dictionary<string, int> dicServers = new Dictionary<string, int>()
        {
            {"smtp.mail.ru", 25 },
            {"smtp.yandex.ru", 25 },
            {"smtp.gmail.com", 25 }
        };
    }
}
