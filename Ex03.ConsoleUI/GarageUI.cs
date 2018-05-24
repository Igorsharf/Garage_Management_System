using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private const string quitInputUpperCase = "Q";
        private const string quitInputLowwerCase = "q";

        private static readonly Garage r_Garage = new Garage();
        private bool m_QuitProgram = false;

        public bool QuitProgram
        {
            get
            {
                return this.m_QuitProgram;
            }

            set
            {
                this.m_QuitProgram = value;
            }
        }

        internal void StartMenu()
        {
            string userInput;
            int userInputNumber;
            while (QuitProgram != true)
            {
                Console.Clear();
                string mainMenuString = string.Format("Main Menu{0}Optional operations :{0}1. Insert a new vehicle to the garage.{0}2. Display a list of license numbers currently in the garage.{0}3. Change a certain vehicle’s status.{0}4. Inflate tires to maximum.{0}5. Refuel a fuel-based vehicle.{0}6. Charge an electric-based vehicle.{0}7. Display vehicle information.{0}Enter the number of the wanted operation or Q to quit : ", Environment.NewLine);
                Console.Write(mainMenuString);

                userInput = Console.ReadLine();
                while ((!userInput.Equals(quitInputUpperCase) && !userInput.Equals(quitInputLowwerCase) && !int.TryParse(userInput, out userInputNumber)) || (int.TryParse(userInput, out userInputNumber) && (userInputNumber > 7 || userInputNumber < 1)))
                {
                    Console.WriteLine("Wrong input, Please try again.");
                    userInput = Console.ReadLine();
                }

                if (userInput == quitInputUpperCase || userInput == quitInputLowwerCase)
                {
                    QuitProgram = true;
                }
                else
                {
                    GarageOperations(userInputNumber);
                }
            }
        }

        private static void GarageOperations(int i_UserInputOperation)
        {
            Console.Clear();
            Console.Write("Wanted operation : ");
            switch (i_UserInputOperation)
            {
                case 1:
                    Console.WriteLine("Insert a new vehicle to the garage.");
                    InsertNewVehicle();
                    break;
                case 2:
                    Console.WriteLine("Display a list of license numbers currently in the garage.");
                    DisplayListOfLicenseNumbers();
                    break;
                case 3:
                    Console.WriteLine("Change a certain vehicle’s status.");
                    ChangeVehicleStatus();
                    break;
                case 4:
                    Console.WriteLine("Inflate tires to maximum.");
                    inflateWheelsToMax();
                    break;
                case 5:
                    Console.WriteLine("Refuel a fuel-based vehicle.");
                    RefuelFuelBasedVehicle();
                    break;

                case 6:
                    Console.WriteLine("Charge an electric-based vehicle.");
                    ChargeElectricBasedVehicle();
                    break;

                case 7:
                    Console.WriteLine("Display vehicle information.");
                    DisplayVehicleInformation();
                    break;
            }

            enterButtonToMainMenu();
        }

        public static string GetLicenseNumberFromUser()
        {
            string inputLicenseNumber;
            Console.WriteLine("Enter the license number of the vehicle : ");
            inputLicenseNumber = Console.ReadLine();
            while (string.IsNullOrEmpty(inputLicenseNumber) == true)
            {
                Console.WriteLine("Wrong license number! Try again:");
                inputLicenseNumber = Console.ReadLine();
            }

            return inputLicenseNumber;
        }

        public static Vehicle CreateVehicle(string i_VehicleLisenceNumber)
        {
            bool validInput = false;
            Vehicle createdVehicle;
            StringBuilder vehiclesMenu = new StringBuilder();
            vehiclesMenu.Append(string.Format("- New Vehicle -{0}", Environment.NewLine));
            int indexOfEnum = 1;
            foreach (string item in Enum.GetNames(typeof(VehicleCreator.eVehicleTypes)))
            {
                vehiclesMenu.Append(string.Format("{0}. {1}{2}", indexOfEnum, item, Environment.NewLine));
                indexOfEnum++;
            }

            string inputString;
            int wantedVehicle;

            Console.WriteLine(vehiclesMenu.ToString());
            inputString = Console.ReadLine();
            int.TryParse(inputString, out wantedVehicle);
            while (wantedVehicle < 1 || wantedVehicle > Enum.GetNames(typeof(VehicleCreator.eVehicleTypes)).Length)
            {
                Console.WriteLine("Wrong option,Try again.");
                inputString = Console.ReadLine();
                int.TryParse(inputString, out wantedVehicle);
            }

            createdVehicle = VehicleCreator.MakeVehicle((VehicleCreator.eVehicleTypes)Enum.ToObject(typeof(VehicleCreator.eVehicleTypes), wantedVehicle));
            Dictionary<string, string> vehicleDictionary = createdVehicle.CreateParametersFormat();
            createdVehicle.VehicleLicenseNumber = i_VehicleLisenceNumber;
            Console.WriteLine("-Please enter vehicle parameters-");

            while (validInput == false)
            {
                try
                {
                    FillOutParameters(ref vehicleDictionary, createdVehicle);
                    validInput = true;
                    createdVehicle.GetParameters(vehicleDictionary);
                }
                catch (ArgumentException currentException)
                {
                    validInput = false;
                    vehicleDictionary[currentException.Message] = null;
                    Console.WriteLine(string.Format("{0} has an invalid value. Please try again.", currentException.Message));
                }
                catch (FormatException currentException)
                {
                    validInput = false;
                    vehicleDictionary[currentException.Message] = null;
                    Console.WriteLine(string.Format("{0} is not properly formatted. Please try again:", currentException.Message));
                }
                catch (ValueOutOfRangeException currentException)
                {
                    validInput = false;
                    vehicleDictionary[currentException.Message] = null;
                    Console.WriteLine(string.Format("{0} - {1}. Please try again:", currentException.Message, currentException.ToString()));
                }
            }

            return createdVehicle;
        }

        private static void FillOutParameters(ref Dictionary<string, string> io_Dict, Vehicle i_vhicle)
        {
            Dictionary<string, string> newDictWithUserInput = new Dictionary<string, string>();

            foreach (string currentKey in io_Dict.Keys)
            {
                string enumPrinter = string.Empty;
                if (io_Dict[currentKey] == null)
                {
                    enumPrinter = i_vhicle.InfoPrinterByKey(currentKey);
                    Console.Write(enumPrinter);
                    Console.Write(string.Format("{0}: ", currentKey));
                    newDictWithUserInput.Add(currentKey, Console.ReadLine());
                }
                else
                {
                    newDictWithUserInput.Add(currentKey, io_Dict[currentKey]);
                }
            }

            io_Dict = newDictWithUserInput;
        }

        private static void InsertNewVehicle()
        {
            string inputLicenseNumber;
            inputLicenseNumber = GetLicenseNumberFromUser();
            if (r_Garage.ChangeConditionOfVehicleInGarage(inputLicenseNumber, Vehicle.eVehicleConditionInGarage.Fixing) == true)
            {
                Console.WriteLine("The vehicle in the garage with 'Fixing' status.");
            }
            else
            {
                Vehicle wantedVehicleToInsert = CreateVehicle(inputLicenseNumber);
                r_Garage.InsertVehicleIntoGarage(wantedVehicleToInsert);
                Console.WriteLine("The vehicle was inserted into the garage.");
            }
        }

        private static void DisplayListOfLicenseNumbers()
        {
            if (r_Garage.VehiclesInGarage.Count == 0)
            {
                Console.WriteLine("The garage is empty.");
            }
            else
            {
                string inputString;
                string outputString = string.Empty;
                int inputChooseNumber;
                StringBuilder optionsList = new StringBuilder();
                optionsList.Append(string.Format("Show list by :{0}1. Show all vehicles.{0}", Environment.NewLine));
                int indexOfEnum = 2;
                foreach (string item in Enum.GetNames(typeof(Vehicle.eVehicleConditionInGarage)))
                {
                    optionsList.Append(string.Format("{0}. {1} status vehicles.{2}", indexOfEnum, item, Environment.NewLine));
                    indexOfEnum++;
                }

                Console.WriteLine(optionsList);
                inputString = Console.ReadLine();
                while (int.TryParse(inputString, out inputChooseNumber) == false || inputChooseNumber < 1 || inputChooseNumber > Enum.GetNames(typeof(Vehicle.eVehicleConditionInGarage)).Length + 1)
                {
                    Console.WriteLine("Wrong input, Try again.");
                    inputString = Console.ReadLine();
                }

                if (inputChooseNumber == 1)
                {
                    outputString = r_Garage.ShowListOfVehiclesByCondition(null);
                }
                else
                {
                    outputString = r_Garage.ShowListOfVehiclesByCondition((Vehicle.eVehicleConditionInGarage)(--inputChooseNumber));
                }

                Console.WriteLine(outputString);
            }
        }

        private static void ChangeVehicleStatus()
        {
            string inputChooseString;
            int optionValue;
            string userInputLicenseNumber = GetLicenseNumberFromUser();
            if (r_Garage.GetVehicleInGarageByLicense(userInputLicenseNumber) == null)
            {
                Console.WriteLine("The current car is not in the garage.");
                return;
            }

            StringBuilder vehiclesStatusOptions = new StringBuilder();
            vehiclesStatusOptions.Append(string.Format("- Status options : -{0} ", Environment.NewLine));
            int indexOfEnum = 1;
            foreach (string item in Enum.GetNames(typeof(Vehicle.eVehicleConditionInGarage)))
            {
                vehiclesStatusOptions.Append(string.Format("{0}. {1}{2}", indexOfEnum, item, Environment.NewLine));
                indexOfEnum++;
            }

            Console.WriteLine(vehiclesStatusOptions.ToString());
            inputChooseString = Console.ReadLine();
            while (int.TryParse(inputChooseString, out optionValue) == false || optionValue < 1 || optionValue > Enum.GetNames(typeof(Vehicle.eVehicleConditionInGarage)).Length)
            {
                Console.WriteLine("Wrong input, Try again.");
                inputChooseString = Console.ReadLine();
            }

            r_Garage.ChangeConditionOfVehicleInGarage(userInputLicenseNumber, (Vehicle.eVehicleConditionInGarage) optionValue);
            Console.WriteLine("Vehicle status has been changed.");
        }

        private static void inflateWheelsToMax()
        {
            string userInputLicenseNumber = GetLicenseNumberFromUser();
            r_Garage.InflatedWheelsOfVehicleIntoMax(userInputLicenseNumber);
            Console.WriteLine("Wheels were inflated to maximal air pressure.");
        }

        private static void RefuelFuelBasedVehicle()
        {
            string userInputLicenseNumber = GetLicenseNumberFromUser();
            Vehicle vehicleToRefuel = r_Garage.GetVehicleInGarageByLicense(userInputLicenseNumber);
            string fuelAmountinput;

            if (vehicleToRefuel == null)
            {
                Console.WriteLine("The wanted vehicle is not in the garage!");
            }
            else if (vehicleToRefuel.VehicleEnergy.EnergyType.Equals(VehicleEnergy.eEnergyType.Electricity.ToString()))
            {
                Console.WriteLine("The vehicle is electric based. Refual are not allowed.");
            }
            else
            {
                float fuelAmoutToRefuel, currentFuelAmount;
                float.TryParse(vehicleToRefuel.VehicleEnergy.RemainingEnergyAmount, out currentFuelAmount);
                bool validInput = false;
                string fuelInfo = string.Format("Vehicle current amount of fuel: {0} ,Vehicle maximal amount of fuel: {1}.{2}Enter Amount to refuel: ", vehicleToRefuel.VehicleEnergy.RemainingEnergyAmount, vehicleToRefuel.VehicleEnergy.MaximalEnergyAmount, Environment.NewLine);
                Console.WriteLine(fuelInfo);
                while (validInput == false)
                {
                    try
                    {
                        fuelAmountinput = Console.ReadLine();
                        while (float.TryParse(fuelAmountinput, out fuelAmoutToRefuel) == false)
                        {
                            Console.WriteLine("Wrong input, Try again.");
                            fuelAmountinput = Console.ReadLine();
                        }

                        r_Garage.RefuelOrRechargeVehicleInGarage(userInputLicenseNumber, (VehicleEnergy.eEnergyType)Enum.Parse(typeof(VehicleEnergy.eEnergyType), vehicleToRefuel.VehicleEnergy.EnergyType), fuelAmoutToRefuel);
                        validInput = true;
                    }
                    catch (ValueOutOfRangeException currentException)
                    {
                        Console.WriteLine(currentException.ToString());
                        Console.WriteLine("Try again:");
                    }
                }

                Console.WriteLine("The vehicale had been refuled.");
            }
        }

        private static void ChargeElectricBasedVehicle()
        {
            string userInputLicenseNumber = GetLicenseNumberFromUser();
            Vehicle vehicleToCharge = r_Garage.GetVehicleInGarageByLicense(userInputLicenseNumber);
            string BatteryChargeTimeinput;

            if (vehicleToCharge == null)
            {
                Console.WriteLine("The wanted vehicle is not in the garage!");
            }
            else if (vehicleToCharge.VehicleEnergy.EnergyType.Equals(VehicleEnergy.eEnergyType.Electricity.ToString()) == false)
            {
                Console.WriteLine("The vehicle is fuel based. Charge are not allowed.");
            }
            else
            {
                float BatteryTimeToCharge, currentBatteryAmount;
                float.TryParse(vehicleToCharge.VehicleEnergy.RemainingEnergyAmount, out currentBatteryAmount);
                bool validInput = false;
                string fuelInfo = string.Format("Vehicle current battery time: {0} ,Vehicle maximal battery time: {1}.{2}Enter time to charge: ", vehicleToCharge.VehicleEnergy.RemainingEnergyAmount, vehicleToCharge.VehicleEnergy.MaximalEnergyAmount, Environment.NewLine);
                Console.WriteLine(fuelInfo);
                while (validInput == false)
                {
                    try
                    {
                        BatteryChargeTimeinput = Console.ReadLine();
                        while (float.TryParse(BatteryChargeTimeinput, out BatteryTimeToCharge) == false)
                        {
                            Console.WriteLine("Wrong input, Try again.");
                            BatteryChargeTimeinput = Console.ReadLine();
                        }

                        r_Garage.RefuelOrRechargeVehicleInGarage(userInputLicenseNumber, (VehicleEnergy.eEnergyType)Enum.Parse(typeof(VehicleEnergy.eEnergyType), vehicleToCharge.VehicleEnergy.EnergyType), BatteryTimeToCharge);
                        validInput = true;
                    }
                    catch (ValueOutOfRangeException currentException)
                    {
                        Console.WriteLine(currentException.ToString());
                        Console.WriteLine("Try again:");
                    }
                }

                Console.WriteLine("The vehicle had been charged.");
            }
        }

        private static void DisplayVehicleInformation()
        {
            string userInputLicenseNumber = GetLicenseNumberFromUser();
            string vehicleInfoString = r_Garage.GetVehicleDetails(userInputLicenseNumber);
            Console.WriteLine(string.IsNullOrEmpty(vehicleInfoString) ? "There is no vehicle with this license number in the garage." : vehicleInfoString);
        }

        private static void enterButtonToMainMenu()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Press 'Enter' to go back to main menu");
            Console.ReadLine();
        }
    }
}