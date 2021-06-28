Option Strict Off
Option Explicit On
Module Constantes
	
	'**-Objective: Constant that indicates that an integer type value is assigned a null for passing as a parameter
	'-Objetivo: Constante que indica que a un valor de tipo entero se le asigna null para pasarlo como parametro
	Public Const intNull As Short = -32768
	
	'**-Objective: Constant that indicates that a date type value is assigned a null for passing it as a parameter
	'-Objetivo: Objective: Constante que indica que a un valor de tipo fecha se le asigna null para pasarlo como parametro
	Public Const dtmNull As Date = Nothing
	
	'**-Objective: Constant that indicates that a decimal type value is assigned a null for passing it as a parameter
	'-Objetivo: Objective: Constante que indica que a un valor de tipo decimal se le asigna null para pasarlo como parametro
	Public Const dblNull As Double = -32768.3276
	
	'**-Objective: Constant that indicates that a string type value is assigned a null for passing it as a parameter
	'-Objetivo: Constante que indica que a un valor de tipo cadena se le asigna null para pasarlo como parametro
	Public Const strNull As String = ""
	
	'**-Objective:
	'-Objetivo:
	Public Enum eTypeClient
		Company = 2
		Person = 1
	End Enum
End Module










