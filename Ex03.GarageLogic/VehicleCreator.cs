using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public enum eVehicleTypes
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            FuelTruck
        }

        public static Vehicle MakeVehicle(eVehicleTypes i_VehicleType)
        {
            Vehicle Vehicle;
            switch (i_VehicleType)
            {
                case eVehicleTypes.FuelCar:
                    Vehicle = new Car();
                    Vehicle.VehicleEnergy.EnergyType = VehicleEnergy.eEnergyType.Octan98.ToString();
                    Vehicle.VehicleEnergy.MaximalEnergyAmount = 45f;
                    break;
                case eVehicleTypes.ElectricCar:
                    Vehicle = new Car();
                    Vehicle.VehicleEnergy.EnergyType = VehicleEnergy.eEnergyType.Electricity.ToString();
                    Vehicle.VehicleEnergy.MaximalEnergyAmount = 3.2f;
                    break;
                case eVehicleTypes.FuelMotorcycle:
                    Vehicle = new Motorcycle();
                    Vehicle.VehicleEnergy.EnergyType = VehicleEnergy.eEnergyType.Octan96.ToString();
                    Vehicle.VehicleEnergy.MaximalEnergyAmount = 6f;
                    break;
                case eVehicleTypes.ElectricMotorcycle:
                    Vehicle = new Motorcycle();
                    Vehicle.VehicleEnergy.EnergyType = VehicleEnergy.eEnergyType.Electricity.ToString();
                    Vehicle.VehicleEnergy.MaximalEnergyAmount = 1.8f;
                    break;
                case eVehicleTypes.FuelTruck:
                    Vehicle = new FuelTruck();
                    break;
                default:
                    throw new System.Exception("The vehicle's type is not supported.");
            }

            return Vehicle;
        }
    }
}