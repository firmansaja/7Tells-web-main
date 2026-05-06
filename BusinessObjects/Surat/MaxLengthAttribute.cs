using System;

namespace BusinessObjects.Surat
{
    internal class MaxLengthAttribute : Attribute
    {
        private int v;

        public MaxLengthAttribute(int v, string ErrorMessage)
        {
            this.v = v;
            this.ErrorMessage = ErrorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}