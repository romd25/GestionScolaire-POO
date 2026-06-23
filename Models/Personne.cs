namespace GestionScolaireFINAL.Models
{
    public abstract class Personne
    {
        public string Nom { get; private set; }
        public string Prenom { get; private set; }
        public string Email { get; private set; }

        protected Personne(string nom, string prenom, string email)
        {
            if (string.IsNullOrWhiteSpace(nom))
                throw new ArgumentException("Le nom ne peut pas être vide.");
            if (string.IsNullOrWhiteSpace(prenom))
                throw new ArgumentException("Le prénom ne peut pas être vide.");
            if (!email.Contains('@'))
                throw new ArgumentException("L'email n'est pas valide.");

            Nom = nom;
            Prenom = prenom;
            Email = email;
        }

        public string NomComplet()
        {
            return Prenom + " " + Nom;
        }

        public abstract string GetRole();

        public override string ToString()
        {
          return NomComplet() + " (" + Email + ")";
        }
    }
}
