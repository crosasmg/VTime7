Option Strict Off
Option Explicit On
Public Class LettParam
	'**+Objetive: Clase generada a partir de la tabla 'LETTPARAM' que es Parámetros requeridos por el modelo de carta
	'**+Version: $$Revision: 9 $
	'+Objetivo: Clase generada a partir de la tabla 'LETTPARAM' Parameters required in a letter template
	'+Version: $$Revision: 9 $
	
	'**-Objective:
	'-Objetivo:
	Private mvarLettParams As LettParams
	
	'**-Objective: Number identifying the letter template.
	'-Objetivo: Código del modelo de carta.
	Public nLetterNum As Short
	
	'**-Objective: Parameter Code.The possible values as per table 622.
	'-Objetivo: Código del parámetro.Valores posibles según tabla 622.
	Public nParameters As Short
	
	'**-Objective: Date which from the record is valid.
	'-Objetivo: Fecha de efecto del registro.
	Public dEffecDate As Date
	
	'**-Objective: Date when the record is cancelled.
	'-Objetivo: Fecha de anulación del registro.
	Public dNullDate As Date
	
	'**-Objective: Computer date when the record is updated or created.
	'-Objetivo: Fecha del computador en que se crea o actualiza el registro.
	Public dCompdate As Date
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Short
	
	'**-Objective:
	'-Objetivo:
	Public sDesLettParam As String
	
	'**%Objective:
	'**%Parameters:
	'**%  nLetterNum
	'%Objetivo:
	'%Parámetros:
	'%  nLetterNum
	Public Function Find(ByVal nLetterNum As Short) As Boolean
		Dim lrecreaLettParam As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		
		lrecreaLettParam = New eRemoteDB.Execute
		
		Find = False
		With lrecreaLettParam
			.StoredProcedure = "reaLettParam"
			.Parameters.Add("nLetterNum", nLetterNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				nLetterNum = .FieldToClass("nLetterNum")
				nParameters = .FieldToClass("nParameters")
				dEffecDate = .FieldToClass("dEffecDate")
				dNullDate = .FieldToClass("dNullDate")
				dCompdate = .FieldToClass("dNullDate")
				nUsercode = .FieldToClass("nUsercode")
				sDesLettParam = String.Empty
				.RCloseRec()
			End If
		End With
		lrecreaLettParam = Nothing
		
		Exit Function
		lrecreaLettParam = Nothing
	End Function
	
	'**%Objective:
	'%Objetivo:
	
	'**%Objective:
	'%Objetivo:
	Public Property LettParams() As LettParams
		Get
			If Not IsIDEMode Then
			End If
			
			If mvarLettParams Is Nothing Then
				mvarLettParams = New LettParams
			End If
			LettParams = mvarLettParams
			
            Exit Property
		End Get
		Set(ByVal Value As LettParams)
			If Not IsIDEMode Then
			End If
			
			mvarLettParams = Value
			
			Exit Property
		End Set
	End Property
	
	'**%Objective:
	'%Objetivo:
	Private Sub Class_Terminate_Renamed()
		If Not IsIDEMode Then
		End If
		
		mvarLettParams = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











