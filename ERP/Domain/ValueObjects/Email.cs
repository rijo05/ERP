using ERP.Domain.Entities;

namespace ERP.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        private Email() { }
        public Email(string email) 
        { 
            ValidateEmail(email);
            Value = email;
        }


        //Todo() - Adicionar regex mas n importante por agora
        private void ValidateEmail(string email)
        {
            if(email is null) { throw new ArgumentNullException("email nao pode ser null"); }
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            var other = (Email)obj;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
