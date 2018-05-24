using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle 
    {
        private const string k_LicenseTypeKey = "License Type";
        private const string k_CapacityKey = "Capacity";

        public enum eLicenseType
        {
            A,
            A1,
            B1,
            B2,
        }

        private readonly int r_NumberOfWheels = 2;
        private readonly float r_MaximalAirPressure = 30.0f;

        private eLicenseType m_LicenseType;
        private int m_EngineCapacityInCc;

        public override void GetParameters(Dictionary<string, string> i_Parameters)
        {
            base.GetParameters(i_Parameters);

            string licenseTypeStr;
            if (!i_Parameters.TryGetValue(k_LicenseTypeKey, out licenseTypeStr))
            {
                throw new KeyNotFoundException(k_LicenseTypeKey);
            }

            string capacityStr;
            if (!i_Parameters.TryGetValue(k_CapacityKey, out capacityStr))
            {
                throw new KeyNotFoundException(k_CapacityKey);
            }

            LicenseType = licenseTypeStr;
            EngineCapacityInCc = capacityStr;
            WheelsOfVehicle = GetWheelsParameters(i_Parameters, r_NumberOfWheels, r_MaximalAirPressure);
        }

        public override Dictionary<string, string> CreateParametersFormat()
        {
            Dictionary<string, string> format = base.CreateParametersFormat();
            format.Add(k_LicenseTypeKey, null);
            format.Add(k_CapacityKey, null);
            Wheel.CreateParametersFormat(ref format, r_NumberOfWheels);

            return format;
        }

        public string LicenseType
        {
            get { return m_LicenseType.ToString(); }
            set
            {
                if (!Enum.IsDefined(typeof(eLicenseType), value))
                {
                    throw new FormatException(k_LicenseTypeKey);
                }

                try
                {
                    m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), value);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException(k_LicenseTypeKey);
                }
            }
        }

        public string EngineCapacityInCc
        {
            get { return m_EngineCapacityInCc.ToString(); }
            set
            {
                try
                {
                    m_EngineCapacityInCc = int.Parse(value);
                }
                catch (FormatException)
                {
                    throw new FormatException(k_CapacityKey);
                }
            }
        }

        public static string LicenseTypeKey
        {
            get { return k_LicenseTypeKey; }
        }

        public override string ToString()
        {
            string motorcycltInfo = string.Format("{0}----More :----{2}License Type: {1}{2}Engine Capacity In Cc: {3}", base.ToString(), m_LicenseType, Environment.NewLine, m_EngineCapacityInCc);
            return motorcycltInfo;
        }
    }
}
