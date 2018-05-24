using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const string k_ColorKey = "Color";
        private const string k_NumberofDoorsKey = "Number of Doors";

        private readonly int r_NumberOfWheels = 4;
        private readonly float r_MaximalAirPressure = 32.0f;

        private eColor m_Color;
        private eNumberOfDoors m_NumberOfDoors;

        public enum eColor
        {
            Gray,
            Blue,
            White,
            Black       
        }

        public enum eNumberOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        public override void GetParameters(Dictionary<string, string> i_Parameters)
        {
            base.GetParameters(i_Parameters);
            string colorStr;
            if (!i_Parameters.TryGetValue(k_ColorKey, out colorStr))
            {
                throw new KeyNotFoundException(k_ColorKey);
            }

            string numberOfDoorsStr;
            if (!i_Parameters.TryGetValue(k_NumberofDoorsKey, out numberOfDoorsStr))
            {
                throw new KeyNotFoundException(k_NumberofDoorsKey);
            }

            Color = colorStr;
            NumberOfDoors = numberOfDoorsStr;
            WheelsOfVehicle = GetWheelsParameters(i_Parameters, r_NumberOfWheels, r_MaximalAirPressure);
        }

        public override Dictionary<string, string> CreateParametersFormat()
        {
            Dictionary<string, string> format = base.CreateParametersFormat();

            format.Add(k_ColorKey, null);
            format.Add(k_NumberofDoorsKey, null);
            Wheel.CreateParametersFormat(ref format, r_NumberOfWheels);

            return format;
        }

        public string Color
        {
            get { return m_Color.ToString(); }
            set
            {
                if (!Enum.IsDefined(typeof(eColor), value))
                {
                    throw new FormatException(k_ColorKey);
                }

                try
                {
                    m_Color = (eColor)Enum.Parse(typeof(eColor), value);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException(k_ColorKey);
                }
            }
        }

        public string NumberOfDoors
        {
            get { return m_NumberOfDoors.ToString(); }
            set
            {
                if (!Enum.IsDefined(typeof(eNumberOfDoors), value))
                {
                    throw new FormatException(k_NumberofDoorsKey);
                }

                try
                {
                    m_NumberOfDoors = (eNumberOfDoors)System.Enum.Parse(typeof(eNumberOfDoors), value);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException(k_NumberofDoorsKey);
                }
            }
        }

        public static string ColorKey
        {
            get { return k_ColorKey; }
        }

        public static string NumberofDoorsKey
        {
            get { return k_NumberofDoorsKey; }
        }

        public override string ToString()
        {
            string carInfo = string.Format("{0}----More :----{2}Color: {1}{2}Number Of Doors: {3}", base.ToString(), m_Color, Environment.NewLine, m_NumberOfDoors);
            return carInfo;
        }
    }
}