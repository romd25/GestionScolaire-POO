# GestionScolaireFINAL

Application console de gestion scolaire — POO C# 

## Lancer le projet

dotnet restore
dotnet run

## Structure

```
GestionScolaireFINAL/
├── Models/
│   ├── Personne.cs
│   ├── Etudiant.cs
│   ├── Professeur.cs
│   ├── Cours.cs
│   ├── Note.cs
│   └── Classe.cs
├── Patterns/
│   ├── Observer.cs
│   ├── Factory.cs
│   ├── Strategy.cs
│   └── Decorator.cs
├── UI/
│   └── MenuPrincipal.cs
├── GestionnaireEcole.cs
├── Program.cs
└── diagramme_uml.puml
```

## Patterns utilisés

- **Observer** : alertes automatiques pour les notes inférieures à 10
- **Factory Method** : création des étudiants et professeurs
- **Strategy** : tri des étudiants selon différents critères
- **Decorator** : génération de bulletins par couches
