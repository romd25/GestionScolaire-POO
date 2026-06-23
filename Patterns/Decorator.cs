using GestionScolaireFINAL.Models;

namespace GestionScolaireFINAL.Patterns
{
    public abstract class BulletinDecorator
    {
        protected Etudiant _etudiant;

        public Etudiant Etudiant { get { return _etudiant; } }

        protected BulletinDecorator(Etudiant etudiant)
        {
            _etudiant = etudiant;
        }

        public abstract string Generer();
    }

    public class BulletinSimple : BulletinDecorator
    {
        public BulletinSimple(Etudiant etudiant) : base(etudiant) { }

        public override string Generer()
        {
            string resultat = "";
            resultat += "Étudiant : " + _etudiant.NomComplet() + "\n";
            resultat += "Matricule : " + _etudiant.Matricule + "\n";
            resultat += "Année : " + _etudiant.Annee + "\n";
            resultat += "---\n";

            foreach (var note in _etudiant.Notes)
              resultat += "  " + note.Cours.Intitule + " : " + note.Valeur + "/20\n";

            return resultat;
        }
    }

    public class BulletinAvecMoyenne : BulletinDecorator
    {
        private BulletinDecorator _base;

        public BulletinAvecMoyenne(BulletinDecorator b) : base(b.Etudiant)
        {
            _base = b;
        }

        public override string Generer()
        {
            double moy = _etudiant.CalculerMoyenne();
            string mention = Note.MentionDepuis(moy);
            return _base.Generer() + "\nMoyenne générale : " + moy.ToString("F2") + "/20 — " + mention;
        }
    }

    public class BulletinAvecEntete : BulletinDecorator
    {
        private BulletinDecorator _base;
        private string _etablissement;

        public BulletinAvecEntete(BulletinDecorator b, string etablissement) : base(b.Etudiant)
        {
          _base = b;
          _etablissement = etablissement;
        }

        public override string Generer()
        {
            string entete = "=== " + _etablissement + " ===\n";
            entete += "Date : " + DateTime.Now.ToString("dd/MM/yyyy") + "\n\n";
            return entete + _base.Generer();
        }
    }
}
