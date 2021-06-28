Option Strict Off
Option Explicit On
'UPGRADE_NOTE: Property was upgraded to Property_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
<System.Runtime.InteropServices.ProgId("Property_Renamed_NET.Property_Renamed")> Public Class Property_Renamed
	'%-------------------------------------------------------%'
	'% $Workfile:: Property.cls                             $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 33                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system on November 06,2000
	'- Propiedades según la tabla en el sistema al 06/11/2000
	'**- The key fields of the table corresponds to: sCertype, nBranch, nProduct, nPolicy, nCertif, nId, dEffecdate
	'- Los campos llave de la tabla corresponden a: sCertype, nBranch, nProduct, nPolicy, nCertif, nId, dEffecdate
	
	'- Column_name                     Type    Computed    Length   Prec  Scale Nullable      TrimTrailingBlanks                  FixedLenNullInSource
	'- -------------------------------------------------------------------------------------------------------------------------------- ------------
	Public sCertype As String 'char       no          1                   no                no                                  no
	Public nBranch As Integer 'smallint   no          2       5     0     no                (n/a)                               (n/a)
	Public nProduct As Integer 'smallint   no          2       5     0     no                (n/a)                               (n/a)
	Public nPolicy As Double 'int        no          4      10     0     no                (n/a)                               (n/a)
	Public nCertif As Double 'int        no          4      10     0     no                (n/a)                               (n/a)
	Public nId As Integer 'smallint   no          2       5     0     no                (n/a)                               (n/a)
	Public dEffecdate As Date 'datetime   no          8                   no                (n/a)                               (n/a)
	Public nCode_good As Integer 'smallint   no          2       5     0     yes               (n/a)                               (n/a)
	Public nCapital As Double 'decimal    no          9      12     0     yes               (n/a)                               (n/a)
	Public sDescript As String 'char       no        200                   yes               no                                  yes
	Public sFrandedi As String 'char       no          1                   yes               no                                  yes
	Public nDiscount As Double 'decimal    no          5       4     2     yes               (n/a)                               (n/a)
	Public nLost_capit As Double 'decimal    no          9      14     2     yes               (n/a)                               (n/a)
	Public nNotenum As Integer 'smallint   no          2       5     0     yes               (n/a)                               (n/a)
	Public nFixamount As Double 'decimal    no          9      10     0     yes               (n/a)                               (n/a)
	Public dNulldate As Date 'datetime   no          8                   yes               (n/a)                               (n/a)
	Public nMaxamount As Double 'decimal    no          9      10     0     yes               (n/a)                               (n/a)
	Public nPremium As Double 'decimal    no          9      10     2     yes               (n/a)                               (n/a)
	Public nMinamount As Double 'decimal    no          9      10     0     yes               (n/a)                               (n/a)
	Public nRateProp As Double 'decimal    no          5       6     4     yes               (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint   no          2       5     0     yes               (n/a)                               (n/a)
	Public nRate As Double 'decimal    no          5       4     2     yes               (n/a)                               (n/a)
	Public nCurrency As Integer 'smallint   no          2       5     0     yes               (n/a)                               (n/a)
	Public nServ_order As Double 'number                22       0     5     N
	
	'- Se define la variable para almacenar si la transacción esta con o sin contenido
	Public sContent As String
	
	
	'**% Add: insert an insuranced good in the policy.
	'% Add: Inserta un bien asegurado dentro de la póliza
	Public Function Add() As Boolean
		Dim lreccreProperty As eRemoteDB.Execute
		
		lreccreProperty = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		'**+ Parameter definition for stored procedure 'insudb.creProperty'
		'+ Definición de parámetros para stored procedure 'insudb.creProperty'
		With lreccreProperty
			.StoredProcedure = "creProperty"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFixamount", nFixamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nMinamount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrandedi", sFrandedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateprop", nRateProp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreProperty = Nothing
	End Function
	
	'**% Delete: Deletes an insuranced good in the policy.
	'% Delete: Elimina un bien asegurado dentro de la póliza
	'-----------------------------------------------------------
	Public Function Delete(ByVal sAction As String) As Boolean
		'-----------------------------------------------------------
		Dim lrecdelProperty As eRemoteDB.Execute
		
		lrecdelProperty = New eRemoteDB.Execute
		
		On Error GoTo Delete_err
		
		'**+ Parameter definition for stored procedure 'insudb.delProperty'
		'+ Definición de parámetros para stored procedure 'insudb.delProperty'
		'**+ Information read on November 08, 2000   09:44:52 a.m.
		'+ Información leída el 08/11/2000 09:44:52 AM
		With lrecdelProperty
			.StoredProcedure = "delProperty"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelProperty = Nothing
	End Function
	
	'*** FindPropertyID: Restores the correlative code assigned to a new good.
	'* FindPropertyID: Devuelve el código correlativo asignado al nuevo Bien
	Public ReadOnly Property FindPropertyID(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal lblnFind As Boolean = False) As Integer
		Get
			Dim lrecreaProperty_ID As eRemoteDB.Execute
			
			lrecreaProperty_ID = New eRemoteDB.Execute
			
			On Error GoTo FindPropertyID_Err
			
			'**+ Parameter definition for stored procedure 'insudb.reaProperty_ID'
			'+ Definición de parámetros para stored procedure 'insudb.reaProperty_ID'
			'**+ Information read on November08,2000  10:16:56 a.m.
			'+ Información leída el 08/11/2000 10:16:56 AM
			With lrecreaProperty_ID
				.StoredProcedure = "reaProperty_ID"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					If (.FieldToClass("nId") = eRemoteDB.Constants.intNull) Then
						FindPropertyID = 1
					Else
						FindPropertyID = .FieldToClass("nId") + 1
					End If
					.RCloseRec()
				End If
			End With
			
