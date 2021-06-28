Option Strict Off
Option Explicit On
Public Class Decla_Benef
	'%-------------------------------------------------------%'
	'% $Workfile:: Decla_Benef.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.35                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Public sCerType As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public nNumdecla As Integer
	Public dEffecdate As Date
	Public sIrrevoc As String
	Public dDatedecla As Date
	Public nUsercode As Integer
	
	
	
	'%Find: Busca los datos en la tabla decla_benef
	Public Function Find(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal ldblPolicy As Double, ByVal ldblCertif As Double, ByVal ldtmEffecdate As Date) As Boolean
		Dim lrecreaClaim_1 As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If lintBranch <> nBranch Or lintProduct <> nProduct Or ldblPolicy <> nPolicy Or ldblCertif <> nCertif Or ldtmEffecdate <> dEffecdate Then
			
			
			Me.nBranch = lintBranch
			Me.nProduct = lintProduct
			Me.nPolicy = ldblPolicy
			Me.nCertif = ldblCertif
			Me.dEffecdate = ldtmEffecdate
			
			lrecreaClaim_1 = New eRemoteDB.Execute
			
			'Definición de parámetros para stored procedure 'insudb.reaClaim_1'
			'Información leída el 20/09/1999 08:02:03 AM
			With lrecreaClaim_1
				.StoredProcedure = "READECLA_BENEF"
				.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", Me.nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", Me.nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.sCerType = .FieldToClass("sCertype")
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nPolicy = .FieldToClass("nPolicy")
					Me.nCertif = .FieldToClass("nCertif")
					Me.nNumdecla = IIf(.FieldToClass("nNumdecla") = eRemoteDB.Constants.intNull, 0, .FieldToClass("nNumdecla"))
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.sIrrevoc = .FieldToClass("sIrrevoc")
					Me.dDatedecla = .FieldToClass("dDatedecla")
					Me.nUsercode = .FieldToClass("nUsercode")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
			lrecreaClaim_1 = Nothing
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






