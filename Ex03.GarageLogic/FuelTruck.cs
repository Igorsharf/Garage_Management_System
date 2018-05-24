using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
        public class FuelTruck : Vehicle
        {
        private const string k_MaximalCarryingWeightKey = "Maximal Carrying Weight";
        private const string k_IsCarryingDangerousMaterialKey = "Is Carrying Dangerous Material";

        private readonly int r_NumberOfWheels = 12;
        private readonly float r_MaximalAirPressure = 28.0f;

        private float m_MaximalCarryingWeight;
        private bool m_IsCarryingDangerousMaterial;

        public FuelTruck()
        {
            vehicleEnergy.EnergyType = VehicleEnergy.eEnergyType.Octan96.ToString();
            vehicleEnergy.MaximalEnergyAmount = 115.0f;
        }

        public override void GetParameters(Dictionary<string, string> i_Parameters)
        {
            base.GetParameters(i_Parameters);

            string mMaximalCarryingWeightKeyStr;
            if (!i_Parameters.TryGetValue(k_MaximalCarryingWeightKey, out mMaximalCarryingWeightKeyStr))
            {
                throw new KeyNotFoundException(k_MaximalCarryingWeightKey);
            }
        
            string IsCarryingDangerousMaterialKeyStr;
            if (!i_Parameters.TryGetValue(k_IsCarryingDangerousMaterialKey, out IsCarryingDangerousMaterialKeyStr))
            {
                throw new KeyNotFoundException(k_IsCarryingDangerousMaterialKey);
            }

            MaximalCarryingWeight = mMaximalCarryingWeightKeyStr;
            IsCarryingDangerousMaterial = IsCarryingDangerousMaterialKeyStr;
            WheelsOfVehicle = GetWheelsParameters(i_Parameters, r_NumberOfWheels, r_MaximalAirPressure);
        }

        public override Dictionary<string, string> CreateParametersFormat()
        {
            Dictionary<string, string> format = base.CreateParametersFormat();
            format.Add(k_MaximalCarryingWeightKey, null);
            format.Add(k_IsCarryingDangerousMaterialKey, null);

            Wheel.CreateParametersFormat(ref format, r_NumberOfWheels);

            return format;
        }

        public string MaximalCarryingWeight
        {
            get { return m_MaximalCarryingWeight.ToString(); }
            set
            {
                try
                {
                    m_MaximalCarryingWeight = int.Parse(value);
                }
                catch (FormatException)
                {
                    throw new FormatException(k_MaximalCarryingWeightKey);
                }
            }
        }

        public string IsCarryingDangerousMaterial
        {
            get { return m_IsCarryingDangerousMaterial.ToString(); }
            set
            {
                try
                {
                    m_IsCarryingDangerousMaterial = bool.Parse(value);
                }
                catch (FormatException)
                {
                    throw new FormatException(k_IsCarryingDangerousMaterialKey);
                }
            }
        }

        public static string IsCarryingDangerousMaterialKey
        {
            get { return k_IsCarryingDangerousMaterialKey; }
        }

        public override string ToString()
        {
            string truckInfo = string.Format("{0}----More :----{2}Maximal Carrying Weight: {1}{2}Is Carrying Dangerous Material: {3}", base.ToString(), m_MaximalCarryingWeight, Environment.NewLine, m_IsCarryingDangerousMaterial);
            return truckInfo;
        }
    }
}
