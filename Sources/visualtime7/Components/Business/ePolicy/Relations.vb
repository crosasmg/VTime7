Option Strict Off
Option Explicit On
Public Class Relations
	'%-------------------------------------------------------%'
	'% $Workfile:: Relations.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system on November 09, 2000.
	'- Propiedades según la tabla en el sistema 09/11/2000
	
	'Column_name              Type                              Computed  Length  Prec  Scale Nullable                          TrimTrailingBlanks                  FixedLenNullInSource
	'------------------------ --------------------------------- --------- ------- ----- ----- --------------------------------- ----------------------------------- -----------------------------------
	Public sClientr As String 'char       no        14                      no                                  no                                   no
	Public sClient As String 'char       no        14                      no                                  no                                   no
	Public dCompdate As Date 'datetime   no         8                      yes                                 (n/a)                               (n/a)
	Public nRelaship As Integer 'smallint   no         2           5     0    no                                  (n/a)                               (n/a)
	Public nUsercode As String 'smallint   no         2           5     0    yes                                 (n/a)                               (n/a)
	
	Public Function FindClientRelations(ByVal lsrtClient As String, ByVal lsrtClientr As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Static lblnRead As Boolean
		
		Dim lrecreaRelations_2 As eRemoteDB.Execute
		
		lrecreaRelations_2 = New eRemoteDB.Execute
		
		If sClient <> lsrtClient Or sClientr <> lsrtClientr Or lblnFind Then
			
			sClient = lsrtClient
			sClientr = lsrtClientr
			
			'**+ Parameter definition for stored procedure 'insudb.reaRelations_2'
			'+Definición de parámetros para stored procedure 'insudb.reaRelations_2'
			'**+ Information read on November 13,2000  18:22:43
			'+Información leída el 13/11/2000 18:22:43
			
			With lrecreaRelations_2
				.StoredProcedure = "reaRelations_2"
				.Parameters.Add("sClient", lsrtClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClientr", lsrtClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sClientr = .FieldToClass("sClientr")
					sClient = .FieldToClass("sClient")
					nRelaship = .FieldToClass("nRelaship")
					lblnRead = True
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
		End If
		
		FindClientRelations = lblnRead
		
		'UPGRADE_NOTE: Object lrecreaRelations_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaRelations_2 = Nothing
		
	End Function
	'**%Update: Updates records in the table "Relations".  It returns TRUE or FALSE depending on the execution of the stored procedure.
	'%Update: Este método se encarga de actualizar registros en la tabla "Relations". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		
		Dim lrecupdRelations As eRemoteDB.Execute
		
		lrecupdRelations = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.updRelations'
		'+Definición de parámetros para stored procedure 'insudb.updRelations'
		'**+ Information read on Novemeber 13,2000  18:02:58
		'+Información leída el 13/11/2000 18:02:58
		
		With lrecupdRelations
			.StoredProcedure = "updRelations"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientr", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdRelations may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdRelations = Nothing
		
	End Function
	
	'**%ADD: Adds new records to the table "Relations".  It returns TRUE or FALSE if stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Relations". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		
		Dim lreccreRelations As eRemoteDB.Execute
		
		lreccreRelations = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.creRelations'
		'+Definición de parámetros para stored procedure 'insudb.creRelations'
		'**+ Information read on Novemeber 13,2000  18:04:59
		'+Información leída el 13/11/2000 18:04:59
		
		With lreccreRelations
			.StoredProcedure = "creRelations"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClientr", sClientr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelaship", nRelaship, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreRelations may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreRelations = Nothing
		
	End Function
End Class






