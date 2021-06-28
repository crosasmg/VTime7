Option Strict Off
Option Explicit On
Public Class Tab_ships
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table tab_ships in the system 26/01/2005
	'+Objetivo: Propiedades según la tabla Tab_ships en el sistema 26/01/2005
	
	'+ Column_Name                       Type         Length
	'------------------------------    -------------  -------
	Public sName_licen As String 'Char           20
	Public dEffecdate As Date 'Date
	Public sDescript As String 'Char           30
	Public sShipCompClass As String 'Char           30
	Public nManyears As Short 'Number         5
	Public dNullDate As Date 'Date
	Public nUsercode As Integer 'Number         5
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl       - Logical code of the transaction
	'**%    sAction        - Action being executed on the transaction
	'**%    sName          - License plate of the ship
	'**%    sDescript      - Ship name
	'**%    sShipCompClass - Company dedicated to the merchantmen ships classification
	'**%    nManyears      - Years of manufacture
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl       - Código lógico de la transacción
	'%    sAction        - Acción que se ejecuta en la transacción
	'%    sName          - Número de registro o matrícula de la transacción
	'%    sDescript      - Número de Vapor o nombre de la embarcación
	'%    sShipCompClass - Entidad dedicada a la clasificación de buques mercantes
	'%    nManyears      - Años de fabricación
	Public Function InsValTRC6000(ByVal sCodispl As String, ByVal sAction As String, ByVal sName As String, ByVal sDescript As String, ByVal sShipCompClass As String, ByVal nManyears As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
        'If Not IsIDEMode Then
        'End If
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			If (sName = String.Empty And sDescript = String.Empty And sShipCompClass = String.Empty And nManyears = eRemoteDB.Constants.intNull) Then
				.ErrorMessage(sCodispl, 1068)
			End If
			
			If Trim(sName) = "%" Or Trim(sDescript) = "%" Or Trim(sShipCompClass) = "%" Then
				.ErrorMessage(sCodispl, 90137)
			End If
			
			
			InsValTRC6000 = lclsErrors.Confirm
			
		End With
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
End Class











