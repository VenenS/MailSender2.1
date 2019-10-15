using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Common
{
    public partial class EmailsModelContainer:DbContext
    {
        public EmailsModelContainer() : base("MailSenderDB") { }
        public virtual DbSet<Email> Emails { get; set; }
    }
}