FindPropertyID_Err: 
			If Err.Number Then
				FindPropertyID = False
			End If
			'UPGRADE_NOTE: Object lrecreaProperty_ID may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaProperty_ID = Nothing
		End Get
	End Property
	
	'**% Update: Updates the insuranced goods in a policy.
	'% Update: Actualiza los bienes asegurados dentro de la póliza
	'-------------------------------------------------------------
	Public Function Update() As Boolean
		'-------------------------------------------------------------
		Dim lrecinsProperty As eRemoteDB.Execute
		
		lrecinsProperty = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+ Parameter definition for stored procedure 'insudb.insProperty'
		'+ Definición de parámetros para stored procedure 'insudb.insProperty'
		'**+ Information read on November 08,2000  10:07:20 a.m.
		'+ Información leída el 08/11/2000 10:07:20 AM
		With lrecinsProperty
			.StoredProcedure = "insProperty"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_good", nCode_good, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMaxamount", nMaxamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFixamount", nFixamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMinamount", nMinamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrandedi", sFrandedi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateprop", nRateProp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecinsProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsProperty = Nothing
	End Function
	
	'% Find: Devuelve un registro de la tabla property
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nId As Integer) As Boolean
		Dim lrecreaProperty As eRemoteDB.Execute
		
		lrecreaProperty = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'**+ Parameter definition for stored procedure 'insudb.reaProperty_ID'
		'+ Definición de parámetros para stored procedure 'insudb.reaProperty_ID'
		'**+ Information read on November08,2000  10:16:56 a.m.
		'+ Información leída el 08/11/2000 10:16:56 AM
		With lrecreaProperty
			.StoredProcedure = "reaProperty_1"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Me.sCertype = .FieldToClass("sCertype")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.nId = .FieldToClass("nId")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.nCode_good = .FieldToClass("nCode_good")
				Me.nCapital = .FieldToClass("nCapital")
				Me.sDescript = .FieldToClass("sDescript")
				Me.sFrandedi = .FieldToClass("sFrandedi")
				Me.nDiscount = .FieldToClass("nDiscount")
				Me.nLost_capit = .FieldToClass("nLost_capit")
				Me.nNotenum = .FieldToClass("nNotenum")
				Me.nFixamount = .FieldToClass("nFixamount")
				Me.dNulldate = .FieldToClass("dNulldate")
				Me.nMaxamount = .FieldToClass("nMaxamount")
				Me.nPremium = .FieldToClass("nPremium")
				Me.nMinamount = .FieldToClass("nMinamount")
				Me.nRateProp = .FieldToClass("nRateprop")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.nRate = .FieldToClass("nRate")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.nServ_order = .FieldToClass("nServ_order")
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaProperty may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProperty = Nothing
	End Function
	
	'% insValCA010: Valida la información general del bien a asegurar
    Public Function insValCA010(ByVal sCodispl As String, ByVal sAction As String, ByVal nTabGoods As Integer, ByVal sDescript As String, ByVal nCurrency As Integer, ByVal nCapital As Double, ByVal nRateProp As Double, ByVal nPremium As Double, ByVal nRate As Double, ByVal nFixamount As Double, ByVal nMinamount As Double, ByVal nMaxamount As Double, ByVal nOriginalRateProp As Double, ByVal nOriginalPremium As Double, ByVal nFranDedi As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nServ_order As Double, ByVal sCertype As String, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nId As Double) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsTab_goods As Tab_goods
        Dim lclsProperty As ePolicy.Property_Renamed
        Dim lcolPropertys As ePolicy.Propertys
        Dim lclsAuto As ePolicy.Automobile
        Dim lclsProf_ord As Object
        Dim intCount As Integer
        Dim nCapitalAseg As Double
        Dim nSumValorBienes As Double

        lobjErrors = New eFunctions.Errors
        lclsTab_goods = New Tab_goods
        lclsProperty = New ePolicy.Property_Renamed
        lcolPropertys = New ePolicy.Propertys
        lclsAuto = New ePolicy.Automobile

        lclsProf_ord = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Prof_ord")

        insValCA010 = CStr(True)
        On Error GoTo insValCA010_Err

        '+ Tipo de bien a asegurar inválido
        If nTabGoods <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 1084)
            insValCA010 = CStr(False)
        Else
            If ((nPremium = eRemoteDB.Constants.intNull Or nPremium = 0) And (nRateProp = eRemoteDB.Constants.intNull Or nRateProp = 0)) Then
                Call lobjErrors.ErrorMessage(sCodispl, 60208)
            End If

            '+ Descripción de bien, vacía
            If Trim(sDescript) = "" Then
                Call lobjErrors.ErrorMessage(sCodispl, 3831)
                insValCA010 = CStr(False)
            End If
        End If

        '+ Moneda inválida
        If nCurrency <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 1351)
            insValCA010 = CStr(False)
        End If

        '+ Capital inválido
        If nCapital <= 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 3832)
            insValCA010 = CStr(False)
        End If

        '+ Numero de orden de servicio
        If nServ_order > 0 And nServ_order <> eRemoteDB.Constants.intNull Then
            If Not lclsProf_ord.Find_nServ(nServ_order) Then
                Call lobjErrors.ErrorMessage(sCodispl, 4056)
                insValCA010 = CStr(False)
            End If
        End If

        '+ Selección de franquicia o deducible
        If nFranDedi > 0 Then

            '+ (%)Franquicia/Deducible o Importe fijo
            '+ Valida que por lo menos unos de los campos (porcentaje o fijo) tenga valor
            If nRate > 0 Then
                If nFixamount > 0 Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3046)
                    insValCA010 = CStr(False)
                End If

                '+ (%)Franquicia/Deducible fuera de rango
                '+ Valida que el porcentaje se encuentre entre el rango permitido
                If nRate < 0 And nRate > 100 Then
                    Call lobjErrors.ErrorMessage(sCodispl, 1935)
                    insValCA010 = CStr(False)
                End If

                '+ (%)Franquicia/Deducible fuera de rango
                '+ Valida que por lo menos unos de los campos (porcentaje o fijo) tenga valor
            ElseIf Not nFranDedi = 1 Then
                If nRate <= 0 Then
                    If nFixamount <= 0 Then
                        Call lobjErrors.ErrorMessage(sCodispl, 3046)
                        insValCA010 = CStr(False)
                    End If
                End If
            End If
        End If

        '+ Si se especifica el Importe fijo no es permitido un máximo o mínimo.
        If nFixamount > 0 Then
            If nMinamount > 0 Or nMaxamount > 0 Then
                Call lobjErrors.ErrorMessage(sCodispl, 3055)
                insValCA010 = CStr(False)
            End If
        End If

        '+ El monto mínimo no puede ser mayor al máximo.
        If nMinamount > 0 And nMaxamount > 0 Then
            If nMinamount > nMaxamount Then
                Call lobjErrors.ErrorMessage(sCodispl, 3462)
                insValCA010 = CStr(False)
            End If
        End If
        '+Si el ramo es vehículo, la sumatoria del Valor de los bienes asegurados no puede ser mayor que el 20% del valor asegurado del vehículo.
        If nBranch = 6 Then
            nCapitalAseg = 0
            If lclsAuto.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                If lclsAuto.nVeh_valor <> eRemoteDB.Constants.intNull Then
                    nCapitalAseg = lclsAuto.nVeh_valor
                End If
            End If
            nSumValorBienes = nCapital
            If lcolPropertys.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                For Each lclsProperty In lcolPropertys
                    If nId <> lclsProperty.nId Then
                        nSumValorBienes = nSumValorBienes + lclsProperty.nCapital
                    End If
                Next
            End If

            If nSumValorBienes > (nCapitalAseg * 0.20000000000000001) Then
                Call lobjErrors.ErrorMessage(sCodispl, 900045)
            End If
        End If

        '+ Se realizan las validaciones particulares en relación con el diseñador
        Call lclsTab_goods.insValRate(sCodispl, nBranch, nProduct, nTabGoods, nCapital, nRateProp, nPremium, lobjErrors)

        insValCA010 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsTab_goods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_goods = Nothing
        'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProf_ord = Nothing

