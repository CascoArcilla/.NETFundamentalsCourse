using System.Text.RegularExpressions;

namespace Domain
{
    // Para el curso se establce Entity al final del nombre de la clase, pero en la práctica se suele omitir, quedando solo Person
    public class PersonEntity
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;

        public PersonEntity(string code, string firstName, string lastName, string email, string phoneNumber)
        {
            ValidateCode(code);
            ValidateName(firstName);
            ValidateLastName(lastName);
            ValidateEmail(email);
            ValidatePhoneNumber(phoneNumber);

            Id = Guid.NewGuid();
            Code = code;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        private void ValidateCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("El código no puede ser vacío.", nameof(code));

            if (code.Length < 3)
                throw new ArgumentException("El código debe tener al menos 3 caracteres.", nameof(code));
            
            if (code.Length > 20)
                throw new ArgumentException("El código debe tener maximo 20 caracteres.", nameof(code));
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre no puede ser vacío.", nameof(name));

            if (name.Length < 2)
                throw new ArgumentException("El nombre debe tener al menos 2 caracteres.", nameof(name));

            if (name.Length > 50)
                throw new ArgumentException("El nombre debe tener maximo 50 caracteres.", nameof(name));
        }

        private void ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("El apellido no puede ser vacío.", nameof(lastName));

            if (lastName.Length < 2)
                throw new ArgumentException("El apellido debe tener al menos 2 caracteres.", nameof(lastName));

            if (lastName.Length > 50)
                throw new ArgumentException("El apellido debe tener maximo 50 caracteres.", nameof(lastName));
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo electrónico no puede ser vacío.", nameof(email));

            if (email.Length > 100)
                throw new ArgumentException("El correo electrónico debe tener maximo 100 caracteres.", nameof(email));

            // Seria mas correcto madar esto a otra clase para ser usado por otras entidades, pero para el curso lo dejamos aquí
            var patern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, patern))
            {
                throw new ArgumentException("El correo electrónico no tiene un formato válido.", nameof(email));
            }
        }

        private void ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("El apellido no puede ser vacío.", nameof(phoneNumber));

            if (phoneNumber.Length < 2)
                throw new ArgumentException("El apellido debe tener al menos 2 caracteres.", nameof(phoneNumber));

            if (phoneNumber.Length > 50)
                throw new ArgumentException("El apellido debe tener maximo 50 caracteres.", nameof(phoneNumber));
        }
    }
}
