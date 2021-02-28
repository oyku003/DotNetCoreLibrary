using Hangfire.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.Web.BackgroundJobs
{
    public class FireAndForgetJobs
    {
        public static void EmailSendToUserJob(string userId, string message)
        {
            Hangfire.BackgroundJob.Enqueue<IEmailSender>(x => x.Sender(userId, message));//burada veritabanına job, alınan parametreler serilaze edilerek kaydedileryor.O yüzden parametreler cok buyuk olmasın yoksa sqlde cok yer kaplar.

            //Hangfire.BackgroundJob.Enqueue(EmailSender.sender());//yukardakinin diğer yolu
        }
    }
}
