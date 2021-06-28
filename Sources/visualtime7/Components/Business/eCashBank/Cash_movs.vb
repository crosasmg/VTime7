Option Strict Off
Option Explicit On
Public Class Cash_movs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cash_movs.cls                            $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 14/01/04 7:30p                               $%'
	'% $Revision:: 71                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Indicator to know if the cash movements, checks or vouchers have been deposited or not
	'-Indicador de si los movimientos de caja de efectivo, cheque o vouchers están depositados o no
	
	Public Enum eMovementType
		Deposited = 1
		NoDeposited = 2
	End Enum
	
	Private mCol As Collection
	
	'**-Auxiliary Variables
	'-Variables auxiliares
	
	Private mintCurrency As Integer
	Private mdtmEffecdate As Date
	Private mintOffice As Integer
	Private mintMov_Type As Integer
	Private mintAcc_bank As Integer
	Private mstrDep_number As String
	Private mdblCash_Amount As Double
	Private mdblCash_Amount_Total As Double
	Private mdblMin_Amount As Double
	Private mdblAmount As Double
	
	Public FirstCash_mov As Cash_mov
	
	'- Objeto para utilizar en la carga de la colección
	Private mclsCash_mov As Cash_mov
	
	'**%FindCashMovInterchange: Inquires about the information of a check from the file of cash movements
	'%FindCashMovInterchange :Consulta la informacion de un cheque en el archivo de movimientos de caja
	Public Function FindCashMovInterchange(ByVal nBank_code As Integer, ByVal sSche_number As String) As Boolean
		Dim lrecreaCash_mov_Interchange As eRemoteDB.Execute
		Dim lblnFirst As Boolean
		
		lblnFirst = True
		'**-+Parameter definition for stored procedure 'insudb.reaCash_mov_Interchancge'
		'**-+Information read on March 16, 2001  08:56:46 a.m.
		'+Definición de parámetros para stored procedure 'insudb.reaCash_mov_Interchange'
		'+Información leída el 16/03/2001 08:56:46 a.m.
		FindCashMovInterchange = True
		lrecreaCash_mov_Interchange = New eRemoteDB.Execute
		FirstCash_mov = New Cash_mov
		With lrecreaCash_mov_Interchange
			.StoredProcedure = "reaCash_mov_Interchange"
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChe_number", sSche_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					If lblnFirst Then
						'**+Stores just the first element found to make necessary future validations
						'+Se almacena sólo el primer elemento encontrado para efectuar las validaciones de rigor.
						FirstCash_mov.dEffecdate = .FieldToClass("dEffecdate")
						FirstCash_mov.nAmount = .FieldToClass("nAmount")
						FirstCash_mov.nAcc_bank = .FieldToClass("nAcc_bank")
						FirstCash_mov.sDep_number = .FieldToClass("sDep_number")
						FirstCash_mov.sNull_movem = .FieldToClass("sNull_movem")
						FirstCash_mov.nCurrency = .FieldToClass("nCurrency")
						FirstCash_mov.nReplacedCount = .FieldToClass("nReplacedCount")
						lblnFirst = False
					End If
					'**+Adds a new element to the collection
					'+Se agrega un nuevo elemento a la colección.
					mclsCash_mov = New Cash_mov
					mclsCash_mov.dEffecdate = .FieldToClass("dEffecdate")
					mclsCash_mov.nAmount = .FieldToClass("nAmount")
					mclsCash_mov.nCurrency = .FieldToClass("nCurrency")
					mclsCash_mov.nAcc_bank = .FieldToClass("nAcc_bank")
					mclsCash_mov.sDep_number = .FieldToClass("sDep_number")
					mclsCash_mov.sNull_movem = .FieldToClass("sNull_movem")
					mclsCash_mov.nReplacedCount = .FieldToClass("nReplacedCount")
					Call Add(mclsCash_mov)
					'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mclsCash_mov = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindCashMovInterchange = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCash_mov_Interchange may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCash_mov_Interchange = Nothing
		'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCash_mov = Nothing
	End Function
	
	'**%Add: Adds a new instance of the "Cash_mov" class to the collection
	'%Add: Añade una nueva instancia de la clase "Cash_mov" a la colección
	Public Function Add(ByRef mclsCash_mov As Cash_mov) As Cash_mov
		On Error GoTo Add_Err
		
		mCol.Add(mclsCash_mov)
		
		'**+Returns the created object
		'+Retorna el objeto creado
		
		Add = mclsCash_mov
		'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCash_mov = Nothing
		
