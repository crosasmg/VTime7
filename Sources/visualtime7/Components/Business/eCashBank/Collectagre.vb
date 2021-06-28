Option Strict Off
Option Explicit On
Public Class Collectagre
	'%-------------------------------------------------------%'
	'% $Workfile:: Collectagre.cls                          $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 29/09/03 3:17p                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on February 6, 2001.
	'+ Propiedades según la tabla en el sistema al 06/02/2001.
	'**+ The key field in the table correspond to: nIdCollect.
	'+ El campo llave de la tabla corresponde a: nIdCollect.
	
	'   Column_name                    Type      Computed  Length      Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	Public nIdCollect As Integer 'int         no        4           10    0     no          (n/a)                (n/a)
	Public nIntermed As Integer 'int         no        4           10    0     yes         (n/a)                (n/a)
	Public nAmount As Double 'decimal     no        9           10    2     yes         (n/a)                (n/a)
	Public nBalance As Double 'decimal     no        9           10    2     yes         (n/a)                (n/a)
	Public nCurrency As Integer 'smallint    no        2           5     0     yes         (n/a)                (n/a)
	Public dPayIntere As Date 'datetime    no        8                       yes         (n/a)                (n/a)
	Public dCharge As Date 'datetime    no        8                       yes         (n/a)                (n/a)
	Public dPayCancel As Date 'datetime    no        8                       yes         (n/a)                (n/a)
	Public nBordereaux As Double 'int         no        4           10    0     yes         (n/a)                (n/a)
	Public nUsercode As Integer 'smallint    no        2           5     0     no          (n/a)                (n/a)
	
	'**- Auxiliary Variables
	'- Variables Auxiliares
	
	Public nTotal As Double
	
	'**% FindIntermedAcum: Verifies that the amount is the same or more than the accumulate
	'**% for an intermediary in the Collectagre table.
	'% FindIntermedAcum: Verifica que el monto sea mayor o igual al acumulado
	'%  para un intermediario en la tabla Collectagre
	Public Function FindIntermedAcum(ByVal lngIntermed As Integer, ByVal lngCurrAmo As Integer, ByVal strEffecdate As String) As Boolean
		
		'**- Variable definition lrecreaCollectagre_max
		'- Se define la variable lrecreaCollectagre_max
		
		Dim lrecreaCollectagre_max As eRemoteDB.Execute
		lrecreaCollectagre_max = New eRemoteDB.Execute
		
		On Error GoTo FindIntermedAcum_Err
		
		'**+ Parameter definition for stored procedure 'insudb.reaCollectagre_max'
		'+ Definición de parámetros para stored procedure 'insudb.reaCollectagre_max'
		'**+ Data of February 06,2001  09:44:37
		'+ Información leída el 06/02/2001 9:44:37
		
		With lrecreaCollectagre_max
			.StoredProcedure = "reaCollectagre_max"
			.Parameters.Add("nIntermed", lngIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrAmo", lngCurrAmo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sEffecdate", strEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				nTotal = .FieldToClass("nTotal")
				FindIntermedAcum = True
				.RCloseRec()
			Else
				FindIntermedAcum = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaCollectagre_max may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCollectagre_max = Nothing
		
FindIntermedAcum_Err: 
		If Err.Number Then
			FindIntermedAcum = False
		End If
		On Error GoTo 0
	End Function
End Class






