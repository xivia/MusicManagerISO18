using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Server.Core.Config
{
    public class SmtpSettings
    {
        public string SmtpServerAddress { get; set; }
        public int SmtpServerPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
