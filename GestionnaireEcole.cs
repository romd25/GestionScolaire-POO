using GestionScolaireFINAL.Models;
using GestionScolaireFINAL.Patterns;

namespace GestionScolaireFINAL
{
    public class GestionnaireEcole
    {
        private List<Etudiant> _etudiants = new List<Etudiant>();
        private List<Cours> _cours = new List<Cours>();

        public AlerteNoteBasse Alertes = new AlerteNoteBasse(10.0);

        public List<Etudiant> Etudiants { get { return _etudiants; } }
        public List<Cours> Cours { get { return _cours; } }

        public Etudiant AjouterEtudiant(string nom, string prenom, string email, int annee)
        {
            var etudiant = new Etudiant(nom, prenom, email, annee);
            etudiant.AjouterObservateur(Alertes);
            _etudiants.Add(etudiant);
            return etudiant;
        }

        public Professeur AjouterProfesseur(string nom, string prenom, string email, string specialite)
        {
          return new Professeur(nom, prenom, email, specialite);
        }

        public Cours AjouterCours(string intitule, double coefficient)
        {
            var cours = new Cours(intitule, coefficient);
            _cours.Add(cours);
            return cours;
        }

        public Etudiant? TrouverEtudiant(string matricule)
        {
            foreach (var e in _etudiants)
            {
              if (e.Matricule == matricule)
                  return e;
            }
            return null;
        }

        public Cours? TrouverCours(string code)
        {
            foreach (var c in _cours)
            {
                if (c.Code == code)
                    return c;
            }
            return null;
        }

        public void SaisirNote(Etudiant etudiant, Cours cours, double valeur, string? commentaire = null)
        {
            var note = new Note(cours, valeur, commentaire);
            etudiant.AjouterNote(note);
            cours.InscrireEtudiant(etudiant);
        }

        public double MoyenneGlobale()
        {
            if (_etudiants.Count == 0) return 0;

            double total = 0;
            int compteur = 0;

            foreach (var e in _etudiants)
            {
                double moy = e.CalculerMoyenne();
                if (moy > 0)
                {
                    total += moy;
                    compteur++;
                }
            }

            if (compteur == 0) return 0;
            return total / compteur;
        }
    }
}
