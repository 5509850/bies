using System;
using System.Collections.Generic;
using System.Text;

namespace bies.Entity
{
    public class ComboBoxSubItems : ComboBoxItems
    {
        private int mainId;

        public int MainId
        {
            get { return mainId; }
            set { mainId = value; }
        }
    }
}
