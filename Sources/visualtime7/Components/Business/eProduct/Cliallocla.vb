Option Strict Off
Option Explicit On
Public Class Cliallocla
	'%-------------------------------------------------------%'
	'% $Workfile:: Cliallocla.cls                           $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 8/09/03 18.30                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on April 09,2001
	'**-(brought to ASP on April 09,2001)
	'**-The key fields of the table corresponds to: nBranch, nProduct and nRole.
	'-Propiedades según la tabla en el sistema al 09/04/2001.
	'-(traida a ASP el 09/04/2001)
	'-Los campos llave de la tabla corresponden a: nBranch, nProduct y nRole.
	
	'   Column_name                    Type    Computed Length      Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	Public nBranch As Integer 'smallint   no      2           5     0     no          (n/a)                 (n/a)
	Public nProduct As Integer 'smallint   no      2           5     0     no          (n/a)                 (n/a)
	Public nRole As Integer 'smallint   no      2           5     0     no          (n/a)                 (n/a)
	Public sRequire As String 'char       no      1                       yes         no                    yes
	Public nUsercode As Integer 'smallint   no      2           5     0     yes         (n/a)                 (n/a)
	Public nMaxnum_rol As Integer 'smallint   no      2           5     0     yes         (n/a)                 (n/a)
	
	'**-Auxiliary variables
	'-Variables auxiliares
	
	Private mstrSelected As String
	Private mstrExist As String
	Private mdtmEffecdate As Date
	
	'- Variable para almacenar la descripción de la figuara asociada
    Public sDescript As String

    Public SDEFAULT_CLA_IND As String
	
	'**%Delete: This method is in charge of Deleting records in the table "Cliallocla".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Delete: Este método se encarga de eliminar registros en la tabla "Cliallocla". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		
		Dim lrecdelCliallocla As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecdelCliallocla = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.deCliallocla'
		'**+Information read on April 09,2001  08:35:47 a.m.
		'+Definición de parámetros para stored procedure 'insudb.delCliallocla'
		'+Información leída el 09/04/2001 08:35:47 a.m.
		
		With lrecdelCliallocla
			.StoredProcedure = "delCliallocla"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecdelCliallocla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCliallocla = Nothing
	End Function
	
	'**%Update: This method is in charge of updating records in the table "Cliallocla".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Cliallocla". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		
		Dim lrecinsCliallocla As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsCliallocla = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.insClialocla'
		'**+Information read on April 09,2001  09:06:11 a.m.
		'+Definición de parámetros para stored procedure 'insudb.insCliallocla'
		'+Información leída el 09/04/2001 09:06:11 a.m.
		
		With lrecinsCliallocla
			.StoredProcedure = "insCliallocla"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SDEFAULT_CLA_IND", SDEFAULT_CLA_IND, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			Update = .Run(False)
		End With
		
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsCliallocla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCliallocla = Nothing
		
	End Function
	
	'**%insValDP056: This method validates the page "DP056" as described in the functional specifications
	'%InsValDP056: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "DP056")
	Public Function insValDP056(ByVal sCodispl As String, Optional ByVal sSelected As String = "", Optional ByVal sRequire As String = "", Optional ByVal sExist As String = "", Optional ByVal nCounter As Integer = 0) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lblnSel As Boolean
		
		On Error GoTo insValDP056_Err
		
		lclsErrors = New eFunctions.Errors
		
		If sSelected = "1" Or (sSelected = "2" And sExist = "1") Then
			lblnSel = True
		End If
		
		If sRequire = "1" And sSelected = "2" Then
			Call lclsErrors.ErrorMessage(sCodispl, 1084, nCounter + 1)
		End If
		
		'**+Send the corresponding error message, in case there is no selected record
		'+Se envía el mensaje de error correspondiente, en caso de no existir ningún
		'+registro seleccionado.
		
		If Not lblnSel Then
			Call lclsErrors.ErrorMessage(sCodispl, 11181)
		End If
		
		insValDP056 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValDP056_Err: 
		If Err.Number Then
			insValDP056 = insValDP056 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insPostDP056: This method updates the database (as described in the functional specifications)
	'**%for the page "DP056"
	'%insPostDP056: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP056"
    Public Function insPostDP056(ByVal nAction As Integer,
                                 ByVal sCodispl As String,
                                 Optional ByVal sSelected As String = "",
                                 Optional ByVal nBranch As Integer = 0,
                                 Optional ByVal nProduct As Integer = 0,
                                 Optional ByVal nRole As Integer = 0,
                                 Optional ByVal sRequire As String = "",
                                 Optional ByVal nUsercode As Integer = 0,
                                 Optional ByVal sExist As String = "",
                                 Optional ByVal dEffecdate As Date = #12:00:00 AM#,
                                 Optional ByVal sDefaultClaInd As String = "") As Boolean

        insPostDP056 = True


        mstrSelected = sSelected
        Me.nBranch = nBranch
        Me.nProduct = nProduct
        Me.nRole = nRole
        Me.sRequire = sRequire
        Me.nUsercode = nUsercode
        mstrExist = sExist
        mdtmEffecdate = dEffecdate
        Me.SDEFAULT_CLA_IND = sDefaultClaInd
        If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
            If Me.insValDP056(sCodispl, sSelected, sRequire, sExist) = String.Empty Then
                If insUpdDP056() = True Then
                    insPostDP056 = True
                Else
                    insPostDP056 = False
                End If
            End If
        End If

    End Function
	
	'**%insUpdDP056: Run the Tdbgrid updating record by record in the table "Cliallocla"
	'%insUpdDP056: Recorre el Tdbgrid actualizando registro a registro en la tabla "Cliallocla"
	Private Function insUpdDP056() As Boolean
		
		Dim lclsCliallocla As eProduct.Cliallocla
		Dim lclsProd_win As eProduct.Prod_win
		

		lclsCliallocla = New eProduct.Cliallocla
		lclsProd_win = New eProduct.Prod_win
		
		insUpdDP056 = True
		
		With lclsCliallocla
			If (mstrSelected = "1" And mstrExist = "1") Or (mstrSelected = "1" And mstrExist = "2") Then
				.nBranch = Me.nBranch
				.nProduct = Me.nProduct
				.nRole = Me.nRole
				.sRequire = Me.sRequire
                .nUsercode = Me.nUsercode
                .SDEFAULT_CLA_IND = Me.SDEFAULT_CLA_IND
				
				Call .Update()
			Else
				If mstrSelected = "2" And mstrExist = "1" Then
					.nBranch = Me.nBranch
					.nProduct = Me.nProduct
					.nRole = Me.nRole

					Call .Delete()
				End If
			End If
		End With
		
		If insUpdDP056 = True Then
			Call lclsProd_win.Add_Prod_win(Me.nBranch, Me.nProduct, mdtmEffecdate, "DP056", "2", Me.nUsercode)
		Else
			Call lclsProd_win.Add_Prod_win(Me.nBranch, Me.nProduct, mdtmEffecdate, "DP056", "1", Me.nUsercode)
		End If
		
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		'UPGRADE_NOTE: Object lclsCliallocla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCliallocla = Nothing
		
    End Function
End Class






