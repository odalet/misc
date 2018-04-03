# Fusion Monitoring

Quelques remarques :

* Tu t’es attribué la tâche de lecture des fichiers binaires… pour écriture en base ? Avec filtrage avant/après … pas clair
* Structure de la base ; on mélange plusieurs choses là : les données brutes et le filtrage en vecteurs… Et dans le détail (au moins pour les données brutes) on aurait plutôt ça :
	* 2 collections / build
		* RawData
			* Timestamp, thx, thx’, thy, thy’, thz, thz’, pl, c1, c2, c3
			* NB : pas besoin d’ID explicite (il y en a un généré automatiquement par mongo)
		* Vectors -> déduit de raw data (demain en direct depuis le fichier ?)
			* Cette structure évoluera lorsqu’on y intègrera les données calculées et le filtrage…
		* LayersIndex
			* Layer Index, Begin Timestamp, End Timestamp
* Question : comment tu passes de tes estimations ‘bitstream’ à ‘Bdd structurée’ ?
