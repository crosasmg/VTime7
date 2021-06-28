Option Strict Off
Option Explicit On
Public Class Cl_cov_bil
	'%-------------------------------------------------------%'
	'% $Workfile:: Cl_cov_bil.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	Public nModulec As Integer
	Public nCover As Integer
	Public nBranch As Integer
	Public nPay_concep As Integer
	Public nProduct As Integer
	Public dEffecdate As Date
	Public dCompdate As Date
	Public dNulldate As Date
	Public nUsercode As Integer
	Public sStatregt As String
	
	Public sDesPay_concep As String
	
	'**%Function Find: This function is in charge to obtain the concept data of a coverage payment
	'%Funcion Find. Esta funcion se encarge de obtener los datos de los conceptos de pago de una cobertura
	Public Function Find(ByVal lintModulec As Integer, ByVal lintCover As Integer, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintPay_concep As Integer, ByVal ldtmEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecinsReaCl_cov_bil As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		Find = True
		
		If lintModulec <> nModulec Or lintCover <> nCover Or lintBranch <> nBranch Or lintProduct <> nProduct Or lintPay_concep <> nPay_concep Or ldtmEffecdate <> dEffecdate Or lblnFind Then
			
			lrecinsReaCl_cov_bil = New eRemoteDB.Execute
			
			nModulec = lintModulec
			nCover = lintCover
			nBranch = lintBranch
			nProduct = lintProduct
			nPay_concep = lintPay_concep
			dEffecdate = ldtmEffecdate
			
			'**Parameters definition for the stored procedure 'insudb.insReaCl_cov_bil'
			'Definición de parámetros para stored procedure 'insudb.insReaCl_cov_bil'
			'**Data read on 01/29/2001 2:58:38 PM
			'Información leída el 29/01/2001 2:58:38 PM
			With lrecinsReaCl_cov_bil
				.StoredProcedure = "insReaCl_cov_bil"
				.Parameters.Add("nModulec", lintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", lintCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPay_concep", lintPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nPay_concep = .FieldToClass("nPay_concep")
					sDesPay_concep = .FieldToClass("sDescript")
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			lrecinsReaCl_cov_bil = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Find_Pay_concep(ByVal lintModulec As Integer, ByVal lintCover As Integer, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal ldtmEffecdate As Object) As Boolean
		
		Dim lrecinsReaCL_cov_bil_SI008 As eRemoteDB.Execute
		
		On Error GoTo Find_Pay_concep_Err
		Find_Pay_concep = False
		lrecinsReaCL_cov_bil_SI008 = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insudb.insReaCL_cov_bil_SI008'
		'Definición de parámetros para stored procedure 'insudb.insReaCL_cov_bil_SI008'
		'**Data read on 01/29/2001 2:55:27 PM
		'Información leída el 29/01/2001 2:55:27 PM
		With lrecinsReaCL_cov_bil_SI008
			.StoredProcedure = "insReaCL_cov_bil_SI008"
			.Parameters.Add("nModulec", lintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", lintCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("sFind") = "Found" Then
					Find_Pay_concep = True
					nPay_concep = .FieldToClass("nPay_concep")
					sDesPay_concep = .FieldToClass("sDescript")
				End If
				.RCloseRec()
			End If
		End With
		lrecinsReaCL_cov_bil_SI008 = Nothing
		
Find_Pay_concep_Err: 
		If Err.Number Then
			Find_Pay_concep = False
		End If
		On Error GoTo 0
	End Function
	
	'**%valExistDP049: Verifies the existence of the required field for the window DP049
	'% valExistDP049: verifica la existencia de campos requeridos para la ventana DP049
	Public Function valExistDP049(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nPay_concep As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreacl_cov_bil2 As eRemoteDB.Execute
		
		On Error GoTo valExistDP049_err
		
		lrecreacl_cov_bil2 = New eRemoteDB.Execute
		
		valExistDP049 = False
		
		'**+Parameters definition for the stored procedure 'insudb.reacl_cov_bil2'
		'+ Definición de parámetros para stored procedure 'insudb.reacl_cov_bil2'
		'**+ Data read on 04/18/2001 04:55:17 p.m.
		'+ Información leída el 18/04/2001 04:55:17 p.m.
		
		With lrecreacl_cov_bil2
			.StoredProcedure = "reacl_cov_bil2"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_concep", nPay_concep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					If .FieldToClass("selection") > 0 Then
						valExistDP049 = True
						Exit Do
					End If
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		lrecreacl_cov_bil2 = Nothing
		
valExistDP049_err: 
		If Err.Number Then
			valExistDP049 = False
		End If
		On Error GoTo 0
	End Function
End Class






