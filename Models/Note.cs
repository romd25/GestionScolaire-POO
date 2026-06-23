namespace GestionScolaireFINAL.Models
{
    public class Note
    {
        public Cours Cours { get; }
        public double Valeur { get; private set; }
        public string? Commentaire { get; set; }

        public Note(Cours cours, double valeur, string? commentaire = null)
        {
            if (valeur < 0 || valeur > 20)
                throw new ArgumentException("Une note doit être comprise entre 0 et 20.");

            Cours = cours;
            Valeur = valeur;
            Commentaire = commentaire;
        }

        public string Mention()
        {
            if (Valeur >= 16) return "Très bien";
            if (Valeur >= 14) return "Bien";
            if (Valeur >= 12) return "Assez bien";
            if (Valeur >= 10) return "Passable";
            return "Insuffisant";
        }

        public static string MentionDepuis(double valeur)
        {
          if (valeur >= 16) return "Très bien";
          if (valeur >= 14) return "Bien";
          if (valeur >= 12) return "Assez bien";
          if (valeur >= 10) return "Passable";
          return "Insuffisant";
        }

        public override string ToString()
        {
            return Cours.Intitule + " : " + Valeur + "/20 (" + Mention() + ")";
        }
    }
}
