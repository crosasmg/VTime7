Option Strict Off
Option Explicit On
Public Class Guar_Saving_Pol
	'%-------------------------------------------------------%'
	'% $Workfile:: Guar_Saving_Pol.cls                           $%'
	'% $Author:: Gazuaje                                   $%'
	'% $Date:: 5-04-06 21:53                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according the table in the system on 11/23/2000
	'-Propiedades según la tabla en el sistema el 26/03/2002
	'-La llave primaria corresponde a sCertype , nBranch, nProduct, nPolicy, nCertif, sClient, dEffecdate, nId
	
	
	'Column_name               Type                        Computed   Length Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	'------------------------  -------------------------   --------   ------ ----- ----- -------- ------------------  --------------------
	Public sCertype As String 'char       no         1      no    no       no
	Public nBranch As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
	Public nProduct As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
	Public nPolicy As Double 'int        no         4      10    0        no    (n/a)               (n/a)
	Public nCertif As Double 'int        no         4      10    0        no    (n/a)               (n/a)
	Public dEffecdate As Date 'datetime   no         8      no                   (n/a)               (n/a)
	Public nGuarsavid As Double 'decimal    no         5      5     2        yes   (n/a)               (n/a)
	Public nGuarsav_year As Integer 'smallint   no         2      5     0        no    (n/a)               (n/a)
	Public nUsercode As Integer 'smallint   no         2      5     0        yes   (n/a)               (n/a)
	Public dStart_guarsav As Date 'smallint   no         2      5     0        yes   (n/a)               (n/a)
	Public dEnd_guarsav As Date 'smallint   no         2      5     0        yes   (n/a)               (n/a)
	Public nGuarsav_value As Double 'int        no         8                     yes   (n/a)               (n/a)
	Public nCurrency As Integer 'smallint   no         1                     yes   (n/a)               (n/a)
	Public nGuarsav_cost As Double 'int        no         1                     yes   (n/a)               (n/a)
	Public nGuarsav_stat As Integer 'smallint   no         1                     yes   (n/a)               (n/a)
	Public nRen_guarsav As Double
	Public sDeppremind As String
	Public nGuarsav_prem As Double
	Public nReceipt As Double
	
	
	'**-Auxilliary properties
	'-Propiedades auxiliares
	
	'**-Clients name
	'-Nombre del Cliente
	Public sCliename As String
	
	'**%Find: Function that returns TRUE to make the reading of the records in the 'Guar_Saving_Pol' table
	'%Find: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Guar_Saving_Pol'
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaGuar_Saving_Pol As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecReaGuar_Saving_Pol = New eRemoteDB.Execute
		
		'**+Parameters definition to stored procedure ' insudb.reaGuar_Saving_Pol'
		'**+Data read on 11/23/2000 3:52:14 p.m.
		'+Definición de parámetros para stored procedure 'insudb.reaGuar_Saving_Pol'
		'+Información leída el 23/11/2000 3:52:14 p.m.
		
		With lrecReaGuar_Saving_Pol
			.StoredProcedure = "INSVI8000PKG.Reaguar_saving_pol"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nGuarsavid = .FieldToClass("nGuarsavid")
				dEffecdate = .FieldToClass("dEffecdate")
				nGuarsav_year = .FieldToClass("nGuarsav_year")
				dStart_guarsav = .FieldToClass("dStart_guarsav")
				dEnd_guarsav = .FieldToClass("dEnd_guarsav")
				nGuarsav_value = .FieldToClass("nGuarsav_value")
				nCurrency = .FieldToClass("nCurrency")
				nGuarsav_cost = .FieldToClass("nGuarsav_cost")
				nGuarsav_stat = .FieldToClass("nGuarsav_stat")
				nRen_guarsav = .FieldToClass("nRen_guarsav")
				sDeppremind = .FieldToClass("sDeppremind")
				nGuarsav_prem = .FieldToClass("nGuarsav_prem")
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaGuar_Saving_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaGuar_Saving_Pol = Nothing
	End Function
	
	'% insValVI8000: Realiza la validación de los campos a actualizar en la ventana VI8000
	Public Function insValVI8000(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGuarsavid As Integer, ByVal nGuarsav_year As Integer, ByVal dStart_guarsav As Date, ByVal dEnd_guarsav As Date, ByVal nGuarsav_value As Double, ByVal nCurrency As Integer, ByVal nGuarsav_cost As Double, ByVal nGuarsav_stat As Integer, ByVal nRen_guarsav As Double, ByVal sMassive As String, ByVal sAction As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lcolGuar_Saving_Pol As Guar_Saving_Pols
		Dim lrecinsValVi8000 As eRemoteDB.Execute
        Dim lstrError As String = String.Empty
		
		
		On Error GoTo insValVI8000_Err
		
		lclsErrors = New eFunctions.Errors
		lrecinsValVi8000 = New eRemoteDB.Execute
		
		'+Validar el ahorro garantizado se está adquiriendo por traspaso de fondo para una póliza vigente,
		'+debe existir en los Fondos de inversión al menos la cantidad de primas mínimas mensuales indicadas
		'+en el producto
		With lrecinsValVi8000
			.StoredProcedure = "INSVI8000PKG.insValVI8000"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsavid", nGuarsavid, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_year", nGuarsav_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStart_guarsav", dStart_guarsav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_guarsav", dEnd_guarsav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_value", nGuarsav_value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_cost", nGuarsav_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_stat", nGuarsav_stat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRen_guarsav", nRen_guarsav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMassive", sMassive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lclsErrors = New eFunctions.Errors
				With lclsErrors
					.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
					insValVI8000 = .Confirm()
				End With
				
			End If
		End With
		
insValVI8000_Err: 
		If Err.Number Then
			insValVI8000 = "insValVI8000: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostVI8000: Se realiza la actualización de los datos en la ventana VI8000
	Public Function insPostVI8000(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGuarsavid As Integer, ByVal nGuarsav_year As Integer, ByVal dStart_guarsav As Date, ByVal dEnd_guarsav As Date, ByVal nGuarsav_value As Double, ByVal nCurrency As Integer, ByVal nGuarsav_cost As Double, ByVal nGuarsav_stat As Integer, ByVal nRen_guarsav As Double, ByVal nGuarsav_prem As Double, ByVal nUsercode As Integer, ByVal sDeppremind As String, ByVal sAction As String) As Boolean
		'- Declaración de los objetos a ser utilizados
		Dim lcolGuar_Saving_Pols As Guar_Saving_Pols
		Dim lclsPolicyWin As Policy_Win
		Dim lrecpostGuar_Saving_Pol As eRemoteDB.Execute
		
		On Error GoTo insPostVI8000_Err
		
		mstrContent = String.Empty
		
		lrecpostGuar_Saving_Pol = New eRemoteDB.Execute
		
		'**+Parameters definition to stored procedure ' insudb.creGuar_Saving_Pol'
		'**+Data read on 11/23/2000 1:44:31 p.m.
		'+Definición de parámetros para stored procedure 'insudb.creGuar_Saving_Pol'
		'+Información leída el 23/11/2000 1:44:31 p.m.
		
		With lrecpostGuar_Saving_Pol
			.StoredProcedure = "INSVI8000PKG.INSPOSTVI8000"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsavid", nGuarsavid, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_year", nGuarsav_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStart_guarsav", dStart_guarsav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_guarsav", dEnd_guarsav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_value", nGuarsav_value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_prem", nGuarsav_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_cost", nGuarsav_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarsav_stat", nGuarsav_stat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDeppremind", IIf(sDeppremind = "1", "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRen_guarsav", nRen_guarsav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostVI8000 = .Run(False)
		End With
		
		If insPostVI8000 Then
			'+ Se llama a la función FIND de la colección Guar_Saving_Pols para saber si hay o no registros
			lcolGuar_Saving_Pols = New ePolicy.Guar_Saving_Pols
			Call lcolGuar_Saving_Pols.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
			
			If lcolGuar_Saving_Pols.ncount = 0 Then
				mstrContent = "1"
			Else
				mstrContent = "2"
			End If
		End If
		
		lclsPolicyWin = New ePolicy.Policy_Win
		Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "VI8000", mstrContent)
		
