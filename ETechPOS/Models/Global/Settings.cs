using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ETech.Models.Global
{
    [Serializable]
    public class Setting
    {
        public string Title
        {
            get;
            set;
        }
        public string SubTitle
        {
            get;
            set;
        }
        public object Value1
        {
            get;
            set;
        }
        public object Value2
        {
            get;
            set;
        }
        public object Value3
        {
            get;
            set;
        }

        public Setting()
            : this("", "", new object(), new object(), new object())
        {
        }
        public Setting(string title, string subTitle, object value1, object value2, object value3)
        {
            Title = title;
            SubTitle = subTitle;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }
    }

    [Serializable]
    public class Settings : BindingList<Setting>
    {
        public object GetValue1(string sourceTitle)
        {
            return GetValue1(sourceTitle, "");
        }
        public object GetValue1(string sourceTitle, string sourceSubtitle)
        {
            object value =
                (from setting in this
                where setting.Title == sourceTitle &&
                      setting.SubTitle == sourceSubtitle
                select setting.Value1).First();
            return value;
        }
        public object GetValue2(string sourceTitle)
        {
            return GetValue2(sourceTitle, "");
        }
        public object GetValue2(string sourceTitle, string sourceSubtitle)
        {
            object value =
                (from setting in this
                where setting.Title == sourceTitle &&
                      setting.SubTitle == sourceSubtitle
                select setting.Value2).First();
            return value;
        }
        public object GetValue3(string sourceTitle)
        {
            return GetValue3(sourceTitle, "");
        }
        public object GetValue3(string sourceTitle, string sourceSubtitle)
        {
            object value =
                (from setting in this
                where setting.Title == sourceTitle &&
                      setting.SubTitle == sourceSubtitle
                 select setting.Value3).First();
            return value;
        }

        public void SetValue1(object destinationValue, string sourceTitle)
        {
            SetValue1(destinationValue, sourceTitle, "");
        }
        public void SetValue1(object destinationValue, string sourceTitle, string sourceSubtitle)
        {
            (from setting in this
             where setting.Title == sourceTitle &&
                   setting.SubTitle == sourceSubtitle
             select setting).ToList().ForEach(setting => setting.Value1 = destinationValue);
        }
        public void SetValue2(object destinationValue, string sourceTitle)
        {
            SetValue2(destinationValue, sourceTitle, "");
        }
        public void SetValue2(object destinationValue, string sourceTitle, string sourceSubtitle)
        {
            (from setting in this
             where setting.Title == sourceTitle &&
                   setting.SubTitle == sourceSubtitle
             select setting).ToList().ForEach(setting => setting.Value2 = destinationValue);
        }
        public void SetValue3(object destinationValue, string sourceTitle)
        {
            SetValue3(destinationValue, sourceTitle, "");
        }
        public void SetValue3(object destinationValue, string sourceTitle, string sourceSubtitle)
        {
            (from setting in this
             where setting.Title == sourceTitle &&
                   setting.SubTitle == sourceSubtitle
             select setting).ToList().ForEach(setting => setting.Value3 = destinationValue);
        }
    }
}