Add_Err: 
		If Err.Number Then
            Add = Nothing
		End If
		On Error GoTo 0
	End Function
	
	'**%FindByCash: Resturns the values of the cash movements for a given day
	'%FindByCash: Devuelve los valores de los movimientos de caja en un día dado
	Public Function FindByCash(ByVal dDate_ini As Date, ByVal dDate_end As Date, ByVal nOffice As Integer, ByVal nCurrency As Integer, ByVal nMov_type As Integer, ByVal nCashNum As Integer, ByVal nConcept As Integer) As Boolean
		
		'**-The variable lrecreaCash_mov:OPC001 is declared
		'-Se define la variable lrecreaCash_mov_OPC001
		
		Dim lrecreaCash_mov_OPC001 As eRemoteDB.Execute
		
		On Error GoTo FindByCash_Err
		
		lrecreaCash_mov_OPC001 = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.reaCash_mov_OPC001'
		'**+Information read on March 06, 2001  11.33.05
		'+Definición de parámetros para stored procedure 'insudb.reaCash_mov_OPC001'
		'+Información leída el 6/3/01 11.33.05
		
		With lrecreaCash_mov_OPC001
			.StoredProcedure = "reaCash_mov_OPC001"
			.Parameters.Add("dDate_ini", dDate_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_end", dDate_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					mclsCash_mov = New Cash_mov
					mclsCash_mov.nTransac = .FieldToClass("nTransac")
					'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
					mclsCash_mov.dEffecdate = IIf(IsDbNull(.FieldToClass("dEffecdate")) Or IsNothing(.FieldToClass("dEffecdate")), dtmNull, .FieldToClass("dEffecdate"))
					mclsCash_mov.nMov_type = .FieldToClass("nMov_type")
					mclsCash_mov.sDocnumbe = .FieldToClass("sDocnumbe")
					mclsCash_mov.nBank_code = .FieldToClass("nBank_code")
					mclsCash_mov.dDoc_date = .FieldToClass("dDoc_date")
					mclsCash_mov.dRealDep = .FieldToClass("dRealDep")
					mclsCash_mov.nAmount = .FieldToClass("nAmount")
					mclsCash_mov.nConcept = .FieldToClass("nConcept")
					mclsCash_mov.sDep_number = .FieldToClass("sDep_number")
					mclsCash_mov.nBordereaux = .FieldToClass("nBordereaux")
					mclsCash_mov.nCash_id = .FieldToClass("nCash_id")
					mclsCash_mov.dCompdate = .FieldToClass("dCompdate")
					mclsCash_mov.sCard_num = .FieldToClass("sCard_num")
					mclsCash_mov.nAcc_bank = .FieldToClass("nAcc_bank")
					mclsCash_mov.sBank_descript = .FieldToClass("sBank_descript")
					mclsCash_mov.sDes_Concep = .FieldToClass("sDes_Concep")
					mclsCash_mov.sMov_typeDes = .FieldToClass("sMov_typeDes")
					Call Add(mclsCash_mov)
					'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mclsCash_mov = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				FindByCash = True
			Else
				FindByCash = False
			End If
		End With
		
FindByCash_Err: 
		If Err.Number Then
			FindByCash = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCash_mov = Nothing
		'UPGRADE_NOTE: Object lrecreaCash_mov_OPC001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCash_mov_OPC001 = Nothing
	End Function
	'**%Find: Restores an object collection of type Cash_mov
	'%Find: Devuelve una coleccion de objetos de tipo Cash_mov
	'------------------------------------------------------------
    Public Function Find(ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nMov_type As Integer, ByVal nAcc_bank As Integer, ByVal sDep_number As String, ByVal Movement As eMovementType, ByVal nCompany As Integer, ByVal nCashNum As Integer, ByVal nChequeLocat As Integer, Optional ByVal nIntermed As Integer = 0, Optional ByVal lblnFind As Boolean = False) As Boolean

        '------------------------------------------------------------
        Dim lrecreaCash_mov_OP002_a As eRemoteDB.Execute

        '**-The variable that will determine the result of the function (Tru/False) is declared
        '-Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '**-Declaration of the object that will contain the class to be treated
        '-Declaración del objeto contenedor de la clase a tratar
        Dim lintChequeLocat As Integer
        On Error GoTo Find_Err

        If mintCurrency <> nCurrency Or mdtmEffecdate <> dEffecdate Or mintOffice <> nOffice Or mintMov_Type <> nMov_type Or mintAcc_bank <> nAcc_bank Or mstrDep_number <> sDep_number Or lblnFind Then

            mintCurrency = nCurrency
            mdtmEffecdate = dEffecdate
            mintOffice = nOffice
            mintMov_Type = nMov_type
            mintAcc_bank = nAcc_bank
            mstrDep_number = sDep_number
            If nChequeLocat = eRemoteDB.Constants.intNull Or nChequeLocat = 0 Then
                lintChequeLocat = eRemoteDB.Constants.intNull
            Else
                lintChequeLocat = nChequeLocat
            End If
            lrecreaCash_mov_OP002_a = New eRemoteDB.Execute
            With lrecreaCash_mov_OP002_a
                If Movement = eMovementType.NoDeposited Then
                    .StoredProcedure = "reaCash_mov_OP002_a"
                ElseIf Movement = eMovementType.Deposited Then
                    .StoredProcedure = "reaCash_mov_OP002_BankDep"
                End If
                .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nMov_Type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nCompany", IIf(nCompany = eRemoteDB.Constants.intNull, System.DBNull.Value, nCompany), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                .Parameters.Add("nCashNum", IIf(nCashNum = eRemoteDB.Constants.intNull, System.DBNull.Value, nCashNum), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nChequeLocat", lintChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If Movement = eMovementType.Deposited Then
                    .Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sDep_number", sDep_number, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End If
                .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    Do While Not .EOF
                        mclsCash_mov = New Cash_mov
                        mclsCash_mov.nAcc_cash = .FieldToClass("nAcc_cash")
                        mclsCash_mov.nOffice = .FieldToClass("nOffice")
                        mclsCash_mov.nTransac = .FieldToClass("nTransac")
                        mclsCash_mov.dEffecdate = .FieldToClass("dEffecdate")
                        mclsCash_mov.nMov_type = .FieldToClass("nMov_type")
                        mclsCash_mov.sDocnumbe = .FieldToClass("sDocnumbe")
                        mclsCash_mov.nBank_code = .FieldToClass("nBank_code")
                        mclsCash_mov.nCard_typ = .FieldToClass("nCard_type")
                        mclsCash_mov.dDoc_date = .FieldToClass("dDoc_date")
                        mclsCash_mov.nAmount = .FieldToClass("nAmount")
                        mclsCash_mov.sCard_num = .FieldToClass("sCard_num")
                        mclsCash_mov.nCurrency = nCurrency
                        If lintChequeLocat = eRemoteDB.Constants.intNull Then
                            mclsCash_mov.nChequeLocat = .FieldToClass("nChequelocat")
                        Else
                            mclsCash_mov.nChequeLocat = lintChequeLocat
                        End If
                        mclsCash_mov.dCompdate = .FieldToClass("dCompdate")
                        mclsCash_mov.nOri_Amount = .FieldToClass("nOri_Amount")
                        mclsCash_mov.sDes_Ori_Curr = .FieldToClass("sDes_Ori_Curr")
                        mclsCash_mov.nBranch = .FieldToClass("nBranch")
                        mclsCash_mov.nProduct = .FieldToClass("nProduct")
                        mclsCash_mov.nPolicy = .FieldToClass("nPolicy")
                        mclsCash_mov.nCashNum = .FieldToClass("nCashnum")
                        Call Add(mclsCash_mov)

                        mdblAmount = .FieldToClass("nAmount")
                        'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        mclsCash_mov = Nothing
                        .RNext()
                    Loop

                    .RCloseRec()
                    lblnRead = True
                Else
                    lblnRead = False
                End If
            End With
        End If

        Find = lblnRead

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsCash_mov = Nothing
        'UPGRADE_NOTE: Object lrecreaCash_mov_OP002_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCash_mov_OP002_a = Nothing
    End Function
	
	'**%FindOP001: This method fills the collection with records from the table "Cash_mov" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%FindOP001: Este metodo carga la coleccion de elementos de la tabla "Cash_mov" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindOP001(ByVal nMov_type As Integer, ByVal dEffecdate As Date, ByVal nCash_id As Integer, ByVal nOffice As Integer, ByVal dValDate As Date, ByVal nOri_Curr As Integer, ByVal nOri_Amount As Double, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nCompany As Integer, ByVal nConcept As Integer, ByVal nAcc_bank As Integer, ByVal sDocnumbe As String, ByVal sCard_num As String, ByVal nCard_typ As Integer, ByVal nChequeLocat As Integer, ByVal nInputChannel As Integer, ByVal nBank_code As Integer, Optional ByVal nBordereaux As Integer = 0, Optional ByVal nTransac As Integer = 0, Optional ByVal nNoteNum As Integer = 0, Optional ByVal nInsur_area As Integer = 0) As Boolean
		Dim lclsCash_mov As Cash_mov
		Dim lrecinsReaop001 As eRemoteDB.Execute
		
		On Error GoTo FindOP001_Err
		lrecinsReaop001 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insReaop001 al 06-19-2003 10:27:28
		'+
		With lrecinsReaop001
			.StoredProcedure = "insReaOP001"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCash_id", nCash_id, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValdate", dValDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOri_amount", nOri_Amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOri_curr", nOri_Curr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAcc_bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCard_num", sCard_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCard_type", nCard_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChequelocat", nChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInputchannel", nInputChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindOP001 = True
				Do While Not .EOF
					mclsCash_mov = New Cash_mov
					mclsCash_mov.nTransac = .FieldToClass("nTransac")
					mclsCash_mov.dEffecdate = .FieldToClass("dEffecdate")
					mclsCash_mov.nMov_type = .FieldToClass("nMov_type")
					mclsCash_mov.nOffice = .FieldToClass("nOffice")
					mclsCash_mov.nCurrency = .FieldToClass("nCurrency")
					mclsCash_mov.nAmount = .FieldToClass("nAmount")
					mclsCash_mov.nConcept = .FieldToClass("nConcept")
					mclsCash_mov.sDocnumbe = .FieldToClass("sDocnumbe")
					mclsCash_mov.dDoc_date = .FieldToClass("dDoc_Date")
					mclsCash_mov.nAcc_bank = .FieldToClass("nAcc_bank")
					mclsCash_mov.nBank_code = .FieldToClass("nBank_code")
					mclsCash_mov.sCard_num = .FieldToClass("sCard_num")
					mclsCash_mov.nCard_typ = .FieldToClass("nCard_type")
					mclsCash_mov.dCard_expir = .FieldToClass("dCard_expir")
					mclsCash_mov.sClient = .FieldToClass("sClient")
					mclsCash_mov.nIntermed = .FieldToClass("nIntermed")
					mclsCash_mov.nCompanyc = .FieldToClass("nCompanyc")
					mclsCash_mov.sNumForm = .FieldToClass("sNumForm")
					mclsCash_mov.nBordereaux = .FieldToClass("nBordereaux")
					mclsCash_mov.nClaim = .FieldToClass("nClaim")
					mclsCash_mov.nContrat = .FieldToClass("nContrat")
					mclsCash_mov.nDraft = .FieldToClass("nDraft")
					mclsCash_mov.nTyp_acco = .FieldToClass("nTyp_acco")
					mclsCash_mov.nChequeLocat = .FieldToClass("nChequeLocat")
					mclsCash_mov.nInsur_area = .FieldToClass("nInsur_area")
					mclsCash_mov.nCod_Agree = .FieldToClass("nCod_Agree")
					mclsCash_mov.nInputChannel = .FieldToClass("nInputChannel")
					mclsCash_mov.dValDate = .FieldToClass("dValDate")
					mclsCash_mov.nOri_Curr = .FieldToClass("nOri_Curr")
					mclsCash_mov.nOri_Amount = .FieldToClass("nOri_Amount")
					mclsCash_mov.nSupport_Id = .FieldToClass("nSupport_Id")
					mclsCash_mov.nTypesupport = .FieldToClass("nTypesupport")
					mclsCash_mov.dCollection = .FieldToClass("dCollection")
					mclsCash_mov.nCash_id = .FieldToClass("nCash_id")
					mclsCash_mov.nFin_Int = .FieldToClass("nFin_int")
					mclsCash_mov.nBranch = .FieldToClass("nBranch")
					mclsCash_mov.nProduct = .FieldToClass("nProduct")
					mclsCash_mov.nBank_Agree = .FieldToClass("nBank_Agree")
					mclsCash_mov.nProponum = .FieldToClass("nProponum")
					mclsCash_mov.nCompany = .FieldToClass("nCompany")
					mclsCash_mov.sType_acc = .FieldToClass("sType_acc")
					mclsCash_mov.nVoucher = .FieldToClass("nVoucher")
					mclsCash_mov.nNoteNum = .FieldToClass("nNoteNum")
					mclsCash_mov.nCurrencyPay = .FieldToClass("nCurrency")
					mclsCash_mov.sDigit = .FieldToClass("sDigit")
					mclsCash_mov.sCliename = .FieldToClass("sCliename")
					mclsCash_mov.sConcept = .FieldToClass("sConcept")
					mclsCash_mov.sCurrAcc = .FieldToClass("sCurrAcc")
					mclsCash_mov.sBank_descript = .FieldToClass("sAccBank")
					mclsCash_mov.sProduct = .FieldToClass("sProduct")
					mclsCash_mov.sInter_name = .FieldToClass("sIntermed")
					mclsCash_mov.sCompany = .FieldToClass("sCompany")
					mclsCash_mov.nCase_Num = .FieldToClass("nCase_num")
					
					Call Add(mclsCash_mov)
					
					'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mclsCash_mov = Nothing
					
					.RNext()
				Loop 
			End If
		End With
		
FindOP001_Err: 
		If Err.Number Then
			FindOP001 = False
		End If
		'UPGRADE_NOTE: Object lclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCash_mov = Nothing
		'UPGRADE_NOTE: Object lrecinsReaop001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReaop001 = Nothing
	End Function
	'**%FindOP006: This method fills the collection with records from the table "Cash_mov" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%FindOP006: Este metodo carga la coleccion de elementos de la tabla "Cash_mov" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindOP006(ByVal lclsCash_mov As Cash_mov) As Boolean
		Dim lclsConstruct As eRemoteDB.ConstructSelect
		Dim lclsExeTime As eRemoteDB.Execute
		Dim lintWhere As Integer
		
		On Error GoTo FindOP006_Err
		FindOP006 = False
		lclsConstruct = New eRemoteDB.ConstructSelect
		lclsExeTime = New eRemoteDB.Execute
		
		lintWhere = 0
		
		With lclsCash_mov
			If .dEffecdate <> dtmNull Then
				lclsConstruct.WhereClause("cash_mov.dEffecdate", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, "=" & .dEffecdate, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				lintWhere = lintWhere + 1
			End If
			
			If .nCurrency <> eRemoteDB.Constants.intNull And .nCurrency <> 0 Then
				lclsConstruct.WhereClause("cash_mov.nCurrency", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nCurrency, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				lintWhere = lintWhere + 1
			End If
			
			If .nConcept <> eRemoteDB.Constants.intNull And .nConcept <> 0 Then
				lclsConstruct.WhereClause("cash_mov.nConcept", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nConcept, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				lintWhere = lintWhere + 1
			End If
			
			If .sDescript <> strNull Then
				lclsConstruct.WhereClause("cash_mov.sDescript", eRemoteDB.ConstructSelect.eTypeValue.TypCString, .sDescript, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				lintWhere = lintWhere + 1
			End If
			
			If .nAmount <> eRemoteDB.Constants.intNull And .nAmount <> 0 Then
				lclsConstruct.WhereClause("cash_mov.nAmount", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nAmount, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				lintWhere = lintWhere + 1
			End If
			
			If .sClient <> strNull Then
				lclsConstruct.WhereClause("cash_mov.sClient", eRemoteDB.ConstructSelect.eTypeValue.TypCString, .sClient, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				lintWhere = lintWhere + 1
			End If
			
			If .sInter_pay <> strNull Then
				lclsConstruct.WhereClause("cash_mov.sInter_pay", eRemoteDB.ConstructSelect.eTypeValue.TypCString, .sInter_pay, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				lintWhere = lintWhere + 1
			End If
			
			If .nUser_sol <> eRemoteDB.Constants.intNull And .nUser_sol <> 0 Then
				lclsConstruct.WhereClause("cash_mov.nUser_sol", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=" & .nUser_sol, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				lintWhere = lintWhere + 1
			End If
			
			If .dLedger_dat <> .dLedger_dat Then
				lclsConstruct.WhereClause("cash_mov.dLedger_dat", eRemoteDB.ConstructSelect.eTypeValue.TypCDate, "=" & .dLedger_dat, eRemoteDB.ConstructSelect.eWordConnection.eAnd)
				lintWhere = lintWhere + 1
			End If
			
			If lintWhere = 0 Then
				lclsConstruct.WhereClause("1", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, "=1", eRemoteDB.ConstructSelect.eWordConnection.eAnd)
			End If
			
			
			If lclsExeTime.Server = eFunctions.Tables.sTypeServer.sOracle Then
				lclsConstruct.SelectClause(("cash_mov.nAcc_cash, cash_mov.nCurrency, cash_mov.nOffice as nCashOffice, cash_mov.nTransac, cash_mov.dEffecdate, " & "cash_mov.nConcept, cash_mov.sDescript, cash_mov.nAmount, cash_mov.sClient, cash_mov.sInter_pay," & "cash_mov.nUser_sol, cash_mov.dLedger_dat, cash_mov.nBordereaux, clibenef.scliename as sBenefName, cliinter.scliename as sInterName, " & "cliuser.scliename as sUserName, cash_mov.nNotenum, cheques.nRequest_nu, cheques.dIssue_dat,cheques.nCurrencyPay,cheques.nOffice, " & "cheques.nOfficeAgen,cheques.Agency "))
				
				lclsConstruct.NameFatherTable("cash_mov", "cash_mov ")
				
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "client", "clibenef", "cash_mov.sclient = clibenef.sclient")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "client", "cliinter", "cash_mov.sinter_pay = cliinter.sclient")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "users", "users", "cash_mov.nuser_sol = users.nusercode")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "client", "cliuser", "users.sclient = cliuser.sclient")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "cheques", "cheques", "cash_mov.nAcc_cash = cheques.nAcc_bank " & "AND cash_mov.dEffecdate = cheques.dDat_propos " & "AND cash_mov.nConcept = cheques.nConcept " & "AND cash_mov.nAmount = cheques.nAmount " & "AND cash_mov.sclient = cheques.sclient " & "AND cash_mov.nUser_sol = cheques.nUser_sol")
			Else
				lclsConstruct.SelectClause(("cash_mov.nAcc_cash, cash_mov.nCurrency, cash_mov.nOffice, cash_mov.nTransac, cash_mov.dEffecdate, " & "cash_mov.nConcept, cash_mov.sDescript, cash_mov.nAmount, cash_mov.sClient, cash_mov.sInter_pay," & "cash_mov.nUser_sol, cash_mov.dLedger_dat, cash_mov.nBordereaux, clibenef.scliename as sBenefName, cliinter.scliename as sInterName, " & "cliuser.scliename as sUserName, cash_mov.nNotenum, cheques.nRequest_nu, cheques.dIssue_dat,cheques.nCurrencyPay "))
				
				lclsConstruct.NameFatherTable("cash_mov", "cash_mov ")
				
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "client", "clibenef", "cash_mov.sclient = clibenef.sclient")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "client", "cliinter", "cash_mov.sinter_pay = cliinter.sclient")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "users", "users", "cash_mov.nuser_sol = users.nusercode")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "client", "cliuser", "users.sclient = cliuser.sclient")
				lclsConstruct.RelationsTables(eRemoteDB.ConstructSelect.eRelationTables.RelLeft, "cheques", "cheques", "cheques.nAcc_bank = cash_mov.nAcc_Cash " & "AND CONVERT(VARCHAR(10),cheques.dDat_propos,112) = CONVERT(VARCHAR(10),cash_mov.dEffecdate,112) " & "AND cheques.nConcept = cash_mov.nConcept " & "AND cheques.nAmount = cash_mov.nAmount " & "AND cheques.sClient = cash_mov.sClient " & "AND cheques.nUser_sol = cash_mov.nUser_sol ")
			End If
			
			lclsExeTime.Sql = lclsConstruct.Answer
		End With
		
		With lclsExeTime
			If .Run Then
				FindOP006 = True
				Do While Not (.EOF)
					mclsCash_mov = New Cash_mov
					mclsCash_mov.nAcc_cash = .FieldToClass("nAcc_cash")
					mclsCash_mov.nOffice = .FieldToClass("nOffice")
					mclsCash_mov.nOfficeAgen = .FieldToClass("nOfficeAgen")
					mclsCash_mov.nAgency = .FieldToClass("nAgency")
					mclsCash_mov.nTransac = .FieldToClass("nTransac")
					mclsCash_mov.dEffecdate = .FieldToClass("dEffecdate")
					mclsCash_mov.nAmount = .FieldToClass("nAmount")
					mclsCash_mov.nCurrency = .FieldToClass("nCurrency")
					mclsCash_mov.nConcept = .FieldToClass("nConcept")
					mclsCash_mov.sClient = .FieldToClass("sClient")
					mclsCash_mov.nBordereaux = .FieldToClass("nBordereaux")
					mclsCash_mov.sDescript = .FieldToClass("sDescript")
					mclsCash_mov.sInter_pay = .FieldToClass("sInter_pay")
					mclsCash_mov.nUser_sol = .FieldToClass("nUser_sol")
					mclsCash_mov.dLedger_dat = .FieldToClass("dLedger_dat")
					mclsCash_mov.nNoteNum = .FieldToClass("nNotenum")
					mclsCash_mov.nRequest_nu = .FieldToClass("nRequest_nu")
					mclsCash_mov.dIssue_Dat = .FieldToClass("dIssue_dat")
					Call Add(mclsCash_mov)
					'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mclsCash_mov = Nothing
					.RNext()
				Loop 
			End If
			.RCloseRec()
		End With
		
