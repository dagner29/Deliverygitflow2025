using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.ValueObjects
{
    public class Address
    {
        public int Id { get; set; }  // ✅ Clave primaria
        public string Street { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        //public float Latitude { get; private set; }
        //public float Longitude { get; private set; }

        public Address(string street, string city, string postalCode)
         // public Address(string street, string city, float latitude, float longitude)
        {

            // ✅ Validaciones para evitar valores inválidos
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street es obligatorio", nameof(street));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City es obligatorio", nameof(city));

            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentException("PostalCode es obligatorio", nameof(postalCode));



            Street = street;
            City = city;
            PostalCode = postalCode;
            //Latitude = latitude;
            //Longitude = longitude;
        }

    }
}
