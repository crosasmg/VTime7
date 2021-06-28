Option Strict Off
Option Explicit On
Public Class Tab_bk_age
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_bk_age.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Description of the Tab_bk_age
	'- Descripciòn de la Tab_bk_age
	'**- The key fields are: nBank_code, nBk_agency
	'- los campos llave son: nBank_code, nBk_agency
	'- Column_name              Type                   Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'- ------------------------ ----------------------- --------- ------ ----- ----- -------- ------------------ --------------------
	Public nBank_code As Integer 'smallint  no        2      5     0     no       (n/a)              (n/a)
	Public nBk_agency As Integer 'smallint  no        2      5     0     no       (n/a)              (n/a)
	Public dCompdate As Date 'datetime  no        8                  no       (n/a)              (n/a)
	Public sShort_des As String 'char      no        14                 no       yes                no
	Public sStatregt As String 'char      no        14                 no       yes                no
	Public sN_Aba As String 'char      no        14                 no       yes                no
	
	'**%Find: Returns TRUE or FALSE if the records exists in the table "Tab_bk_age"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Tab_bk_age"
	'  --------------------------------------------------------------------------------------------
	Public Function Find(ByVal nBank_code As String, ByVal nBk_agency As String) As String
		Dim lrecTab_bk_age As eRemoteDB.Execute
		
		lrecTab_bk_age = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		
		'**+ Parameter definition for stored procedure 'insudb.reaTab_bk_age'
		'+Definición de parámetros para stored procedure 'insudb.reaTab_bk_age'
		'**+ Information read on Decemeber 02,1999  09:55:46 a.m.
		'+Información leída el 02/12/1999 09:55:46 AM
		
		With lrecTab_bk_age
			.StoredProcedure = "reaTab_bk_age"
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBk_agency", nBk_agency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = CStr(.Run(False))
		End With
		'UPGRADE_NOTE: Object lrecTab_bk_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_bk_age = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = CStr(False)
		End If
		
		On Error GoTo 0
		
	End Function
End Class