insPostVI8000_Err: 
		If Err.Number Then
			insPostVI8000 = False
		End If
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
		'UPGRADE_NOTE: Object lcolGuar_Saving_Pols may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolGuar_Saving_Pols = Nothing
		'UPGRADE_NOTE: Object lrecpostGuar_Saving_Pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecpostGuar_Saving_Pol = Nothing
		On Error GoTo 0
	End Function
	
	
	'*sContent: Obtiene el indicador de contenido de la transacción
	Public ReadOnly Property sContent() As String
		Get
			sContent = mstrContent
		End Get
	End Property
	
	'% insShowVI8000: Realiza la validación de los campos a actualizar en la ventana VI8000
	Public Function insShowVI8000(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nGuarsav_value As Integer, ByVal nGuarsav_year As Integer, ByVal nRen_guarsav As Double, ByVal sDeppremind As String, ByVal nOption As Short) As Boolean
		Dim lrecinsShowVI8000 As eRemoteDB.Execute
		
		On Error GoTo insShowVI8000_Err
		
		lrecinsShowVI8000 = New eRemoteDB.Execute
		
		With lrecinsShowVI8000
			If nOption = 1 Then
				.StoredProcedure = "INSVI8000PKG.REAGUAR_SAVING_RENT"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGuarsav_value", nGuarsav_value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGuarsav_year", nGuarsav_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRen_guarsav", nRen_guarsav, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				insShowVI8000 = .Run(False)
				Me.nRen_guarsav = .Parameters("nRen_guarsav").Value
			Else
				.StoredProcedure = "INSVI8000PKG.INSCALPREM_GUAR_SAVING"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGuarsav_value", nGuarsav_value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGuarsav_year", nGuarsav_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRen_guarsav", nRen_guarsav, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sDeppremind", sDeppremind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGuarsav_cost", nGuarsav_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGuarsav_prem", nGuarsav_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				insShowVI8000 = .Run(False)
				Me.nGuarsav_cost = .Parameters("nGuarsav_cost").Value
				Me.nGuarsav_prem = .Parameters("nGuarsav_prem").Value
			End If
		End With
		
insShowVI8000_Err: 
		If Err.Number Then
			insShowVI8000 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsShowVI8000 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsShowVI8000 = Nothing
	End Function
End Class






