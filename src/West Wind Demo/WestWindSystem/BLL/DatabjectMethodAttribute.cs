using System;
using System.ComponentModel;

namespace WestWindSystem.BLL
{
    internal class DatabjectMethodAttribute : Attribute
    {
        private DataObjectMethodType select;

        public DatabjectMethodAttribute(DataObjectMethodType select)
        {
            this.select = select;
        }
    }
}