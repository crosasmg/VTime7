Option Strict Off
Option Explicit On
Module Constantes
	'%-------------------------------------------------------%'
	'% $Workfile:: Constantes.bas                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'**% A constant that indicates that null is assigned to an long type value in order to pass it as a parameter
	'% Constante que indica que a un valor de tipo entero se le asigna null para pasarlo como parametro
	
	Public Const intNull As Integer = -32768
	
	'**% A constant that indicates that null is assigned to a date type value in order to pass it as a parameter
	'% Constante que indica que a un valor de tipo fecha se le asigna null para pasarlo como parametro
	
	Public Const dtmNull As Date = Nothing
	
	'**% A constant that indicates that null is assigned to a decimal type value in order to pass it as a parameter
	'% Constante que indica que a un valor de tipo decimal se le asigna null para pasarlo como parametro
	
	Public Const dblNull As Double = -32768.3276
	
	'**% A constant that indicates that null is assigned to a chain type value in order to pass it as a parameter
	'% Constante que indica que a un valor de tipo cadena se le asigna null para pasarlo como parametro
	
	Public Const strNull As String = ""
	
	
	Enum TypeDefaulti
		cintYes = 1
		cintNot = 0
	End Enum
End Module






