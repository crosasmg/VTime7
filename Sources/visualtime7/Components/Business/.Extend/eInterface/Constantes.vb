Option Strict Off
Option Explicit On
Module Constantes
	
	'**- Constant that indicates that an Long type value is assigned a null for passing as a parameter
	'- Constante que indica que a un valor de tipo entero se le asigna null para pasarlo como parametro
	
	Public Const intNull As Integer = -32768
	
	'**- Constant that indicates that a date type value is assigned a null for passing it as a parameter
	'- Constante que indica que a un valor de tipo fecha se le asigna null para pasarlo como parametro
	
	Public Const dtmNull As Date = Nothing
	
	'**- Constant that indicates that a decimal type value is assigned a null for passing it as a parameter
	'- Constante que indica que a un valor de tipo decimal se le asigna null para pasarlo como parametro
	
	Public Const dblNull As Double = -32768.3276
	
	'**- Constant that indicates that a string type value is assigned a null for passing it as a parameter
	'- Constante que indica que a un valor de tipo cadena se le asigna null para pasarlo como parametro
	
	Public Const strNull As String = ""
	
	Public Enum eTypeClient
		Company
		Person
	End Enum
	
	Public Const numNull As Integer = -32768
End Module






