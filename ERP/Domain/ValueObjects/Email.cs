using ERP.Domain.Entities;

namespace ERP.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }
        public Email(string email) 
        { 
            ValidateEmail(email);
            Value = email;
        }


        //Todo() - Adicionar regex mas n importante por agora
        private void ValidateEmail(string email)
        {
            if(email == null) { throw new ArgumentNullException("email nao pode ser null"); }
        }
    }
}
