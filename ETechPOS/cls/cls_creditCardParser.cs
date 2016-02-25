using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ETech.cls
{
    public class cls_creditCardParser
    {
        public string Name
        {
            get;
            private set;
        }
        public string Number
        {
            get;
            private set;
        }
        public DateTime ExpirationDate
        {
            get;
            private set;
        }
        public cls_creditCardParser(string input)
        {
            string[] temp;
            Regex regex = new Regex(@"([%]B(\d+)[>^]([\w/\s]+)[.\s>^]*(\d{2})(\d{2})\d*[?][\n\r])?[;](\d+)([=](\d{2})(\d{2})(\d+)?)?([=](\d+))?[?][\n\r]");
            input = input.Replace("\r\n", "");
            MatchCollection match = regex.Matches(input);
            if (match == null || match.Count == 0)
            {
                // For magnetic swipers that only read track 2 without '?' and ';'
                regex = new Regex(@"(\d+)[=](\d{2})(\d{2})(\d+)?");
                match = regex.Matches(input);
                if (match == null || match.Count == 0)
                    throw new NullReferenceException();
                Name = "";
                Number = match[0].Groups[1].ToString();
                int tempInt = int.TryParse(match[0].Groups[3].ToString(), out tempInt) ? tempInt : 0;
                ExpirationDate = new DateTime(int.Parse(match[0].Groups[2].ToString()) + 2000, tempInt == 0 ? 12 : tempInt, 1);
                return;
            }
            try
            {
                Number = match[0].Groups[2].ToString();
                temp = match[0].Groups[3].ToString().Split('/');
                Name = temp[0].Trim();
                if (temp.Length > 1)
                    Name = temp[1].Trim() + " " + Name;
                int tempInt = int.TryParse(match[0].Groups[5].ToString(), out tempInt) ? tempInt : 0;
                ExpirationDate = new DateTime(int.Parse(match[0].Groups[4].ToString()) + 2000, tempInt == 0 ? 12 : tempInt, 1);
            }
            catch { }
            try
            {
                int tempInt = int.TryParse(match[0].Groups[8].ToString(), out tempInt) ? tempInt : 0;
                ExpirationDate = new DateTime(int.Parse(match[0].Groups[7].ToString()) + 2000, tempInt == 0 ? 12 : tempInt, 1);
            }
            catch { }
            Number = match[0].Groups[6].ToString();
        }
    }
}
