Option Strict Off
Option Explicit On
Public Class Out_moveme
	'%-------------------------------------------------------%'
	'% $Workfile:: Out_moveme.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according the table in the system on 11/17/2000.
	'**-The key fields of the table are: sCertype, nBranch, nProduct, nPolicy, nCertif, nMovnumbe y nDigit.
	'-Propiedades según la tabla en el sistema al 17/11/2000.
	'-Los campos llave de la tabla corresponden a: sCertype, nBranch, nProduct, nPolicy, nCertif, nMovnumbe y nDigit.
	'   Column_name                    Type     Computed  Length  Prec  Scale Nullable     TrimTrailingBlanks     FixedLenNullInSource
	Public sCertype As String 'char       no        1                   no              no                       no
	Public nBranch As Integer 'smallint   no        2       5     0     no              (n/a)                    (n/a)
	Public nProduct As Integer 'smallint   no        2       5     0     no              (n/a)                    (n/a)
	Public nPolicy As Double 'int        no        4      10     0     no              (n/a)                    (n/a)
	Public nCertif As Double 'int        no        4      10     0     no              (n/a)                    (n/a)
	Public nMovnumbe As Integer 'int        no        4      10     0     no              (n/a)                    (n/a)
	Public nDigit As Integer 'smallint   no        2       5     0     no              (n/a)                    (n/a)
	Public nCapital As Double 'decimal    no        9      12     0     yes             (n/a)                    (n/a)
	Public nCurrency As Integer 'smallint   no        2       5     0     yes             (n/a)                    (n/a)
	Public nExchange As Double 'decimal    no        9      10     6     yes             (n/a)                    (n/a)
	Public dExpirDat As Date 'datetime   no        8                   yes             (n/a)                    (n/a)
	Public nPremium As Double 'decimal    no        9      10     2     yes             (n/a)                    (n/a)
	Public sStatus_mov As String 'char       no        1                   yes             no                       yes
	Public nTaxamou As Double 'decimal    no        9      10     2     yes             (n/a)                    (n/a)
	Public nTratypei As Integer 'smallint   no        2       5     0     yes             (n/a)                    (n/a)
	Public nUsercode As Integer 'smallint   no        2       5     0     yes             (n/a)                    (n/a)
	Public nYear_month As Integer 'int        no        4      10     0     yes             (n/a)                    (n/a)
	Public nProvince As Integer 'smallint   no        2       5     0     yes             (n/a)                    (n/a)
	Public dStartDate As Date 'datetime   no        8                   yes             (n/a)                    (n/a)
	
	'**- Auxiliary variables
	'- Variables auxialiares
	Public nCommi_rate As Double
	Public sType_detai As String
	Public nCommision As Double
	Public nPremAnual As Double
	Public sDescript As String
	Public nBill_item As Integer

	Public dMaxEffecdate As Date

	'**%Update_Status: Updates the transaction status.
	'%Update_Status: Realiza las actualizaciones al estado del movimiento.
	Public Function Update_Status(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal sStatus_mov As String) As Boolean

		Dim lrecupdOut_moveme_sStatus_mov As eRemoteDB.Execute

		On Error GoTo Update_Status_Err

		lrecupdOut_moveme_sStatus_mov = New eRemoteDB.Execute

		'**+Stored procedure parameters definition 'insudb.updOut_moveme_sStatus_mov'
		'**+Data on 12/11/2000 16:07:41
		'+Definición de parámetros para stored procedure 'insudb.updOut_moveme_sStatus_mov'
		'+Información leída el 11/12/2000 16:07:41
		With lrecupdOut_moveme_sStatus_mov
			.StoredProcedure = "updOut_moveme_sStatus_mov"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus_mov", sStatus_mov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_Status = .Run(False)
		End With

Update_Status_Err:
		If Err.Number Then
			Update_Status = False
		End If
		'UPGRADE_NOTE: Object lrecupdOut_moveme_sStatus_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdOut_moveme_sStatus_mov = Nothing
		On Error GoTo 0
	End Function

	'**%reaMaxDateOutMoveme: it find the maximum date in the group of records to be proccessed
	'%reaMaxDateOutMoveme: busca la máxima fecha para el grupo de registros a procesar
	'--------------------------------------------------------------------------------
	Public Function reaMaxDateOutMoveme(ByVal sCertype As String,
										ByVal nBranch As Long,
										ByVal nProduct As Long,
										ByVal nPolicy As Double,
										Optional ByVal nCertif As Double = 0,
										Optional ByVal nIndCA028 As Integer = 0) As Boolean
		'--------------------------------------------------------------------------------
		Dim lrecreaOut_moveme As eRemoteDB.Execute

		On Error GoTo reaMaxDateOutMoveme_err

		lrecreaOut_moveme = New eRemoteDB.Execute

		With lrecreaOut_moveme
			.StoredProcedure = "reaMaxDateOutMoveme"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndCA028", nIndCA028, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				dMaxEffecdate = .Parameters.Item("dEffecdate").Value
				reaMaxDateOutMoveme = True
			Else
				dMaxEffecdate = Today
				reaMaxDateOutMoveme = False
			End If
		End With

reaMaxDateOutMoveme_err:
		lrecreaOut_moveme = Nothing
	End Function
End Class






