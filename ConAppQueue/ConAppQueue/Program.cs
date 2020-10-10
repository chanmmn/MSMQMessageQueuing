using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConAppQueue
{
    class Program
    {
        public static MessageQueue mq;
        public static int j = 0;
        static void Main(string[] args)
        {
            bool blnRun = true;
            int option = 0;
            while(blnRun)
            {
                Console.WriteLine("Press 1 to Enqueue, 2 to Dequeue, 3 to Exit");
                option = int.Parse(Console.ReadLine());
                if (option==1)
                {
                    Enqueue();
                }
                else if (option==2)
                {
                    Dequeue();
                }
                else if (option == 3)
                {
                    blnRun = false;
                }
            }
        }

        protected static void Enqueue()
        {
            mq = new MessageQueue(@".\Private$\myqueue");
            System.Messaging.Message mm = new System.Messaging.Message();
            mm.Body = "Queue :" + j;            
            j++;
            mq.Send(mm);
        }

        protected static void Dequeue()
        {
            Message mes;
            string m;
            mes = mq.Receive(new TimeSpan(0, 0, 3));
            mes.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            m = mes.Body.ToString();
            Console.WriteLine(m);
        }
    }
}
