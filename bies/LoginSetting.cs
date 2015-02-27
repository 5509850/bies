using System;
using System.Collections.Generic;
using System.Text;

namespace bies
{
    public class LoginSetting
    {
        private string login;
        private DateTime tradeDateFrom;
        public string L
        {
            get { return login; }
            set { login = value; }
        }

        public DateTime TradeDateFrom
        {
            get { return tradeDateFrom; }
            set { tradeDateFrom = value; }
        }
    }
}
