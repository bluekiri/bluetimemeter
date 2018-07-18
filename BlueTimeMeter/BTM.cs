using System;
using System.Linq;

namespace BlueTimeMeter
{
    class BTM
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public TimeSpan timeSpan { get; set; }
        public DateTime date { get; set; }
        public string NameAssigned { get; set; }
        public int CodeLine { get; set; }
        public string ProjectName { get; set; }

        public BTM()
        {
            date = DateTime.Now;
            timeSpan = new TimeSpan(date.Ticks);
        }

        public BTM(string name, string filePath, string methodName, int codeLine)
        {
            date = DateTime.Now;
            timeSpan = new TimeSpan(date.Ticks);
            ClassName = filePath?.Split('/').Last() ?? string.Empty;
            CodeLine = codeLine;
            MethodName = methodName;
            NameAssigned = name;
            ProjectName = filePath?.Split('/').First() ?? string.Empty;
        }

    }
}