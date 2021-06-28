Option Strict Off
Option Explicit On
Public Class Agents
	'%-------------------------------------------------------%'
	'% $Workfile:: Agents.cls                               $%'
	'% $Author:: Nvaplat9                                   $%'
	'% $Date:: 22/10/03 3:54p                               $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on January 28,2000
	'+ Propiedades según la tabla en el sistema el 28/01/2000
	'**+ The key field correspond to nIntermed.
	'+ El campo llave corresponde a nIntermed.
	
	'+  Column name               Type                  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+  ------------------------- --------------------- ------ ----- ----- -------- ------------------ ---------------------
	Public nIntermed As Integer 'int      4      10    0     no       (n/a)              (n/a)
	Public sClient As String 'char     14                 yes      yes                yes
	Public nComtabge As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public nComtabli As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public dInpdate As Date 'datetime 8                  yes      (n/a)              (n/a)
	Public nInt_status As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public nInterTyp As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public nNullcode As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                  yes      (n/a)              (n/a)
	Public nOffice As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public nSupervis As Integer 'int      4      10    0     yes      (n/a)              (n/a)
	Public nTable_cod As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public nTax As Double 'decimal  5      4     2     yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public sCol_agree As String 'char     1                  yes      yes                yes
	Public nNotenum As Integer 'int      4      10    0     yes      (n/a)              (n/a)
	Public nEco_sche As Integer 'smallint 2      5     0     yes      (n/a)              (n/a)
	Public sInter_id As String 'int      4      10    0     yes      (n/a)              (n/a)
	Public sAgreeInt As String 'char     1      1           yes      yes                yes
	
	'**- Auxiliary Tables
	'-Variables auxiliares
	Public blnCol_Agree As Boolean
	Public sCliename As String
	
	Public WithInformation As String
	
	'**%Find: Searches the information for a specific intermediary
	'% Find: Busca la información de un determinado intermediario
	Public Function Find(ByVal IntermediaCode As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaIntermedia As eRemoteDB.Execute
		
		If CDbl(IntermediaCode) = nIntermed And Not lblnFind Then
			Find = True
		Else
			
			lrecreaIntermedia = New eRemoteDB.Execute
			
			'**+ Parameter definitions for stored procedure 'insud.reaClient'
			'+ Definición de parámetros para stored procedure 'insudb.reaClient'
			'**+ Data of July 1st,1999  03:20:55 p.m.
			'+ Información leída el 01/07/1999 03:20:55 PM
			
			With lrecreaIntermedia
				.StoredProcedure = "reaIntermedia"
				.Parameters.Add("nIntermedia", IntermediaCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nIntermed = CInt(IntermediaCode)
					sClient = .FieldToClass("sClient")
                    nComtabge = .FieldToClass("nComtabge", intNull)
                    nComtabli = .FieldToClass("nComtabli", intNull)
					dInpdate = .FieldToClass("dInpdate")
					nInt_status = .FieldToClass("nInt_status")
					nInterTyp = .FieldToClass("nIntertyp")
					nNullcode = .FieldToClass("nNullcode")
					dNulldate = .FieldToClass("dNulldate")
					nOffice = .FieldToClass("nOffice")
					nSupervis = .FieldToClass("nSupervis")
                    nTable_cod = .FieldToClass("nTable_cod", intNull)
					nTax = .FieldToClass("nTax")
					nUsercode = .FieldToClass("nUsercode")
					sCol_agree = .FieldToClass("sCol_agree")
					nNotenum = .FieldToClass("nNotenum")
                    nEco_sche = .FieldToClass("nEco_sche", intNull)
					sInter_id = .FieldToClass("sInter_id")
					sAgreeInt = .FieldToClass("sAgreeInt")
					sCliename = .FieldToClass("sCliename")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaIntermedia = Nothing
		End If
		
	End Function
	
	'**% Find: Searches for the information of an intermediary
	'% Find: Busca la información de un determinado intermediario
	Public Function Remove(ByVal IntermediaCode As String) As Boolean
		Dim lrecdelIntermedia As eRemoteDB.Execute
		lrecdelIntermedia = New eRemoteDB.Execute
		'**+ Parameter definition for stored procedure 'insudb.delIntermedia'
		'+Definición de parámetros para stored procedure 'insudb.delIntermedia'
		'**+Data of February, 06,2001  9.40.18
		'+Información leída el 06/02/2001 9.40.18
		With lrecdelIntermedia
			.StoredProcedure = "delIntermedia"
			.Parameters.Add("nIntermed", IntermediaCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Remove = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelIntermedia = Nothing
	End Function
	
	'**% FindTypeInter_Client: verifies that the intermediary's type for a client exists
	'% FindTypeInterm_Client: verifica que el tipo de intermediario para un cliente exista
	Public Function FindTypeInterm_Client(ByVal Client As String, ByVal InterTyp As Integer) As Boolean
		Dim lrecreaIntermTyp_Client As eRemoteDB.Execute
		
		lrecreaIntermTyp_Client = New eRemoteDB.Execute
		
		'**+ Parameter definitions for stored procedure 'insudb.reaIntermTyp_Client'
		'+ Definición de parámetros para stored procedure 'insudb.reaIntermTyp_Client'
		'**+ Data of February 25,2000  11:04:37 a.m.
		'+ Información leída el 25/02/2000 11:04:37 AM
		
		With lrecreaIntermTyp_Client
			.StoredProcedure = "reaIntermTyp_Client"
			.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", InterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nIntermed = .FieldToClass("nIntermed")
				sClient = .FieldToClass("sClient")
				nInterTyp = .FieldToClass("nIntertyp")
				FindTypeInterm_Client = True
				.RCloseRec()
			Else
				FindTypeInterm_Client = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaIntermTyp_Client may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaIntermTyp_Client = Nothing
	End Function
	
	'**%Find: This method fills the collection with records from the table "Intermedia" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Intermedia" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function findIntermediaClient(ByVal nIntermed As Integer, ByVal nInterTyp As Integer, ByVal dEffecdate As Date) As Boolean
		
		'**- Variable definition lrec_Intermed that will be used as a cursor.
		'-Se define la variable lrec_Intermed que se utilizará como cursor.
		
		Dim lrec_Intermed As eRemoteDB.Execute
		
		lrec_Intermed = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaIntermediaClient'
		'+Definición de parámetros para stored procedure 'insudb.reaIntermediaClient'
		'**+ Data of Novemeber 15,2000  04:49:59 a.m.
		'+Información leída el 15/11/2000 04:49:59 a.m.
		
		With lrec_Intermed
			.StoredProcedure = "reaIntermediaClient"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				If .ErrorNumber = eRemoteDB.Execute.ErrorDB.clngOK Then
					findIntermediaClient = True
					Me.nIntermed = .FieldToClass("nIntermed")
					Me.nInterTyp = .FieldToClass("nIntertyp")
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					If Not IsDbNull(.FieldToClass("sCol_agree")) Then
						Me.blnCol_Agree = CBool("1")
					Else
						Me.blnCol_Agree = False
					End If
					Me.nSupervis = .FieldToClass("nSupervis", 0)
					Me.sClient = .FieldToClass("sClient")
					Me.sCliename = .FieldToClass("sCliename")
					.RCloseRec()
				End If
			End If
		End With
		
		
		'**+ Execute the store procedure to verify if the recept exists or not, and can obtain its information.
		'+Se ejecuta el store procedure para verificar si existe o no el recibo y obtener sus datos.
		
		'Set lrec_Intermed = insExecuteQuery("insudb.reaIntermediaClient", clngQuery, lvntParameters(), True, True)
		
	End Function
	
	'**%UpdIntermedia: Update records in the table "intermedia".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%UpdIntermedia: Este método se encarga de actualizar registros en la tabla "Intermedia". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function UpdIntermedia() As Boolean
		Dim lrecupdIntermedia As eRemoteDB.Execute
		lrecupdIntermedia = New eRemoteDB.Execute
		
		With lrecupdIntermedia
			.StoredProcedure = "updIntermedia"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComtabge", nComtabge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTable_cod", nTable_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEco_sche", nEco_sche, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCol_agree", sCol_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAgreeInt", sAgreeInt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdIntermedia = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdIntermedia = Nothing
	End Function
	
	'**%Find_Supervis_v: Returns TRUE if the records exists in the table "Intermedia", otherwise returns False
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Intermedia"
	Public Function Find_Supervis_v() As Boolean
		Dim lrecinter As eRemoteDB.Execute
		
		On Error GoTo Find_Supervis_v_Err
		
		lrecinter = New eRemoteDB.Execute
		
		With lrecinter
			.StoredProcedure = "reaIntermed_v"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Supervis_v = True
				sCol_agree = .FieldToClass("sCol_agree", String.Empty)
				.RCloseRec()
			Else
				Find_Supervis_v = False
			End If
		End With
		
Find_Supervis_v_Err: 
		If Err.Number Then
			Find_Supervis_v = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinter = Nothing
	End Function
	
	'**% Find: This method performs the reading of table Intermedia through the stored procedure "reaIntermedia_a",
	'**%returning true or false if the call to the stored procedure returns any records
	'% Find_a. Este metodo Realiza la lectura de la tabla Intermedia mediante el Stored Procedure "reaIntermedia_a",
	'% devolviendo verdadero o falso dependiendo si el llamado al Stored proedure retorna o no registros.
	Public Function Find_a() As Boolean
		Dim lrecinter As eRemoteDB.Execute
		lrecinter = New eRemoteDB.Execute
		
		With lrecinter
			.StoredProcedure = "reaIntermedia_a"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_a = True
				nSupervis = .FieldToClass("nSupervis")
				dInpdate = .FieldToClass("dInpdate")
				sInter_id = .FieldToClass("sInter_id")
				sClient = .FieldToClass("sClient")
				dNulldate = .FieldToClass("dNulldate")
				nOffice = .FieldToClass("nOffice")
				If .FieldToClass("nIntertyp") = String.Empty Then
					nInterTyp = intNull
				Else
					nInterTyp = .FieldToClass("nIntertyp")
				End If
				nInt_status = .FieldToClass("nInt_status")
				nNullcode = .FieldToClass("nNullCode")
			Else
				Find_a = False
			End If
			.RCloseRec()
		End With
		'UPGRADE_NOTE: Object lrecinter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinter = Nothing
	End Function
	
	'**%reaIntermed_v1: Returns TRUE if the records exists in the table "Intermedia", otherwise returns False
	'%reaIntermed_v1: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Intermedia"
	Public Function reaIntermed_v1() As Boolean
		Dim lrecinter As eRemoteDB.Execute
		lrecinter = New eRemoteDB.Execute
		
		With lrecinter
			.StoredProcedure = "reaIntermed_v1"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_id", sInter_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			reaIntermed_v1 = .Run
			dInpdate = lrecinter.FieldToClass("dInpdate")
			nSupervis = lrecinter.FieldToClass("nSupervis")
			nOffice = lrecinter.FieldToClass("nOffice")
			nInterTyp = lrecinter.FieldToClass("nIntertyp")
			nInt_status = lrecinter.FieldToClass("nInt_status")
			sClient = lrecinter.FieldToClass("sClient")
			.RCloseRec()
		End With
		'UPGRADE_NOTE: Object lrecinter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinter = Nothing
	End Function
	
	'**%ADD: Add new records to the table "Intermedia".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Intermedia". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lrecIntermedia As eRemoteDB.Execute
		lrecIntermedia = New eRemoteDB.Execute
		
		With lrecIntermedia
			.StoredProcedure = "creIntermedia"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nInt_status", IIf((nInt_status = 0), System.DBNull.Value, nInt_status), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nIntertyp", IIf((nInterTyp = 0), System.DBNull.Value, nInterTyp), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nNullcode", IIf((nNullcode = 0), System.DBNull.Value, nNullcode), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nOffice", IIf((nOffice = 0), System.DBNull.Value, nOffice), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nSupervis", IIf((nSupervis = 0), System.DBNull.Value, nSupervis), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_id", sInter_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIntermedia = Nothing
	End Function
	
	'**% This method erases the records in the table indicated as parameters associated with an intermediary code
	'**%( also passed as parameter)
	'% Deletetablename. Este metodo borra los registros de la tabla indicada como parametros, asociados
	'% un codigo de intermediario (tambien pasado como parametro)
	Public Function Deletetablename(ByRef lstrIntermed As String, ByRef lstrtablename As String) As Boolean
		Dim lregTable As eRemoteDB.Execute
		lregTable = New eRemoteDB.Execute
		
		With lregTable
			.StoredProcedure = "delTablename"
			.Parameters.Add("sTableName", lstrtablename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIntermed", lstrIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Deletetablename = .Run(False)
		End With
		'UPGRADE_NOTE: Object lregTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lregTable = Nothing
	End Function
	
	'**%UpdIntermedia_nInterm_id: Update records in the table "Intermedia".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%UpdIntermedia_nInterm_id: Este método se encarga de actualizar registros en la tabla "Intermedia". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function UpdIntermedia_nInterm_id() As Boolean
		Dim lrecIntermedia As eRemoteDB.Execute
		lrecIntermedia = New eRemoteDB.Execute
		
		With lrecIntermedia
			.StoredProcedure = "updIntermedia_nInter_id"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInter_id", sInter_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdIntermedia_nInterm_id = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecIntermedia = Nothing
	End Function
	
	'**% This method performs the reading of the table Intermedia through the use of stored procedure "reaIntermed_VG",
	'**% in this way returning a true or false value depending if the call to the stored procedure returns some record
	'% Find_Intermed_vG. Este metodo Realiza la lectura de la tabla Intermedia mediante el Stored Procedure "reaIntermed_vG",
	'% devolviendo verdadero o falso dependiendo si el llamado al Stored proedure retorna o no registros.
	Public Function Find_Intermed_vG() As Boolean
		Dim lrecinter As eRemoteDB.Execute
		lrecinter = New eRemoteDB.Execute
		With lrecinter
			.StoredProcedure = "reaIntermed_vG"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Intermed_vG = True
				nIntermed = .FieldToClass("nIntermed")
				sInter_id = .FieldToClass("sInter_id")
				dInpdate = .FieldToClass("dInpdate")
				nSupervis = .FieldToClass("nSupervis")
				nOffice = .FieldToClass("nOffice")
				nInterTyp = .FieldToClass("nIntertyp")
				nInt_status = .FieldToClass("nInt_status")
			Else
				Find_Intermed_vG = False
			End If
			.RCloseRec()
		End With
	End Function
	
	'**%findValIntermed_Receipt: Verify if the client is the leader of any of the receipts
	'**% previously introduced.
	'%findValIntermed_Receipt: Rutina que verificar si el cliente es titular de alguno de los recibos
	'%previamente introducidos
	Public Function findValIntermed_Receipt(ByVal sClient As String, ByVal nreceipt As Integer) As Boolean
		Dim lrecreaIntermedia_Receipt As eRemoteDB.Execute
		
		findValIntermed_Receipt = False
		
		lrecreaIntermedia_Receipt = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaIntermedia_Receipt'
		'+Definición de parámetros para stored procedure 'insudb.reaIntermedia_Receipt'
		'**+ Data of October 11,2000  15:37:01
		'+Información leída el 11/10/2000 15:37:01
		
		With lrecreaIntermedia_Receipt
			.StoredProcedure = "reaIntermedia_Receipt"
			.Parameters.Add("nReceipt", nreceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				findValIntermed_Receipt = True
			End If
			.RCloseRec()
		End With
		'UPGRADE_NOTE: Object lrecreaIntermedia_Receipt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaIntermedia_Receipt = Nothing
	End Function
	
	'**%Update_statusNull: this method is in charge of updating the annullation date of the intermediary
	'%Update_statusNull. Este metodo se encarga de actualizar la fecha de anulacion del intermediario
	Public Function Update_statusNull() As Boolean
		Dim lrecupdIntermediaNull As eRemoteDB.Execute
		lrecupdIntermediaNull = New eRemoteDB.Execute
		'**+Parameter definitions for stored procedure 'insudb.updIntermediaNull'
		'+Definición de parámetros para stored procedure 'insudb.updIntermediaNull'
		'**+ Data of February 05,2001 9.57.40
		'+Información leída el 05/02/2001 9.57.40
		With lrecupdIntermediaNull
			.StoredProcedure = "updIntermediaNull"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInt_status", nInt_status, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_statusNull = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdIntermediaNull may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdIntermediaNull = Nothing
	End Function
	
	'**%Update_status: This method is in charge of updating the intermediary status
	'%Update_status. Este metodo se encarga de actualizar el estado del intermediario
	Public Function Update_status() As Boolean
		Dim lrecupdIntermediaStatus As eRemoteDB.Execute
		lrecupdIntermediaStatus = New eRemoteDB.Execute
		
		'**+ Parameter definitions for stored procedure 'insudb.updIntermediaStatus'
		'+Definición de parámetros para stored procedure 'insudb.updIntermediaStatus'
		'**+ Data of February 06, 2001       10.04.52
		'+Información leída el 06/02/2001 10.04.52
		
		With lrecupdIntermediaStatus
			.StoredProcedure = "updIntermediaStatus"
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInt_status", nInt_status, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_status = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdIntermediaStatus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdIntermediaStatus = Nothing
	End Function
	
	'**%insValRequired_Interm: This method returns a true of false value depending if the intermediary posses all the information required.
	'%insValRequired_Interm. Este metodo devuelve verdarero o falso, dependiendo si el intermediario
	'%posee toda la informacion requerida
	Public Function ValRequired(ByVal Intermed As Integer) As Boolean
		Dim lrecinsValRequired_Interm As eRemoteDB.Execute
		lrecinsValRequired_Interm = New eRemoteDB.Execute
		
		'**+ Parameter definitions for stored procedure 'insudb.insValRequired_Interm'
		'+Definición de parámetros para stored procedure 'insudb.insValRequired_Interm'
		'**+ Data of February 05, 2001  16.21.47
		'+Información leída el 05/02/2001 16.21.47
		
		With lrecinsValRequired_Interm
			.StoredProcedure = "insValRequired_Interm"
			.Parameters.Add("nIntermed", Intermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.WithInformation = .FieldToClass("WithInformation")
				ValRequired = True
				.RCloseRec()
			Else
				ValRequired = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsValRequired_Interm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValRequired_Interm = Nothing
	End Function
	
	'% insvalAGL005_K: se realizan las validaciones de la página
	Public Function insvalAGL005_K(ByVal dInitDate As Date, ByVal dEnddate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insvalAGL005_K_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			
			'+ La fecha de inicio debe estar llena
			
			If dInitDate = dtmNull Then
				Call .ErrorMessage("AGL005", 9071)
			End If
			
			'+ La fecha final debe estar llena
			
			If dEnddate = dtmNull Then
				Call .ErrorMessage("AGL005", 9072)
			End If
			
			If dInitDate <> dtmNull And dEnddate <> dtmNull Then
				If dInitDate > dEnddate Then
					'+ La fecha de inicio debe ser menor o igual a la fecha final
					Call .ErrorMessage("AGL005", 3240)
				End If
			End If
			
			insvalAGL005_K = .Confirm
		End With
		
insvalAGL005_K_err: 
		If Err.Number Then
			insvalAGL005_K = "insvalAGL005_K: " & insvalAGL005_K
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'**% insValClientInter: Verify if the Client has at least an Intermediary associted to.
	'% insValClientInter: Verifica que al menos un Intermediario esté asociado a el Cliente
	Public Function insValClientInter(ByVal sClient As String, Optional ByVal sCol_agree As String = "") As Boolean
		Dim lintExists As Short
		Dim lrecvalIntermedia_o As eRemoteDB.Execute
		lrecvalIntermedia_o = New eRemoteDB.Execute
		On Error GoTo insValClientInter_Err
		
		insValClientInter = False
		
		With lrecvalIntermedia_o
			.StoredProcedure = "valIntermedia_o"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			insValClientInter = (.Parameters("nExists").Value = 1)
		End With
		
insValClientInter_Err: 
		If Err.Number Then
			insValClientInter = False
		End If
		'UPGRADE_NOTE: Object lrecvalIntermedia_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalIntermedia_o = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValAGL003_K: This function perform all validations related to the transactio AGL003
	'% insValAGL003_K: Esta función valida los campos insertados en la transacción AGL003
	Public Function insValAGL003_K(ByVal sCodispl As String, ByVal dInitDate As Date, ByVal dEnddate As Date, ByVal nRepType As Integer, ByVal nZone As Integer, ByVal nIntermed As Integer, ByVal sClient As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsAgents As eAgent.Agents
		Dim lclsClient As eClient.Client
		
		lobjErrors = New eFunctions.Errors
		lclsAgents = New eAgent.Agents
		lclsClient = New eClient.Client
		
		With lobjErrors
			
			'**+If the Initial Date have been included, the Final Date should be include
			'+ Si se insertó la fecha de inicio, se debe insertar la fecha final
			
			If dInitDate <> dtmNull And dEnddate = dtmNull Then
				.ErrorMessage(sCodispl, 9072)
			End If
			
			'**+The Final Date should be greater than the Initial Date
			'+La fecha final debe ser mayor que la inicial
			
			If dInitDate <> dtmNull And dEnddate <> dtmNull Then
				If dInitDate > dEnddate Then
					.ErrorMessage(sCodispl, 3240)
				End If
			End If
			
			'**+If the report is by Zone, this one should be include
			'+ Si el reporte es por Zona, ésta debe haber sido incluida.
			If nRepType = 0 Then
				If nZone = 0 Or nZone = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 9120)
				End If
				
				'**+If the report is by Intermedia, this one should be include
				'+ Si el reporte es por Intermedia, ésta debe haber sido incluida.
			ElseIf nRepType = 1 Then 
				If nIntermed = 0 Or nIntermed = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 21038)
				Else
					lclsAgents.nIntermed = nIntermed
					If Not lclsAgents.Find_a() Then
						Call .ErrorMessage(sCodispl, 3634)
					End If
				End If
				
				'**+If the report is by Client, this one should be include
				'+ Si el reporte es por Client, ésta debe haber sido incluida.
			ElseIf nRepType = 2 Then 
				If sClient = String.Empty Then
					Call .ErrorMessage(sCodispl, 8053)
				Else
					If Not lclsClient.Find(sClient) Then
						Call .ErrorMessage(sCodispl, 7050)
					Else
						If Not insValClientInter(sClient) Then
							Call .ErrorMessage(sCodispl, 9121)
						End If
					End If
				End If
			End If
			
			insValAGL003_K = .Confirm
		End With
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsAgents may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgents = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
	End Function
	
	'**%Class_Initialize: Controls the creation of an instance of the class
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'    nUsercode = GetSetting("TIME", "GLOBALS", "USERCODE", 0)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






