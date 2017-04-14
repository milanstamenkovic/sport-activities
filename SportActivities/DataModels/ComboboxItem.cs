﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportActivities
{
    public class ComboboxItem
    {
        public ComboboxItem() { }

        public ComboboxItem(string text, object value)
        {
            Text = text;
            Value = value;
        }

        public static ComboboxItem getInstance()
        {
            return new ComboboxItem();
        }

        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }


    }
}
