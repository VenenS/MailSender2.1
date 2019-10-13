using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender2.Classes
{
    /// <summary>
    /// Класс, который отвечает за работу с базой данных
    /// </summary>
    class DBClass
    {
        private EmailsDataContext emails = new EmailsDataContext();
        public IQueryable<Email> Emails
        {
            get
            {
                return from c in emails.Email select c;
            }
        }
    }
}
