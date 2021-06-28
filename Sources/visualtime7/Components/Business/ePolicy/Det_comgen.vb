Option Strict Off
Option Explicit On
Public Class Det_comgen
	'%-------------------------------------------------------%'
	'% $Workfile:: Det_comgen.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on 12/28/2000
	'**-The key fields are nComtabge, nBranch, nProduct, nCover y dEffecdate.
	'-Propiedades según la tabla en el sistema al 28/12/2000.
	'-Los campos llave de la tabla corresponden a: nComtabge, nBranch, nProduct, nCover y dEffecdate.
	
	'   Column_name                   Type         Computed  Length      Prec  Scale Nullable    TrimTrailingBlanks    FixedLenNullInSource
	'-------------------------------- ------------ --------- ----------- ----- ----- ----------- --------------------- --------------------
	Public nComtabge As Integer 'smallint    no        2           5     0     no          (n/a)                 (n/a)
	Public nBranch As Integer 'smallint    no        2           5     0     no          (n/a)                 (n/a)
	Public nProduct As Integer 'smallint    no        2           5     0     no          (n/a)                 (n/a)
	Public nCover As Integer 'smallint    no        2           5     0     no          (n/a)                 (n/a)
	Public dEffecdate As Date 'datetime    no        8                       no          (n/a)                 (n/a)
	Public nRate_first As Double 'decimal     no        5           4     2     yes         (n/a)                 (n/a)
	Public nRate_renew As Double 'decimal     no        5           4     2     yes         (n/a)                 (n/a)
	Public nUsercode As Integer 'smallint    no        2           5     0     no          (n/a)                 (n/a)
	Public dNulldate As Date 'datetime    no        8                       yes         (n/a)                 (n/a)
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Det_comgen"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Det_comgen"
	Public Function Find(ByVal nComtabge As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Static lblnRead As Boolean
		
		'**-Defines the variable lrecreaDet_comgen_v
		'-Se define la variable lrecreaDet_comgen_v
		
		Dim lrecreaDet_comgen_v As eRemoteDB.Execute
		lrecreaDet_comgen_v = New eRemoteDB.Execute
		
		If Me.nComtabge <> nComtabge Or Me.dEffecdate <> dEffecdate Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nCover <> nCover Or lblnFind Then
			
			Me.nComtabge = nComtabge
			Me.dEffecdate = dEffecdate
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nCover = nCover
			
			'**+Stored procedure parameters definition 'insudb.reaDet_comgen_v'
			'**+Data of 12/28/2000 11:37:31
			'+Definición de parámetros para stored procedure 'insudb.reaDet_comgen_v'
			'+Información leída el 28/12/2000 11:37:31
			
			With lrecreaDet_comgen_v
				.StoredProcedure = "reaDet_comgen_v"
				.Parameters.Add("nComtabge", nComtabge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nComtabge = .FieldToClass("nComtabge")
					nBranch = .FieldToClass("nBranch")
					nProduct = .FieldToClass("nProduct")
					nCover = .FieldToClass("nCover")
					dEffecdate = .FieldToClass("dEffecdate")
					nRate_first = .FieldToClass("nRate_first")
					nRate_renew = .FieldToClass("nRate_renew")
					nUsercode = .FieldToClass("nUsercode")
					dNulldate = .FieldToClass("dNulldate")
					
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		
		Find = lblnRead
		'UPGRADE_NOTE: Object lrecreaDet_comgen_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDet_comgen_v = Nothing
	End Function
End Class






