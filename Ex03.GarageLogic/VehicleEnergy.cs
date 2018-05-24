using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
   public class VehicleEnergy
    {
        public enum eEnergyType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98,
            Electricity,
        }

        private eEnergyType m_EnergyType;
        private float m_RemainingEnergyAmount;
        private float m_MaximalEnergyAmount;

        protected internal void EnergyFilling(float i_AmountOfEnergyToAdd, eEnergyType i_EnergyType, ref Vehicle io_Vehicle)
        {
            if ((i_AmountOfEnergyToAdd + m_RemainingEnergyAmount > m_MaximalEnergyAmount) || (i_AmountOfEnergyToAdd < 0))
            {
                throw new ValueOutOfRangeException("VehicleEnergy", 0.0f, m_MaximalEnergyAmount - m_RemainingEnergyAmount);
            }

            if (i_EnergyType != m_EnergyType)
            {
                throw new ArgumentException("Wrong Energy type at Fill()", "i_FuelType");
            }

            m_RemainingEnergyAmount += i_AmountOfEnergyToAdd;
            io_Vehicle.CalculateRemainingEnergyPercent();
        }

        public string RemainingEnergyAmount
        {
            get { return m_RemainingEnergyAmount.ToString(); }
            set
            {
                try
                {
                    m_RemainingEnergyAmount = float.Parse(value);
                }
                catch (FormatException)
                {
                    throw new FormatException("Remaining Energy Amount");
                }

                if (float.Parse(value) > m_MaximalEnergyAmount || float.Parse(value) < 0)
                {
                    throw new ValueOutOfRangeException("Remaining Energy Amount", 0.0f, m_MaximalEnergyAmount);
                }
            }
        }

        public float MaximalEnergyAmount
        {
        get { return m_MaximalEnergyAmount; }
        set { m_MaximalEnergyAmount = value; }
        }

        public string EnergyType
        {
            get { return m_EnergyType.ToString(); }
            set
            {
                if (!Enum.IsDefined(typeof(eEnergyType), value))
                {
                    throw new FormatException("Wrong Energy Type");
                }

                try
                {
                    m_EnergyType = (eEnergyType)Enum.Parse(typeof(eEnergyType), value);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("Energy Type");
                }
            }
        }

        public override string ToString()
        {
            string energyInfo = string.Format("EnergyType: {0}{2}Remaining Energy Amount: {1}{2}Maximum Energy Amount: {3}", m_EnergyType, RemainingEnergyAmount, Environment.NewLine, MaximalEnergyAmount);
            return energyInfo;
        }
    }
}