insValCA010_Err:
        If Err.Number Then
            insValCA010 = insValCA010 & Err.Description
        End If
        On Error GoTo 0

    End Function


    '% insValCA010: Valida la información general del bien a asegurar
    Public Function insValCA010All(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sCertype As String, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsTab_goods As Tab_goods
        Dim lclsProperty As ePolicy.Property_Renamed
        Dim lcolPropertys As ePolicy.Propertys
        Dim lclsAuto As ePolicy.Automobile
        Dim lclsProf_ord As Object
        Dim intCount As Integer
        Dim nCapitalAseg As Double
        Dim nSumValorBienes As Double

        lobjErrors = New eFunctions.Errors
        lclsTab_goods = New Tab_goods
        lclsProperty = New ePolicy.Property_Renamed
        lcolPropertys = New ePolicy.Propertys
        lclsAuto = New ePolicy.Automobile

        lclsProf_ord = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Prof_ord")

        insValCA010All = CStr(True)
        On Error GoTo insValCA010All_Err


        '+Si el ramo es vehículo, la sumatoria del Valor de los bienes asegurados no puede ser mayor que el 20% del valor asegurado del vehículo.
        If nBranch = 6 Then
            nCapitalAseg = 0
            If lclsAuto.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                If lclsAuto.nVeh_valor <> eRemoteDB.Constants.intNull Then
                    nCapitalAseg = lclsAuto.nVeh_valor
                End If
            End If

            If lcolPropertys.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                For Each lclsProperty In lcolPropertys
                    nSumValorBienes = nSumValorBienes + lclsProperty.nCapital
                Next
            End If

            If nSumValorBienes > (nCapitalAseg * 0.20000000000000001) Then
                Call lobjErrors.ErrorMessage(sCodispl, 900045)
            End If
        End If


        insValCA010All = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsTab_goods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_goods = Nothing
        'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProf_ord = Nothing

insValCA010All_Err:
        If Err.Number Then
            insValCA010All = insValCA010All & Err.Description
        End If
        On Error GoTo 0

    End Function


    '% insValCA010: Valida la información general del bien a asegurar
    Public Function insPostCA010All(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sCertype As String, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nType_amend As Double, ByVal nTransactio As Double) As Boolean
        Dim lobjErrors As eFunctions.Errors
        Dim lclsTab_goods As Tab_goods
        Dim lclsProperty As ePolicy.Property_Renamed
        Dim lcolPropertys As ePolicy.Propertys
        Dim lclsAuto As ePolicy.Automobile
        Dim lclsProf_ord As Object
        Dim intCount As Integer
        Dim nCapitalAseg As Double
        Dim nSumValorBienes As Double
        Dim lclsCertificat As ePolicy.Certificat
        lclsCertificat = New ePolicy.Certificat
        Dim lclsDepreciatedCapital As ePolicy.DepreciatedCapital = New DepreciatedCapital()
        lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)

        lobjErrors = New eFunctions.Errors
        lclsTab_goods = New Tab_goods
        lclsProperty = New ePolicy.Property_Renamed
        lcolPropertys = New ePolicy.Propertys
        lclsAuto = New ePolicy.Automobile

        lclsProf_ord = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Prof_ord")


        On Error GoTo insPostCA010All_Err

        If lclsCertificat.sInd_Multiannual = "1" Then

            If lcolPropertys.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                For Each lclsProperty In lcolPropertys
                    nSumValorBienes = nSumValorBienes + lclsProperty.nCapital
                Next
            End If

            insPostCA010All = lclsDepreciatedCapital.CalculateDepreciatedCapitalByCoverage(sCertype, nBranch, nProduct, nPolicy, nCertif, lclsCertificat.nGroup, dEffecdate, "CA_C_BA", nSumValorBienes, nType_amend, dEffecdate, nTransactio)
        Else
            insPostCA010All = True
        End If

        lobjErrors = Nothing        
        lclsTab_goods = Nothing        
        lclsProf_ord = Nothing

