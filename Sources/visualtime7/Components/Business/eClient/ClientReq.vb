Option Strict Off
Option Explicit On
Public Class ClientReq
	'%-------------------------------------------------------%'
	'% $Workfile:: ClientReq.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on June 01, 2000
	'+ Propiedades según la tabla en el sistema 01/06/2000
	'**+ The key fields corresponds to nBranch, nProduct, nRole and dEffecdate
	'+ Los campos llaves corresponden a nBranch, nProduct, nRole y dEffecdate
	
	'+ Column_name               Type                    Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------------- ----------------------- --------- ------ ----- ----- -------- ------------------ --------------------
	Public nBranch As Integer ' smallint     no       2      5     0     no        (n/a)               (n/a)
	Public nProduct As Integer ' smallint     no       2      5     0     no        (n/a)               (n/a)
	Public nRole As Integer ' smallint     no       2      5     0     no        (n/a)               (n/a)
	Public dEffecdate As Date ' datetime     no       8                  no        (n/a)               (n/a)
	Public sBirthdai As String ' char         no       1                  yes        yes                 yes
	Public sCivistai As String ' char         no       1                  yes        yes                 yes
	Public dNulldate As Date ' datetime     no       8                  yes       (n/a)               (n/a)
	Public sOccupati As String ' char         no       1                  yes        yes                 yes
	Public sSexinsui As String ' char         no       1                  yes        yes                 yes
	Public nUsercode As Integer ' number
	Public sTax_situa As String ' char         no       1                  yes        yes                 yes
	Public sAddress As String ' char         no       1                  yes        yes                 yes
	Public sCreditLine As String ' char         no       1                  yes        yes                 yes
	
	Public Finded As Boolean
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "ClientReq"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "ClientReq"'% Find: MISSING
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nRole As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecClient_req As eRemoteDB.Execute
		
		lrecClient_req = New eRemoteDB.Execute
		
		If nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nRole <> Me.nRole Or bFind Then
			
			With lrecClient_req
				.StoredProcedure = "reaClient_req"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nRole = nRole
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.sBirthdai = .FieldToClass("sBirthdai")
					Me.sCivistai = .FieldToClass("sCivistai")
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.sOccupati = .FieldToClass("sOccupati")
					Me.sSexinsui = .FieldToClass("sSexinsui")
					Me.sTax_situa = .FieldToClass("sTax_situa")
					Me.sAddress = .FieldToClass("sAddress")
					Me.sCreditLine = .FieldToClass("sCreditLine")
					.RCloseRec()
					Find = True
				End If
			End With
		Else
			Find = True
		End If
		'UPGRADE_NOTE: Object lrecClient_req may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecClient_req = Nothing
		Finded = Find
	End Function
End Class






