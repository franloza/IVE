using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Experiment
{
    class ExperimentLogger
    {
        private const string logFile = "Assets/Data/experimentLog.txt";

        public static string Log(params object[] data)
        {
            //String transformation
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString());
                sb.Append(" ");
            }
            string s = sb.ToString();

            //Write to file
            StreamWriter log;
            if (!File.Exists(logFile)) log = new StreamWriter(logFile);
            else log = File.AppendText(logFile);

            log.WriteLine(s);

            log.Close();

            return s;
        }
    }
}