insPostCA010All_Err:
        If Err.Number Then
            insPostCA010All = False
        End If
        On Error GoTo 0

    End Function
    '% insPostCA010: Se realiza la actualización de los datos de los bienes asegurables y el
    '%               estado de la forma (PolicyWin)
    Public Function insPostCA010(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nId As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, Optional ByVal nCode_good As Integer = 0, Optional ByVal nCapital As Double = 0, Optional ByVal nMaxamount As Double = 0, Optional ByVal nFixamount As Double = 0, Optional ByVal nMinamount As Double = 0, Optional ByVal nRate As Double = 0, Optional ByVal sDescript As String = "", Optional ByVal sFrandedi As String = "", Optional ByVal nNotenum As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nPremium As Double = 0, Optional ByVal nRateProp As Double = 0, Optional ByVal nServ_order As Double = 0, Optional ByVal nCapital_acum As Double = 0) As Boolean
        Dim lclsPolicy_Win As ePolicy.Policy_Win
        Dim lclsPropertys As Propertys

        On Error GoTo insPostCA010_Err
        Select Case nTransaction
            '+ Consulta de: póliza, certificados, cotización, solicitud
            Case 8, 9, 10, 11
            Case Else
                If insUpdTdbCa010(nTransaction, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, nId, sAction, nCode_good, nCapital, nMaxamount, nFixamount, nMinamount, nRate, sDescript, sFrandedi, nNotenum, nCurrency, nPremium, nRateProp, nServ_order, nCapital_acum) Then
                    insPostCA010 = True
                    lclsPropertys = New ePolicy.Propertys
                    If lclsPropertys.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                        Me.sContent = "2"
                    Else
                        Me.sContent = "1"
                    End If

                    lclsPolicy_Win = New ePolicy.Policy_Win
                    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA010", Me.sContent)
                    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014", "3")
                    If nBranch = 21 Then
                        Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "MU700", "3")
                        Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA009", "3")
                    End If

                Else
                    insPostCA010 = False
                End If
        End Select