FindOP006_Err: 
		If Err.Number Then
			FindOP006 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCash_mov = Nothing
		'UPGRADE_NOTE: Object lclsConstruct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsConstruct = Nothing
		'UPGRADE_NOTE: Object lclsExeTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExeTime = Nothing
	End Function
	'**%InsPreOP002: This method initializes all of the properties utilized by the page "OP002"
	'%InsPreOP002: Este metodo inicializa todas las propiedades utilizada por la pagina "OP002"
    Public Function InsPreOP002(ByVal nAction As Integer, ByVal nOptDeposit As Integer, ByVal nAcc_bank As Integer, ByVal dEffecdate As Date, ByVal sDeposit As String, ByVal nUsercode As Integer, ByVal nOffice As Integer, ByVal nCashNum As Integer, ByVal nCompany As Integer, ByVal nChequeLocat As Integer, ByVal nIntermed As Integer) As Boolean
        Dim lclsBank_acc As eCashBank.Bank_acc
        Dim lclsCash_acc As eCashBank.Cash_acc
        Dim lblnData As Boolean

        On Error GoTo InsPreOP002_Err

        lblnData = True
        InsPreOP002 = True
        lclsBank_acc = New eCashBank.Bank_acc

        lclsBank_acc.Find(nAcc_bank)

        If nAction = eFunctions.Menues.TypeActions.clngActionadd And nOptDeposit = 1 Then
            If nCashNum <> eRemoteDB.Constants.intNull Then
                lclsCash_acc = New eCashBank.Cash_acc
                lclsCash_acc.dEffecdate = dEffecdate
                If lclsCash_acc.Find(9998, nOffice, lclsBank_acc.nCurrency, nCashNum, nCompany) Then
                    mdblCash_Amount = lclsCash_acc.nAvailable_By_Day
                    mdblCash_Amount_Total = lclsCash_acc.nAvailable
                    mdblMin_Amount = lclsCash_acc.nMin_Amount
                    If Not Find_OP002(9998, lclsBank_acc.nCurrency, dEffecdate, nOffice, nCashNum, "", nIntermed) Then
                        lblnData = False
                    End If
                Else
                    lblnData = False
                End If
                'UPGRADE_NOTE: Object lclsCash_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsCash_acc = Nothing

            Else
                If Not Find_OP002(9998, lclsBank_acc.nCurrency, dEffecdate, nOffice, nCashNum, "", nIntermed) Then
                    lblnData = False
                End If
            End If
        End If

        If (nAction = eFunctions.Menues.TypeActions.clngActionQuery Or nAction = eFunctions.Menues.TypeActions.clngActioncut) And nOptDeposit = 1 Then
            InsPreOP002 = Find_OP002(9998, lclsBank_acc.nCurrency, dEffecdate, nOffice, nCashNum, sDeposit, nIntermed)
        End If

        InsPreOP002 = lblnData
        If InsPreOP002 And nOptDeposit <> 1 Then
            If nAction = eFunctions.Menues.TypeActions.clngActionQuery Or nAction = eFunctions.Menues.TypeActions.clngActioncut Then
                InsPreOP002 = Find(lclsBank_acc.nCurrency, dEffecdate, nOffice, nOptDeposit, nAcc_bank, sDeposit, eMovementType.Deposited, nCompany, nCashNum, nChequeLocat, nIntermed)
            Else
                InsPreOP002 = Find(lclsBank_acc.nCurrency, dEffecdate, nOffice, nOptDeposit, eRemoteDB.Constants.intNull, String.Empty, eMovementType.NoDeposited, nCompany, nCashNum, nChequeLocat, nIntermed)
            End If
        End If

