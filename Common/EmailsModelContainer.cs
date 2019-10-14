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
        public EmailsModelContainer() : base("MailsAndSendersConnectionString") { }
        public virtual DbSet<Email> Emails { get; set; }
    }
}
