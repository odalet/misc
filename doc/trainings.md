# Trainings

## OO

* Naming, naming, naming --> No ever abbreviations
* Encapsulation! **In doubt, always favor the least-visible accessibility**
* Prefer composition over inheritance (coz inheritance is usually too much strong a coupling of entities)
* Type values: `double distanceInMiles`cannot be added to `double speedInKmh`...
	* See F#'s **UnitOfMeasures** 
* Complexity cannot really be reduced, at best it can be better organized: 
	* a few huge classes --> many cooperating simple classes. The complexity is deported from the inners of classes to their relationships... 
* Functional composition? [http://blog.ploeh.dk/2014/03/10/solid-the-next-step-is-functional/](http://blog.ploeh.dk/2014/03/10/solid-the-next-step-is-functional/)
	* Objects are **data (aka state) with behavior**
	* data immutability
	* SRP + ISP used to their conclusion gives one-method interfaces and very very simple classes --> this seems a bit verbose for its intent
	* (Pure) Function: that's a behavior without data (everything needed for the computation is passed in the function arguments
	* Closure = Function + environment --> this is **behavior with data**...
	* Closure / Object is an example of **Duality**
	* The author's conclusion is to use F# when attaining the limits of SRP/ISP...
	* I'd rather stick to C# but use loose forms of class extensibility such a Extension methods (traits, mixins...): they provide a container for functional-style code and are bound to public (well... internal as well) APIs only.  
		* [http://www.fascinatedwithsoftware.com/blog/post/2012/06/26/Extension-Methods-and-Helper-Classes-as-Design-Elements.aspx](http://www.fascinatedwithsoftware.com/blog/post/2012/06/26/Extension-Methods-and-Helper-Classes-as-Design-Elements.aspx)
		* [https://www.reddit.com/r/csharp/comments/34svg2/c_extension_methods_are_empowering_openclose/](https://www.reddit.com/r/csharp/comments/34svg2/c_extension_methods_are_empowering_openclose/)
		* [https://stackoverflow.com/questions/34338760/the-use-of-c-sharp-extension-methods-to-show-intention](https://stackoverflow.com/questions/34338760/the-use-of-c-sharp-extension-methods-to-show-intention)
		* [https://www.linkedin.com/pulse/functional-programming-c-john-peters/](https://www.linkedin.com/pulse/functional-programming-c-john-peters/)
		* Open/Close: [https://codeblog.jonskeet.uk/2013/03/15/the-open-closed-principle-in-review/](https://codeblog.jonskeet.uk/2013/03/15/the-open-closed-principle-in-review/)
		* Extension methods seem to violate encapsulation... Is this really a problem?

### SOLID

* [https://www.codeproject.com/Tips/1033646/SOLID-Principle-with-Csharp-Example](https://www.codeproject.com/Tips/1033646/SOLID-Principle-with-Csharp-Example)
* William Durand:
	* [https://williamdurand.fr/2013/07/30/from-stupid-to-solid-code/](https://williamdurand.fr/2013/07/30/from-stupid-to-solid-code/)
	* [https://williamdurand.fr/2013/06/03/object-calisthenics/](https://williamdurand.fr/2013/06/03/object-calisthenics/)
* Open/Close: use encapsulation!

### KISS & DRY

* OK, but beware, KISS and DRY can contradict themselves: 
	* KISS can lead to code duplication
	* DRY can lead to over-complex class hierarchies for the sake of reuse...

## `C#` 

## VS2017