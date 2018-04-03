# Stories & TODOs

## Image Processing -> Usine logicielle

* Nugetiser
* Revue de code/architecture globale de l'existant 
* Utiliser les versions "achetées" d'OpenCV (nugetiser OpenCV ?)

## Dette technique : Etat des dépôts

* Mettre à jour la page [Dépôts Git](https://fmas-ul01.fmas.local:8093/pages/viewpage.action?pageId=4554962)
	* Nettoyage dépôts inutiles
	* Ajout dépôts manquants
	* Ajout de colonnes pour : CI/Tests/Sonar/Obfuscation
* Obfusquer le code non encore obfusqué.

## Dette technique : Sonar / NCore Server

* Sécher des warnings Sonar
* Mais surtout, redéfinir les règles à appliquer :

Exemples :

* En profiter pour supprimer les règles dont on ne veut pas
	* Augmenter le seuil de la complexité cyclomatique
	* Supprimer "le retour à la ligne après un if"
* Et garder :
	* Comparaison des flottants (quitte à la désactiver à la main la plupart du temps).
	* Pas de paramètre par défaut sur les méthodes visibles.

--> A estimer en moyens et pas résultats. 

				
## Intégration Segment -> AddUp.Geometry

* Déplacer la classe Segment de mctl vers **AddUp.Geometry**
	* Renommer en **Interval**
	* Ajouter des Tests unitaires

# Done

## Modular Architecture - Story #1

* Créer une application console qui :
	* charge un addproj
	* le convertit en "Modèle Projet" (ne gérer que le chauffage de la plateforme)
	* Crée une machine
	* Valide le projet par rapport à la machine
	* Donne un compte-rendu de chargement / validation 

* Entités
	* ProjectLoader
	* Parameter Definition
	* Parameter
	* Machine Definition
	* Machine
	* ProjectValidator
	* Validation framework
		* Chaînage des validations (and, or, if...),
		* Conservation (concaténation) des erreurs
	* Service Provider ?

* Développer cette story en multiples petits cycles :
	1. code
	2. discussion nico
	3. refactorisation / ajouts	

* Faire du TDD-like (avec des vrais Tests **Unitaires**)
	* Ne pas s'astreindre à écrire le code minimal qui fait passer les tests (mais bon pas trop quand même...)
	* En revanche, capitaliser dès le début sur des entités testables et pour ça : écrire les jeux d'essai (et les TUs concernés) avant.		
	* PS : ces TUs n'excluent pas des tests auto plus foncctionnels (plus de bout en bout).	

## Addproj Reader V2

Upgrader les composants suivants :

* NCore Server
* AddUp.Extensibility
* Algos d'ordonnancement pièce.