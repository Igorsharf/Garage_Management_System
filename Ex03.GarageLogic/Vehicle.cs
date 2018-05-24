using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private const string k_Name0fModelKey = "Model Name";
        private const string k_VehicleLicenseNumberKey = "Vehicle license number";
        private const string k_RemainingEnergyAmountKey = "Remaining Energy Amount";
        private const string k_RemainingEnergyPrecentKey = "Remaining Energy Precent";
        private const string k_OwnerNameKey = "Owner Name";
        private const string k_OwnersPhoneNumberKey = "Owner Phone Number";
        private const string k_EnergySource = "Energy Source";
        private const float k_MaximalPercentageNumber = 100.0f;
        private const float k_MinimalPercentageNumber = 0.0f;

        private eVehicleConditionInGarage m_VehicleConditionInGarage;
        private string m_Name0fModel;
        private string m_VehicleLicenseNumber;
        private float m_RemainingEnergyPercent;
        private List<Wheel> m_WheelsOfVehicle;
        private string m_OwnerName;
        private string m_OwnersPhoneNumber;
        protected VehicleEnergy vehicleEnergy;
  
        public enum eVehicleConditionInGarage
        {
            Fixing = 1,
            Fixed = 2,
            Paid = 3
        }

        protected Vehicle()
        {
            vehicleEnergy = new VehicleEnergy();
            VehicleConditionInGarage = eVehicleConditionInGarage.Fixing;
        }

        protected static bool IsValidString(string i_StringToCheck)
        {
            bool validString = true;
            if (string.IsNullOrEmpty(i_StringToCheck) == true)
            {
                validString = false;
            }

            return validString;
        }

        public string Name0fModel
        {
            get
            {
                return this.m_Name0fModel;
            }

            set
            {
                if (IsValidString(value))
                {
                    this.m_Name0fModel = value;
                }
                else
                {
                    throw new ArgumentException(k_Name0fModelKey);
                }
            }
        }

        public string VehicleLicenseNumber
        {
            get
            {
                return this.m_VehicleLicenseNumber;
            }

            set
            {
                if (IsValidString(value) == true)
                {
                    this.m_VehicleLicenseNumber = value;
                }
                else
                {
                    throw new ArgumentException(k_VehicleLicenseNumberKey);
                }
            }
        }

        public void CalculateRemainingEnergyPercent()
        {
            float RemainingEnergyAmountFloat;
            try
            {
                RemainingEnergyAmountFloat = float.Parse(vehicleEnergy.RemainingEnergyAmount);
            }
            catch (FormatException)
            {
                throw new FormatException(k_RemainingEnergyAmountKey);
            }

            this.m_RemainingEnergyPercent = (RemainingEnergyAmountFloat / vehicleEnergy.MaximalEnergyAmount) * 100;
        }

        public float RemainingEnergyPercent
        {
            get
            {
                return this.m_RemainingEnergyPercent;
            }
        }

        public List<Wheel> WheelsOfVehicle
        {
            get
            {
                return this.m_WheelsOfVehicle;
            }

            set
            {
                this.m_WheelsOfVehicle = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return this.m_OwnerName;
            }

            set
            {
                if (IsValidString(value))
                {
                    this.m_OwnerName = value;
                }
                else
                {
                    throw new ArgumentException(k_OwnerNameKey);
                }
            }
        }

        public string OwnersPhoneNumber
        {
            get
            {
                return this.m_OwnersPhoneNumber;
            }

            set
            {
                if (IsValidString(value) == true)
                {
                    this.m_OwnersPhoneNumber = value;
                }
                else
                {
                    throw new ArgumentException(k_OwnersPhoneNumberKey);
                }
            }
        }

        public eVehicleConditionInGarage VehicleConditionInGarage
        {
            get
            {
                return this.m_VehicleConditionInGarage;
            }

            set
            {
                this.m_VehicleConditionInGarage = value;
            }
        }

        public VehicleEnergy VehicleEnergy
        {
            get
            {
                return this.vehicleEnergy;
            }
        }

        public virtual void GetParameters(Dictionary<string, string> i_Parameters)
        {
            string modelNameInput;
            if (i_Parameters.TryGetValue(k_Name0fModelKey, out modelNameInput) == false)
            {
                throw new KeyNotFoundException(k_Name0fModelKey);
            }

            string RemainingEnergyAmountInput;
            if (i_Parameters.TryGetValue(k_RemainingEnergyAmountKey, out RemainingEnergyAmountInput) == false)
            {
                throw new KeyNotFoundException(k_RemainingEnergyAmountKey);
            }

            string ownerNameInput;
            if (i_Parameters.TryGetValue(k_OwnerNameKey, out ownerNameInput) == false)
            {
                throw new KeyNotFoundException(k_OwnerNameKey);
            }

            string ownerPhoneNumberInput;
            if (i_Parameters.TryGetValue(k_OwnersPhoneNumberKey, out ownerPhoneNumberInput) == false)
            {
                throw new KeyNotFoundException(k_OwnersPhoneNumberKey);
            }

            OwnerName = ownerNameInput;
            OwnersPhoneNumber = ownerPhoneNumberInput;
            Name0fModel = modelNameInput;
            vehicleEnergy.RemainingEnergyAmount = RemainingEnergyAmountInput;
            CalculateRemainingEnergyPercent();
        }

        public virtual Dictionary<string, string> CreateParametersFormat()
        {
            Dictionary<string, string> format = new Dictionary<string, string>();
            format.Add(k_OwnerNameKey, null);
            format.Add(k_OwnersPhoneNumberKey, null);
            format.Add(k_Name0fModelKey, null);
            format.Add(k_RemainingEnergyAmountKey, null);

            return format;
        }

        protected List<Wheel> GetWheelsParameters(Dictionary<string, string> i_Parametres, int i_NumberOfWheels, float i_MaximumAirPressure)
        {
            List<Wheel> wheels = new List<Wheel>(i_NumberOfWheels);

            for (int i = 1; i <= i_NumberOfWheels; ++i)
            {
                wheels.Add(Wheel.MakeSingleWheel(i_Parametres, i, i_MaximumAirPressure));
            }

            return wheels;
        }

        public override string ToString()
        {
            StringBuilder wheelsInfo = new StringBuilder();
            int wheelNumber = 1;
            foreach (Wheel currentWheel in WheelsOfVehicle)
            {
                wheelsInfo.Append(string.Format("Wheel Number : {0}{1}", wheelNumber, Environment.NewLine));
                wheelsInfo.Append(currentWheel.ToString());
                wheelNumber++;
            }

            string vehicleInfo = string.Format("----Car Details----{2}{0} : {1}{2}{3} : {4}{2}{5} : {6}{2}Status : {7}{2}----Wheels---- {2}{8}{2}----Energy source info :---- {2}{10} : {11}%{2}{9}{2}", k_VehicleLicenseNumberKey, VehicleLicenseNumber, Environment.NewLine, k_Name0fModelKey, Name0fModel, k_OwnerNameKey, m_OwnerName, VehicleConditionInGarage, wheelsInfo, vehicleEnergy.ToString(), k_RemainingEnergyPrecentKey, m_RemainingEnergyPercent);
            return vehicleInfo;
        }

        public string InfoPrinterByKey(string key)
        {
            System.Type objectType = null;
            StringBuilder optionsString = new StringBuilder();
            if (this is Motorcycle)
            {
                if (key.Equals(Motorcycle.LicenseTypeKey))
                {
                    objectType = typeof(Motorcycle.eLicenseType);
                    optionsString.Append(string.Format("License Type options : {0}", Environment.NewLine));
                }
            }
            else if (this is Car)
            {
                if (key.Equals(Car.ColorKey))
                {
                    objectType = typeof(Car.eColor);
                    optionsString.Append(string.Format("Color options : {0}", Environment.NewLine));
                }
                else if (key.Equals(Car.NumberofDoorsKey))
                {
                    objectType = typeof(Car.eNumberOfDoors);
                    optionsString.Append(string.Format("Number of Doors options : {0}", Environment.NewLine));
                }
            }
            else if (this is FuelTruck)
            {
                if (key.Equals(FuelTruck.IsCarryingDangerousMaterialKey))
                {
                    optionsString.Append(string.Format("*Please insert True / False* {0}", Environment.NewLine));
                }
            }

            if (objectType != null)
            {
                foreach (string enumItem in Enum.GetNames(objectType))
                {
                    optionsString.Append(string.Format("{0}{1}", enumItem, Environment.NewLine));
                }
            }

            if (key.Equals(k_RemainingEnergyAmountKey))
            {
                optionsString.Append(string.Format("*The maximum amount of energy to fill is {0}{1}{2}", this.vehicleEnergy.MaximalEnergyAmount, "*", Environment.NewLine));
            }

            return optionsString.ToString();
        }
    }
}