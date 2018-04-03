# Modular Architecture

## TODO

* Simplifier la paire Module/Definition --> Tout dans Definition
* Simplifier le fx de validation ?
	* Moins fonctionnel, plus procédural
* Revoir les interfaces d'exposition des résultats de validation
	* Done: moins d'interfaces ; on renvoie directement des `ValidationRule<string>`

## Process-based Architecture

* Brighter: [https://brightercommand.github.io/Brighter/](https://brightercommand.github.io/Brighter/)

##Threading

* Limiting concurrent threads: [http://markheath.net/post/constraining-concurrent-threads-csharp](http://markheath.net/post/constraining-concurrent-threads-csharp)

## Micro-services

* Polly (circuit breaker and the likes...): [https://github.com/App-vNext/Polly](https://github.com/App-vNext/Polly)
	* [https://github.com/App-vNext/Polly/wiki/Circuit-Breaker](https://github.com/App-vNext/Polly/wiki/Circuit-Breaker)
	* [https://github.com/App-vNext/Polly/wiki/Bulkhead](https://github.com/App-vNext/Polly/wiki/Bulkhead)