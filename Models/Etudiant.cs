using GestionScolaireFINAL.Patterns;

namespace GestionScolaireFINAL.Models
{
    public class Etudiant : Personne
    {
        private static int _compteur = 1;
        private List<Note> _notes = new List<Note>();
        private List<IObservateurEtudiant> _observateurs = new List<IObservateurEtudiant>();

        public string Matricule { get; }
        public int Annee { get; set; }
        public List<Note> Notes { get { return _notes; } }

        public Etudiant(string nom, string prenom, string email, int annee) : base(nom, prenom, email)
        {
            if (annee < 1 || annee > 3)
                throw new ArgumentException("L'année doit être entre 1 et 3.");

            Annee = annee;
            Matricule = "ETU" + _compteur++.ToString("D4");
        }

        public override string GetRole()
        {
            return "Étudiant";
        }

        public void AjouterNote(Note note)
        {
            _notes.Add(note);
            foreach (var obs in _observateurs)
                obs.SurNouvelleNote(this, note);
        }

        public double CalculerMoyenne()
        {
            if (_notes.Count == 0) return 0;

            double totalCoeff = 0;
            double totalPondere = 0;

            foreach (var note in _notes)
            {
                totalCoeff   += note.Cours.Coefficient;
                totalPondere += note.Valeur * note.Cours.Coefficient;
            }

            if (totalCoeff == 0) return 0;
            return totalPondere / totalCoeff;
        }

        public void AjouterObservateur(IObservateurEtudiant obs)
        {
            _observateurs.Add(obs);
        }

        public override string ToString()
        {
            return "[" + Matricule + "] " + NomComplet() + " — Année " + Annee;
        }
    }
}
