Option Strict Off
Option Explicit On
Public Class Cl_m_cover
	'%-------------------------------------------------------%'
	'% $Workfile:: Cl_m_cover.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'**- All the main properties of the corresponding class from the table Cl_m_cover are defined.
	'-Se definen las propiedades principales de la clase correspondientes a la tabla Cl_m_cover.
	
	'+   Column_name               Type         Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	'+--------------------------   ------------ ----------------------------------- ----------- ----- ----- ----------------------------------- ----------------------------------- ----------------------------------- --------------------------------------------------------------------------------------------------------------------------------
	Public nClaim As Double 'int         no                                  4           10    0     no                                  (n/a)                               (n/a)                               NULL
	Public nCase_num As Integer 'smallint    no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nDeman_type As Integer 'smallint    no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nTransac As Integer 'smallint    no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nModulec As Integer
	Public nCover As Integer 'smallint    no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public sClient As String
	Public nCurrency As Integer 'smallint    no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nAmount As Double 'decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public sAut_adjust As String 'char        no                                  1                       yes                                 no                                  yes                                 SQL_Latin1_General_CP1_CI_AS
	Public nDed_amount As Double 'decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public nExchange As Double 'decimal     no                                  9           10    6     yes                                 (n/a)                               (n/a)                               NULL
	Public nExp_amount As Double 'decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public nGroup As Integer 'smallint    no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	Public nLoc_amount As Double 'decimal     no                                  9           14    2     yes                                 (n/a)                               (n/a)                               NULL
	Public nQuantity As Integer 'smallint    no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	Public nUsercode As Integer 'smallint    no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	Public nVa_tax As Double 'decimal     no                                  5           4     2     yes                                 (n/a)                               (n/a)                               NULL
	Public nVat_amount As Double 'decimal     no                                  9           10    2     yes                                 (n/a)                               (n/a)                               NULL
	Public sCurrency As String
	
	'**%Auxiliaries property
	'%  Propiedades auxiliares
	Public sStaclaim As String
	Public sGencov As String
	Public sConcept As String
	Public sDescript As String
	Public nServ_Order As Double
	
	'**%insValSIC004_K: It makes the validations of the fields of headed of page
	'**% SIC004 - the Consultation of the movi./cobertura removal.
	'% insValSIC004_K: Realiza las validaciones de los campos del encabezado de la página
	'% SIC004 - Consulta del desglose de movi./cobertura.
	Public Function insValSIC004_K(ByVal sCodispl As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nOper_type As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValSIC004_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'**+ Validation of the field "Claim"
		'+Validacion del campo "Siniestro"
		
		If nClaim = 0 Or nClaim = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 4006)
		Else
			If Not insExistClaim(nClaim) Then
				Call lclsErrors.ErrorMessage(sCodispl, 4005)
			Else
				If sStaclaim = "6" Then
					Call lclsErrors.ErrorMessage(sCodispl, 4305)
				End If
			End If
		End If
		
		'**+ Validation of the field "Case"
		'+Validacion del campo "Caso"
		
		If nCase_num = 0 Or nCase_num = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 4289)
		End If
		
		'**+ Validation of the field "Movement"
		'+Validacion del campo "Movimiento"
		
		If nOper_type = 0 Or nOper_type = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 4042)
		End If
		
		insValSIC004_K = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
insValSIC004_K_Err: 
		If Err.Number Then
			insValSIC004_K = insValSIC004_K & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'%**insExistClaim: It verifies the existence and the state of the Claim field
	'%  insExistClaim: Verifica la existencia y el estado del campo Siniestro
	Public Function insExistClaim(ByVal llngClaim As Double) As Boolean
		Dim lreaClaim_o As eRemoteDB.Execute
		
		lreaClaim_o = New eRemoteDB.Execute
		
		On Error GoTo insExistClaim_Err
		
		insExistClaim = False
		
		With lreaClaim_o
			.StoredProcedure = "reaClaim_o"
			.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				insExistClaim = True
				sStaclaim = .FieldToClass("sStaclaim")
			End If
			
			.RCloseRec()
		End With
		
		lreaClaim_o = Nothing
		
insExistClaim_Err: 
		If Err.Number Then
			insExistClaim = False
		End If
		
		On Error GoTo 0
	End Function
End Class






