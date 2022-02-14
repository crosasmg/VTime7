Option Strict Off
Option Explicit On
Public Class ValClient
	'%-------------------------------------------------------%'
	'% $Workfile:: valClient.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'- Se codifican las constantes
	Public Enum eTypeValClientErr
		FieldEmpty
		TypeNotFound
		StructInvalid
		IsNotNumeric
		FieldNotFound
		FieldNew
	End Enum
	
	'- Variable del tipo de enumeracion
	Private mvarStatus As eTypeValClientErr
	
	'- Variable codigo del cliente
	Private mstrClientCode As String
	
	'- Variable nombre del cliente
	Private mstrClientName As String
	
	'- Variable tipo del cliente
	Private mvarClientType As Client.eClientType
	
	'- Variable temporal
	Private mblnTemporal As Boolean
	
	'- Variable de digito verificador
	Public sDigit As String
	
	'% Temporal: manejo de retorno de varible temporal
	'-----------------------------------------------------------
	Public ReadOnly Property Temporal() As Boolean
		Get
			'-----------------------------------------------------------
			Temporal = mblnTemporal
		End Get
	End Property
	
	'% ClientType: manejo de retorno de varible tipo de cliente
	'-----------------------------------------------------------
	Public ReadOnly Property ClientType() As Integer
		Get
			'-----------------------------------------------------------
			ClientType = mvarClientType
		End Get
	End Property
	
	'% ClientCode: manejo de retorno de varible codigo del cliente
	'-----------------------------------------------------------
	Public ReadOnly Property ClientCode() As String
		Get
			'-----------------------------------------------------------
			ClientCode = mstrClientCode
		End Get
	End Property
	
	'% sCliename: manejo de retorno de varible nombre del cliente
	'-----------------------------------------------------------
	Public ReadOnly Property sCliename() As String
		Get
			'-----------------------------------------------------------
			sCliename = mstrClientName
		End Get
	End Property
	
	'% Status: manejo de retorno de varible tipo de enumeracion
	'-----------------------------------------------------------
	Public ReadOnly Property Status() As eTypeValClientErr
		Get
			'-----------------------------------------------------------
			Status = mvarStatus
		End Get
	End Property
	
	'% Validate: se verifican los datos del cliente
	Public Function Validate(ByVal sCodClient As String, ByVal nAction As eFunctions.Menues.TypeActions, Optional ByVal bFind As Boolean = True, Optional ByVal bAllowInvalidFormat As Boolean = False) As Boolean
		Dim lclsClient As eClient.Client
		
		'-Se define la variable encargada de indicar si hubo algun error para algun punto de la
		'-validación
		Dim lblnErr As Boolean
		
		'- Se define la variable temporal para justificar el código del cliente
		Dim lstrVarAux As String
		
		On Error GoTo Validate_err
		
		mvarStatus = 0
		mvarClientType = -1
		mstrClientCode = String.Empty
		mstrClientName = String.Empty
		lstrVarAux = Trim(sCodClient)
		
		Validate = True
		
		If Len(lstrVarAux) Then
			lclsClient = New eClient.Client

            If bAllowInvalidFormat Then
				lstrVarAux = lclsClient.ExpandCode(lstrVarAux)
				mstrClientCode = lstrVarAux
				If bFind Then
					If lclsClient.Find(lstrVarAux) Then
						mstrClientName = lclsClient.sCliename
						Select Case lclsClient.nPerson_typ
							Case 1
								mvarClientType = Client.eClientType.ctPerson
							Case 2
								mvarClientType = Client.eClientType.ctCompany
							Case Else
								mvarStatus = eTypeValClientErr.TypeNotFound
								lblnErr = True
						End Select
						sDigit = lclsClient.sDigit
					Else
						mvarStatus = eTypeValClientErr.FieldNotFound
						lblnErr = True
					End If
				End If
            Else 
			    If IsNumeric(lstrVarAux) Then
				    If CDbl(lstrVarAux) > 0 Then
					    lstrVarAux = lclsClient.ExpandCode(lstrVarAux)
					    mstrClientCode = lstrVarAux
					    If bFind Then
						    If lclsClient.Find(lstrVarAux) Then
							    mstrClientName = lclsClient.sCliename
							    Select Case lclsClient.nPerson_typ
								    Case 1
									    mvarClientType = Client.eClientType.ctPerson
								    Case 2
									    mvarClientType = Client.eClientType.ctCompany
								    Case Else
									    mvarStatus = eTypeValClientErr.TypeNotFound
									    lblnErr = True
							    End Select
							    sDigit = lclsClient.sDigit
						    Else
							    mvarStatus = eTypeValClientErr.FieldNotFound
							    lblnErr = True
						    End If
					    End If
				    Else
					    mvarStatus = eTypeValClientErr.StructInvalid
					    lblnErr = True
				    End If
			    Else
				    '+Si la acción es registrar.
				    If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					    '+Si el código corresponde con la letra "E"; indica que se desea generar un nuevo cliente automáticamente.
					    If UCase(Trim(lstrVarAux)) = "E" Then
						    mstrClientCode = lclsClient.GetNewClientCode
						    mvarStatus = eTypeValClientErr.FieldNew
					    Else
						    lblnErr = True
						    mvarStatus = eTypeValClientErr.IsNotNumeric
					    End If
				    Else
					    lblnErr = True
					    mvarStatus = eTypeValClientErr.IsNotNumeric
				    End If
			    End If
            End If 
		Else
			mvarStatus = eTypeValClientErr.FieldEmpty
			lblnErr = True
		End If
		
		Validate = Not lblnErr
		
Validate_err: 
		If Err.Number Then
			Validate = False
		End If
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
		On Error GoTo 0
	End Function
End Class






