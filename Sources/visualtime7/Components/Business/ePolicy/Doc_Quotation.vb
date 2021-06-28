Option Strict Off
Option Explicit On
Public Class Doc_Quotation
	'%-------------------------------------------------------%'
	'% $Workfile:: Doc_Quotation.cls                        $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 29/01/04 18.01                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ The key fields are nBranch, nProduct, nQuotation
	'+ Los campos llaves corresponden a nBranch, nProduct, nQuotation
	
	'+ Column_name              Type                   Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------------ ----------------------- --------- ------ ----- ----- -------- ------------------ --------------------
	Public nBranch As Integer 'int       no        2                 no       yes                no
	Public nProduct As Integer 'int       no        2                 no       yes                no
	Public nQuotation As Integer 'int       no        4                 no       yes                no
	Public sDocument As String 'char      no       15                 no       yes                no
	Public dCompdate As Date 'date      no        8                 no       yes                no
	Public nUsercode As Integer 'int       no        2                 no       yes                no
	
	'**% insReaDocQuotation: This routine validates if the original policy exists in the table of policies (policy)
	'% insReaDocQuotation: Esta rutina se encarga de validar la existencia de la póliza original en la tabla policy
    Public Function insReaDocQuotation(ByVal sDocument As String) As Integer
        On Error GoTo insReaDocQuotation_Err

        Dim lrecreaDoc_QuotationCount As eRemoteDB.Execute

        lrecreaDoc_QuotationCount = New eRemoteDB.Execute

        '**+Stored procedure parameters definition 'insudb.reaDoc_QuotationCount'
        '**+Data of 01/11/2001 11:16:09 a.m.
        'Definición de parámetros para stored procedure 'insudb.reaDoc_QuotationCount'
        'Información leída el 11/01/2001 11:16:09 a.m.
        With lrecreaDoc_QuotationCount
            .StoredProcedure = "reaDoc_QuotationCount"
            .Parameters.Add("sDocument", sDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                insReaDocQuotation = .FieldToClass("nCount")
                .RCloseRec()
            Else
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If IsDBNull(insReaDocQuotation) Then
                    insReaDocQuotation = 0
                End If
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaDoc_QuotationCount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaDoc_QuotationCount = Nothing

insReaDocQuotation_Err:
        If Err.Number Then
            insReaDocQuotation = False
        End If
        On Error GoTo 0
    End Function
	
	'**% insReaDocQuotation2: This routine validates if the original policy exists in the table of policies (policy)
	'% insReaDocQuotation2: Esta rutina se encarga de validar la existencia de la póliza original en la tabla Policy
	Public Function insReaDocQuotation2(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nQuotation As Integer) As Boolean
		On Error GoTo insReaDocQuotation2_Err
		
		Dim lrecreaDoc_Quotation As eRemoteDB.Execute
		
		lrecreaDoc_Quotation = New eRemoteDB.Execute
		
		'**+Stored procedure parameters definition 'insudb.reaDoc_Quotation'
		'**+Data of 01/11/2001 11:32:59 a.m.
		'Definición de parámetros para stored procedure 'insudb.reaDoc_Quotation'
		'Información leída el 11/01/2001 11:32:59 a.m.
		insReaDocQuotation2 = False
		
		With lrecreaDoc_Quotation
			.StoredProcedure = "reaDoc_Quotation"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuotation", nQuotation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.sDocument = .FieldToClass("sDocument")
				insReaDocQuotation2 = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaDoc_Quotation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDoc_Quotation = Nothing
		
insReaDocQuotation2_Err: 
		If Err.Number Then
			insReaDocQuotation2 = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Add: Adds a record in the table Doc_Quotation
	'% Add: Inserta en la tabla Doc_Quotation
	Public Function Add() As Boolean
		Dim lreccreDoc_Quotation As eRemoteDB.Execute
		
		lreccreDoc_Quotation = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		'**+Stored procedure parameters definition 'insudb.creDoc_Quotation'
		'**+Data of 11/02/2000 11:34:33 a.m.
		'+ Definición de parámetros para stored procedure 'insudb.creDoc_Quotation'
		'+ Información leída el 02/11/2000 11:34:33 a.m.
		With lreccreDoc_Quotation
			.StoredProcedure = "creDoc_Quotation"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuotation", nQuotation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocument", sDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreDoc_Quotation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreDoc_Quotation = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Update: Updates the table Doc_Quotation
	'% Update: Actualiza en la tabla Doc_Quotation
	Public Function Update(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nQuotation As Integer, ByVal sDocument As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecupdDoc_Quotation As eRemoteDB.Execute
		
		lrecupdDoc_Quotation = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+Stored procedure parameters definition 'insudb.updDoc_Quotation'
		'**+Data of 11/02/2000 11:51:42 a.m.
		'Definición de parámetros para stored procedure 'insudb.updDoc_Quotation'
		'Información leída el 02/11/2000 11:51:42 a.m.
		With lrecupdDoc_Quotation
			.StoredProcedure = "updDoc_Quotation"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuotation", nQuotation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocument", sDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdDoc_Quotation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdDoc_Quotation = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**% FindCount: This routine searches the quantity of documents with the same number in the table Doc_quotation
	'% FindCount: Busca la cantidad de veces que se encuentra el documento en la tabla Doc_quotation
	Public Function FindCount(ByVal sDocument As Integer) As Boolean
		Dim lrecreaDoc_QuotationCount As eRemoteDB.Execute
		
		lrecreaDoc_QuotationCount = New eRemoteDB.Execute
		
		On Error GoTo FindCount_Err
		
		'**+Stored procedure parameters definition 'insudb.reaDoc_QuotationCount'
		'**+Data of 11/02/2000 11:51:45 a.m.
		'Definición de parámetros para stored procedure 'insudb.reaDoc_QuotationCount'
		'Información leída el 02/11/2000 01:51:45 p.m.
		With lrecreaDoc_QuotationCount
			.StoredProcedure = "reaDoc_QuotationCount"
			.Parameters.Add("sDocument", sDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			FindCount = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecreaDoc_QuotationCount may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDoc_QuotationCount = Nothing
		
FindCount_Err: 
		If Err.Number Then
			FindCount = False
		End If
		On Error GoTo 0
	End Function
End Class






