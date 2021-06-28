Option Strict Off
Option Explicit On
Public Class Officeins
	'%-------------------------------------------------------%'
	'% $Workfile:: Officeins.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according the table in the system on 11/06/2000
	'**+ The key fields are nCompany, nOfficeIns
	'+ Propiedades según la tabla en el sistema el 06/11/2000
	'+ Los campos llave corresponden a nCompany, nOfficeIns
	
	'+ Column_name              Type                 Computed Length Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	'+ ------------------------ -------------------- -------- ------ ----- ----- -------- ------------------  --------------------
	Public nCompany As Integer 'smallint no       2      5     0     no       (n/a)               (n/a)
	Public nOfficeIns As Integer 'smallint no       2      5     0     no       (n/a)               (n/a)
	Public sDescript As String 'char     no       30                 yes      no                  yes
	Public sShort_des As String 'char     no       12                 yes      no                  yes
	Public sStatregt As String 'char     no       1                  yes      no                  yes
	Public nUsercode As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	
	'**% Find_Descript: busca la descripción de la oficina
	'% Find_Descript: busca la descripción de la oficina
	Public Function Find_Descript(ByVal lintCompany As Integer, ByVal lintOfficeins As Integer) As Boolean
		Dim lrecreaOriginalOffice As eRemoteDB.Execute
		
		lrecreaOriginalOffice = New eRemoteDB.Execute
		
		On Error GoTo Find_Descript_Err
		
		'**+Stored procedure parameters definition 'insudb.reaOriginalOffice'
		'**+Data of 11/06/2000 02:53:23 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaOriginalOffice'
		'+ Información leída el 06/11/2000 02:53:23 p.m.
		
		With lrecreaOriginalOffice
			.StoredProcedure = "reaOriginalOffice"
			.Parameters.Add("nCompany", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeIns", lintOfficeins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Descript = True
				sDescript = .FieldToClass("sDescript")
				.RCloseRec()
			Else
				Find_Descript = False
				sDescript = String.Empty
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaOriginalOffice may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaOriginalOffice = Nothing
		
Find_Descript_Err: 
		If Err.Number Then
			Find_Descript = False
		End If
		On Error GoTo 0
	End Function
End Class






