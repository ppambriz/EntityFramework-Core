Creacion de relaciones.

Relacion 1-N
En articulos se define la propiedad fk y se le agrega la propiedad de navegacion abajo.
En Categoria se agrega una propiedad tipo lista de la clase articulos.
Tambien se puede hacer de manera explicita con una Data Annotations: [ForeingKey(nameof(Categoria))]



Relaciones 1 a 1
Se realiza en ambas clases lo siguiente:
	-En la clase PerfilUsuario se agrega UsuarioId pero se le pone una DataAnnotatios [key] que
	indica sera la pk y la fk
 	-En ambas clases se agrega la propiedad de navegacion inversa, es decir una propiedad
	del tipo de la clase inversa.

Tambien se puede hacer de manera explicita: [key ] + [ForeingKey(nameof(Usuario))]


Relaciones N-N


