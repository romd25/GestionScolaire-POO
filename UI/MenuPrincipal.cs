using Spectre.Console;
using GestionScolaireFINAL.Models;
using GestionScolaireFINAL.Patterns;

namespace GestionScolaireFINAL.UI
{
    public class MenuPrincipal
    {
        private GestionnaireEcole _gestionnaire;
        private ClassementEtudiants _classement;

        public MenuPrincipal(GestionnaireEcole gestionnaire)
        {
            _gestionnaire = gestionnaire;
            _classement = new ClassementEtudiants(new TriParMoyenne());
            ChargerDonneesDemo();
        }

        public void Lancer()
        {
            bool continuer = true;
            while (continuer)
            {
                var choix = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Menu principal")
                        .AddChoices("Ajouter etudiant", "Lister etudiants", "Ajouter cours", "Saisir note", "Classement", "Bulletin", "Alertes", "Quitter"));

                switch (choix)
                {
                    case "Ajouter etudiant": AjouterEtudiant(); break;
                    case "Lister etudiants": ListerEtudiants(); break;
                    case "Ajouter cours": AjouterCours(); break;
                    case "Saisir note": SaisirNote(); break;
                    case "Classement": Classement(); break;
                    case "Bulletin": Bulletin(); break;
                    case "Alertes": Alertes(); break;
                    case "Quitter": continuer = false; break;
                }
            }
        }

        private void AjouterEtudiant()
        {
            try
            {
                string nom = AnsiConsole.Ask<string>("Nom :");
                string prenom = AnsiConsole.Ask<string>("Prenom :");
                string email = AnsiConsole.Ask<string>("Email :");
                int annee = AnsiConsole.Ask<int>("Annee (1-3) :");
                var e = _gestionnaire.AjouterEtudiant(nom, prenom, email, annee);
                AnsiConsole.WriteLine("OK : " + e.NomComplet() + " (" + e.Matricule + ")");
            }
            catch (Exception ex) { AnsiConsole.WriteLine("Erreur : " + ex.Message); }
        }

        private void ListerEtudiants()
        {
            foreach (var e in _gestionnaire.Etudiants)
                AnsiConsole.WriteLine(e.Matricule + " | " + e.NomComplet() + " | An." + e.Annee + " | " + e.CalculerMoyenne().ToString("F2") + "/20");
        }

        private void AjouterCours()
        {
            try
            {
                string intitule = AnsiConsole.Ask<string>("Intitule :");
                double coeff = AnsiConsole.Ask<double>("Coefficient :");
                var c = _gestionnaire.AjouterCours(intitule, coeff);
                AnsiConsole.WriteLine("OK : " + c.Intitule + " (" + c.Code + ")");
            }
            catch (Exception ex) { AnsiConsole.WriteLine("Erreur : " + ex.Message); }
        }

        private void SaisirNote()
        {
            if (_gestionnaire.Etudiants.Count == 0 || _gestionnaire.Cours.Count == 0)
            {
                AnsiConsole.WriteLine("Il faut au moins un etudiant et un cours.");
                return;
            }

            var choixE = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Etudiant :").AddChoices(_gestionnaire.Etudiants.Select(e => e.Matricule + " " + e.NomComplet())));
            var etudiant = _gestionnaire.TrouverEtudiant(choixE.Split(' ')[0]);

            var choixC = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Cours :").AddChoices(_gestionnaire.Cours.Select(c => c.Code + " " + c.Intitule)));
            var cours = _gestionnaire.TrouverCours(choixC.Split(' ')[0]);

            if (etudiant == null || cours == null) return;

            try
            {
                double valeur = AnsiConsole.Ask<double>("Note (0-20) :");
                _gestionnaire.SaisirNote(etudiant, cours, valeur);
                AnsiConsole.WriteLine("Note enregistree.");
            }
            catch (Exception ex) { AnsiConsole.WriteLine("Erreur : " + ex.Message); }
        }

