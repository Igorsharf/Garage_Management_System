using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;

        public float MinValue
        {
            get { return m_MinValue; }
            set { m_MinValue = value; }
        }

        public float MaxValue
        {
            get { return m_MaxValue; }
            set { m_MaxValue = value; }
        }

        public ValueOutOfRangeException(string i_Message, float i_MinValue, float i_MaxValue) : base(i_Message)
        {
            MaxValue = i_MaxValue;
            MinValue = i_MinValue;
        }

        public override string ToString()
        {
            return string.Format("Value must be between {0} and {1}.", MinValue, MaxValue);
        }
    }
}