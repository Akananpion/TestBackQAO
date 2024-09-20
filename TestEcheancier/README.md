# Introduction

Ce projet permet d'afficher un échéancier basé sur les paramètres fournis. Pour définir l'échéancier, les paramètres suivants sont requis :

- Un montant annuel en euros
- Une date de début

En fonction des paramètres fournis, trois scénarios de calcul sont possibles :

1. **Date de début, date de fin, et périodicité** :
   - Exemple : Début = 01/01/2023, Fin = 31/12/2023, Périodicité = 4 mois, Montant annuel = 1200€
   - Résultat attendu : Trois échéances comme suit :
     - du 01/01/2023 au 30/04/2023 pour un montant de 400,00 €
     - du 01/05/2023 au 31/08/2023 pour un montant de 400,00 €
     - du 01/09/2023 au 31/12/2023 pour un montant de 400,00 €

2. **Date de début, nombre d'échéances, et périodicité** :
   - Exemple : Début = 01/01/2023, Nombre d'échéances = 2, Périodicité = 4 mois, Montant annuel = 1200€
   - Résultat attendu : Deux échéances comme suit :
     - du 01/01/2023 au 30/04/2023 pour un montant de 600,00 €
     - du 01/05/2023 au 31/08/2023 pour un montant de 600,00 €

3. **Date de début, nombre d'échéances, et date de fin** :
   - Exemple : Début = 01/01/2023, Nombre d'échéances = 2, Fin = 31/12/2023, Montant annuel = 1200€
   - Résultat attendu : Deux échéances comme suit :
     - du 01/01/2023 au 30/06/2023 pour un montant de 600,00 €
     - du 01/07/2023 au 31/12/2023 pour un montant de 600,00 €

## Arguments

Les arguments sont passés en ligne de commande comme suit :

- `-d` : Date de début (format ISO 8601) => obligatoire
- `-f` : Date de fin (format ISO 8601)
- `-p` : Périodicité en mois
- `-n` : Nombre d'échéances
- `-m` : Montant annuel décimal => obligatoire

Les arguments doivent être suivis du symbole `=` et de leur valeur respective. Exemple : `-d=2023-09-18`

## Fonctionnalités

Les modifications apportées au fichier `App.cs` incluent :

- **Ajout des méthodes** :
  - `CalculerEcheancierAvecPeriode` : Gère le scénario avec date de début, date de fin et périodicité.
  - `CalculerEcheancierAvecNombreEcheances` : Gère le scénario avec date de début, nombre d'échéances et périodicité.
  - `CalculerEcheancierAvecFin` : Gère le scénario avec date de début, nombre d'échéances et date de fin.

- **Formatage des montants** : Les montants sont affichés en euros avec un formatage adapté à la culture française (`fr-FR`).

- **Affichage des résultats** : Les messages de sortie utilisent la phrase "pour un montant de :" pour présenter les montants.

## Configuration des Services

L'application utilise l'injection de dépendances pour gérer ses services. Voici comment les services sont configurés :

1. **Configuration des Services** : Les services sont configurés dans la méthode `ConfigureServices` de `Program.cs`. Actuellement, le service principal `App` est enregistré en tant que service transitoire.

    ```csharp
    private static IServiceCollection ConfigureServices()
    {
        IServiceCollection services = new ServiceCollection();

        // Ajoute le service principal de l'application
        services.AddTransient<App>();
        return services;
    }
    ```

2. **Injection de Dépendances** : Le conteneur d'injection de dépendances est construit à l'aide de `serviceProvider`, et le service `App` est résolu pour exécuter l'application.

    ```csharp
    var serviceProvider = services.BuildServiceProvider();
    serviceProvider.GetService<App>().Run(arguments);
    ```

## Compilation et Exécution

Pour compiler le projet, utilisez la commande suivante :

```bash
dotnet build
```

Pour exécuter le programme, utilisez la commande suivante en remplaçant les arguments par les valeurs souhaitées :

Pour le 1er cas (Avoir une date de début, une date de fin, et une périodicité)

```bash
dotnet run -- -d=2023-01-01 -f=2023-12-31 -p=4 -m=1200
```

Pour le 2e cas (Avoir une date de début, un nombre d'échéances, et une périodicité)

```bash
dotnet run -- -d=2023-01-01 -n=2 -p=4 -m=1200
```

Pour le 3e cas (Avoir une date de début, un nombre d'échéances, et une date de fin)

```bash
dotnet run -- -d=2023-01-01 -f=2023-12-31 -n=2 -m=1200
```