insPostCA010_Err:
        If Err.Number Then
            insPostCA010 = False
        End If
        lclsPropertys = New ePolicy.Propertys
        'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy_Win = Nothing
        On Error GoTo 0
    End Function

    '% insUpdTdbCa010: Actualiza los datos de los bienes asegurables
    Private Function insUpdTdbCa010(ByVal nTransaction As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nId As Integer, ByVal sAction As String, Optional ByVal nCode_good As Integer = 0, Optional ByVal nCapital As Double = 0, Optional ByVal nMaxamount As Double = 0, Optional ByVal nFixamount As Double = 0, Optional ByVal nMinamount As Double = 0, Optional ByVal nRate As Double = 0, Optional ByVal sDescript As String = "", Optional ByVal sFrandedi As String = "", Optional ByVal nNotenum As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nPremium As Double = 0, Optional ByVal nRateProp As Double = 0, Optional ByVal nServ_order As Double = 0, Optional ByVal nCapital_acum As Double = 0) As Boolean
        Dim lclsProdMaster As eProduct.Product
        Dim lclsAuto As Automobile

        lclsProdMaster = New eProduct.Product
        lclsAuto = New Automobile

        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nUsercode = nUsercode
            .nId = nId

            Select Case sAction

                '+ Registro nuevo
                Case "Add"
                    .nCode_good = nCode_good
                    .nCapital = nCapital
                    .nMaxamount = nMaxamount
                    .nFixamount = nFixamount
                    .nMinamount = nMinamount
                    .nRate = nRate
                    .sDescript = Trim(sDescript)
                    .sFrandedi = sFrandedi
                    .nNotenum = nNotenum
                    .nCurrency = nCurrency
                    .nPremium = nPremium
                    .nRateProp = nRateProp
                    .nId = .FindPropertyID(sCertype, nBranch, nProduct, nPolicy, nCertif)
                    .nServ_order = nServ_order
                    If nCapital_acum > 0 Then
                        Call lclsAuto.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, True)
                        lclsAuto.nVal_extra = nCapital_acum
                        lclsAuto.nNullcode = eRemoteDB.Constants.intNull
                        Call lclsAuto.Update()
                    End If
                    insUpdTdbCa010 = .Add

                    '+ Eliminación de registro
                Case "Del"
                    Select Case nTransaction
                        Case 1, 3, 4, 5, 6, 7

                            '+ Eliminación de registro : 1 - Por rechazo de siniestro
                            insUpdTdbCa010 = .Delete("1")
                        Case Else

                            '+ Eliminación de registro : 2 - Por anulación
                            insUpdTdbCa010 = .Delete("2")
                    End Select

                    '+ Actualización de registro
                Case "Update"
                    .nCode_good = nCode_good
                    .nCapital = nCapital
                    .nMaxamount = nMaxamount
                    .nFixamount = nFixamount
                    .nMinamount = nMinamount
                    .nRate = nRate
                    .sDescript = Trim(sDescript)
                    .sFrandedi = sFrandedi
                    .nNotenum = nNotenum
                    .nCurrency = nCurrency
                    .nPremium = nPremium
                    .nRateProp = nRateProp
                    .nId = nId
                    .nServ_order = nServ_order
                    If nCapital_acum > 0 Then
                        Call lclsAuto.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, True)
                        lclsAuto.nVal_extra = nCapital_acum
                        lclsAuto.nNullcode = eRemoteDB.Constants.intNull
                        Call lclsAuto.Update()
                    End If
                    insUpdTdbCa010 = .Update
            End Select

        End With

        'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsAuto = Nothing
        'UPGRADE_NOTE: Object lclsProdMaster may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProdMaster = Nothing

    End Function
End Class