        private void Classement()
        {
            var choix = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Tri :").AddChoices("Par moyenne", "Alphabetique", "Par annee"));

            IStrategieTri s;
            if (choix == "Alphabetique")
                s = new TriAlphabetique();
            else if (choix == "Par annee")
                s = new TriParAnnee();
            else
                s = new TriParMoyenne();

            _classement.ChangerStrategie(s);

            int rang = 1;
            foreach (var e in _classement.Classer(_gestionnaire.Etudiants))
                AnsiConsole.WriteLine(rang++ + ". " + e.NomComplet() + " - " + e.CalculerMoyenne().ToString("F2") + "/20 - " + Note.MentionDepuis(e.CalculerMoyenne()));
        }

        private void Bulletin()
        {
            if (_gestionnaire.Etudiants.Count == 0) { AnsiConsole.WriteLine("Aucun etudiant."); return; }

            var choix = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Etudiant :").AddChoices(_gestionnaire.Etudiants.Select(e => e.Matricule + " " + e.NomComplet())));
            var etudiant = _gestionnaire.TrouverEtudiant(choix.Split(' ')[0]);
            if (etudiant == null) return;

            BulletinDecorator b = new BulletinSimple(etudiant);
            b = new BulletinAvecMoyenne(b);
            b = new BulletinAvecEntete(b, "Ecole Superieure d'Informatique");
            AnsiConsole.WriteLine(b.Generer());
        }

        private void Alertes()
        {
            if (_gestionnaire.Alertes.Alertes.Count == 0) { AnsiConsole.WriteLine("Aucune alerte."); return; }
            foreach (var a in _gestionnaire.Alertes.Alertes)
                AnsiConsole.WriteLine(a);
        }

        private void ChargerDonneesDemo()
        {
            var maths = _gestionnaire.AjouterCours("Mathematiques", 3);
            var algo = _gestionnaire.AjouterCours("Algorithmique", 4);
            var bdd = _gestionnaire.AjouterCours("Bases de donnees", 2);
            var anglais = _gestionnaire.AjouterCours("Anglais", 1);

            var dupont = _gestionnaire.AjouterProfesseur("Dupont", "Marie", "m.dupont@ecole.be", "Mathematiques");
            var lebrun = _gestionnaire.AjouterProfesseur("Lebrun", "Paul", "p.lebrun@ecole.be", "Informatique");
            maths.AssignerProfesseur(dupont);
            algo.AssignerProfesseur(lebrun);
            bdd.AssignerProfesseur(lebrun);

            var alice = _gestionnaire.AjouterEtudiant("Martin", "Alice", "alice.martin@etud.be", 2);
            var bob = _gestionnaire.AjouterEtudiant("Durand", "Bob", "bob.durand@etud.be", 2);
            var clara = _gestionnaire.AjouterEtudiant("Simon", "Clara", "clara.simon@etud.be", 1);
            var david = _gestionnaire.AjouterEtudiant("Leroy", "David", "david.leroy@etud.be", 3);

            _gestionnaire.SaisirNote(alice, maths, 15.5);
            _gestionnaire.SaisirNote(alice, algo, 17.0);
            _gestionnaire.SaisirNote(alice, bdd, 14.0);
            _gestionnaire.SaisirNote(alice, anglais, 13.0);
            _gestionnaire.SaisirNote(bob, maths, 8.5, "A surveiller");
            _gestionnaire.SaisirNote(bob, algo, 11.0);
            _gestionnaire.SaisirNote(bob, bdd, 9.0);
            _gestionnaire.SaisirNote(clara, maths, 12.0);
            _gestionnaire.SaisirNote(clara, algo, 16.5);
            _gestionnaire.SaisirNote(david, maths, 6.0, "Doit revoir les bases");
            _gestionnaire.SaisirNote(david, bdd, 18.5);
            _gestionnaire.SaisirNote(david, anglais, 15.0);
        }
    }
}