InsPreOP002_Err:
        If Err.Number Then
            InsPreOP002 = False
        End If
        'UPGRADE_NOTE: Object lclsBank_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsBank_acc = Nothing
        On Error GoTo 0
    End Function
	'***nCash_Amount:This property returns the contents of the variable "nCash_Amount"
	'*nCash_Amount:Esta propiedad devuelve el contenido de la variable "nCash_Amount"
	ReadOnly Property nCash_Amount_Total() As Double
		Get
			nCash_Amount_Total = mdblCash_Amount_Total
		End Get
	End Property
	
	
	'***nCash_Amount:This property returns the contents of the variable "nCash_Amount"
	'*nCash_Amount:Esta propiedad devuelve el contenido de la variable "nCash_Amount"
	ReadOnly Property nCash_Amount() As Double
		Get
			nCash_Amount = mdblCash_Amount
		End Get
	End Property
	
	'***nAmount:This property returns the contents of the variable "nAmount"
	'*nAmount:Esta propiedad devuelve el contenido de la variable "nAmount"
	ReadOnly Property nAmount() As Double
		Get
			nAmount = mdblAmount
		End Get
	End Property
	
	
	'***nMin_Amount:This property returns the contents of the variable "nMin_Amount"
	'*nMin_Amount:Esta propiedad devuelve el contenido de la variable "nMin_Amount"
	ReadOnly Property nMin_Amount() As Double
		Get
			nMin_Amount = mdblMin_Amount
		End Get
	End Property
	
	'***Item: Returns an element to the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cash_mov
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the number of elements that the collection owns
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection so that it can be used in a For Each...Next loop
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Remove: Removes an element from the collection
	'%Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mdblCash_Amount = eRemoteDB.Constants.intNull
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the delete of an instance of the collection
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		'UPGRADE_NOTE: Object FirstCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		FirstCash_mov = Nothing
		'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCash_mov = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%InsPreOPC717: Restores an object collection of type Cash_mov
	'%InsPreOPC717: Devuelve una coleccion de objetos de tipo Cash_mov
	'------------------------------------------------------------
	Public Function InsPreOPC717(ByVal dStartDate As Date, ByVal dEndDate As Date, ByVal nCurrency As Integer, ByVal nBank As Integer, ByVal nChequeLocat As Integer, ByVal nCheque_Stat As Integer, ByVal sDocNumber As String, ByVal sTypeInfo As String, ByVal nCard_Type As Integer, ByVal sSupervisor As String) As Boolean
		'------------------------------------------------------------
		Dim lrecreaCash_mov_OPC717 As eRemoteDB.Execute
		
		On Error GoTo InsPreOPC717_Err
		
		lrecreaCash_mov_OPC717 = New eRemoteDB.Execute
		
		With lrecreaCash_mov_OPC717
			.StoredProcedure = "reaCash_mov_OPC717"
			.Parameters.Add("dStartDate", dStartDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEndDate", dEndDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChequeLocat", nChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDocnumber", sDocNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCheque_stat", nCheque_Stat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeInfo", sTypeInfo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCard_Type", nCard_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSupervisor", sSupervisor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					mclsCash_mov = New Cash_mov
					mclsCash_mov.sDocnumbe = .FieldToClass("sDocnumbe")
					mclsCash_mov.nBank_code = .FieldToClass("nBank_code")
					mclsCash_mov.nAmount = .FieldToClass("nAmount")
					mclsCash_mov.nBordereaux = .FieldToClass("nBordereaux")
					mclsCash_mov.nChequeLocat = .FieldToClass("nChequeLocat")
					mclsCash_mov.dCollection = .FieldToClass("dCollection")
					mclsCash_mov.nCase_Num = .FieldToClass("nCashNum")
					
					mclsCash_mov.dDoc_date = .FieldToClass("dDoc_date")
					mclsCash_mov.nCheque_Stat = .FieldToClass("nCheque_stat")
					mclsCash_mov.nConcept = .FieldToClass("nConcept")
					mclsCash_mov.nCash_id = .FieldToClass("nCash_id")
					mclsCash_mov.nOfficeAgen = .FieldToClass("nOfficeAgen")
					mclsCash_mov.dEffecdate = .FieldToClass("dEffecdate")
					
					mclsCash_mov.sDes_Bank = .FieldToClass("sDes_Bank")
					mclsCash_mov.sDes_Cheloc = .FieldToClass("sDes_Cheloc")
					mclsCash_mov.sDes_Chestat = .FieldToClass("sDes_Chestat")
					mclsCash_mov.sDes_Concep = .FieldToClass("sDes_Concep")
					mclsCash_mov.sDes_Office = .FieldToClass("sDes_Office")
					mclsCash_mov.dRealDep = .FieldToClass("dRealDep")
					mclsCash_mov.sDesCard_type = .FieldToClass("sDesCard_type")
					
					Call Add(mclsCash_mov)
					'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mclsCash_mov = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				InsPreOPC717 = True
			End If
		End With
		
InsPreOPC717_Err: 
		If Err.Number Then
			InsPreOPC717 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCash_mov = Nothing
		'UPGRADE_NOTE: Object lrecreaCash_mov_OPC717 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCash_mov_OPC717 = Nothing
	End Function
	
	'% FindDepositOP752: busca los datos de los depósitos/redepósitos a tratar en la OP752
	Public Function FindDepositOP752(ByVal nCheopertyp As Integer, ByVal nMov_type As Integer, ByVal nChequeLocat As Integer, ByVal dDoc_date As Date, ByVal nBank_code As Double, ByVal nCurrency As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo FindDepositOP752_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaCash_Mov_OP752"
			.Parameters.Add("nCheopertyp", nCheopertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMov_type", nMov_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChequeLocat", nChequeLocat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDoc_date", dDoc_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Do While Not .EOF
					mclsCash_mov = New Cash_mov
					mclsCash_mov.sDocnumbe = .FieldToClass("sDocnumbe")
					mclsCash_mov.nBank_code = .FieldToClass("nBank_code")
					mclsCash_mov.nChequeLocat = .FieldToClass("nChequeLocat")
					mclsCash_mov.nVoucher = .FieldToClass("nVoucher")
					mclsCash_mov.nCard_typ = .FieldToClass("nCard_Type")
					mclsCash_mov.nAmount = .FieldToClass("nAmount")
					mclsCash_mov.nBordereaux = .FieldToClass("nBordereaux")
					mclsCash_mov.dDoc_date = .FieldToClass("dDoc_date")
					mclsCash_mov.nOffice = .FieldToClass("nOffice")
					mclsCash_mov.nCashNum = .FieldToClass("nCashNum")
					mclsCash_mov.nTransac = .FieldToClass("nTransac")
					Call Add(mclsCash_mov)
					'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mclsCash_mov = Nothing
					.RNext()
				Loop 
				.RCloseRec()
				FindDepositOP752 = True
			End If
		End With
		
FindDepositOP752_Err: 
		If Err.Number Then
			FindDepositOP752 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsCash_mov = Nothing
	End Function
	
	'%Find_OP002: devuelve los depositos en efectivo
	'------------------------------------------------------------
    Public Function Find_OP002(ByVal nCash_Acc As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nCashNum As Integer, ByVal sDep_number As String, ByVal nIntermed As Integer) As Boolean
        '------------------------------------------------------------
        Dim lrecFind_OP002 As eRemoteDB.Execute
        Dim mclsCash_mov As eCashBank.Cash_mov

        '-Declaración del objeto contenedor de la clase a tratar
        On Error GoTo Find_OP002_Err

        lrecFind_OP002 = New eRemoteDB.Execute
        With lrecFind_OP002
            .StoredProcedure = "REAOP002"
            .Parameters.Add("nCash_Acc", nCash_Acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sDep_number", IIf(sDep_number = "", System.DBNull.Value, sDep_number), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                Do While Not .EOF
                    mclsCash_mov = New Cash_mov
                    mclsCash_mov.nCash_id = .FieldToClass("nCash_id")
                    mclsCash_mov.nAmount = .FieldToClass("nAmount")
                    mclsCash_mov.dEffecdate = .FieldToClass("dEffecdate")
                    mclsCash_mov.dCompdate = .FieldToClass("dCompdate")
                    mclsCash_mov.nOri_Amount = .FieldToClass("nOri_Amount")
                    mclsCash_mov.sDes_Ori_Curr = .FieldToClass("sDes_Ori_Curr")
                    mclsCash_mov.nBranch = .FieldToClass("nBranch")
                    mclsCash_mov.nProduct = .FieldToClass("nProduct")
                    mclsCash_mov.nPolicy = .FieldToClass("nPolicy")
                    mclsCash_mov.nCashNum = .FieldToClass("nCashnum")
                    mclsCash_mov.nTransac = .FieldToClass("nTransac")
                    mclsCash_mov.nOffice = .FieldToClass("nOffice")

                    Call Add(mclsCash_mov)
                    'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    mclsCash_mov = Nothing
                    .RNext()
                Loop

                .RCloseRec()
                Find_OP002 = True
            Else
                Find_OP002 = False
            End If
        End With

Find_OP002_Err:
        If Err.Number Then
            Find_OP002 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecFind_OP002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecFind_OP002 = Nothing
        'UPGRADE_NOTE: Object mclsCash_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mclsCash_mov = Nothing
    End Function
End Class






