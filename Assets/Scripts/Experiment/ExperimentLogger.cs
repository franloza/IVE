﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Experiment
{
    class ExperimentLogger
    {
        private const string logFile = "Assets/Data/results.csv";
        private const string idFile = "Assets/Data/id.txt";

        public static string Log(params object[] data)
        {
            string s = formatString(data);

            //Write to file
            StreamWriter log;
            if (!File.Exists(logFile))
            {
                log = new StreamWriter(logFile);
                log.WriteLine(formatString("Id","Stage","Challenge","Time","Head Movement","Correct Answer"));
            }
            else log = File.AppendText(logFile);

            log.WriteLine(s);

            log.Close();

            return s;
        }

        private static string formatString(params object[] data)
        {
            //String transformation
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length-1; i++)
            {
                sb.Append(data[i].ToString());
                sb.Append(",");
            }
            sb.Append(data[data.Length - 1].ToString());
            string s = sb.ToString();
            return s;
        }

        public static Tuple<long, int> loadId()
        {
            using (TextReader reader = File.OpenText(idFile))
            {
                long id = long.Parse(reader.ReadLine());
                int numCorrect = int.Parse(reader.ReadLine());

                return new Tuple<long, int>(id, numCorrect);
            }
        }
    }
}
