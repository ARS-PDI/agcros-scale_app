using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
namespace MeasurementEquipment.Types
{
    [Serializable()]
    public class SerialPortValue
    {
        public string DisplayName { get; set; }
        public string PortName { get; set; }

        public override bool Equals(object obj)
        {
            var other = (SerialPortValue)obj;
            return this.DisplayName.Equals(other.DisplayName) && this.PortName.Equals(other.PortName);
        }

        public override int GetHashCode()
        {
            var hashCode = -355259797;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PortName);
            return hashCode;
        }
    }
}
