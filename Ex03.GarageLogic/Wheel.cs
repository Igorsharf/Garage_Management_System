using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const string k_WheelModelNameKey = "Wheel Model Name";
        internal const string k_CurrentAirPressureKey = "Wheel Current Air Pressure";
        private const string k_CurrentWheelPrefix = "Wheel {0} ";

        private string m_WheelModelName;
        private float m_WheelCurrentAirPressure;
        private float m_WheelMaximalAirPressure;

        public Wheel()
        {
            m_WheelModelName = null;
            m_WheelCurrentAirPressure = 0.0f;
            m_WheelMaximalAirPressure = 0.0f;
        }

        internal static void CreateParametersFormat(ref Dictionary<string, string> i_Parametresm, int i_NumberOfWheels)
        {
            for (int i = 1; i <= i_NumberOfWheels; i++)
            {
                string currentWheelKeyStr = string.Format(k_CurrentWheelPrefix, i);

                i_Parametresm.Add(currentWheelKeyStr + k_WheelModelNameKey, null);
                i_Parametresm.Add(currentWheelKeyStr + k_CurrentAirPressureKey, null);
            }
        }

        internal static Wheel MakeSingleWheel(Dictionary<string, string> i_Parameters, int i_CurrentWheelIndex, float i_MaxAirPressure)
        {
            string manufacturerWheelKey;
            string currentAirPressureWheelKey;

            currentAirPressureWheelKey = string.Format(k_CurrentWheelPrefix, i_CurrentWheelIndex);
            manufacturerWheelKey = currentAirPressureWheelKey;

            manufacturerWheelKey += k_WheelModelNameKey;
            currentAirPressureWheelKey += k_CurrentAirPressureKey;

            string manufacturerWheelStr;
            if (!i_Parameters.TryGetValue(manufacturerWheelKey, out manufacturerWheelStr))
            {
                throw new KeyNotFoundException(manufacturerWheelKey);
            }

            string airCurrentPressureWheelStr;
            if (!i_Parameters.TryGetValue(currentAirPressureWheelKey, out airCurrentPressureWheelStr))
            {
                throw new KeyNotFoundException(currentAirPressureWheelKey);
            }

            Wheel wheelToReturn = new Wheel();
            wheelToReturn.m_WheelMaximalAirPressure = i_MaxAirPressure;
            wheelToReturn.m_WheelModelName = manufacturerWheelStr;

            try
            {
                wheelToReturn.WheelCurrentAirPressure = airCurrentPressureWheelStr;
            }
            catch (ValueOutOfRangeException valueException)
            {
                throw new ValueOutOfRangeException(currentAirPressureWheelKey, valueException.MinValue, valueException.MaxValue);
            }
            catch (FormatException)
            {
                throw new FormatException(currentAirPressureWheelKey);
            }

            return wheelToReturn;
        }

        internal void WheelInflating(float i_AmountOfAirToAdd)
        {
            if (i_AmountOfAirToAdd + m_WheelCurrentAirPressure <= m_WheelMaximalAirPressure)
            {
                m_WheelCurrentAirPressure += i_AmountOfAirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException("Wheel Inflate", 0.0f, m_WheelMaximalAirPressure - m_WheelCurrentAirPressure);
            }
        }

        public string WheelCurrentAirPressure
    {
        get { return m_WheelCurrentAirPressure.ToString(); }
        set
        {
            float floatAirPressureValue;
            try
            {
                floatAirPressureValue = float.Parse(value);
            }
            catch (FormatException)
            {
                throw new FormatException(k_CurrentAirPressureKey);
            }

            if (floatAirPressureValue > WheelMaximalAirPressure || floatAirPressureValue < 0.0f)
            {
                throw new ValueOutOfRangeException(k_CurrentAirPressureKey, 0.0f, WheelMaximalAirPressure);
            }

            m_WheelCurrentAirPressure = floatAirPressureValue;
        }
    }

    public float WheelMaximalAirPressure
        {
        get { return m_WheelMaximalAirPressure; }
         set { m_WheelMaximalAirPressure = value; }
    }

    public string WheelModelName
        {
        get { return m_WheelModelName; }
        set { m_WheelModelName = value; }
    }

        public override string ToString()
        {
            string wheelInfo = string.Format("ModelName: {0} Current Air Pressure: {2} Maximal Air Pressure: {3}{1}", WheelModelName, Environment.NewLine, WheelCurrentAirPressure, WheelMaximalAirPressure);
            return wheelInfo;
        }
    }
}