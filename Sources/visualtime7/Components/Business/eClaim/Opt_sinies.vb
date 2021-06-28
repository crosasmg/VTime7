Option Strict Off
Option Explicit On
Public Class Opt_sinies
	'%-------------------------------------------------------%'
	'% $Workfile:: Opt_sinies.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Public nCurrency As Integer
	Public sIndReserv As String
	Public nUsercode As Integer
	
	'**%Find. this function is in charge to obtain the option data installation
	'%Find. Esta funcion se encarga de obtener los datos de las opciones de instalación
	Public Function Find() As Boolean
		Dim lrecreaOpt_sinies As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecreaOpt_sinies = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insudb.reaOpt_sinies'
		'**Data read on 02/02/2001 4:07:36 PM
		'Definición de parámetros para stored procedure 'insudb.reaOpt_sinies'
		'Información leída el 02/02/2001 4:07:36 PM
		With lrecreaOpt_sinies
			.StoredProcedure = "reaOpt_sinies"
			If .Run Then
				Find = True
				nCurrency = .FieldToClass("nCurrency")
				sIndReserv = .FieldToClass("sIndReserv")
				.RCloseRec()
			End If
		End With
		lrecreaOpt_sinies = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






