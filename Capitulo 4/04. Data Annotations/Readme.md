# &#x09;**Data Annotations**



#### **Description**



&#x09;

* Son atributos en C# que se colocan sobre propiedades o c lases.
* Permitir configurar como EF Core debe mapear una entidad a la base de datos.
* Evitan escribir configuraciones largas en Fluent API.



Pequeños atributos en el modelo que cambia cómo EF Core crea y usa las tablas. Son útiles para seguridad desde el servidor y se menos expuesta que en la aplicación.





###### Atributos Comunes

* \[Key]: define la clave  primaria. (autocremental y not Null)
* \[Required]: no admite NULL.
* \[MaxLength(100)]: limita la longitud de la columna. Si no se define pro default usa NVARCHAR.
* \[EmailAddress]: valida formato.
* \[Column(NombreColumna)]: personaliza el nombre en la BD.
* \[Precisión(x,y)]: precisión y escala de decimales.
* \[ForeingKey(NombreColumna)]: define la relación con otra entidad.







###### Detalles Generales

* Definir el nombre de una tabla

  * Encima del nombre de la clase(entidad, tabla, modelo): \[Table("NombreDeLaTabla")].
  * Nombre de la tabla y esquema: \[Table("NombreDeLaTabla", Schema = "nombre")]. -->Solo para BD que lo soporten.
* Columnas

  * &#x20;Nombrar y agregar tipo de dato a columna: \[Column("Nombre", TypeName = "varchar(50)")].
  * NOT NULL y tamaño máximo de caracteres: \[Required, MaxLength(50)].
* Primary Key

  * No necesaria al solo poner id al nombre de la columna.

    * En caso de querer definirla usar: \[Key].





###### Llaves primarias

&#x09;

* Por convención la llave primaria se define de manera automática si en el nombre de la propiedad usamos la palabra id.
* En caso contrario: si no lleva id, agregar \[key].
* Definir que sea la base de datos quien determine el autoincrementable: \[DatabaseGenerated()DatabaseGeneratedOption].
* Definir que sea la aplicación quien determine el autoincrementable: \[DatabaseGenerated()DatabaseGeneratedOption.None].
* No se pueden hacer llaves compuestas, se definen en Fluent Api.
* Es posible cambiar el nombre del constrain de la PK, después de la migración y antes del update. Revisar los cambios de la migración.





###### MAXLENGTH

* Si no se define un MAXLENGTH por default se define NVARCHAR(MAX).
* Se define: \[MaxLength(100)].
* Para usar un valor mínimo y uno máximo:

  * Usar STRINGLENGTH: \[StringLength(200, MinimumLength = 3)].





INDICE

* Definir una propiedad cómo índice: \[Index(nameof(NombreColumna), IsUnique = true)].
* Se define encima de la tabla, clase.





###### Propiedades no mapeadas

* Su característica es que esta propiedad o clase no es contemplada en la persistencia hacia la base de datos. Al hacer las migraciones no es tomada en cuenta.
* Sirve para procesos transitorios.
* Se define como: \[NotMapped].





###### Personalizar los nombres de las columnas para la interfaz gráfica: DISPLAY.

* No afecta a la persistencia.
* Se define: \[Display(Name = "Nombre a Mostrar")].





###### Rango de valores con Range

* Se usa para enviar un mensaje de error cuando se supera el limite definido.
* Se define: \[Range(1, 120, ErrorMessage = "El tiempo estimado debe estar entre 1 y 120")]. --> para enteros y decimales.
* Para usar rangos entre fechas: \[Range(typeof(DateTime), "1/1/2000", "12/31/2100")].





###### Uso del atributo Required

* Es más usado en string, ya que si se define un entero por defecto se indica que no es null, a menos que lo definas como : int? o lo declaras como null.
* Required con mensaje de error: \[Required(ErrorMessage = "El contenido es obligatorio")].
* Si no se especifica el error, igual mostrará un error en inglés.





###### Expresiones regulares

* No afecta a la base de datos, solo hace validaciones dentro de la aplicación.
* Funciona sobre strign.
* Se define: \[RegularExpression(@"^\[a-z0-9-]+$"), ErrorMessage = "Solo puede tener letras minusculas, numeros y guiones"].
* @ --> Marca el inicio.
* $ --> Marca el final.
* Dentro de los corchetes se indica el patron: a-z 0-9 - y +(acepta 1 o más).





###### Formato y validación de fechas

* Se define \[DataType(DataType.Date)] para display.
* Para BD se usa: \[Column(TypeName = "date")]. -->En SQL guarda solo la fecha sin hora.





###### Validar correo electrónico

* Valida formato correo electrónico.
* Se define: \[EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")].





###### Formato de Display Format, fechas, lenguajes y moneda.

* Se define para fechas: \[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
* Se define para separador de miles: \[DisplayFormat(DataFormatString = "{0:N0}")].





###### Configurar el Datatype: tipo de dato.

* No afecta a la persistencia, solo a la aplicación y a los formularios del HTML y display.
* Se define: \[Datatype(Datatype.Date)]. --> para fechas.
* Se define: \[Datatype(Datatype.time)]. --> para horas.
* Se define: \[Datatype(Datatype.url)]. --> para urls.
* Se define: \[Datatype(Datatype.Password)]. --> para contraseñas.
* Se define: \[Datatype(Datatype.Currency)]. --> para monedas.
* Se define: \[Datatype(Datatype.MultiLineText)]. --> para textareas(escribir en varias lineas).





###### Opciones generadas automáticamente en la base de datos (GUID, Identity, etc.).

* GUID: tipo de dato id con poca probabilidad de repetirse. Se genera automáticamente.

  * public Guid UsuarioId {get; set;}





###### Atributos Personalizados

* Clase heredada de validation attributes. Permite definir nuestra propia lógica de validación.
* Se agrega en una clase en una carpeta a parte.
* A la clase heredarle : ValidationAttribute
* Agregar el constructor.
* Construir el mensaje error: ErrorMessage. DENTRO DEL CONSTRUCTOR.
* Agregar como método protected override la validación.



using System.ComponentModel.DataAnnotations;



Namespace DataAnnotanionsEFCore.ValidacionesPersonalizadas

{

&#x09;public class NoEspacionsAttribute : ValidationAttribute

&#x09;{

&#x09;	public NoespaciosAttribute()

&#x09;	{

&#x09;		ErrorMessage = "El campo no debe contener espacios.";

&#x09;	}



&#x09;	protected override ValidationResult IsValid(objetc value, ValidationCOntext validationContext)

&#x09;	{

&#x09;		if (value is string texto \&\& texto.Contains(" "))

&#x09;		{

&#x09;			return new ValidationResult(ErrorMessage);

&#x09;		}

&#x09;		

&#x09;		return ValidationResult.Success;

&#x09;	}

&#x09;}



}



\[Unicode(false)] no acepta caracteres especiales.

