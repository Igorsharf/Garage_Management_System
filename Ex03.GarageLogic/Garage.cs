using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private List<Vehicle> m_VehiclesInGarage;

        public Garage()
        {
            m_VehiclesInGarage = new List<Vehicle>();
        }

        public List<Vehicle> VehiclesInGarage
        {
            get
            {
                return this.m_VehiclesInGarage;
            }

            set
            {
                this.m_VehiclesInGarage = value;
            }
        }

        public bool ChangeConditionOfVehicleInGarage(string i_VehicleLicenseNumberInGarage, Vehicle.eVehicleConditionInGarage i_WantedConditionOfVehicle)
        {
            bool succesChange = false;
            Vehicle wantedVehicleInGarage = GetVehicleInGarageByLicense(i_VehicleLicenseNumberInGarage);
            if (wantedVehicleInGarage != null)
            {
                succesChange = true;
                wantedVehicleInGarage.VehicleConditionInGarage = i_WantedConditionOfVehicle;
            }

            return succesChange;
        }

        public void InflatedWheelsOfVehicleIntoMax(string i_VehicleLicenseNumberInGarage)
        {
            Vehicle wantedVehicleInGarage = GetVehicleInGarageByLicense(i_VehicleLicenseNumberInGarage);

            if (wantedVehicleInGarage != null)
            {
                float WheelcurrentAirPressureFloat;
                foreach (Wheel wheelOfCurrentVehicle in wantedVehicleInGarage.WheelsOfVehicle)
                {
                    try
                    {
                        WheelcurrentAirPressureFloat = float.Parse(wheelOfCurrentVehicle.WheelCurrentAirPressure);
                    }
                    catch (FormatException)
                    {
                        throw new FormatException("float number");
                    }

                    wheelOfCurrentVehicle.WheelInflating(wheelOfCurrentVehicle.WheelMaximalAirPressure - WheelcurrentAirPressureFloat);
                }
            }
        }

        public void RefuelOrRechargeVehicleInGarage(string i_VehicleLicenseNumberInGarage, VehicleEnergy.eEnergyType i_FuelTypeToRefuel, float i_AmountOfFuel)
        {
            Vehicle wantedVehicleInGarage = GetVehicleInGarageByLicense(i_VehicleLicenseNumberInGarage);
            if (wantedVehicleInGarage != null)
            {
                wantedVehicleInGarage.VehicleEnergy.EnergyFilling(i_AmountOfFuel, i_FuelTypeToRefuel, ref wantedVehicleInGarage);
            }
        }

        public string GetVehicleDetails(string i_VehicleLicenseNumberInGarage)
        {
            Vehicle wantedVehicleInGarage = GetVehicleInGarageByLicense(i_VehicleLicenseNumberInGarage);
            string vehicleDetails = string.Empty;
            if (wantedVehicleInGarage != null)
            {
                vehicleDetails = wantedVehicleInGarage.ToString();
            }

            return vehicleDetails;
        }

        public Vehicle GetVehicleInGarageByLicense(string i_VehicleLicenseNumber)
        {
            Vehicle wantedVehicle = null;
            foreach (Vehicle vehicleInList in m_VehiclesInGarage)
            {
                if (vehicleInList.VehicleLicenseNumber.Equals(i_VehicleLicenseNumber))
                {
                    wantedVehicle = vehicleInList;
                    break;
                }
            }

            return wantedVehicle;
        }

        public void InsertVehicleIntoGarage(Vehicle i_CurrentVehicle)
        {
            VehiclesInGarage.Add(i_CurrentVehicle);
        }

        public string ShowListOfVehiclesByCondition(Vehicle.eVehicleConditionInGarage? i_CurrentConditionOfVehicle)
        {
            StringBuilder vehiclesLicenseNumbersInGarage = new StringBuilder();
            vehiclesLicenseNumbersInGarage.Append(string.Format("List of lisence number : {0}" ,Environment.NewLine));
            int counterOfCars = 0;
            foreach (Vehicle vehicleInList in m_VehiclesInGarage)
            {
                if (i_CurrentConditionOfVehicle == null || vehicleInList.VehicleConditionInGarage == i_CurrentConditionOfVehicle)
                {
                    vehiclesLicenseNumbersInGarage.Append(string.Format("{0}{1}",vehicleInList.VehicleLicenseNumber,Environment.NewLine));
                    counterOfCars++;
                }
            }

            if (counterOfCars == 0)
            {
                vehiclesLicenseNumbersInGarage.Append("There is no Vehicls in this list.");
            }

            return vehiclesLicenseNumbersInGarage.ToString();
        }
    }
}